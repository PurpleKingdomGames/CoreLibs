
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
        public static GridNode2D[] Seek(GridNode2D[,] grid, Point2D start, Point2D target)
        {

            return new GridNode2D[0];
        }
    }
}
