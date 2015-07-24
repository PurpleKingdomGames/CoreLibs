using NUnit.Framework;
using PurpleKingdomGames.Core;
using PurpleKingdomGames.Core.Pathfinding;
using PurpleKingdomGames.Core.Pathfinding.Seekers;

namespace PurpleKingdomGames.Test.Core.Pathfinding.Seekers
{
    [TestFixture]
    class AStarTest
    {
        /// <summary>
        /// Test that we can seek in 2D without additional options
        /// </summary>
        [Test]
        public void TestSeekSimple()
        {
            // The following are marked for clarity
            // S = start
            // A = first target
            // B = second target
            char[,] charGrid = new char[10, 10] {
                { '*', '*', '*', '*', '*', '*', '*', '*', '*', '*' },
                { '*', '*', '*', 'x', '*', '*', '*', '*', '*', '*' },
                { '*', '*', '*', 'x', '*', '9', 'x', 'x', 'x', '*' },
                { '*', '*', '*', 'x', '*', '9', 'x', '*', '*', '*' },
                { '*', '*', '*', '*', '*', '9', 'x', '*', '*', '*' },
                { '*', 'S', '*', '*', '*', '9', 'x', '*', '*', 'A' },
                { 'x', '9', 'x', 'x', '*', 'x', 'x', '*', 'x', 'x' },
                { '*', '9', '*', '*', '*', '*', '*', '*', 'x', '*' },
                { 'B', '9', '*', '*', '*', '*', '*', '*', 'x', '*' },
                { 'x', 'x', 'x', 'x', 'x', 'x', 'x', 'x', 'x', '*' },
            };

            // Initialise the grid
            GridNode2D[,] grid = buildGridFromChars(charGrid);

            /*Point2D start = new Point2D(1, 5);
            Point2D target = new Point2D(9, 5);*/

            Point2D start = new Point2D(1, 2);
            Point2D target = new Point2D(5, 2);

            Point2D[] path = AStar.Seek(grid, start, target);
            for (int i = 0; i < path.Length; i++) {

            }
        }

        private GridNode2D[,] buildGridFromChars(char[,] gridDef)
        {
            // Build a grid:
            //   * = empty 
            //   x = impassable
            //   1 - 9 = penalty
            GridNode2D[,] grid = new GridNode2D[gridDef.GetLength(0), gridDef.GetLength(1)];
            for (int x = 0; x < gridDef.GetLength(0); x++) {
                for (int y = 0; y < gridDef.GetLength(1); y++) {
                    grid[x, y] = new GridNode2D(new Point2D(x, y));
                    switch (gridDef[y, x]) {
                        case 'x':
                            grid[x, y].Passable = false;
                            break;
                        case '1':
                        case '2':
                        case '3':
                        case '4':
                        case '5':
                        case '6':
                        case '7':
                        case '8':
                        case '9':
                            grid[x, y].Penalty = int.Parse(gridDef[y, x].ToString());
                            grid[x, y].Passable = true;
                            break;
                        default:
                            grid[x, y].Passable = true;
                            break;
                    }
                }
            }

            return grid;
        }
    }
}
