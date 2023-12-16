namespace PathFinder.DataStructures
{
    /// <summary>
    /// A* algorithm.
    /// </summary>
    public class Astar
    {
        private readonly Graph graph;
        private readonly PathVisualizer pathVisualizer;

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

            cameFrom[start] = null;
            costSoFar[start] = 0;

            while (priorityQueue.Count > 0)
            {
                var currentNode = priorityQueue.Dequeue();

                this.pathVisualizer.VisualizePath(currentNode);

                if (currentNode == end)
                {
                    break;
                }

                foreach (var (neighborNode, cost) in this.graph.GetNeighborsWithCosts(currentNode))
                {
                    double newCost;
                    if (costSoFar.ContainsKey(neighborNode))
                    {
                        newCost = costSoFar[neighborNode] + cost;
                    }
                    else
                    {
                        continue;
                    }

                    if (!costSoFar.ContainsKey(neighborNode) || newCost < costSoFar[neighborNode])
                    {
                        costSoFar[neighborNode] = newCost;
                        double priority = newCost + this.Heuristic(end, neighborNode);
                        priorityQueue.Enqueue(neighborNode, priority);
                        cameFrom[neighborNode] = currentNode;
                    }
                }
            }

            return ShortestPathBuilder.ShortestPath(end);
        }

        private double Heuristic(Node end, Node neighborNode)
        {
            return Math.Abs(end.X - neighborNode.X) + Math.Abs(end.Y - neighborNode.Y);
        }
    }
}
