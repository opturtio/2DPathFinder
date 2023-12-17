namespace PathFinder.DataStructures
{
    /// <summary>
    /// A* algorithm.
    /// </summary>
    public class Astar
    {
        private readonly Graph graph;
        private readonly PathVisualizer pathVisualizer;

        /// <summary>
        /// Initializes a new instance of the <see cref="Astar"/> class.
        /// </summary>
        /// <param name="graph">The graph to be processed by the A* algorithm.</param>
        /// <param name="visualizer">The path visualizer to visualize the A* algorithm in the console.</param>
        public Astar(Graph graph, PathVisualizer visualizer)
        {
            this.graph = graph;
            this.pathVisualizer = visualizer;
        }

        /// <summary>
        /// Finds the shortest path between two given nodes.
        /// </summary>
        /// <param name="start">The point where the path begins.</param>
        /// <param name="end">The point where the path ends.</param>
        /// <returns>Shortest path in a form of a list of nodes.</returns>
        public List<Node> FindShortestPath(Node start, Node end)
        {
            start.Cost = 0;
            var priorityQueue = new PriorityQueue<Node, double>();
            priorityQueue.Enqueue(start, 0);

            var cameFrom = new Dictionary<Node, Node?>();
            var costSoFar = new Dictionary<Node, double>();

            // Initialize all nodes with max cost
            foreach (var row in this.graph.Nodes)
            {
                foreach (var node in row)
                {
                    costSoFar[node] = double.MaxValue;
                }
            }

            costSoFar[start] = 0;

            while (priorityQueue.Count > 0)
            {
                var currentNode = priorityQueue.Dequeue();

                if (currentNode.Visited)
                {
                    continue;
                }

                currentNode.Visited = true;


                this.pathVisualizer.VisualizePath(currentNode);

                if (currentNode == end)
                {
                    break;
                }

                foreach (var (neighborNode, cost) in this.graph.GetNeighborsWithCosts(currentNode))
                {
                    if (neighborNode.Visited)
                    {
                        continue;
                    }

                    double newCost = costSoFar[currentNode] + cost;

                    if (!costSoFar.ContainsKey(neighborNode) || newCost < costSoFar[neighborNode])
                    {
                        costSoFar[neighborNode] = newCost;
                        double priority = newCost + this.Heuristic(end, neighborNode);
                        priorityQueue.Enqueue(neighborNode, priority);
                        neighborNode.Parent = currentNode;
                    }
                }
            }

            return ShortestPathBuilder.ShortestPath(end);
        }

        /// <summary>
        /// This method estimates how close a node is to the end point. It uses the Euclidean distance,
        /// which is just adding up the horizontal and vertical distances. This helps the algorithm
        /// decide which paths are worth looking at first to find the shortest route faster.
        /// </summary>
        /// <param name="end">The end point given by the user.</param>
        /// <param name="neighborNode">The node currently processed.</param>
        /// <returns>An estimated distance from the current node to the end point.</returns>
        private double Heuristic(Node end, Node node)
        {
            return Math.Sqrt(Math.Pow(end.X - node.X, 2) + Math.Pow(end.Y - node.Y, 2));
        }
    }
}
