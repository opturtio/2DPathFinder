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
        private Dijkstra dijkstra;

        /// <summary>
        /// Initializes a new instance of the <see cref="AlgorithmComparisonManager"/> class.
        /// </summary>
        /// <param name="comparisonGraph">The graph used to compare the Dijkstra and JPS algorithms.</param>
        public AlgorithmComparisonManager(Graph comparisonGraph)
        {
            this.graph = comparisonGraph;
        }

        /// <summary>
        /// Initializes the algorihms.
        /// </summary>
        public void Initialize()
        {
            this.dijkstra = new Dijkstra(this.graph);
            //this.dijkstra.FindShortestPath(graph.Nodes[0][0], graph.Nodes[9][9]);
        }
    }
}
