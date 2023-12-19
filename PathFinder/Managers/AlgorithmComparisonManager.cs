namespace PathFinder.Managers
{
    using System.Diagnostics;
    using PathFinder.Algorithms;
    using PathFinder.DataStructures;

    /// <summary>
    /// Manages algorithm comparison.
    /// </summary>
    public class AlgorithmComparisonManager
    {
        private readonly string currentMap;
        private readonly string debugInput;
        private readonly Graph graph;
        private readonly CommandManager commandManager;
        private Dijkstra dijkstra;
        private Astar aStar;
        private JPS jps;
        private PathVisualizer pathVisualizer;
        private List<Node> shortestPathDijkstra;
        private List<Node> shortestPathAstar;
        private List<Node> shortestPathJps;
        private OutputManager outputManager;
        private Stopwatch dijkstraStopwatch;
        private Stopwatch aStarStopwatch;
        private Stopwatch jpsStopwatch;

        /// <summary>
        /// Initializes a new instance of the <see cref="AlgorithmComparisonManager"/> class.
        /// </summary>
        /// <param name="comparisonGraph">The graph used to compare the Dijkstra and JPS algorithms.</param>
        /// /// <param name="currentMap">Current map in a string form.</param>
        public AlgorithmComparisonManager(Graph comparisonGraph, string currentMap)
        {
            this.graph = comparisonGraph;
            this.currentMap = currentMap;
            this.debugInput = debugInput;
            this.pathVisualizer = new PathVisualizer(this.graph, this.currentMap);
            this.shortestPathDijkstra = new List<Node>();
            this.shortestPathAstar = new List<Node>();
            this.shortestPathJps = new List<Node>();
            this.commandManager = new CommandManager();
            this.outputManager = new OutputManager();
            this.dijkstraStopwatch = new Stopwatch();
            this.aStarStopwatch = new Stopwatch();
            this.jpsStopwatch = new Stopwatch();
        }

        /// <summary>
        /// Initializes the algorihms.
        /// </summary>
        public void Initialize()
        {
            var debugInput = this.commandManager.ProcessDebugOption();
            if (debugInput == "y")
            {
                this.pathVisualizer.ActivateDebugger();
            }
            else if (debugInput == "n")
            {
                this.pathVisualizer.DeactivateDebugger();
            }
            else
            {
                return;
            }

            var coords = PathCoordinatesValidator.StartValidation(this.graph, this.currentMap);

            this.jps = new JPS(this.graph, this.pathVisualizer);
            this.jpsStopwatch.Start();
            this.shortestPathJps = this.jps.FindShortestPath(this.graph.Nodes[coords[0]][coords[1]], this.graph.Nodes[coords[2]][coords[3]]);
            this.jpsStopwatch.Stop();

            this.pathVisualizer.ClearVisitedNodes();
            this.graph.ResetNodes();

            this.aStar = new Astar(this.graph, this.pathVisualizer);
            this.aStarStopwatch.Start();
            this.shortestPathAstar = this.aStar.FindShortestPath(this.graph.Nodes[coords[0]][coords[1]], this.graph.Nodes[coords[2]][coords[3]]);
            this.aStarStopwatch.Stop();

            this.pathVisualizer.ClearVisitedNodes();
            this.graph.ResetNodes();

            this.dijkstra = new Dijkstra(this.graph, this.pathVisualizer);
            this.dijkstraStopwatch.Start();
            this.shortestPathDijkstra = this.dijkstra.FindShortestPath(this.graph.Nodes[coords[0]][coords[1]], this.graph.Nodes[coords[2]][coords[3]]);
            this.dijkstraStopwatch.Stop();

            this.PrintResults();
        }

        /// <summary>
        /// Prints to console the results of the Algorithm comparison.
        /// </summary>
        public void PrintResults()
        {
            var dijkstraMap = this.pathVisualizer.VisualizeShortestPath(this.shortestPathDijkstra);
            var aStarMap = this.pathVisualizer.VisualizeShortestPath(this.shortestPathAstar);
            Console.Clear();
            Console.WriteLine("Dijkstra shortest path:");
            Console.WriteLine(dijkstraMap);
            Console.WriteLine("A* shortest path:");
            Console.WriteLine(aStarMap);
            Console.WriteLine("Results:");
            Console.WriteLine("------------------------------------------------------");
            Console.WriteLine(String.Format("| {0,-10} | {1,-14} | {2,-20} |", "Algorithm", "Visited nodes", "Time(milliseconds)"));
            Console.WriteLine(String.Format("| {0,-10} | {1,-14} | {2,-20} |", "Dijkstra", this.dijkstra.GetVisitedNodes(), this.dijkstraStopwatch.ElapsedMilliseconds));
            Console.WriteLine(String.Format("| {0,-10} | {1,-14} | {2,-20} |", "A*", this.aStar.GetVisitedNodes(), this.aStarStopwatch.ElapsedMilliseconds));
            Console.WriteLine(String.Format("| {0,-10} | {1,-14} | {2,-20} |", "JPS", this.jps.GetVisitedNodes(), this.jpsStopwatch.ElapsedMilliseconds));
            Console.WriteLine("------------------------------------------------------");
            Console.WriteLine();
        }
    }
}
