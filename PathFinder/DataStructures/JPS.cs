namespace PathFinder.DataStructures
{
    /// <summary>
    /// Jump Point Search algorithm.
    /// </summary>
    public class JPS
    {
        private readonly Graph graph;
        private readonly PathVisualizer pathVisualizer;
        private int visitedNodes = 0;

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

            this.visitedNodes++;

            var costSoFar = new Dictionary<Node, double>();
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

                foreach (var neighborNode in this.GetJumpPointSuccessors(currentNode, end))
                {
                    if (neighborNode.Visited)
                    {
                        continue;
                    }

                    double newCost = costSoFar[currentNode] + this.Heuristic(currentNode, neighborNode);

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

        private IEnumerable<Node> GetJumpPointSuccessors(Node current, Node end)
        {
            var successors = new List<Node>();

            foreach (var direction in this.GetDirections())
            {
                var jumpPoint = this.Jump(current, direction, end);
                if (jumpPoint != null)
                {
                    successors.Add(jumpPoint);
                }
            }

            return successors;
        }

        private Node? Jump(Node current, (int x, int y) direction, Node end)
        {
            int nextX = current.X + direction.x;
            int nextY = current.Y + direction.y;

            // Out of bound.
            if (!this.IsValid(nextX, nextY))
            {
                return null;
            }

            Node nextNode = this.graph.Nodes[nextY][nextX];

            // Node is an obstacle.
            if (nextNode.IsObstacle)
            {
                return null;
            }

            // Reached the end node.
            if (nextNode == end)
            {
                return nextNode;
            }

            // If the direction is diagonal, check in horizontal and vertical directions
            if (direction.x != 0 && direction.y != 0)
            {
                if (this.Jump(nextNode, (direction.x, 0), end) != null || this.Jump(nextNode, (0, direction.y), end) != null)
                {
                    return nextNode;
                }
            }

            // Continue jumping in the current direction
            return this.Jump(nextNode, direction, end);
        }

        private bool IsValid(int x, int y)
        {
            if (y >= 0 && y < this.graph.Nodes.Count)
            {
                if (x >= 0 && x < this.graph.Nodes[y].Count)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
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

        /// <summary>
        /// Retrieves the total number of nodes that have been visited during the pathfinding.
        /// </summary>
        /// <returns>An integer representing the count of visited nodes.</returns>
        public int GetVisitedNodes()
        {
            return this.visitedNodes;
        }
    }
}