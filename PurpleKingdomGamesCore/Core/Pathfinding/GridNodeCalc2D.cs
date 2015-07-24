using System;

namespace PurpleKingdomGames.Core.Pathfinding
{
    internal class GridNodeCalc2D : IComparable
    {
        public GridNode2D ReferenceNode { get; set; }
        public int MovementCost { get; set; }
        public int HeuristicCost { get; set; }
        public GridNodeCalc2D Parent { get; set; }

        public int TotalCost
        {
            get
            {
                return MovementCost + HeuristicCost + ReferenceNode.Penalty;
            }
        }

        public int CompareTo(object obj)
        {
            if (obj.GetType() == typeof(GridNodeCalc2D)) {
                return TotalCost - ((GridNodeCalc2D) obj).TotalCost;
            }

            return 0;
        }
    }
}
