using System.ComponentModel;
using System.Diagnostics;

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
        private Stopwatch jpsStopwatch;
        private int visitedNodes = 0;
        private bool pathFound = false;

        /// <summary>
        /// Initializes a new instance of the <see cref="JPS"/> class.
        /// </summary>
        /// <param name="graph">Graph on which pathfinding is performed.</param>
        /// <param name="visualizer">Visualizer for path visualization.</param>
        public JPS(Graph graph, PathVisualizer visualizer)
        {
            this.graph = graph;
            this.pathVisualizer = visualizer;
            this.jpsStopwatch = new Stopwatch();
        }

        /// <summary>
        /// Finds the shortest path between the start and end nodes using Jump Point Search algorithm.
        /// </summary>
        /// <param name="start">The start node of the path.</param>
        /// <param name="end">The end node of the path.</param>
        /// <returns>List of nodes representing the shortest path.</returns>
        public List<Node> FindShortestPath(Node start, Node end)
        {
            this.jpsStopwatch = new Stopwatch();
            this.jpsStopwatch.Start();

            start.Cost = 0;

            var openList = new PriorityQueue<Node, double>();
            openList.Enqueue(start, start.Cost);

            while (openList.Count > 0)
            {
                var currentNode = openList.Dequeue();

                this.visitedNodes++;

                if (currentNode == end)
                {
                    this.pathFound = true;
                    break;
                }

                var neighbors = this.PruneNeighbors(currentNode);

                foreach (var neighbor in neighbors)
                {
                    var jumpPointCoords = this.Jump(neighbor.X, neighbor.Y, currentNode.X, currentNode.Y, start, end);

                    if (jumpPointCoords == null)
                    {
                        continue;
                    }

                    var jumpPoint = this.graph.Nodes[jumpPointCoords.Value.y][jumpPointCoords.Value.x];
                    Console.WriteLine(jumpPoint.GetNodeInfo());

                    double newCost = currentNode.Cost + this.Heuristic(jumpPoint, end);

                    if (newCost < jumpPoint.Cost)
                    {
                        jumpPoint.Cost = newCost;
                        jumpPoint.JumpPoint = true;
                        Console.WriteLine(jumpPoint.GetNodeInfo());
                        openList.Enqueue(jumpPoint, newCost);
                    }
                }
            }

            this.jpsStopwatch.Stop();

            if (this.pathFound)
            {
                return ShortestPathBuilder.ShortestPath(end);
            }

            return new List<Node>();
        }

        private int MovementDirection(int to, int from)
        {
            int direction = to - from;

            if (direction > 0)
            {
                return 1;
            }
            else if (direction < 0)
            {
                return -1;
            }

            return 0;
        }

        private List<Node> PruneNeighbors(Node current)
        {
            List<Node> neighbors = new List<Node>();

            if (current.Parent != null)
            {
                int x = current.X;
                int y = current.Y;
                int px = current.Parent.X;
                int py = current.Parent.Y;

                int dx = this.MovementDirection(x, px);
                int dy = this.MovementDirection(y, py);

                // Diagonal movement
                if (dx != 0 && dy != 0)
                {
                    if (this.IsValidPosition(x, y + dy) && this.graph.CanMove(x, y + dy))
                    {
                        neighbors.Add(this.graph.Nodes[y + dy][x]);
                    }

                    if (this.IsValidPosition(x + dx, y) && this.graph.CanMove(x + dx, y))
                    {
                        neighbors.Add(this.graph.Nodes[y][x + dx]);
                    }

                    if (this.IsValidPosition(x + dx, y + dy) && this.graph.CanMove(x + dx, y + dy))
                    {
                        neighbors.Add(this.graph.Nodes[y + dy][x + dx]);
                    }

                    if (this.IsValidPosition(x - dx, y) && !this.graph.CanMove(x - dx, y))
                    {
                        neighbors.Add(this.graph.Nodes[y + dy][x - dx]);
                    }

                    if (this.IsValidPosition(x, y - dy) && !this.graph.CanMove(x, y - dy))
                    {
                        neighbors.Add(this.graph.Nodes[y - dy][x + dx]);
                    }
                }
                // Horizontal or vertical movement
                else
                {
                    if (dx == 0)
                    {
                        if (this.IsValidPosition(x, y + dy) && this.graph.CanMove(x, y + dy))
                        {
                            neighbors.Add(this.graph.Nodes[y + dy][x]);
                        }

                        if (this.IsValidPosition(x + 1, y + dy) && !this.graph.CanMove(x + 1, y))
                        {
                            neighbors.Add(this.graph.Nodes[y + dy][x + 1]);
                        }

                        if (this.IsValidPosition(x - 1, y + dy) && !this.graph.CanMove(x - 1, y))
                        {
                            neighbors.Add(this.graph.Nodes[y + dy][x - 1]);
                        }
                    }
                    else
                    {
                        if (this.IsValidPosition(x + dx, y) && this.graph.CanMove(x + dx, y))
                        {
                            neighbors.Add(this.graph.Nodes[y][x + dx]);
                        }

                        if (this.IsValidPosition(x + dx, y + 1) && !this.graph.CanMove(x, y + 1))
                        {
                            neighbors.Add(this.graph.Nodes[y + 1][x + dx]);
                        }

                        if (this.IsValidPosition(x + dx, y - 1) && !this.graph.CanMove(x, y - 1))
                        {
                            neighbors.Add(this.graph.Nodes[y - 1][x + dx]);
                        }
                    }
                }
            }
            else
            {
                var neighborCosts = this.graph.GetNeighborsWithCosts(current);

                foreach (var (neighbor, cost) in neighborCosts)
                {
                    if (this.IsValidPosition(neighbor.X, neighbor.Y))
                    {
                        neighbors.Add(neighbor);
                    }
                }
            }

            neighbors.ForEach(node => Console.WriteLine(node.GetNodeInfo()));
            return neighbors;
        }

        private bool IsValidPosition(int x, int y)
        {
            return y >= 0 && y < this.graph.Nodes.Count &&
                   x >= 0 && x < this.graph.Nodes[y].Count;
        }


        /// <summary>
        /// Recursive function to find a jump point in a specific direction.
        /// </summary>
        /// <param name="currentNode">The current node to jump from.</param>
        /// <param name="direction">The direction to jump in.</param>
        /// <param name="end">The end node of the pathfinding process.</param>
        /// <returns>The jump point node if one is found, otherwise null.</returns>
        private (int x, int y)? Jump(int x, int y, int px, int py, Node start, Node end)
        {
            int dx = x - px;
            int dy = y - py;

            // Check if the next position is within bounds and not an obstacle
            if (!this.graph.CanMove(x, y))
            {
                return null;
            }

            this.graph.Nodes[y][x].Parent = this.graph.Nodes[py][px];

            this.pathVisualizer.VisualizePath(this.graph.Nodes[y][x], start, end, true);

            // Check if the current position is the end node
            if (this.graph.Nodes[y][x] == this.graph.Nodes[end.Y][end.X])
            {
                return (x, y);
            }

            // Check for forced neighbors when moving diagonally
            if (dx != 0 && dy != 0)
            {
                if ((this.graph.CanMove(x - dx, y + dy) && !this.graph.CanMove(x - dx, y)) ||
                    (this.graph.CanMove(x + dx, y - dy) && !this.graph.CanMove(x, y - dy)))
                {
                    return (x, y);
                }

                // When moving diagonally, must check for vertical/horizontal jump points
                if (this.Jump(x + dx, y, x, y, start, end) != null || this.Jump(x, y + dy, x, y, start, end) != null)
                {
                    return (x, y);
                }
            }
            else
            {
                // Check for forced neighbors when moving horizontally or vertically
                if (dx != 0) // Moving along x-axis
                {
                    if ((this.graph.CanMove(x + dx, y + 1) && !this.graph.CanMove(x, y + 1)) ||
                        (this.graph.CanMove(x + dx, y - 1) && !this.graph.CanMove(x, y - 1)))
                    {
                        return (x, y);
                    }
                }
                else // Moving along y-axis
                {
                    if ((this.graph.CanMove(x + 1, y + dy) && !this.graph.CanMove(x + 1, y)) ||
                        (this.graph.CanMove(x - 1, y + dy) && !this.graph.CanMove(x - 1, y)))
                    {
                        return (x, y);
                    }
                }
            }

            // Recursive call in the direction of movement
            return this.Jump(x + dx, y + dy, x, y, start, end);
        }

        /// <summary>
        /// This method estimates how close a node is to the end point. It uses the Euclidean distance,
        /// which is just adding up the horizontal and vertical distances. This helps the algorithm
        /// decide which paths are worth looking at first to find the shortest route faster.
        /// </summary>
        /// <param name="end">The end point given by the user.</param>
        /// <param name="neighborNode">The node currently processed.</param>
        /// <returns>An estimated distance from the current node to the end point.</returns>
        private double Heuristic(Node jumpPoint, Node end)
        {
            /*
            Console.WriteLine($"end.X: {end.X}");
            Console.WriteLine($"end.Y: {end.Y}");
            Console.WriteLine($"jumpPoint.X: {jumpPoint.X}");
            Console.WriteLine($"jumpPoint.Y: {jumpPoint.Y}");
            */
            return Math.Sqrt(Math.Pow(end.X - jumpPoint.X, 2) + Math.Pow(end.Y - jumpPoint.Y, 2));
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
        /// Retrieves the time JPS took to find the end node.
        /// </summary>
        /// <returns>The time in milliseconds.</returns>
        public double GetStopwatchTime()
        {
            return this.jpsStopwatch.Elapsed.TotalMilliseconds;
        }

        /// <summary>
        /// Determines whether a path from the start node to the end node has been found.
        /// </summary>
        /// <returns>A boolean value indicating whether a path was successfully found. Returns true if a path exists, otherwise false.</returns>
        public bool IsPathFound()
        {
            return this.pathFound;
        }
    }
}