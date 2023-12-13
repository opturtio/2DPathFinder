namespace PathFinder.DataStructures
{
    /// <summary>
    /// A static class to reconstruct the shortest path
    /// </summary>
    static class ShortestPathBuilder
    {
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
