namespace PathFinder2D.Managers
{
    using PathFinder2D.DataStructures;
    using System.Collections.Generic;

    /// <summary>
    /// A base class for visualizing the pathfinding process.
    /// </summary>
    public class PathVisualizer
    {
        protected readonly Graph graph;
        protected Node currentNode;
        protected string currentMap;

        /// <summary>
        /// Initializes a new instance of the <see cref="PathVisualizer"/> class.
        /// </summary>
        /// <param name="graph">The graph that is being visualized.</param>
        public PathVisualizer(Graph graph, string currentMap)
        {
            this.graph = graph;
            this.currentMap = currentMap;
        }

        /// <summary>
        /// Virtual method to visualize the current state of the pathfinding algorithm.
        /// Can be overridden by subclasses to provide specific visualization.
        /// </summary>
        /// <param name="currentNode">The node currently being visited or processed by the algorithm.</param>
        public virtual void VisualizePath(Node currentNode, Node start, Node end, bool jps = false)
        {
            // This method is intended to be overridden in derived classes.
        }
    }
}
