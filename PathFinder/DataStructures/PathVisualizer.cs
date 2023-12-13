namespace PathFinder.DataStructures
{
    using PathFinder.Managers;
    using System.Text;

    /// <summary>
    /// Visualizes the path finding process.
    /// </summary>
    public class PathVisualizer
    {
        private readonly string currentMapInitalized;
        private readonly Graph graph;
        private string currentMap;
        private HashSet<Node> visitedNodes;
        private Node currentNode;
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
            this.currentMapInitalized = currentMap;
            this.visitedNodes = new HashSet<Node>();
            this.isDebug = false;
        }

        /// <summary>
        /// Activates debugger so user can see how the algorithm works.
        /// </summary>
        public void ActivateDebugger()
        {
            this.isDebug = true;
        }

        /// <summary>
        /// Deactivates debugger.
        /// </summary>
        public void DeactiveDebugger()
        {
            this.isDebug = false;
        }

        /// <summary>
        /// Initializes the currentMap string
        /// </summary>
        public void InitializeCurrentMap()
        {
            this.currentMap = this.currentMapInitalized;
        }

        /// <summary>
        /// Visualizes the shortest path of the used algorithm.
        /// </summary>
        /// <param name="nodes">A list of nodes representing the shortest path from the start node to the end node.</param>
        public void VisualizeShortestPath(List<Node> nodes)
        {
            this.InitializeCurrentMap();
            this.visitedNodes.Clear();

            for (int i = 0;  i < nodes.Count; i++)
            {
                this.currentNode = nodes[i];
                this.visitedNodes.Add(this.currentNode);
                Console.WriteLine("The shortest path:");
                this.ShortestPathVisualizer();
            }
        }

        /// <summary>
        /// Visualizes the current state of the path on the map in the console if the debugger is turned on.
        /// </summary>
        /// <param name="currentNode">The node currently being visited or processed by the algorithm.</param>
        public void VisualizePath(Node currentNode)
        {
            if (this.isDebug)
            {
                this.currentNode = currentNode;
                this.visitedNodes.Add(this.currentNode);
                this.Visualize();
            }
        }

        /// <summary>
        /// Visualizes the current and visited nodes on map state.
        /// </summary>
        private void Visualize()
        {
            string[] rows = this.currentMap.Split('\n');
            var outputBuffer = new StringBuilder();

            for (int y = 0; y < rows.Length; y++)
            {
                rows[y] = rows[y].Trim();

                for (int x = 0; x < rows[y].Length; x++)
                {
                    Node node = this.graph.Nodes[y][x];

                    if (node == this.currentNode)
                    {
                        outputBuffer.Append('X');
                    }
                    else if (this.visitedNodes.Contains(node))
                    {
                        outputBuffer.Append('#');
                    }
                    else
                    {
                        outputBuffer.Append(rows[y][x]);
                    }
                }

                outputBuffer.AppendLine();
            }

            Console.SetCursorPosition(0, 0);
            Console.Write(outputBuffer.ToString());
            Thread.Sleep(100);
        }

        private void ShortestPathVisualizer()
        {
            Console.Clear();
            string[] rows = this.currentMap.Split('\n');

            for (int y = 0; y < rows.Length; y++)
            {
                // Cleans the row from carriage return if one occurs.
                // Otherwise, an index out of range occurs.
                rows[y] = rows[y].Trim();

                for (int x = 0; x < rows[y].Length; x++)
                {
                    Node node = this.graph.Nodes[y][x];

                    if (node == this.currentNode)
                    {
                        Console.Write("X");
                    }
                    else if (this.visitedNodes.Contains(node))
                    {
                        Console.Write("#");
                    }
                    else
                    {
                        Console.Write(rows[y][x]);
                    }
                }

                Console.WriteLine();
            }
        }
    }
}
