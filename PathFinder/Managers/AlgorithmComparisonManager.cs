namespace PathFinder.Managers
{
    using PathFinder.Algorithms;
    using PathFinder.DataStructures;

    /// <summary>
    /// Manages algorithm comparison.
    /// </summary>
    public class AlgorithmComparisonManager
    {
        private readonly Graph graph;
        private readonly string currentMap;
        private Dijkstra dijkstra;
        private PathVisualizer pathVisualizer;

        /// <summary>
        /// Initializes a new instance of the <see cref="AlgorithmComparisonManager"/> class.
        /// </summary>
        /// <param name="comparisonGraph">The graph used to compare the Dijkstra and JPS algorithms.</param>
        /// /// <param name="currentMap">Current map in a string form.</param>
        public AlgorithmComparisonManager(Graph comparisonGraph, string currentMap)
        {
            this.graph = comparisonGraph;
            this.currentMap = currentMap;
            this.pathVisualizer = new PathVisualizer(this.graph, this.currentMap);
        }

        /// <summary>
        /// Initializes the algorihms.
        /// </summary>
        public void Initialize()
        {
            this.dijkstra = new Dijkstra(this.graph, this.pathVisualizer);
            this.dijkstra.FindShortestPath(this.graph.Nodes[0][0], this.graph.Nodes[0][7]);
        }
    }
}
