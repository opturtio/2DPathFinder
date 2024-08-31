namespace PathFinder2D.Managers
{
    using PathFinder2D.DataStructures;
    using System.Diagnostics;
    using System.Text;

    /// <summary>
    /// Visualizes the path finding process.
    /// </summary>
    public class PathVisualizer
    {
        private readonly string currentMapInitalized;
        private readonly Graph graph;
        private string currentMap;
        private bool isJps;
        private HashSet<Node> visitedNodes;
        private Node currentNode;
        private Node startNode;
        private Node endNode;
        private bool isDebug;

        /// <summary>
        /// Initializes a new instance of the <see cref="PathVisualizer"/> class.
        /// </summary>
        /// <param name="graph">The graph that to be visualized.</param>
        /// <param name="currentMap">The initial state of the map that will be visualized.</param>
        public PathVisualizer(Graph graph, string currentMap)
        {
            this.graph = graph;
            this.currentMap = currentMap;
            currentMapInitalized = currentMap;
            visitedNodes = new HashSet<Node>();
            isDebug = false;
        }

        public void ClearVisitedNodes()
        {
            visitedNodes.Clear();
        }

        /// <summary>
        /// Initializes the currentMap string.
        /// </summary>
        public void InitializeCurrentMap()
        {
            currentMap = currentMapInitalized;
        }

        /// <summary>
        /// Retrieves current map.
        /// </summary>
        /// <returns>Returns current map.</returns>
        public string GetCurrentMap()
        {
            return currentMap;
        }

        /// <summary>
        /// Visualizes the current state of the path on the map in the console if the debugger is turned on.
        /// </summary>
        /// <param name="currentNode">The node currently being visited or processed by the algorithm.</param>
        public virtual void VisualizePath(Node currentNode, Node start, Node end, bool jps = false)
        {
            if (isDebug)
            {
                this.currentNode = currentNode;
                startNode = start;
                endNode = end;
                isJps = jps;
                visitedNodes.Add(this.currentNode);
            }
        }
    }
}
