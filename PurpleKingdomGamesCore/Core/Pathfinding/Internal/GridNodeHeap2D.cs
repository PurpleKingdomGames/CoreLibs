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
            GridCoord2D pos = item.GridPosition;
            if (!graphHeap.ContainsKey(pos.X)) {
                graphHeap.Add(pos.X, new Dictionary<int, GridNodeCalc2D>());
            }

            graphHeap[pos.X][pos.Y] = item;

            base.Add(item);
        }

        public new GridNodeCalc2D Remove()
        {
            GridNodeCalc2D item = base.Remove();
            GridCoord2D pos = item.GridPosition;
            graphHeap[pos.X].Remove(pos.Y);

            return item;
        }

        public GridNodeCalc2D GetByPosition(GridCoord2D position)
        {
            if (position.X < 0 || position.Y < 0) {
                throw new Exception("X and Y cannot be less than zero");
            }

            if (!graphHeap.ContainsKey(position.X) || !graphHeap[position.X].ContainsKey(position.Y)) {
                return null;
            }

            return graphHeap[position.X][position.Y];
        }
    }
}
