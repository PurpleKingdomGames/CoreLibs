using PurpleKingdomGames.Core;

namespace PurpleKingdomGames.Core.Pathfinding
{
    public struct GridNode2D
    {
        public Point2D Position { get; set; }
        public Point2D GridPosition { get; set; }
        public bool Passable { get; set; }
        public int Penalty { get; set; }

        public GridNodeCalc2D ToGridNodeCalc2D()
        {
            return new GridNodeCalc2D() { ReferenceNode = this };
        }
    }
}
