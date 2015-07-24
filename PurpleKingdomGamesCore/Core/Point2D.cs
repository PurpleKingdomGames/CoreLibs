
namespace PurpleKingdomGames.Core
{
    public struct Point2D
    {
        public float X;
        public float Y;

        public Point2D(float x, float y)
        {
            X = x;
            Y = y;
        }

        public static Point2D operator +(Point2D point1, Point2D point2)
        {
            return new Point2D() { X = point1.X + point2.X, Y = point1.Y + point2.Y };
        }

        public override bool Equals(object obj)
        {
            if (obj is Point2D) {
                return Equals((Point2D) obj);
            } else {
                return base.Equals(obj);
            }
        }

        public bool Equals(Point2D point)
        {
            return this == point;
        }

        public static bool operator !=(Point2D point1, Point2D point2)
        {
            return (point1.X != point2.X || point1.Y != point2.Y);
        }

        public static bool operator ==(Point2D point1, Point2D point2)
        {
            return (point1.X == point2.X && point1.Y == point2.Y);
        }

        // See: http://answers.unity3d.com/questions/908269/c-overloading-equals-what-is-it-about.html#answer-908295
        public override int GetHashCode()
        {
            int hash = 17;
            // Suitable nullity checks etc, of course :)
            hash = hash * 23 + X.GetHashCode();
            hash = hash * 23 + Y.GetHashCode();
            return hash;
        }
    }
}
