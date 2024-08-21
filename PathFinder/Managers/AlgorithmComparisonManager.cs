namespace PathFinder.Managers
{
    using PathFinder.Algorithms;
    using PathFinder.DataStructures;
    using System.Linq;

    /// <summary>
    /// Manages algorithm comparison.
    /// </summary>
    public class AlgorithmComparisonManager
    {
        private readonly string currentMap;
        private readonly Graph graph;
        private Dijkstra dijkstra;
        private Astar aStar;
        private JPS jps;
        private PathVisualizer pathVisualizer;
        private List<Node> shortestPathDijkstra;
        private List<Node> shortestPathAstar;
        private List<Node> shortestPathJps;
        private double shortestPathCostDijkstra;
        private double shortestPathCostAstar;
        private double shortestPathCostJps;

        /// <summary>
        /// Initializes a new instance of the <see cref="AlgorithmComparisonManager"/> class.
        /// </summary>
        /// <param name="comparisonGraph">The graph used to compare the Dijkstra, A*, and JPS algorithms.</param>
        /// <param name="currentMap">Current map in a string form.</param>
        public AlgorithmComparisonManager(Graph comparisonGraph, string currentMap)
        {
            this.graph = comparisonGraph;
            this.currentMap = currentMap;
            this.pathVisualizer = new PathVisualizer(this.graph, this.currentMap);
            this.shortestPathDijkstra = new List<Node>();
            this.shortestPathAstar = new List<Node>();
            this.shortestPathJps = new List<Node>();
        }

        /// <summary>
        /// Initializes the algorithms and compares their performance.
        /// </summary>
        public void Initialize()
        {
            // Get the start and end coordinates from user input.
            var coords = PathCoordinatesValidator.StartValidation(this.graph, this.currentMap);

            // Run JPS algorithm
            this.jps = new JPS(this.graph, this.pathVisualizer);
            var jpsResult = this.jps.FindShortestPath(this.graph.Nodes[coords[0]][coords[1]], this.graph.Nodes[coords[2]][coords[3]]);
            this.shortestPathCostJps = this.jps.GetShortestPathLength();

            this.pathVisualizer.ClearVisitedNodes();
            this.graph.ResetNodes();

            // Run A* algorithm
            this.aStar = new Astar(this.graph, this.pathVisualizer);
            this.shortestPathAstar = this.aStar.FindShortestPath(this.graph.Nodes[coords[0]][coords[1]], this.graph.Nodes[coords[2]][coords[3]]);
            this.shortestPathCostAstar = this.aStar.GetShortestPathCost();

            this.pathVisualizer.ClearVisitedNodes();
            this.graph.ResetNodes();

            // Run Dijkstra algorithm
            this.dijkstra = new Dijkstra(this.graph, this.pathVisualizer);
            this.shortestPathDijkstra = this.dijkstra.FindShortestPath(this.graph.Nodes[coords[0]][coords[1]], this.graph.Nodes[coords[2]][coords[3]]);
            this.shortestPathCostDijkstra = this.dijkstra.GetShortestPathCost();

            // Display results
            this.PrintResults();

            // Check and print if paths were found
            bool jpsFound = this.jps.IsPathFound();
            bool aStarFound = this.aStar.IsPathFound();
            bool dijkstraFound = this.dijkstra.IsPathFound();

            if (!jpsFound && !aStarFound && !dijkstraFound)
            {
                Console.WriteLine("No paths found!\n");
            }
            else
            {
                if (jpsFound)
                {
                    Console.WriteLine("Path found by JPS.");
                }

                if (aStarFound)
                {
                    Console.WriteLine("Path found by A*.");
                }

                if (dijkstraFound)
                {
                    Console.WriteLine("Path found by Dijkstra.");
                }
            }
        }

        /// <summary>
        /// Prints to console the results of the Algorithm comparison.
        /// </summary>
        public void PrintResults()
        {
            var dijkstraMap = this.pathVisualizer.VisualizeShortestPath(this.shortestPathDijkstra);
            var aStarMap = this.pathVisualizer.VisualizeShortestPath(this.shortestPathAstar);
            var jpsMap = this.pathVisualizer.VisualizeShortestPath(this.shortestPathJps);

            Console.Clear();
            Console.WriteLine("Dijkstra shortest path:");
            Console.WriteLine(dijkstraMap);
            Console.WriteLine("A* shortest path:");
            Console.WriteLine(aStarMap);
            Console.WriteLine("JPS shortest path:");
            Console.WriteLine(jpsMap);

            Console.WriteLine("Results:");
            Console.WriteLine("--------------------------------------------------------------------");
            Console.WriteLine(String.Format("| {0,-11} | {1,-14} | {2,-20} | {3,-10} |", "Algorithm", "Visited nodes", "Time(milliseconds)", "Cost"));
            Console.WriteLine(String.Format("| {0,-11} | {1,-14} | {2,-20} | {3,-10} |", "Dijkstra", this.dijkstra.GetVisitedNodes(), this.dijkstra.GetStopwatchTime(), this.shortestPathCostDijkstra));
            Console.WriteLine(String.Format("| {0,-11} | {1,-14} | {2,-20} | {3,-10} |", "A*", this.aStar.GetVisitedNodes(), this.aStar.GetStopwatchTime(), this.shortestPathCostAstar));
            Console.WriteLine(String.Format("| {0,-11} | {1,-14} | {2,-20} | {3,-10} |", "JPS", this.jps.GetVisitedNodes(), this.jps.GetStopwatchTime(), this.shortestPathCostJps));
            Console.WriteLine("--------------------------------------------------------------------");

            Console.WriteLine();
        }
    }
}
