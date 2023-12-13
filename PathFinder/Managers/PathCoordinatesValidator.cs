using PathFinder.DataStructures;

namespace PathFinder.Managers
{
    public static class PathCoordinatesValidator
    {
        private static string startPointX = string.Empty;
        private static string startPointY = string.Empty;
        private static string endPointX = string.Empty;
        private static string endPointY = string.Empty;

        public static void StartValidation(Graph graph)
        {
            ProcessStartPoint();
            ProcessEndPoint();
            Validate(graph);
        }

        private static void ProcessStartPoint()
        {
            Console.WriteLine("Start point of the algorithm.");
            Console.WriteLine("Insert X-coordinate: ");
            startPointX = Console.ReadLine();
            Console.WriteLine("Insert Y-coordinate: ");
            startPointY = Console.ReadLine();
        }

        private static void ProcessEndPoint()
        {
            Console.WriteLine("End point of the algorithm.");
            Console.WriteLine("Insert X-coordinate: ");
            endPointX = Console.ReadLine();
            Console.WriteLine("Insert Y-coordinate: ");
            endPointY = Console.ReadLine();
        }

        private static void Validate(Graph graph)
        {
            for (int i = 0; i < graph.Nodes.Count; i++)
            {

            }
        }
    }
}
