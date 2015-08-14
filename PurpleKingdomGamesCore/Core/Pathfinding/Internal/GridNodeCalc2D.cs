using System;

namespace PurpleKingdomGames.Core.Pathfinding.Internal
{
    /// <summary>
    /// Internal class used for storing movement costs when pathfinding
    /// </summary>
    internal class GridNodeCalc2D : IComparable
    {
        /// <summary>
        /// The node that this uses for reference
        /// </summary>
        public readonly GridNode2D ReferenceNode;

        /// <summary>
        /// The cost of moving to this node from the parent
        /// </summary>
        public int MovementCost { get; set; }

        /// <summary>
        /// The calculated Heuristic cost of moving to this node
        /// </summary>
        public int HeuristicCost { get; set; }

        /// <summary>
        /// The position of this node on the grid
        /// </summary>
        public Point2D GridPosition { get; set; }

        /// <summary>
        /// The parent of this node
        /// </summary>
        public GridNodeCalc2D Parent { get; set; }

        /// <summary>
        /// Calculated total cost
        /// </summary>
        public int TotalCost
        {
            get
            {
                return MovementCost + HeuristicCost + ReferenceNode.Penalty;
            }
        }

        /// <summary>
        /// Creates a new calculated node, using the reference
        /// </summary>
        /// <param name="referenceNode"></param>
        public GridNodeCalc2D(GridNode2D referenceNode)
        {
            ReferenceNode = referenceNode;
        }

        /// <summary>
        /// Compares 2 calculated nodes to see if the totals are bigger or smaller.
        /// When compared to anything that is not a calculated node this method will return zero
        /// </summary>
        /// <param name="obj">The object to compare</param>
        /// <returns>
        /// The total cost of this node, minus that of the comparison node.
        /// Zero if comparison is not a GridNodeCalc2D object
        /// </returns>
        public int CompareTo(object obj)
        {
            if (obj.GetType() == typeof(GridNodeCalc2D)) {
                return TotalCost - ((GridNodeCalc2D) obj).TotalCost;
            }

            return 0;
        }

        public override bool Equals(object obj)
        {
            if (obj is GridNodeCalc2D) {
                return Equals((GridNodeCalc2D) obj);
            } else {
                return base.Equals(obj);
            }
        }

        public bool Equals(GridNodeCalc2D comp)
        {
            return ReferenceNode.Equals(comp.ReferenceNode);
        }
    }
}
