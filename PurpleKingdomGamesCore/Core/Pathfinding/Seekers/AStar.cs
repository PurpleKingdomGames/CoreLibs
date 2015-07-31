using PurpleKingdomGames.Core.Pathfinding.Internal;
using System;
using System.Collections.Generic;

namespace PurpleKingdomGames.Core.Pathfinding.Seekers
{
    /// <summary>
    /// Contains methods for seeking a target within a specified grid using the A* pathfind algorith
    /// </summary>
    public static class AStar
    {
        public enum LockType
        {
            Bottom,
            Top,
            None
        }

        /// <summary>
        /// Seek a target in a 2-dimensional grid
        /// </summary>
        /// <param name="grid">The grid to search</param>
        /// <param name="start">The start point to seek from</param>
        /// <param name="target">The target to seek to</param>
        /// <returns>An array of grid nodes needed to pass through to get to the target</returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// If the start or target are out of range of the grid
        /// </exception>
        public static Point2D[] Seek(GridNode2D[,] grid, Point2D start, Point2D target)
        {
            return Seek(grid, start, target, LockType.None, 0, 0, true);
        }

        /// <summary>
        /// Seek a target in a 2-dimensional grid
        /// </summary>
        /// <param name="grid">The grid to search</param>
        /// <param name="start">The start point to seek from</param>
        /// <param name="target">The target to seek to</param>
        /// <param name="lockType">The start point to seek from</param>
        /// <param name="maxDescent">The target to seek to</param>
        /// <param name="maxClimb">The target to seek to</param>
        /// <param name="cutCorners">Whether or not to cut a corner</param>
        /// <returns>An array of grid nodes needed to pass through to get to the target</returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// If the start or target are out of range of the grid
        /// </exception>
        public static Point2D[] Seek(GridNode2D[,] grid, Point2D start, Point2D target, LockType lockType, int maxDescent, int maxClimb, bool cutCorners)
        {
            // Check that the starting position is not out of range of the grid
            if (start.X > grid.GetLength(0) || start.X < 0 || start.Y >= grid.GetLength(1) || start.Y < 0) {
                throw new ArgumentOutOfRangeException(
                    "Invalid starting grid reference. Got " + start.X + "," + start.Y +
                    ". But grid only runs from 0,0 to " + grid.GetLength(0) + "," + grid.GetLength(1)
                );
            }

            // check that the target position is not out of range of the grid
            if (target.X > grid.GetLength(0) || target.X < 0 || target.Y >= grid.GetLength(1) || target.Y < 0) {
                throw new ArgumentOutOfRangeException(
                    "Invalid target grid reference. Got " + target.X + "," + target.Y +
                    ". But grid only runs from 0,0 to " + grid.GetLength(0) + "," + grid.GetLength(1)
                );
            }

            // If a lock type is specified then  move the starting position to the top or bottom of the Y axis
            if (lockType != LockType.None) {
                int step = (lockType == LockType.Bottom ? 1 : -1);
                int targetI = (lockType == LockType.Bottom ? grid.GetLength(1) : 0);
                for (int i = (int) start.Y; i < targetI; i += step) {
                    if (!grid[(int) start.X, (int) start.Y].Passable) {
                        break;
                    }

                    start.Y = i;
                }
            }

            GridNodeHeap2D openNodes = new GridNodeHeap2D();
            List<GridNode2D> closedNodes = new List<GridNode2D>();
            GridNodeCalc2D currentNode = grid[(int) start.X, (int) start.Y].ToGridNodeCalc2D();
            GridNodeCalc2D targetNode = null;

            // Set the grid position for the first node
            currentNode.GridPosition = new Point2D(start.X, start.Y);

            // Add the starting position to the open list
            openNodes.Add(currentNode);

            // Continue until the open list is empty
            while (openNodes.Count > 0) {
                currentNode = openNodes.Remove();

                // If we have reached the target node then break from the loop
                if (currentNode.GridPosition == target) {
                    targetNode = currentNode;
                    break;
                }

                // If we have a maximum climb, then make sure that the current node hasn't climbed too high
                if (maxClimb != 0 && getAscentDistance(currentNode, grid) > maxClimb) {
                    continue;
                }

                // If we have a maximum climb, then make sure that the current node hasn't fallen too far
                if (maxDescent != 0 && getDescentDistance(currentNode, grid) > maxDescent) {
                    continue;
                }

                // Add the current node to the closed list
                closedNodes.Add(currentNode.ReferenceNode);

                // Add additional nodes to the open list, ready to search
                calculateOpenList(currentNode, target, grid, openNodes, closedNodes, cutCorners);
            }

            if (targetNode == null) {
                return null;
            }

            List<Point2D> path = new List<Point2D>();
            path.Add(targetNode.ReferenceNode.Position);

            while (targetNode.Parent != null) {
                path.Add(targetNode.Parent.ReferenceNode.Position);
                targetNode = targetNode.Parent;
            }

            // The path will be from the target to the start point...
            // reverse it so it makes sense to the consumer
            path.Reverse();

            // Return the new array of path points
            return path.ToArray();
        }

        private static void calculateOpenList(GridNodeCalc2D currentNode, Point2D target, GridNode2D[,] grid, GridNodeHeap2D openNodes, List<GridNode2D> closedNodes, bool cutCorners)
        {
            Point2D currentGridPos = currentNode.GridPosition;

            // Make sure the X and Y coordinates are always within the grid boundaries
            int minX = (int) Math.Max(currentGridPos.X - 1, 0);
            int maxX = (int) Math.Min(currentGridPos.X + 1, grid.GetLength(0));
            int minY = (int) Math.Max(currentGridPos.Y - 1, 0);
            int maxY = (int) Math.Min(currentGridPos.Y + 1, grid.GetLength(1));

            for (int x = minX; x <= maxX; x++) {
                for (int y = minY; y <= maxY; y++) {
                    GridNodeCalc2D node = grid[x, y].ToGridNodeCalc2D();
                    node.GridPosition = new Point2D(x, y);

                    // Skip over this node if it's the one we're currently looking at, or it's in the closed list
                    if (node.GridPosition == currentGridPos || closedNodes.Contains(node.ReferenceNode)) {
                        continue;
                    }

                    // Also skip if it's impassable
                    if (node.ReferenceNode.Passable == false) {
                        continue;
                    }

                    GridNodeCalc2D refNode = openNodes.GetByPosition(node.GridPosition);
                    bool isDiagonal = (
                        node.GridPosition.X != currentGridPos.X &&
                        node.GridPosition.Y != currentGridPos.Y
                    );

                    // If we're not meant to cut corners, tehn we need to check this now
                    if (isDiagonal && !cutCorners) {
                        // If we are further down the grid, then check the square
                        // above to make sure it's also passable
                        if (refNode.GridPosition.Y > 0 && refNode.GridPosition.Y > currentGridPos.Y) {
                            if (!grid[(int) refNode.GridPosition.X, (int) (refNode.GridPosition.Y - 1)].Passable) {
                                continue;
                            }
                        }

                        // If we are further up the grid, then check the square
                        // below to make sure it's also passable
                        if (refNode.GridPosition.Y < maxY && refNode.GridPosition.Y < currentGridPos.Y) {
                            if (!grid[(int) refNode.GridPosition.X, (int) (refNode.GridPosition.Y + 1)].Passable) {
                                continue;
                            }
                        }
                    }

                    // Is this node on the open list already?
                    if (refNode == null) {
                        // If not then set the parent to the current node and calculate the cost
                        node.Parent = currentNode;
                        node.MovementCost = currentNode.TotalCost + 10;

                        // Diagonal... add a bit
                        if (isDiagonal) {
                            node.MovementCost += 4;
                        }

                        // Calculate the heuristic cost
                        node.HeuristicCost = (int) (
                            Math.Abs(node.GridPosition.X - target.X) +
                            Math.Abs(node.GridPosition.Y - target.Y)
                        );

                        openNodes.Add(node);
                        continue;
                    }

                    int currentNodeCost = currentNode.MovementCost + currentNode.ReferenceNode.Penalty;
                    int refNodeCost = refNode.MovementCost + refNode.ReferenceNode.Penalty;

                    // The node was in the open list, so see if it costs less to move there
                    if (refNodeCost < currentNodeCost) {
                        // If so, set then set the parent to the current node
                        refNode.Parent = currentNode.Parent;

                        // Re-calculate the movement cost using the new parent
                        refNode.MovementCost = refNode.Parent.TotalCost + 10;

                        // Diagonal... add a bit
                        if (isDiagonal) {
                            refNode.MovementCost += 4;
                        }

                        openNodes.Sort(openNodes.IndexOf(refNode));
                    }
                }
            }
        }

        private static int getDescentDistance(GridNodeCalc2D node, GridNode2D[,] grid)
        {
            int descentAmount = 0;
            while (node != null) {
                // If where we have come from is higher than where we started then we are descending
                if (node.Parent != null && node.Parent.GridPosition.Y > node.GridPosition.Y) {
                    // Check that we aren't on the ground
                    if ((node.GridPosition.Y + 1) < grid.GetLength(1)) {
                        if (!grid[(int) node.GridPosition.X, (int) (node.GridPosition.Y + 1)].Passable) {
                            break;
                        }
                    }

                    descentAmount++;
                }

                node = node.Parent;
            }

            return descentAmount;
        }

        private static int getAscentDistance(GridNodeCalc2D node, GridNode2D[,] grid)
        {
            int climbAmount = 0;
            while (node != null) {
                // If where we have come from is lower than where we started then we are climbing
                if (node.Parent != null && node.Parent.GridPosition.Y < node.GridPosition.Y) {
                    // Check that we aren't on the ground
                    if ((node.GridPosition.Y + 1) < grid.GetLength(1)) {
                        if (!grid[(int) node.GridPosition.X, (int) (node.GridPosition.Y + 1)].Passable) {
                            break;
                        }
                    }

                    climbAmount++;
                }

                node = node.Parent;
            }

            return climbAmount;
        }
    }
}
