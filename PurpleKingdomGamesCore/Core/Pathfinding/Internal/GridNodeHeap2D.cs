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
            Point2D pos = item.ReferenceNode.Position;
            if (!graphHeap.ContainsKey((int) pos.X)) {
                graphHeap.Add((int) pos.X, new Dictionary<int, GridNodeCalc2D>());
            }

            graphHeap[(int) pos.X][(int) pos.Y] = item;

            base.Add(item);
        }

        public new GridNodeCalc2D Remove()
        {
            GridNodeCalc2D item = base.Remove();
            Point2D pos = item.ReferenceNode.Position;
            graphHeap[(int) pos.X].Remove((int) pos.Y);

            return item;
        }

        public GridNodeCalc2D GetByPosition(Point2D position)
        {
            if (position.X < 0 || position.Y < 0) {
                throw new Exception("X and Y cannot be less than zero");
            }

            if (!graphHeap.ContainsKey((int) position.X) || !graphHeap[(int) position.X].ContainsKey((int) position.Y)) {
                return null;
            }

            return graphHeap[(int) position.X][(int) position.Y];
        }
    }
}
