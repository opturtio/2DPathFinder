namespace PathFinder2D.PathFindingAlgorithms
{
    using PathFinder2D.DataStructures;
    using PathFinder2D.Managers;
    using PathFinder2D.UI;
    using System.Diagnostics;
    using System.Windows;
    using System.Windows.Media;

    /// <summary>
    /// A* algorithm.
    /// </summary>
    public class Astar
    {
        private readonly Graph graph;
        private readonly PathVisualizer pathVisualizer;
        private readonly PathVisualizerWPF pathVisualizerWPF;
        private Stopwatch aStarStopwatch;
        private int visitedNodes = 0;
        private bool pathFound;
        private double shortestPathCost = 0;
        private bool running = false;
        private bool testOn = false;

        /// <summary>
        /// Initializes a new instance of the <see cref="Astar"/> class.
        /// </summary>
        /// <param name="graph">The graph to be processed by the A* algorithm.</param>
        /// <param name="visualizer">The path visualizer to visualize the A* algorithm in the console.</param>
        public Astar(Graph graph, PathVisualizer visualizer)
        {
            this.graph = graph;
            this.pathVisualizer = visualizer;
            this.aStarStopwatch = new Stopwatch();
        }

        /// <summary>
        /// Finds the shortest path between two given nodes.
        /// </summary>
        /// <param name="start">The point where the path begins.</param>
        /// <param name="end">The point where the path ends.</param>
        /// <returns>Shortest path in a form of a list of nodes.</returns>
        public List<Node> FindShortestPath(Node start, Node end)
        {
            this.aStarStopwatch.Start();
            this.running = true;

            start.Cost = 0;
            var gscore = new Dictionary<Node, double>();
            var priorityQueue = new PriorityQueue<Node, double>();
            priorityQueue.Enqueue(start, 0);

            // Initialize all nodes with max cost
            foreach (var row in this.graph.Nodes)
            {
                foreach (var node in row)
                {
                    gscore[node] = double.MaxValue;
                }
            }

            gscore[start] = 0;

            while (priorityQueue.Count > 0 && this.running)
            {
                var currentNode = priorityQueue.Dequeue();

                if (currentNode.Visited)
                {
                    continue;
                }

                currentNode.Visited = true;
                this.visitedNodes++;

                this.pathVisualizer.VisualizePath(currentNode, start, end);

                if (currentNode == end)
                {
                    this.pathFound = true;
                    this.shortestPathCost = gscore[end];

                    var finalPath = ShortestPathBuilder.ReconstructPath(end);
                    for (int i = 1; i < finalPath.Count; i++)
                    {
                        var fromNode = finalPath[i - 1];
                        var toNode = finalPath[i];

                        ((PathVisualizerWPF)this.pathVisualizer).DrawLineBetweenNodes(fromNode, toNode, Brushes.Red);
                    }

                    this.running = false;
                    this.aStarStopwatch.Stop();

                    return finalPath;
                }

                foreach (var (neighborNode, cost) in this.graph.GetNeighborsWithCosts(currentNode))
                {
                    if (!this.running)
                    {
                        break;
                    }

                    if (neighborNode.Visited)
                    {
                        continue;
                    }

                    double tentative_g_score = gscore[currentNode] + cost;

                    if (tentative_g_score < gscore[neighborNode])
                    {
                        gscore[neighborNode] = tentative_g_score;
                        double priority = tentative_g_score + this.Heuristic(end, neighborNode);
                        neighborNode.Parent = currentNode;
                        priorityQueue.Enqueue(neighborNode, priority);
                    }
                }
            }

            this.running = false;
            this.aStarStopwatch.Stop();

            if (!this.testOn)
            {
                MessageBox.Show($"Path not found!");
            }

            return new List<Node>();
        }


        /// <summary>
        /// Retrieves the total number of nodes that have been visited during the pathfinding.
        /// </summary>
        /// <returns>An integer representing the count of visited nodes.</returns>
        public int GetVisitedNodes()
        {
            return this.visitedNodes;
        }

        /// <summary>
        /// Estimates the cost to reach the end node from the current node using the Octile heuristic.
        /// </summary>
        /// <param name="end">The end node.</param>
        /// <param name="node">The current node being evaluated.</param>
        /// <returns>The estimated cost to reach the end node from the current node.</returns>
        private double Heuristic(Node end, Node node)
        {
            int dx = Math.Abs(end.X - node.X);
            int dy = Math.Abs(end.Y - node.Y);
            double D = 1.0;
            double D2 = Math.Sqrt(2);

            return D * (dx + dy) + (D2 - 2 * D) * Math.Min(dx, dy);
        }

        /// <summary>
        /// Retrieves the time A* took to find the end node.
        /// </summary>
        /// <returns>The time in milliseconds.</returns>
        public double GetStopwatchTime()
        {
            return this.aStarStopwatch.Elapsed.TotalMilliseconds;
        }

        /// <summary>
        /// Determines whether a path from the start node to the end node has been found.
        /// </summary>
        /// <returns>A boolean value indicating whether a path was successfully found. Returns true if a path exists, otherwise false.</returns>
        public bool IsPathFound()
        {
            return this.pathFound;
        }

        /// <summary>
        /// Retrieves the cost of the shortest path found by the A* algorithm.
        /// </summary>
        /// <returns>The cost of the shortest path.</returns>
        public double GetShortestPathCost()
        {
            return Math.Round(this.shortestPathCost, 1);
        }

        /// <summary>
        /// Determines if the A* algorithm is currently running.
        /// </summary>
        /// <returns>A boolean value indicating if the algorithm is running. Returns true if running, otherwise false.</returns>
        public bool IsRunning()
        {
            return this.running;
        }

        /// <summary>
        /// Stops the execution of the A* algorithm.
        /// </summary>
        public void StopRunning()
        {
            this.running = false;
        }

        /// <summary>
        /// Enables testing mode to prevent certain UI interactions, such as message boxes.
        /// </summary>
        public void TurnOnTesting()
        {
            this.testOn = true;
        }
    }
}
