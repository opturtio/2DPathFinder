namespace PathFinder2D.Managers
{
    using PathFinder2D.DataStructures;
    
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
            var graph = new Graph();

            string[] rows = graphStr.Split('\n');

            for (int y = 0; y < rows.Length; y++)
            {
                rows[y] = rows[y].Trim();

                var row = new List<Node>();

                for (int x = 0; x < rows[y].Length; x++)
                {
                    bool isObstacle = rows[y][x] == '@';
                    row.Add(new Node(x, y, isObstacle));
                }

                graph.AddRow(row);
            }

            return graph;
        }
    }
}
