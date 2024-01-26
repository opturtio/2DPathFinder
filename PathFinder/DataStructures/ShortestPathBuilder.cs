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
            }

            path.Reverse();
            return path;
        }
    }
}
