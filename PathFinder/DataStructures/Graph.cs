namespace PathFinder.DataStructures
{
    /// <summary>
    /// Creates a graph object.
    /// </summary>
    public class Graph
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Graph"/> class.
        /// </summary>
        public Graph()
        {
            this.Nodes = new List<List<Node>>();
        }

        /// <summary>
        /// Gets a list of lists containing the nodes in the graph.
        /// Using a public getter for the Nodes property allows all classes in the application to access the graph's data.
        /// </summary>
        public List<List<Node>> Nodes { get; private set; }

        /// <summary>
        /// Resets all node's cost, parent and the information is the node visited.
        /// </summary>
        public void ResetNodes()
        {
            foreach (var row in this.Nodes)
            {
                foreach (var node in row)
                {
                    node.Cost = double.MaxValue;
                    node.Parent = null;
                    node.Visited = false;
                }
            }
        }

        /// <summary>
        /// Adds a new row of nodes to the graph.
        /// </summary>
        /// <param name="row">A list of Node objects representing a row in the graph.</param>
        public void AddRow(List<Node> row)
        {
            this.Nodes.Add(row);
        }

        /// <summary>
        /// Used by Dijkstra and A* algorithms. 
        /// Retrieves the neighboring nodes of a given node and the costs associated with moving to each neighbor.
        /// This method considers all eight possible directions (horizontal, vertical, and diagonal) and calculates the cost for each movement.
        /// </summary>
        /// <param name="node">The node for which neighbors are to be found.</param>
        /// <returns>An IEnumerable of tuples, where each tuple contains a Node (representing a neighbor) and a double (representing the movement cost to that neighbor).</returns>
        public IEnumerable<(Node, double)> GetNeighborsWithCosts(Node node)
        {
            // Defines all eight directions and their costs
            var directions = new (int deltaX, int deltaY, double cost)[] { (1, 0, 1), (-1, 0, 1), (0, 1, 1), (0, -1, 1), (1, 1, Math.Sqrt(2)), (-1, 1, Math.Sqrt(2)), (1, -1, Math.Sqrt(2)), (-1, -1, Math.Sqrt(2)) };

            // Iterates over each defined direction to find the neighboring nodes.
            foreach (var (deltaX, deltaY, cost) in directions)
            {
                int neighborX = node.X + deltaX;
                int neighborY = node.Y + deltaY;

                if (!this.CanMove(neighborX, neighborY))
                {
                    continue;
                }

                // Check is the movement diagonal.
                if (Math.Abs(deltaX) == 1 && Math.Abs(deltaY) == 1)
                {
                    // Check if adjacent nodes are obstacles or out of bound in diagonal movement.
                    if (!this.CanMove(node.X + deltaX, node.Y) || !this.CanMove(node.X, node.Y + deltaY))
                    {
                        continue;
                    }
                }

                yield return (this.Nodes[neighborY][neighborX], cost);
            }
        }

        /// <summary>
        /// Checks if movement to a specified grid position is possible.
        /// </summary>
        /// <param name="x">The X-coordinate of the neighbring node.</param>
        /// <param name="y">The Y-coordinate of the neighbring node.</param>
        /// <returns>Returns true if the target position is within grid bounds and is not an obstacle, otherwise returns false.</returns>
        public bool CanMove(int x, int y)
        {
            return y >= 0 && y < this.Nodes.Count &&
                   x >= 0 && x < this.Nodes[y].Count &&
                   !this.Nodes[y][x].IsObstacle;
        }

        /// <summary>
        /// Used by JPS algorithm.
        /// Determines if a node has forced neighbors based on the Jump Point Search algorithm.
        /// Forced neighbors are nodes that must be considered for further search due to potential alternative paths.
        /// </summary>
        /// <param name="current">The current node being evaluated.</param>
        /// <param name="direction">A tuple containing the deltaX and deltaY representing the direction of movement from the current node.</param>
        /// <returns>Returns true if forced neighbors are detected in the specified direction, otherwise returns false.</returns>
        public bool HasForcedNeighbors(Node current, (int deltaX, int deltaY) direction)
        {
            // Horizontal movement: Check for forced neighbors along Y-axis
            if (direction.deltaX != 0)
            {
                if ((!this.CanMove(current.X, current.Y + 1) && this.CanMove(current.X + direction.deltaX, current.Y + 1)) ||
                    (!this.CanMove(current.X, current.Y - 1) && this.CanMove(current.X + direction.deltaX, current.Y - 1)))
                {
                    return true;
                }
            }

            // Vertical movement: Check for forced neighbors along X-axis
            if (direction.deltaY != 0)
            {
                if ((!this.CanMove(current.X + 1, current.Y) && this.CanMove(current.X + 1, current.Y + direction.deltaY)) ||
                    (!this.CanMove(current.X - 1, current.Y) && this.CanMove(current.X - 1, current.Y + direction.deltaY)))
                {
                    return true;
                }
            }

            // Diagonal movement: Check for horizontal and vertical forced neighbors
            if (direction.deltaX != 0 && direction.deltaY != 0)
            {
                // Check forced neighbors in horizontal and vertical direction separately
                return this.HasForcedNeighbors(current, (direction.deltaX, 0)) || this.HasForcedNeighbors(current, (0, direction.deltaY));
            }

            return false;
        }
    }
}