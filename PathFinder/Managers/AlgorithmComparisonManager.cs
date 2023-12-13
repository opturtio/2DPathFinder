namespace PathFinder.Managers
{
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
        private PathVisualizer pathVisualizer;
        private List<Node> shortestPath;

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
            this.shortestPath = new List<Node>();
            this.commandManager = new CommandManager();
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
                this.pathVisualizer.DeactiveDebugger();
            }
            else
            {
                return;
            }

            PathCoordinatesValidator.StartValidation(this.graph);

            this.dijkstra = new Dijkstra(this.graph, this.pathVisualizer);
            this.shortestPath = this.dijkstra.FindShortestPath(this.graph.Nodes[0][0], this.graph.Nodes[255][255]);
            this.pathVisualizer.VisualizeShortestPath(this.shortestPath);
        }
    }
}
