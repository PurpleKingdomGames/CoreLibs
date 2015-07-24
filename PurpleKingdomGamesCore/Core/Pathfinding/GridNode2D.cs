using PurpleKingdomGames.Core;

namespace PurpleKingdomGames.Core.Pathfinding
{
    /// <summary>
    /// Stores a node on a grid with a location in world space, penalty, and whether it's passable
    /// </summary>
    public struct GridNode2D
    {
        /// <summary>
        /// Get the position of this node in world space
        /// </summary>
        public readonly Point2D Position;

        /// <summary>
        /// Whether or not a path can cross through this node
        /// </summary>
        public bool Passable { get; set; }

        /// <summary>
        /// The penalty for passing through this node
        /// </summary>
        public int Penalty { get; set; }

        /// <summary>
        /// Create a new grid node
        /// </summary>
        /// <param name="position">The position in world space</param>
        public GridNode2D(Point2D position)
        {
            Position = position;
            Passable = false;
            Penalty = 0;
        }

        /// <summary>
        /// Convert a grid node to an internal calculated grid node (used for pathfinding)
        /// </summary>
        /// <returns></returns>
        internal GridNodeCalc2D ToGridNodeCalc2D()
        {
            return new GridNodeCalc2D(this);
        }
    }
}
