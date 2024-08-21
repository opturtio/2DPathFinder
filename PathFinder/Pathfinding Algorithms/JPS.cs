using System.Diagnostics;
using PathFinder.Managers;
using System.Collections.Generic;
using System.Linq;

namespace PathFinder.DataStructures
{
    /// <summary>
    /// Jump Point Search (JPS) algorithm implementation.
    /// </summary>
    public class JPS
    {
        private readonly Graph graph;
        private readonly PathVisualizer pathVisualizer;
        private Stopwatch jpsStopwatch;
        private double shortestPathCost = 0;
        private int visitedNodes = 0;
        private bool pathFound = false;

        /// <summary>
        /// Initializes a new instance of the <see cref="JPS"/> class.
        /// </summary>
        /// <param name="graph">The graph to be processed by the JPS algorithm.</param>
        /// <param name="visualizer">The path visualizer to visualize the JPS algorithm in the console.</param>
        public JPS(Graph graph, PathVisualizer visualizer)
        {
            this.graph = graph;
            this.pathVisualizer = visualizer;
            this.jpsStopwatch = new Stopwatch();
        }

        /// <summary>
        /// Finds the shortest path between two given nodes using the JPS algorithm.
        /// </summary>
        /// <param name="start">The point where the path begins.</param>
        /// <param name="end">The point where the path ends.</param>
        /// <returns>A tuple containing the shortest path, number of operations, the total cost, and the list of visited nodes.</returns>
        public (List<Node> Path, int Operations, double Cost, List<Node> Visited) FindShortestPath(Node start, Node end)
        {
            this.jpsStopwatch.Start();

            start.Cost = 0;
            var gscore = new Dictionary<Node, double> { { start, 0 } };
            var openList = new BinaryHeap<Node>();
            openList.Insert(start);

            while (openList.Count > 0)
            {
                var currentNode = openList.ExtractMin();
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
                    double tentative_g_score = gscore[currentNode] + this.Heuristic(currentNode, jumpPoint);

                    if (!gscore.ContainsKey(jumpPoint) || tentative_g_score < gscore[jumpPoint])
                    {
                        gscore[jumpPoint] = tentative_g_score;
                        jumpPoint.Parent = currentNode;
                        jumpPoint.Cost = tentative_g_score + this.Heuristic(jumpPoint, end);
                        jumpPoint.JumpPoint = true;

                        if (!openList.Contains(jumpPoint))
                        {
                            openList.Insert(jumpPoint);
                        }
                    }
                }
            }

            this.jpsStopwatch.Stop();

            if (this.pathFound)
            {
                this.shortestPathCost = Math.Round(gscore[end], 1);
                return (ShortestPathBuilder.ShortestPath(end), this.visitedNodes, gscore[end], gscore.Keys.ToList());
            }

            return (new List<Node>(), this.visitedNodes, 0, new List<Node>());
        }

        /// <summary>
        /// Determines the movement direction between two points.
        /// </summary>
        /// <param name="to">The destination coordinate.</param>
        /// <param name="from">The starting coordinate.</param>
        /// <returns>An integer representing the direction of movement: -1, 0, or 1.</returns>
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

        /// <summary>
        /// Prunes the neighbors of the current node to identify possible jump points.
        /// </summary>
        /// <param name="current">The current node being processed.</param>
        /// <returns>A list of neighboring nodes that are potential jump points.</returns>
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
                    if (this.IsValidPosition(x, y + dy))
                    {
                        neighbors.Add(this.graph.Nodes[y + dy][x]);
                    }

                    if (this.IsValidPosition(x + dx, y))
                    {
                        neighbors.Add(this.graph.Nodes[y][x + dx]);
                    }

                    if (this.IsValidPosition(x + dx, y + dy))
                    {
                        neighbors.Add(this.graph.Nodes[y + dy][x + dx]);
                    }

                    if (!this.IsValidPosition(x - dx, y) && this.IsValidPosition(x - dx, y + dy))
                    {
                        neighbors.Add(this.graph.Nodes[y + dy][x - dx]);
                    }

                    if (!this.IsValidPosition(x, y - dy) && this.IsValidPosition(x + dx, y - dy))
                    {
                        neighbors.Add(this.graph.Nodes[y - dy][x + dx]);
                    }
                }

                // Horizontal or vertical movement
                else
                {
                    if (dx == 0)
                    {
                        if (this.IsValidPosition(x, y + dy))
                        {
                            neighbors.Add(this.graph.Nodes[y + dy][x]);
                        }

                        if (!this.IsValidPosition(x + 1, y) && this.IsValidPosition(x + 1, y + dy))
                        {
                            neighbors.Add(this.graph.Nodes[y + dy][x + 1]);
                        }

                        if (!this.IsValidPosition(x - 1, y) && this.IsValidPosition(x - 1, y + dy))
                        {
                            neighbors.Add(this.graph.Nodes[y + dy][x - 1]);
                        }
                    }
                    else
                    {
                        if (this.IsValidPosition(x + dx, y))
                        {
                            neighbors.Add(this.graph.Nodes[y][x + dx]);
                        }

                        if (!this.IsValidPosition(x, y + 1) && this.IsValidPosition(x + dx, y + 1))
                        {
                            neighbors.Add(this.graph.Nodes[y + 1][x + dx]);
                        }

                        if (!this.IsValidPosition(x, y - 1) && this.IsValidPosition(x + dx, y - 1))
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

            return neighbors;
        }

        /// <summary>
        /// Checks whether a position is valid (within bounds and not an obstacle).
        /// </summary>
        /// <param name="x">The X-coordinate of the position.</param>
        /// <param name="y">The Y-coordinate of the position.</param>
        /// <returns>A boolean indicating if the position is valid.</returns>
        private bool IsValidPosition(int x, int y)
        {
            return y >= 0 && y < this.graph.Nodes.Count &&
                   x >= 0 && x < this.graph.Nodes[y].Count &&
                   !this.graph.Nodes[y][x].IsObstacle;
        }

        /// <summary>
        /// Performs the Jump function, which identifies the next jump point in a given direction.
        /// </summary>
        /// <param name="x">The current X-coordinate in the direction of movement.</param>
        /// <param name="y">The current Y-coordinate in the direction of movement.</param>
        /// <param name="px">The previous X-coordinate (from the parent node).</param>
        /// <param name="py">The previous Y-coordinate (from the parent node).</param>
        /// <param name="start">The start node of the path.</param>
        /// <param name="end">The end node of the path.</param>
        /// <returns>The coordinates of the jump point if found, otherwise null.</returns>
        private (int x, int y)? Jump(int x, int y, int px, int py, Node start, Node end)
        {
            int dx = x - px;
            int dy = y - py;

            if (!this.IsValidPosition(x, y))
            {
                return null;
            }

            Node currentNode = this.graph.Nodes[y][x];
            Node parentNode = this.graph.Nodes[py][px];

            currentNode.Parent = parentNode;
            currentNode.Visited = true;

            this.pathVisualizer.VisualizePath(currentNode, start, end, true);

            if (currentNode == end)
            {
                this.pathFound = true;
                return (x, y);
            }

            // Check for forced neighbors when moving diagonally
            if (dx != 0 && dy != 0)
            {
                if ((this.IsValidPosition(x - dx, y + dy) && !this.IsValidPosition(x - dx, y)) ||
                    (this.IsValidPosition(x + dx, y - dy) && !this.IsValidPosition(x, y - dy)))
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
                if (dx != 0)
                {
                    if ((this.IsValidPosition(x + dx, y + 1) && !this.IsValidPosition(x, y + 1)) ||
                        (this.IsValidPosition(x + dx, y - 1) && !this.IsValidPosition(x, y - 1)))
                    {
                        return (x, y);
                    }
                }
                else
                {
                    if ((this.IsValidPosition(x + 1, y + dy) && !this.IsValidPosition(x + 1, y)) ||
                        (this.IsValidPosition(x - 1, y + dy) && !this.IsValidPosition(x - 1, y)))
                    {
                        return (x, y);
                    }
                }
            }

            return this.Jump(x + dx, y + dy, x, y, start, end);
        }

        /// <summary>
        /// Estimates the distance between the current jump point and the end node using the Octile heuristic.
        /// </summary>
        /// <param name="jumpPoint">The current node being evaluated.</param>
        /// <param name="end">The end node.</param>
        /// <returns>The estimated cost from the current node to the goal node.</returns>
        private double Heuristic(Node jumpPoint, Node end)
        {
            int dx = Math.Abs(end.X - jumpPoint.X);
            int dy = Math.Abs(end.Y - jumpPoint.Y);
            double D = 1.0;
            double D2 = Math.Sqrt(2);

            return D * (dx + dy) + (D2 - 2 * D) * Math.Min(dx, dy);
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

        /// <summary>
        /// Retrieves the length of the shortest path found by the JPS algorithm.
        /// </summary>
        /// <returns>The length of the shortest path in number of nodes.</returns>
        public double GetShortestPathLength()
        {
            return Math.Round(this.shortestPathCost, 1);
        }
    }
}
