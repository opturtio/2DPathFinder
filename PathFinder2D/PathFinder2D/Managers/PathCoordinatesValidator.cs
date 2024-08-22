namespace PathFinder2D.Managers
{
    using PathFinder2D.DataStructures;

    /// <summary>
    /// Validates the start and the end coordinates given by the user.
    /// </summary>
    public static class PathCoordinatesValidator
    {
        private static int startPointX = 0;
        private static int startPointY = 0;
        private static int endPointX = 0;
        private static int endPointY = 0;
        private static int mapHeight = 0;
        private static int mapWidth = 0;
        private static int[] coordinates;
        private static string input = string.Empty;

        /// <summary>
        /// Starts the validation process. The main method of the PathCoordinatesValidator.
        /// </summary>
        /// <param name="graph">The current graph.</param>
        /// <param name="currentMap">The current map.</param>
        /// <returns>An array of the start and the end points.</returns>
        public static int[] StartValidation(Graph graph, string currentMap)
        {
            string[] rows = currentMap.Split('\n');

            // width is the length of rows, and height is the number of rows.
            mapWidth = rows[0].Trim().Length;
            mapHeight = rows.Length;

            while (true)
            {
                ProcessStartPoint();
                if (!Validate(graph, startPointX, startPointY))
                {
                    Console.WriteLine("Invalid start point. Try again.");
                }
                else
                {
                    break;
                }
            }

            while (true)
            {
                ProcessEndPoint();
                if (!Validate(graph, endPointX, endPointY))
                {
                    Console.WriteLine("Invalid end point. Try again.");
                }
                else
                {
                    break;
                }
            }

            Console.Clear();

            // The order of coordinates have to be swapped to align with the graph structure
            coordinates = new int[] { startPointY, startPointX, endPointY, endPointX };
            return coordinates;
        }

        private static void ProcessStartPoint()
        {
            Console.WriteLine("Enter the start point of the algorithm.");
            Console.Write($"Insert X-coordinate (max index {mapWidth - 1}): ");
            startPointX = ReadCoordinate();
            Console.Write($"Insert Y-coordinate (max index {mapHeight - 1}): ");
            startPointY = ReadCoordinate();
        }

        private static void ProcessEndPoint()
        {
            Console.WriteLine("Enter the end point of the algorithm.");
            Console.Write($"Insert X-coordinate (max index {mapWidth - 1}): ");
            endPointX = ReadCoordinate();
            Console.Write($"Insert Y-coordinate (max index {mapHeight - 1}): ");
            endPointY = ReadCoordinate();
        }

        private static int ReadCoordinate()
        {
            while (true)
            {
                input = Console.ReadLine();
                if (int.TryParse(input, out int coordinate))
                {
                    return coordinate;
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter an integer.");
                }
            }
        }

        private static bool Validate(Graph graph, int x, int y)
        {
            if (x >= mapWidth || x < 0)
            {
                Console.WriteLine("X-coordinate out of bounds!");
                return false;
            }

            if (y >= mapHeight || y < 0)
            {
                Console.WriteLine("Y-coordinate out of bounds!");
                return false;
            }

            if (graph.Nodes[y][x].IsObstacle)
            {
                Console.WriteLine("This coordinate is an obstacle!");
                return false;
            }

            return true;
        }
    }
}