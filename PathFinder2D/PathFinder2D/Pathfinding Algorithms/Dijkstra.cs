namespace PathFinder2D.PathFindingAlgorithms
{
    using PathFinder2D.DataStructures;
    using PathFinder2D.Managers;
    using PathFinder2D.UI;
    using System.Diagnostics;
    using System.Windows;
    using System.Windows.Media;

    /// <summary>
    /// Dijkstra algorithm.
    /// </summary>
    public class Dijkstra
    {
        private readonly Graph graph;
        private readonly PathVisualizer pathVisualizer;
        private Stopwatch dijkstraStopwatch;
        private int visitedNodes = 0;
        private bool pathFound = false;
        private double shortestPathCost = 0;
        private bool running = false;
        private bool testOn = false;

        public Dijkstra(Graph graph, PathVisualizer visualizer)
        {
            this.graph = graph;
            this.pathVisualizer = visualizer;
            this.dijkstraStopwatch = new Stopwatch();
        }

        public List<Node> FindShortestPath(Node start, Node end)
        {
            this.dijkstraStopwatch.Start();
            this.running = true;

            start.Cost = 0;
            var gscore = new Dictionary<Node, double>();

            foreach (var row in this.graph.Nodes)
            {
                foreach (var node in row)
                {
                    gscore[node] = double.MaxValue;
                }
            }

            gscore[start] = 0;

            var priorityQueue = new PriorityQueue<Node, double>();
            priorityQueue.Enqueue(start, 0);

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
                    this.dijkstraStopwatch.Stop();

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
                        neighborNode.Parent = currentNode;
                        priorityQueue.Enqueue(neighborNode, tentative_g_score);
                    }
                }
            }

            this.running = false;
            this.dijkstraStopwatch.Stop();

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
        /// Retrieves the time Dijkstra took to find the end node.
        /// </summary>
        /// <returns>The time in milliseconds.</returns>
        public double GetStopwatchTime()
        {
            return this.dijkstraStopwatch.Elapsed.TotalMilliseconds;
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
        /// Retrieves the cost of the shortest path found by the Dijkstra algorithm.
        /// </summary>
        /// <returns>The cost of the shortest path.</returns>
        public double GetShortestPathCost()
        {
            return Math.Round(this.shortestPathCost, 1);
        }

        public bool IsRunning()
        {
            return this.running;
        }

        public void StopRunning()
        {
            this.running = false;
        }

        public void TurnOnTesting()
        {
            this.testOn = true;
        }
    }
}
