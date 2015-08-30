using PurpleKingdomGames.Core.Collections;
using PurpleKingdomGames.Core.Collections.Internal;
using System;
using System.Collections.Generic;

namespace PurpleKingdomGames.Core.Pathfinding.Internal
{
    internal class GridNodeHeap2D : BinaryHeap<GridNodeCalc2D>
    {
        private Dictionary<int, Dictionary<int, Tuple<GridNodeCalc2D, int>>> graphHeap = new Dictionary<int, Dictionary<int, Tuple<GridNodeCalc2D, int>>>();
        
        /// <summary>
        /// Add a single item to the heap
        /// </summary>
        /// <param name="item"></param>
        public new void Add(GridNodeCalc2D item)
        {
            GridCoord2D pos = item.GridPosition;
            if (!graphHeap.ContainsKey(pos.X)) {
                graphHeap.Add(pos.X, new Dictionary<int, Tuple<GridNodeCalc2D, int>>());
            }

            graphHeap[pos.X][pos.Y] = base.AddInternal(item);
        }

        /// <summary>
        /// Get the lowest value from the top of the heap
        /// </summary>
        /// <returns></returns>
        public new GridNodeCalc2D Remove()
        {
            GridNodeCalc2D item = base.Remove();
            GridCoord2D pos = item.GridPosition;
            graphHeap[pos.X].Remove(pos.Y);

            return item;
        }

        /// <summary>
        /// Gets the index of the specified item in the heap
        /// </summary>
        /// <param name="item">The item to search for</param>
        /// <returns>Returns zero or more on success. -1 on failure</returns>
        public new int IndexOf(GridNodeCalc2D item)
        {
            GridCoord2D position = item.GridPosition;
            if (position.X < 0 || position.Y < 0) {
                throw new Exception("X and Y cannot be less than zero");
            }

            if (!graphHeap.ContainsKey(position.X) || !graphHeap[position.X].ContainsKey(position.Y)) {
                return -1;
            }

            return graphHeap[position.X][position.Y].Item2;
        }
    }
}
