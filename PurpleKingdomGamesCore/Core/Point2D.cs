namespace PurpleKingdomGames.Core
{
    /// <summary>
    /// A single 2-dimensional point in world space
    /// </summary>
    public struct Point2D
    {
        /// <summary>
        /// The X position of this point
        /// </summary>
        public float X;

        /// <summary>
        /// The Y position of this point
        /// </summary>
        public float Y;

        /// <summary>
        /// Creates a new point in world space with the co-ordinates supplied
        /// </summary>
        /// <param name="x">The x position of this point</param>
        /// <param name="y">The y position of this point</param>
        public Point2D(float x, float y)
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
        public static Point2D operator +(Point2D point1, Point2D point2)
        {
            return new Point2D() { X = point1.X + point2.X, Y = point1.Y + point2.Y };
        }

        /// <summary>
        /// Determines if 2 objects are  equal
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if (obj is Point2D) {
                return Equals((Point2D) obj);
            } else {
                return base.Equals(obj);
            }
        }

        /// <summary>
        /// Determines if 2 points are equal by comparing their X and Y values
        /// </summary>
        /// <param name="point">The point to compare</param>
        /// <returns></returns>
        public bool Equals(Point2D point)
        {
            return this == point;
        }

        /// <summary>
        /// etermines whether the two points are different from each other
        /// </summary>
        /// <param name="point1"></param>
        /// <param name="point2"></param>
        /// <returns></returns>
        public static bool operator !=(Point2D point1, Point2D point2)
        {
            return (point1.X != point2.X || point1.Y != point2.Y);
        }

        /// <summary>
        /// Determines if 2 points are equal by comparing their X and Y values
        /// </summary>
        /// <param name="point1"></param>
        /// <param name="point2"></param>
        /// <returns></returns>
        public static bool operator ==(Point2D point1, Point2D point2)
        {
            return (point1.X == point2.X && point1.Y == point2.Y);
        }

        /// <summary>
        /// Identify an object in a hash-based collection
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            // http://answers.unity3d.com/questions/908269/c-overloading-equals-what-is-it-about.html#answer-908295
            int hash = 17;
            hash = hash * 23 + X.GetHashCode();
            hash = hash * 23 + Y.GetHashCode();
            return hash;
        }
    }
}
