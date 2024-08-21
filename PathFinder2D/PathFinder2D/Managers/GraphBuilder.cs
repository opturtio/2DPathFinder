using PathFinder.DataStructures;

namespace PathFinder.Managers
{
    /// <summary>
    /// Creates a graph from the chosen string.
    /// </summary>
    public class GraphBuilder
    {
        /// <summary>
        /// Creates a graph from a string where each character is a node.
        /// </summary>
        /// <param name="graphStr">Map as string form selected by the user.</param>
        /// <returns>The graph containing the nodes.</returns>
        public static Graph CreateGraphFromString(string graphStr)
        {
            // Creates a new empty graph
            var graph = new Graph();

            // Splits the string into rows
            string[] rows = graphStr.Split('\n');

            // Iterates through each row of the string
            for (int y = 0; y < rows.Length; y++)
            {
                // Cleans the row from carriage return if one occurs.
                rows[y] = rows[y].Trim();

                // Initializes a new list to represent a row of nodes
                var row = new List<Node>();

                // Iterates through each character in the current row
                for (int x = 0; x < rows[y].Length; x++)
                {
                    // Checks if the character represents an obstacle
                    bool isObstacle = rows[y][x] == '@';

                    // Creates a new node and add it to the row
                    row.Add(new Node(x, y, isObstacle));
                }

                // Add the row of nodes to the graph
                graph.AddRow(row);
            }

            // Returns the constructed graph
            return graph;
        }
    }
}
