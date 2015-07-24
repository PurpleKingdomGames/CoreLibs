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
            // Calculate possible paths
            GridNodeHeap2D openNodes = new GridNodeHeap2D();
            List<GridNode2D> closedNodes = new List<GridNode2D>();

            // TODO: Add this option to the method
            /*LockYAxis lockYAxis = LockYAxis.LockToBottom;

            // Find the starting square
            int startX = (int) Math.Floor(start.X - grid.Offset.X);
            int startY = (int) Math.Floor(start.Y - grid.Offset.Y);

            if (startX > grid.Nodes.GetLength(0) || startX < 0) {
                throw new Exception(
                    "Invalid grid reference. Got " + startX + "," + startY +
                    ". But grid only runs from 0,0 to " + grid.Nodes.GetLength(0) + "," + grid.Nodes.GetLength(1)
                );
            }

            if (lockYAxis != LockYAxis.None) {
                int step = (lockYAxis == LockYAxis.LockToBottom ? 1 : -1);
                int target = (lockYAxis == LockYAxis.LockToBottom ? grid.Nodes.GetLength(1) : 0);
                for (int i = startY; i < target; i += step) {
                    if (!grid.Nodes[startX, startY].Passable) {
                        break;
                    }

                    startY = i;
                }
            }*/

            // Check that the starting position is not out of range of the grid
            if (start.X > grid.GetLength(0) || start.X < 0 || start.Y >= grid.GetLength(1) || start.Y < 0) {
                throw new ArgumentOutOfRangeException(
                    "Invalid starting grid reference. Got " + start.X + "," + start.Y +
                    ". But grid only runs from 0,0 to " + grid.GetLength(0) + "," + grid.GetLength(1)
                );
            }

            // check taht the target position is not out of range of the grid
            if (target.X > grid.GetLength(0) || target.X < 0 || target.Y >= grid.GetLength(1) || target.Y < 0) {
                throw new ArgumentOutOfRangeException(
                    "Invalid target grid reference. Got " + target.X + "," + target.Y +
                    ". But grid only runs from 0,0 to " + grid.GetLength(0) + "," + grid.GetLength(1)
                );
            }

            GridNodeCalc2D currentNode = grid[(int) start.X, (int) start.Y].ToGridNodeCalc2D();
            GridNodeCalc2D targetNode = null;

            // Set the grid position for the first node
            currentNode.GridPosition = new Point2D(start.X, start.Y);

            // Add the starting position to the open list
            openNodes.Add(currentNode);

            // Continue until the open list is empty
            while (openNodes.Count > 0) {
                currentNode = openNodes.Remove();
                closedNodes.Add(currentNode.ReferenceNode);

                if (currentNode.GridPosition == target) {
                    targetNode = currentNode;
                    break;
                }

                Point2D currentGridPos = currentNode.GridPosition;

                // Make sure the X and Y coordinates are alwqys within the grid boundaries
                int minX = (int) Math.Max(currentGridPos.X - 1, 0);
                int maxX = (int) Math.Min(currentGridPos.X + 1, grid.GetLength(0));
                int minY = (int) Math.Max(currentGridPos.Y - 1, 0);
                int maxY = (int) Math.Min(currentGridPos.Y + 1, grid.GetLength(1));

                for (int x = minX; x < maxX; x++) {
                    for (int y = minY; y < maxY; y++) {
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
                            currentNode.HeuristicCost = (int) (
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
    }
}