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

            Point2D start = new Point2D(1, 5);
            Point2D target = new Point2D(9, 5);

            Point2D[] expectedPath = new Point2D[] {
                new Point2D(1, 5), new Point2D(2, 5), new Point2D(3, 5),
                new Point2D(4, 6), new Point2D(5, 7), new Point2D(6, 7),
                new Point2D(7, 6), new Point2D(8, 5), new Point2D(9, 5),
            };
            Point2D[] path = AStar.Seek(grid, start, target);

            // Check that the path taken was the same as the expected path
            Assert.AreEqual(expectedPath, path, "Path from S to A did not match");

            // Set a new target (B)
            target = new Point2D(0, 8);

            expectedPath = new Point2D[] {
                new Point2D(1, 5), new Point2D(1, 6), new Point2D(0, 7),
                new Point2D(0, 8),
            };
            path = AStar.Seek(grid, start, target);

            // Check that the path taken was the same as the expected path
            Assert.AreEqual(expectedPath, path, "Path from S to B did not match");
        }

        /// <summary>
        /// Test that we can seek in 2D without cutting corners and with a high descent cost
        /// </summary>
        [Test]
        public void TestSeekComplicated()
        {
            // The following are marked for clarity
            // S = start
            // A = first target
            // B = second target
            char[,] charGrid = new char[10, 10] {
                { 'S', '*', '*', '*', '*', '*', '*', '*', '*', '*' },
                { 'x', 'x', '*', 'x', 'x', 'x', '*', '*', '*', '*' },
                { '*', '*', '*', 'x', 'A', '*', '*', '*', '*', '*' },
                { '*', '*', '*', '*', '*', 'x', 'x', '*', '*', '*' },
                { '*', '*', '*', '*', '*', '*', 'x', '*', '*', '*' },
                { 'x', 'x', 'x', 'x', 'x', 'x', 'x', '*', '*', '*' },
                { '*', '*', '*', '*', '*', '*', '*', '*', '*', '*' },
                { '*', '*', '*', '*', '*', '*', '*', '*', '*', '*' },
                { '*', '*', '*', '*', '*', '*', '*', '*', '*', '*' },
                { '*', '*', '*', '*', '*', '*', '*', '*', '*', '*' },
            };

            // Initialise the grid
            GridNode2D[,] grid = buildGridFromChars(charGrid);
            Point2D start = new Point2D(0, 0);
            Point2D target = new Point2D(4, 2);
            Point2D[] expectedPath = new Point2D[] {
                new Point2D(0, 0), new Point2D(1, 0), new Point2D(2, 0),
                new Point2D(3, 0), new Point2D(4, 0), new Point2D(5, 0),
                new Point2D(6, 0), new Point2D(6, 1), new Point2D(6, 2),
                new Point2D(5, 2), new Point2D(4, 2),
            };
            Point2D[] path = AStar.Seek(grid, start, target, false, 0, 1000);

            // Check that the path taken to A avoids the larger drop (due to the large descent cost)
            Assert.AreEqual(expectedPath, path, "Path from S to A did not match");
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
