namespace PurpleKingdomGames.Core.Pathfinding.Internal
{
    /// <summary>
    /// Stores 2D grid co-ordinates
    /// </summary>
    internal struct GridCoord2D
    {
        /// <summary>
        /// X Co-ordinates
        /// </summary>
        public int X;

        /// <summary>
        /// Y Co-ordinates
        /// </summary>
        public int Y;

        /// <summary>
        /// Main constructor
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public GridCoord2D(int x, int y)
        {
            X = x;
            Y = y;
        }

        /// <summary>
        /// Adds 2 points together by adding the X and Y positions
        /// </summary>
        /// <param name="point1"></param>
        /// <param name="point2"></param>
        /// <returns></returns>
        public static GridCoord2D operator +(GridCoord2D point1, GridCoord2D point2)
        {
            return new GridCoord2D(point1.X + point2.X, point1.Y + point2.Y);
        }

        /// <summary>
        /// Determines if 2 objects are  equal
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if (obj is GridCoord2D) {
                return Equals((GridCoord2D) obj);
            } else {
                return base.Equals(obj);
            }
        }

        /// <summary>
        /// Determines if 2 points are equal by comparing their X and Y values
        /// </summary>
        /// <param name="point">The point to compare</param>
        /// <returns></returns>
        public bool Equals(GridCoord2D point)
        {
            return this == point;
        }

        /// <summary>
        /// etermines whether the two points are different from each other
        /// </summary>
        /// <param name="point1"></param>
        /// <param name="point2"></param>
        /// <returns></returns>
        public static bool operator !=(GridCoord2D point1, GridCoord2D point2)
        {
            return (point1.X != point2.X || point1.Y != point2.Y);
        }

        /// <summary>
        /// Determines if 2 points are equal by comparing their X and Y values
        /// </summary>
        /// <param name="point1"></param>
        /// <param name="point2"></param>
        /// <returns></returns>
        public static bool operator ==(GridCoord2D point1, GridCoord2D point2)
        {
            return (point1.X == point2.X && point1.Y == point2.Y);
        }

        /// <summary>
        /// Identify an object in a hash-based collection
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            int hash = 17;
            hash = hash * 23 + X.GetHashCode();
            hash = hash * 23 + Y.GetHashCode();
            return hash;
        }
    }
}