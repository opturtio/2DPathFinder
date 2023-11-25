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
        /// Adds a new row of nodes to the graph.
        /// </summary>
        /// <param name="row">A list of Node objects representing a row in the graph.</param>
        public void AddRow(List<Node> row)
        {
            this.Nodes.Add(row);
        }

        /// <summary>
        /// Retrieves the neighboring nodes of a given node and the costs associated with moving to each neighbor.
        /// This method considers all eight possible directions (horizontal, vertical, and diagonal) and calculates the cost for each movement.
        /// </summary>
        /// <param name="node">The node for which neighbors are to be found.</param>
        /// <returns>An IEnumerable of tuples, where each tuple contains a Node (representing a neighbor) and a double (representing the movement cost to that neighbor).</returns>
        public IEnumerable<(Node, double)> GetNeighborsWithCosts(Node node)
        {
            // Defines all eight directions and their costs
            var directions = new (int deltaX, int deltaY, double cost)[]
            {
            // Horizontal and vertical movements (movement cost: 1)
            (1, 0, 1), // Move RIGHT by 1 unit (X + 1), no change in Y.
            (-1, 0, 1), // Move LEFT by 1 unit (X - 1), no change in Y.
            (0, 1, 1), // Move UP by 1 unit (Y + 1), no change in X.
            (0, -1, 1), // Move DOWN by 1 unit (Y - 1), no change in X.

            // Diagonal movements (movement cost: sqrt(2)
            (1, 1, Math.Sqrt(2)),  // Move diagonally to the UPPER RIGHT by 1 unit both in X (X + 1) and Y (Y + 1).
            (-1, 1, Math.Sqrt(2)), // Move diagonally to the UPPER LEFT by 1 unit in X (X - 1) and 1 unit in Y (Y + 1).
            (1, -1, Math.Sqrt(2)), // Move diagonally to the LOWER RIGHT by 1 unit in X (X + 1) and -1 unit in Y (Y - 1).
            (-1, -1, Math.Sqrt(2)), // Move diagonally to the LOWER LEFT by 1 unit in X (X - 1) and -1 unit in Y (Y - 1).
            };

            // Iterates over each defined direction to find the neighboring nodes.
            foreach (var (deltaX, deltaY, cost) in directions)
            {
                // Calculates the X-coordinate of the neighboring node
                int neighborX = node.X + deltaX;

                // Calculates the Y-coordinate of the neighboring node
                int neighborY = node.Y + deltaY;

                // Checks if the neighboring node is within the bounds of the graph and is not an obstacle
                if (neighborY >= 0 && neighborY < this.Nodes.Count && neighborX >= 0 && neighborX < this.Nodes[neighborY].Count && !this.Nodes[neighborY][neighborX].IsObstacle)
                {
                    // Yield each valid neighboring node along with its movement cost
                    yield return (this.Nodes[neighborY][neighborX], cost);
                }
            }
        }
    }
}
