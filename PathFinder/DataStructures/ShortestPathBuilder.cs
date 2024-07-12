namespace PathFinder.DataStructures
{
    /// <summary>
    /// A static class to reconstruct the shortest path
    /// </summary>
    static class ShortestPathBuilder
    {
        /// <summary>
        /// Creates shortest path.
        /// </summary>
        /// <param name="end">The end node.</param>
        /// <returns>The shortest path as list of nodes.</returns>
        public static List<Node> ShortestPath(Node end)
        {
            var path = new List<Node>();
            var currentNode = end;

            while (currentNode != null)
            {
                path.Add(currentNode);
                currentNode = currentNode.Parent;
                //Console.WriteLine("Current Node: " + currentNode.GetNodeInfo());
                //Console.WriteLine("Parent Node: " + currentNode.Parent.GetNodeInfo());
            }

            path.Reverse();
            return path;
        }

        /// <summary>
        /// Calculates the length of the shortest path.
        /// </summary>
        /// <param name="end">The end node.</param>
        /// <returns>The length of the shortest path.</returns>
        public static int ShortestPathLength(Node end)
        {
            int length = 0;
            var currentNode = end;

            while (currentNode != null)
            {
                length++;
                currentNode = currentNode.Parent;
            }

            return length;
        }
    }
}
