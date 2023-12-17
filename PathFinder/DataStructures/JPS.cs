namespace PathFinder.DataStructures
{
    /// <summary>
    /// Jump Point Search algorithm.
    /// </summary>
    public class JPS
    {
        private readonly Graph graph;
        private readonly PathVisualizer pathVisualizer;

        public JPS(Graph graph, PathVisualizer visualizer)
        {
            this.graph = graph;
            this.pathVisualizer = visualizer;
        }

        public List<Node> FindShortestPath(Node start, Node end)
        {
            start.Cost = 0;
            var priorityQueue = new PriorityQueue<Node, double>();
            priorityQueue.Enqueue(start, 0);

            var costSoFar = new Dictionary<Node, double>();

            costSoFar[start] = 0;

            while (priorityQueue.Count > 0)
            {
                var currentNode = priorityQueue.Dequeue();

                this.pathVisualizer.VisualizePath(currentNode);

                if (currentNode == end)
                {
                    break;
                }

                foreach (var direction in this.GetDirections())
                {
                    var neighborNode = this.Jump(currentNode, direction, end);
                    if (neighborNode != null && (!costSoFar.ContainsKey(neighborNode) || currentNode.Cost + this.Heuristic(currentNode, neighborNode) < costSoFar[neighborNode]))
                    {
                        double newCost = currentNode.Cost + this.Heuristic(currentNode, neighborNode);
                        costSoFar[neighborNode] = newCost;
                        double priority = newCost + this.Heuristic(end, neighborNode);
                        priorityQueue.Enqueue(neighborNode, priority);
                        neighborNode.Parent = currentNode;
                    }
                }
            }

            return ShortestPathBuilder.ShortestPath(end);
        }

        private Node? Jump(Node current, (int x, int y) direction, Node end)
        {

            return current;
        }

        private IEnumerable<(int x, int y)> GetDirections()
        {
            return new List<(int x, int y)>
            {
                (1, 0), (-1, 0), (0, 1), (0, -1),
                (1, 1), (-1, -1), (1, -1), (-1, 1),
            };
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