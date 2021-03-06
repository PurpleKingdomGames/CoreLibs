﻿using PurpleKingdomGames.Core.Pathfinding.Internal;
using System;
using System.Collections.Generic;

namespace PurpleKingdomGames.Core.Pathfinding.Seekers
{
    /// <summary>
    /// Contains methods for seeking a target within a specified grid using the A* pathfind algorithm
    /// </summary>
    public static class AStar
    {
        /// <summary>
        /// The default movement cost when not supplied
        /// </summary>
        public const int DEFAULT_MOVEMENTCOST = 10;

        /// <summary>
        /// The default diagonal movement cost when not supplied
        /// </summary>
        public const int DEFAULT_DIAGONALCOST = 14;

        /// <summary>
        /// The default additional ascent cost, when not supplied
        /// </summary>
        public const int DEFAULT_ASCENTCOST = 0;

        /// <summary>
        /// The default additional descent cost, when not supplied
        /// </summary>
        public const int DEFAULT_DESCENTCOST = 0;

        /// <summary>
        /// Seek a target in a 2-dimensional grid
        /// </summary>
        /// <param name="grid">The grid to search</param>
        /// <param name="startPoint">The start point to seek from</param>
        /// <param name="targetPoint">The target to seek to</param>
        /// <returns>An array of points in world space needed to pass through to get to the target</returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// If the start or target are out of range of the grid
        /// </exception>
        public static Point2D[] Seek(GridNode2D[,] grid, Point2D startPoint, Point2D targetPoint)
        {
            return Seek(
                grid, startPoint, targetPoint, true, DEFAULT_MOVEMENTCOST,
                DEFAULT_DIAGONALCOST, DEFAULT_ASCENTCOST, DEFAULT_DESCENTCOST
            );
        }

        /// <summary>
        /// Seek a target in a 2-dimensional grid
        /// </summary>
        /// <param name="grid">The grid to search</param>
        /// <param name="startPoint">The start point to seek from</param>
        /// <param name="targetPoint">The target to seek to</param>
        /// <param name="cutCorners">Whether or not to cut a corner</param>
        /// <returns>An array of points in world space needed to pass through to get to the target</returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// If the start or target are out of range of the grid
        /// </exception>
        public static Point2D[] Seek(GridNode2D[,] grid, Point2D startPoint, Point2D targetPoint, bool cutCorners)
        {
            return Seek(
                grid, startPoint, targetPoint, cutCorners,
                DEFAULT_MOVEMENTCOST, DEFAULT_DIAGONALCOST,
                DEFAULT_ASCENTCOST, DEFAULT_DESCENTCOST
            );
        }

        /// <summary>
        /// Seek a target in a 2-dimensional grid
        /// </summary>
        /// <param name="grid">The grid to search</param>
        /// <param name="startPoint">The start point to seek from</param>
        /// <param name="targetPoint">The target to seek to</param>
        /// <param name="ascentCost">The additional cost to apply when ascending</param>
        /// <returns>An array of points in world space needed to pass through to get to the target</returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// If the start or target are out of range of the grid
        /// </exception>
        public static Point2D[] Seek(GridNode2D[,] grid, Point2D startPoint, Point2D targetPoint, int ascentCost)
        {
            return Seek(
                grid, startPoint, targetPoint, true,
                DEFAULT_MOVEMENTCOST, DEFAULT_DIAGONALCOST,
                ascentCost, DEFAULT_DESCENTCOST
            );
        }

        /// <summary>
        /// Seek a target in a 2-dimensional grid
        /// </summary>
        /// <param name="grid">The grid to search</param>
        /// <param name="startPoint">The start point to seek from</param>
        /// <param name="targetPoint">The target to seek to</param>
        /// <param name="cutCorners">Whether or not to cut a corner</param>
        /// <param name="ascentCost">The additional cost to apply when ascending</param>
        /// <returns>An array of points in world space needed to pass through to get to the target</returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// If the start or target are out of range of the grid
        /// </exception>
        public static Point2D[] Seek(GridNode2D[,] grid, Point2D startPoint, Point2D targetPoint, bool cutCorners, int ascentCost)
        {
            return Seek(
                grid, startPoint, targetPoint, cutCorners,
                DEFAULT_MOVEMENTCOST, DEFAULT_DIAGONALCOST,
                ascentCost, DEFAULT_DESCENTCOST
            );
        }

        /// <summary>
        /// Seek a target in a 2-dimensional grid
        /// </summary>
        /// <param name="grid">The grid to search</param>
        /// <param name="startPoint">The start point to seek from</param>
        /// <param name="targetPoint">The target to seek to</param>
        /// <param name="ascentCost">The additional cost to apply when ascending</param>
        /// <param name="descentCost">The additional cost to apply when descending</param>
        /// <returns>An array of points in world space needed to pass through to get to the target</returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// If the start or target are out of range of the grid
        /// </exception>
        public static Point2D[] Seek(GridNode2D[,] grid, Point2D startPoint, Point2D targetPoint, int ascentCost, int descentCost)
        {
            return Seek(
                grid, startPoint, targetPoint, true,
                DEFAULT_MOVEMENTCOST, DEFAULT_DIAGONALCOST,
                ascentCost, descentCost
            );
        }

        /// <summary>
        /// Seek a target in a 2-dimensional grid
        /// </summary>
        /// <param name="grid">The grid to search</param>
        /// <param name="startPoint">The start point to seek from</param>
        /// <param name="targetPoint">The target to seek to</param>
        /// <param name="cutCorners">Whether or not to cut a corner</param>
        /// <param name="ascentCost">The additional cost to apply when ascending</param>
        /// <param name="descentCost">The additional cost to apply when descending</param>
        /// <returns>An array of points in world space needed to pass through to get to the target</returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// If the start or target are out of range of the grid
        /// </exception>
        public static Point2D[] Seek(GridNode2D[,] grid, Point2D startPoint, Point2D targetPoint, bool cutCorners, int ascentCost, int descentCost)
        {
            return Seek(
                grid, startPoint, targetPoint, cutCorners,
                DEFAULT_MOVEMENTCOST, DEFAULT_DIAGONALCOST,
                ascentCost, descentCost
            );
        }

        /// <summary>
        /// Seek a target in a 2-dimensional grid
        /// </summary>
        /// <param name="grid">The grid to search</param>
        /// <param name="startPoint">The start point to seek from</param>
        /// <param name="targetPoint">The target to seek to</param>
        /// <param name="cutCorners">Whether or not to cut a corner</param>
        /// <param name="movementCost">The cost to move left/right/up/down from one node to another</param>
        /// <param name="diagonalCost">The cost to move in a diagonal from one node to another</param>
        /// <param name="ascentCost">The additional cost to apply when ascending</param>
        /// <param name="descentCost">The additional cost to apply when descending</param>
        /// <returns>An array of points in world space needed to pass through to get to the target</returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// If the start or target are out of range of the grid
        /// </exception>
        public static Point2D[] Seek(
            GridNode2D[,] grid, Point2D startPoint, Point2D targetPoint, bool cutCorners,
            int movementCost, int diagonalCost, int ascentCost, int descentCost
        )
        {
            GridCoord2D start = new GridCoord2D((int) startPoint.X, (int) startPoint.Y);
            GridCoord2D target = new GridCoord2D((int) targetPoint.X, (int) targetPoint.Y);

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

            GridNodeHeap2D openNodes = new GridNodeHeap2D();
            bool[,] closedNodes = new bool[grid.GetLength(0), grid.GetLength(1)];
            GridNodeCalc2D currentNode = grid[start.X, start.Y].ToGridNodeCalc2D();
            GridNodeCalc2D targetNode = null;

            // Set the grid position for the first node
            currentNode.GridPosition = new GridCoord2D(start.X, start.Y);

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

                // Add the current node to the closed list
                closedNodes[currentNode.GridPosition.X, currentNode.GridPosition.Y] = true;

                // Add additional nodes to the open list, ready to search
                calculateOpenList(
                    currentNode, target, grid, openNodes, closedNodes, cutCorners,
                    movementCost, diagonalCost, ascentCost, descentCost
                );
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

        /// <summary>
        /// Calculates the next set of nodes to add to the open list and adds them
        /// </summary>
        /// <param name="currentNode">The node we're calculating from</param>
        /// <param name="target">The target node</param>
        /// <param name="grid">The grid to search</param>
        /// <param name="openNodes">The open list to add to</param>
        /// <param name="closedNodes">The closed list to check</param>
        /// <param name="cutCorners">Whether to cut corners</param>
        /// <param name="movementCost">The cost of moving up/down/left/right</param>
        /// <param name="diagonalCost">The cost of moving diagonally</param>
        /// <param name="ascentCost">The additional cost of ascending</param>
        /// <param name="descentCost">The additional cost of descending</param>
        private static void calculateOpenList(
            GridNodeCalc2D currentNode, GridCoord2D target, GridNode2D[,] grid,
            GridNodeHeap2D openNodes, bool[,] closedNodes, bool cutCorners,
            int movementCost, int diagonalCost, int ascentCost, int descentCost
        )
        {
            GridCoord2D currentGridPos = currentNode.GridPosition;

            // Make sure the X and Y coordinates are always within the grid boundaries
            int minX = Math.Max(currentGridPos.X - 1, 0);
            int maxX = Math.Min(currentGridPos.X + 1, grid.GetLength(0) - 1);
            int minY = Math.Max(currentGridPos.Y - 1, 0);
            int maxY = Math.Min(currentGridPos.Y + 1, grid.GetLength(1) - 1);

            for (int x = minX; x <= maxX; x++) {
                for (int y = minY; y <= maxY; y++) {
                    GridNodeCalc2D node = grid[x, y].ToGridNodeCalc2D();
                    node.GridPosition = new GridCoord2D(x, y);

                    // Skip over this node if it's the one we're currently looking at, or it's in the closed list
                    if (
                        node.GridPosition == currentGridPos ||
                        closedNodes[node.GridPosition.X, node.GridPosition.Y]) {
                        continue;
                    }

                    // Also skip if it's impassable
                    if (node.ReferenceNode.Passable == false) {
                        continue;
                    }

                    bool isDiagonal = (
                        node.GridPosition.X != currentGridPos.X &&
                        node.GridPosition.Y != currentGridPos.Y
                    );

                    // If we're not meant to cut corners, tehn we need to check this now
                    if (isDiagonal && !cutCorners && isCuttingCorner(node, currentGridPos, grid)) {
                        continue;
                    }

                    // Is this node on the open list already?
                    int refIndex = openNodes.IndexOf(node);
                    if (refIndex == -1) {
                        // If not then set the parent to the current node and calculate the cost
                        node.Parent = currentNode;

                        // Calculate the movement cost to this node
                        node.MovementCost = currentNode.TotalCost;
                        node.MovementCost += calculateMovementCost(
                            movementCost, diagonalCost, ascentCost, descentCost,
                            isDiagonal, (node.GridPosition.Y < currentGridPos.Y),
                            (node.GridPosition.Y > currentGridPos.Y)
                        );

                        // Calculate the heuristic cost
                        node.HeuristicCost = (
                            Math.Abs(node.GridPosition.X - target.X) +
                            Math.Abs(node.GridPosition.Y - target.Y)
                        );

                        openNodes.Add(node);
                        continue;
                    }

                    GridNodeCalc2D refNode = openNodes[refIndex];
                    int currentNodeCost = currentNode.MovementCost + currentNode.ReferenceNode.Penalty;
                    int refNodeCost = refNode.MovementCost + refNode.ReferenceNode.Penalty;

                    // The node was in the open list, so see if it costs less to move there
                    if (refNodeCost < currentNodeCost) {
                        // If so, set then set the parent to the current node
                        refNode.Parent = currentNode.Parent;

                        // Re-calculate the movement cost using the new parent
                        refNode.MovementCost = refNode.Parent.TotalCost;
                        refNode.MovementCost += calculateMovementCost(
                            movementCost, diagonalCost, ascentCost, descentCost,
                            isDiagonal, (refNode.GridPosition.Y < currentGridPos.Y),
                            (refNode.GridPosition.Y > currentGridPos.Y)
                        );

                        openNodes.Sort(refIndex);
                    }
                }
            }
        }

        /// <summary>
        /// Checks to see whether passing to this node would cut a corner
        /// </summary>
        /// <param name="node">The node to check</param>
        /// <param name="currentGridPos">The positio0n we're moving from</param>
        /// <param name="grid">The grid</param>
        /// <returns></returns>
        private static bool isCuttingCorner(GridNodeCalc2D node, GridCoord2D currentGridPos, GridNode2D[,] grid)
        {
            // If we are further down the grid, then check the node
            // above to make sure it's also passable
            if (node.GridPosition.Y > 0 && node.GridPosition.Y > currentGridPos.Y) {
                if (!grid[node.GridPosition.X, (node.GridPosition.Y - 1)].Passable) {
                    return true;
                }
            }

            // If we are further up the grid, then check the square
            // below to make sure it's also passable
            if (node.GridPosition.Y < grid.GetLength(1) && node.GridPosition.Y < currentGridPos.Y) {
                if (!grid[node.GridPosition.X, (node.GridPosition.Y + 1)].Passable) {
                    return true;
                }
            }

            // If we are moving to the right then also check the node
            // to the immediate left to ensure it's passable
            if (node.GridPosition.X > 0 && node.GridPosition.X > currentGridPos.X) {
                if (!grid[(node.GridPosition.X - 1), node.GridPosition.Y].Passable) {
                    return true;
                }
            }

            // If we are moving to the left then also check the node
            // to the immediate right to ensure it's passable
            if (node.GridPosition.X < grid.GetLength(0) && node.GridPosition.X < currentGridPos.X) {
                if (!grid[(node.GridPosition.X + 1), node.GridPosition.Y].Passable) {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Calculates teh cost of a node based on whether it's a diagonal, descent, or ascent
        /// </summary>
        /// <param name="movementCost">The cost to move</param>
        /// <param name="diagonalCost">The cost to move if this is a diagonal</param>
        /// <param name="ascendingCost">The additional cost of this is ascending</param>
        /// <param name="descendingCost">The additional cost if this is descending</param>
        /// <param name="isDiagonal">Whether we should calculate as a diagonal</param>
        /// <param name="isAscending">Whether we should calculate as an ascension</param>
        /// <param name="isDescending">Whether we should calculate as a descent</param>
        /// <returns></returns>
        private static int calculateMovementCost(int movementCost, int diagonalCost, int ascendingCost, int descendingCost, bool isDiagonal, bool isAscending, bool isDescending)
        {
            int cost = (isDiagonal ? diagonalCost : movementCost);

            if (isAscending) {
                cost += ascendingCost;
            } else if (isDescending) {
                cost += descendingCost;
            }

            return cost;
        }
    }
}