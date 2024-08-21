namespace PathFinder.Managers
{
    using PathFinder.DataStructures;
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
        /// Activates debugger so user can see how the algorithm works.
        /// </summary>
        public void ActivateDebugger()
        {
            isDebug = true;
        }

        /// <summary>
        /// Deactivates debugger.
        /// </summary>
        public void DeactivateDebugger()
        {
            isDebug = false;
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
        /// Visualizes the shortest path of the used algorithm.
        /// </summary>
        /// <param name="nodes">A list of nodes representing the shortest path from the start node to the end node.</param>
        public string VisualizeShortestPath(List<Node> nodes)
        {
            InitializeCurrentMap();
            visitedNodes.Clear();

            foreach (Node node in nodes)
            {
                // Add each node in the path to the visitedNodes list
                visitedNodes.Add(node);
            }

            // The last node in the list is the current node (end node of the path)
            if (currentNode == null)
            {
                return ShortestPathVisualizer();
            }

            currentNode = nodes.Last();

            // Generate the visualized map as a string
            return ShortestPathVisualizer();
        }

        /// <summary>
        /// Visualizes the current state of the path on the map in the console if the debugger is turned on.
        /// </summary>
        /// <param name="currentNode">The node currently being visited or processed by the algorithm.</param>
        public void VisualizePath(Node currentNode, Node start, Node end, bool jps = false)
        {
            if (isDebug)
            {
                this.currentNode = currentNode;
                startNode = start;
                endNode = end;
                isJps = jps;
                visitedNodes.Add(this.currentNode);
                Visualize();
            }
        }

        /// <summary>
        /// Visualizes the current and visited nodes on map state.
        /// </summary>
        public void Visualize()
        {
            string[] rows = currentMap.Split('\n');
            var outputBuffer = new StringBuilder();

            for (int y = 0; y < rows.Length; y++)
            {
                rows[y] = rows[y].Trim();

                for (int x = 0; x < rows[y].Length; x++)
                {
                    Node node = graph.Nodes[y][x];

                    if (node == currentNode)
                    {
                        outputBuffer.Append('X');
                    }
                    else if (node.JumpPoint && node != endNode && isJps)
                    {
                        outputBuffer.Append('J');
                    }
                    else if (node == startNode)
                    {
                        outputBuffer.Append('S');
                    }
                    else if (node == endNode)
                    {
                        outputBuffer.Append('G');
                    }
                    else if (visitedNodes.Contains(node))
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
            Stopwatch stopwatch = Stopwatch.StartNew();
            while (stopwatch.ElapsedTicks < Stopwatch.Frequency / 100) { }
        }

        /// <summary>
        /// Visualizes the pathfinding process for debugging purposes.
        /// </summary>
        /// <param name="map">Empty string representation of the map.</param>
        /// <param name="jps">Indicates whether the visualization is for the Jump Point Search (JPS) algorithm.
        /// Set to true if the map is being visualized for JPS. Defaults to false for other pathfinding algorithms.</param>
        /// <returns>A string representing the map after the pathfinding process, with special characters denoting key nodes and states.</returns>
        public string DebugVisualize(string map, bool jps = false)
        {
            string[] rows = map.Split('\n');
            var outputBuffer = new StringBuilder();

            for (int y = 0; y < rows.Length; y++)
            {
                rows[y] = rows[y].Trim();

                for (int x = 0; x < rows[y].Length; x++)
                {
                    Node node = graph.Nodes[y][x];

                    if (node.Visited)
                    {
                        outputBuffer.Append('#');
                    }
                    else if (jps == true && node.JumpPoint && node != endNode)
                    {
                        outputBuffer.Append('J');
                    }
                    else if (node == startNode)
                    {
                        outputBuffer.Append('S');
                    }
                    else if (node == endNode)
                    {
                        outputBuffer.Append('G');
                    }
                    else
                    {
                        outputBuffer.Append(rows[y][x]);
                    }
                }

                outputBuffer.AppendLine();
            }

            return outputBuffer.ToString();
        }

        private string ShortestPathVisualizer()
        {
            StringBuilder visualizedMap = new StringBuilder();
            string[] rows = currentMap.Split('\n');

            for (int y = 0; y < rows.Length; y++)
            {
                // Cleans the row from carriage return if one occurs.
                // Otherwise, an index out of range occurs.
                rows[y] = rows[y].Trim();

                for (int x = 0; x < rows[y].Length; x++)
                {
                    Node node = graph.Nodes[y][x];

                    if (node == currentNode)
                    {
                        visualizedMap.Append("X");
                    }
                    else if (visitedNodes.Contains(node))
                    {
                        visualizedMap.Append("#");
                    }
                    else if (node == startNode)
                    {
                        visualizedMap.Append('S');
                    }
                    else if (node == endNode)
                    {
                        visualizedMap.Append('G');
                    }
                    else
                    {
                        visualizedMap.Append(rows[y][x]);
                    }
                }

                visualizedMap.AppendLine();
            }

            return visualizedMap.ToString();
        }
    }
}
