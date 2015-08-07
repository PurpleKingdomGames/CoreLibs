using PurpleKingdomGames.Core.Collections;
using System;
using System.Collections.Generic;

namespace PurpleKingdomGames.Core.Pathfinding.Internal
{
    internal class GridNodeHeap2D : BinaryHeap<GridNodeCalc2D>
    {
        private Dictionary<int, Dictionary<int, GridNodeCalc2D>> graphHeap = new Dictionary<int, Dictionary<int, GridNodeCalc2D>>();

        public new void Add(GridNodeCalc2D item)
        {
            Point2D pos = item.GridPosition;
            if (!graphHeap.ContainsKey(pos.IntX)) {
                graphHeap.Add(pos.IntX, new Dictionary<int, GridNodeCalc2D>());
            }

            graphHeap[pos.IntX][pos.IntY] = item;

            base.Add(item);
        }

        public new GridNodeCalc2D Remove()
        {
            GridNodeCalc2D item = base.Remove();
            Point2D pos = item.GridPosition;
            graphHeap[pos.IntX].Remove(pos.IntY);

            return item;
        }

        public GridNodeCalc2D GetByPosition(Point2D position)
        {
            if (position.IntX < 0 || position.IntY < 0) {
                throw new Exception("X and Y cannot be less than zero");
            }

            if (!graphHeap.ContainsKey(position.IntX) || !graphHeap[position.IntX].ContainsKey(position.IntY)) {
                return null;
            }

            return graphHeap[position.IntX][position.IntY];
        }
    }
}
