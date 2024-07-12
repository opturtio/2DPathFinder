using System.ComponentModel;
using System.Diagnostics;

namespace PathFinder.DataStructures
{
    public class JPS
    {
        private readonly Graph graph;
        private readonly PathVisualizer pathVisualizer;
        private Stopwatch jpsStopwatch;
        private int visitedNodes = 0;
        private bool pathFound = false;

        public JPS(Graph graph, PathVisualizer visualizer)
        {
            this.graph = graph;
            this.pathVisualizer = visualizer;
            this.jpsStopwatch = new Stopwatch();
        }

        public List<Node> FindShortestPath(Node start, Node end)
        {
            this.jpsStopwatch.Start();

            start.Cost = 0;

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
                    var jumpPointCoords = this.Jump(neighbor.X, neighbor.Y, currentNode.X, currentNode.Y, end);

                    if (jumpPointCoords == null)
                    {
                        continue;
                    }

                    var jumpPoint = this.graph.Nodes[jumpPointCoords.Value.y][jumpPointCoords.Value.x];

                    double newCost = currentNode.Cost + this.Heuristic(currentNode, jumpPoint);

                    if (newCost < jumpPoint.Cost)
                    {
                        jumpPoint.Cost = newCost;

                        jumpPoint.JumpPoint = true;

                        openList.Insert(jumpPoint);
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

            return neighbors;
        }

        private bool IsValidPosition(int x, int y)
        {
            return y >= 0 && y < this.graph.Nodes.Count &&
                   x >= 0 && x < this.graph.Nodes[y].Count;
        }

        private (int x, int y)? Jump(int x, int y, int px, int py, Node end)
        {
            int dx = x - px;
            int dy = y - py;

            // Check if the next position is within bounds and not an obstacle
            if (!this.graph.CanMove(x, y))
            {
                return null;
            }

            Node currentNode = this.graph.Nodes[y][x];
            Node parentNode = this.graph.Nodes[py][px];

            // Avoid revisiting nodes
            if (currentNode.Parent != null && currentNode.Parent != parentNode)
            {
                return null;
            }

            currentNode.Parent = parentNode;
            currentNode.Visited = true;

            this.pathVisualizer.VisualizePath(currentNode, parentNode, end, true);

            // Check if the current position is the end node
            if (currentNode == end)
            {
                return (x, y);
            }

            // Check for forced neighbors when moving diagonally
            if (dx != 0 && dy != 0)
            {
                if ((this.IsValidPosition(x - dx, y + dy) && this.graph.CanMove(x - dx, y + dy) && !this.graph.CanMove(x - dx, y)) ||
                    (this.IsValidPosition(x + dx, y - dy) && this.graph.CanMove(x + dx, y - dy) && !this.graph.CanMove(x, y - dy)))
                {
                    return (x, y);
                }

                // When moving diagonally, must check for vertical/horizontal jump points
                if (this.Jump(x + dx, y, x, y, end) != null || this.Jump(x, y + dy, x, y, end) != null)
                {
                    return (x, y);
                }
            }
            else
            {
                // Check for forced neighbors when moving horizontally or vertically
                if (dx != 0) // Moving along x-axis
                {
                    if ((this.IsValidPosition(x + dx, y + 1) && this.graph.CanMove(x + dx, y + 1) && !this.graph.CanMove(x, y + 1)) ||
                        (this.IsValidPosition(x + dx, y - 1) && this.graph.CanMove(x + dx, y - 1) && !this.graph.CanMove(x, y - 1)))
                    {
                        return (x, y);
                    }
                }
                else // Moving along y-axis
                {
                    if ((this.IsValidPosition(x + 1, y + dy) && this.graph.CanMove(x + 1, y + dy) && !this.graph.CanMove(x + 1, y)) ||
                        (this.IsValidPosition(x - 1, y + dy) && this.graph.CanMove(x - 1, y + dy) && !this.graph.CanMove(x - 1, y)))
                    {
                        return (x, y);
                    }
                }
            }

            // Recursive call in the direction of movement
            return this.Jump(x + dx, y + dy, x, y, end);
        }

        private double Heuristic(Node current, Node jumpPoint)
        {
            int dx = Math.Abs(jumpPoint.X - current.X);
            int dy = Math.Abs(jumpPoint.Y - current.Y);
            return Math.Sqrt(dx * dx + dy * dy);
        }

        public int GetVisitedNodes()
        {
            return this.visitedNodes;
        }

        public double GetStopwatchTime()
        {
            return this.jpsStopwatch.Elapsed.TotalMilliseconds;
        }

        public bool IsPathFound()
        {
            return this.pathFound;
        }
    }
}
