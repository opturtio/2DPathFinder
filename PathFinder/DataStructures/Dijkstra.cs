namespace PathFinder.Algorithms
{
    using PathFinder.DataStructures;

    /// <summary>
    /// Dijkstra algorithm.
    /// </summary>
    public class Dijkstra
    {
        private readonly Graph graph;
        private Heap heap;

        /// <summary>
        /// Initializes a new instance of the <see cref="Dijkstra"/> class.
        /// </summary>
        /// <param name="graph">The graph to be processed by the Dijkstra algorithm</param>
        public Dijkstra(Graph graph)
        {
            this.graph = graph;
        }


    }
}
