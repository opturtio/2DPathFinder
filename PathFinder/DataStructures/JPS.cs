namespace PathFinder.DataStructures
{
    /// <summary>
    /// Jump Point Search algorithm class.
    /// Implements the pathfinding logic using the Jump Point Search algorithm.
    /// </summary>
    public class JPS
    {
        private readonly Graph graph;
        private readonly PathVisualizer pathVisualizer;
        private int visitedNodes = 0;

        /// <summary>
        /// Initializes a new instance of the JPS class.
        /// </summary>
        /// <param name="graph">Graph on which pathfinding is performed.</param>
        /// <param name="visualizer">Visualizer for path visualization.</param>
        public JPS(Graph graph, PathVisualizer visualizer)
        {
            this.graph = graph;
            this.pathVisualizer = visualizer;
        }

        /// <summary>
        /// Finds the shortest path between the start and end nodes using Jump Point Search algorithm.
        /// </summary>
        /// <param name="start">The start node of the path.</param>
        /// <param name="end">The end node of the path.</param>
        /// <returns>List of nodes representing the shortest path.</returns>
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

                if (currentNode.Visited)
                {
                    continue;
                }

                currentNode.Visited = true;
                this.visitedNodes++;
                this.pathVisualizer.VisualizePath(currentNode);

                if (currentNode == end)
                {
                    break;
                }

                foreach (var nextNode in this.GetJumpPointSuccessors(currentNode, end))
                {
                    double newCost = costSoFar[currentNode] + this.Heuristic(currentNode, nextNode);

                    if (!costSoFar.ContainsKey(nextNode) || newCost < costSoFar[nextNode])
                    {
                        costSoFar[nextNode] = newCost;
                        double priority = newCost + this.Heuristic(end, nextNode);
                        priorityQueue.Enqueue(nextNode, priority);
                        nextNode.Parent = currentNode;
                    }
                }
            }

            return ShortestPathBuilder.ShortestPath(end);
        }

        /// <summary>
        /// Generates jump point successors for a given node.
        /// </summary>
        /// <param name="currentNode">The node to generate successors for.</param>
        /// <param name="end">The end node of the pathfinding process.</param>
        /// <returns>Enumerable of jump point successors.</returns>

        private IEnumerable<Node> GetJumpPointSuccessors(Node currentNode, Node end)
        {
            var successors = new List<Node>();

            foreach (var direction in this.GetDirections())
            {
                var jumpPoint = this.Jump(currentNode, direction, end);

                if (jumpPoint != null)
                {
                    successors.Add(jumpPoint);
                }
            }

            return successors;
        }

        /// <summary>
        /// Generates potential directions for movement based on the current and end nodes.
        /// </summary>
        /// <returns>A list of tuples representing the possible directions for movement.</returns>
        private IEnumerable<(int x, int y)> GetDirections()
        {
            return new List<(int x, int y)>
            {
                (1, 0), (-1, 0), (0, 1), (0, -1),
                (1, 1), (-1, -1), (1, -1), (-1, 1),
            };
        }

        /// <summary>
        /// Recursive function to find a jump point in a specific direction.
        /// </summary>
        /// <param name="currentNode">The current node to jump from.</param>
        /// <param name="direction">The direction to jump in.</param>
        /// <param name="end">The end node of the pathfinding process.</param>
        /// <returns>The jump point node if one is found, otherwise null.</returns>
        private Node? Jump(Node currentNode, (int x, int y) direction, Node end)
        {
            int nextX = currentNode.X + direction.x;
            int nextY = currentNode.Y + direction.y;

            if (!this.graph.CanMove(nextX, nextY))
            {
                return null;
            }

            Node nextNode = this.graph.Nodes[nextY][nextX];

            if (nextNode == end)
            {
                return nextNode;
            }

            if (this.HasForcedNeighbors(nextNode, direction))
            {
                return nextNode;
            }

            if (direction.x != 0 && direction.y == 0)
            {
                return this.Jump(nextNode, direction, end);
            }
            else if (direction.x == 0 && direction.y != 0)
            {
                return this.Jump(nextNode, direction, end);
            }

            if (direction.x != 0 && direction.y != 0)
            {
                if (this.Jump(nextNode, (direction.x, 0), end) != null || this.Jump(nextNode, (0, direction.y), end) != null)
                {
                    return nextNode;
                }
            }

            return null;
        }

        /// <summary>
        /// Used by JPS algorithm.
        /// Determines if a node has forced neighbors based on the Jump Point Search algorithm.
        /// Forced neighbors are adjacent nodes that could lead to a shortest path but are not along the current path's direct line or diagonal.
        /// </summary>
        /// <param name="current">The current node being evaluated.</param>
        /// <param name="direction">A tuple containing the deltaX and deltaY representing the direction of movement from the current node.</param>
        /// <returns>Returns true if forced neighbors are detected in the specified direction, otherwise returns false.</returns>
        private bool HasForcedNeighbors(Node current, (int x, int y) direction)
        {
            if (direction.x != 0)
            {
                if ((!this.graph.CanMove(current.X, current.Y + 1) && this.graph.CanMove(current.X + direction.x, current.Y + 1)) ||
                    (!this.graph.CanMove(current.X, current.Y - 1) && this.graph.CanMove(current.X + direction.x, current.Y - 1)))
                {
                    return true;
                }
            }

            if (direction.y != 0)
            {
                if ((!this.graph.CanMove(current.X + 1, current.Y) && this.graph.CanMove(current.X + 1, current.Y + direction.y)) ||
                    (!this.graph.CanMove(current.X - 1, current.Y) && this.graph.CanMove(current.X - 1, current.Y + direction.y)))
                {
                    return true;
                }
            }

            if (direction.x != 0 && direction.y != 0)
            {
                if (this.HasForcedNeighbors(current, (direction.x, 0)) || this.HasForcedNeighbors(current, (0, direction.y)))
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// This method estimates how close a node is to the end point. It uses the Euclidean distance,
        /// which is just adding up the horizontal and vertical distances. This helps the algorithm
        /// decide which paths are worth looking at first to find the shortest route faster.
        /// </summary>
        /// <param name="end">The end point given by the user.</param>
        /// <param name="neighborNode">The node currently processed.</param>
        /// <returns>An estimated distance from the current node to the end point.</returns>
        private double Heuristic(Node end, Node neighborNode)
        {
            return Math.Sqrt(Math.Pow(end.X - neighborNode.X, 2) + Math.Pow(end.Y - neighborNode.Y, 2));
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