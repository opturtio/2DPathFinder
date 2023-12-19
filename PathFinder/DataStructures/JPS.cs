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

        /// <summary>
        /// Generates jump point successors for a given node.
        /// </summary>
        /// <param name="current">The node to generate successors for.</param>
        /// <param name="end">The end node of the pathfinding process.</param>
        /// <returns>Enumerable of jump point successors.</returns>

        private IEnumerable<Node> GetJumpPointSuccessors(Node current, Node end)
        {
            var successors = new List<Node>();

            foreach (var direction in this.GetDirections(current, end))
            {
                var jumpPoint = this.Jump(current, direction, end);
                if (jumpPoint != null)
                {
                    successors.Add(jumpPoint);
                }
            }

            return successors;
        }

        /// <summary>
        /// Generates potential directions for movement based on the current and end nodes.
        /// Prioritizes horizontal and vertical directions towards the end node.
        /// Diagonal directions are added only if the current node is not aligned with the end node horizontally or vertically.
        /// (NEEDS TO BE OPTIMISED!!!).
        /// </summary>
        /// <param name="current">The current node from which to calculate directions.</param>
        /// <param name="end">The end node towards which the directions are aimed.</param>
        /// <returns>A list of tuples representing the possible directions for movement.</returns>
        private IEnumerable<(int x, int y)> GetDirections(Node current, Node end)
        {
            var directions = new List<(int x, int y)>();

            // Prioritize horizontal and vertical directions towards the end node
            if (end.X != current.X)
            {
                if (end.X > current.X)
                {
                    directions.Add((1, 0));
                }
                else
                {
                    directions.Add((-1, 0));
                }
            }

            if (end.Y != current.Y)
            {
                if (end.Y > current.Y)
                {
                    directions.Add((0, 1));
                }
                else
                {
                    directions.Add((0, -1));
                }
            }

            // Add diagonal directions only if not aligned horizontally or vertically
            if (end.X != current.X && end.Y != current.Y)
            {
                if (end.X > current.X && end.Y > current.Y)
                {
                    directions.Add((1, 1));
                }
                else if (end.X > current.X && end.Y < current.Y)
                {
                    directions.Add((1, -1));
                }
                else if (end.X < current.X && end.Y > current.Y)
                {
                    directions.Add((-1, 1));
                }
                else
                {
                    directions.Add((-1, -1));
                }
            }

            return directions;
        }

        /// <summary>
        /// Recursive function to find a jump point in a specific direction.
        /// </summary>
        /// <param name="current">The current node to jump from.</param>
        /// <param name="direction">The direction to jump in.</param>
        /// <param name="end">The end node of the pathfinding process.</param>
        /// <returns>The jump point node if one is found, otherwise null.</returns>
        private Node? Jump(Node current, (int x, int y) direction, Node end)
        {
            int nextX = current.X + direction.x;
            int nextY = current.Y + direction.y;

            if (!this.graph.CanMove(nextX, nextY))
            {
                return null;
            }

            Node nextNode = this.graph.Nodes[nextY][nextX];
            if (nextNode == end)
            {
                return nextNode;
            }

            if (this.graph.HasForcedNeighbors(current, direction))
            {
                return nextNode;
            }

            // Continue in the same direction for horizontal/vertical moves
            if (direction.x == 0 || direction.y == 0)
            {
                return this.Jump(nextNode, direction, end);
            }

            // For diagonal moves, check horizontal and vertical directions separately
            if (this.Jump(nextNode, (direction.x, 0), end) != null || this.Jump(nextNode, (0, direction.y), end) != null)
            {
                return nextNode;
            }

            // Continue jumping in the current direction for diagonal moves
            return this.Jump(nextNode, direction, end);
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