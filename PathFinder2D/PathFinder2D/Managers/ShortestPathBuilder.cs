namespace PathFinder2D.Managers
{
    using PathFinder2D.DataStructures;

    /// <summary>
    /// A static class to reconstruct the shortest path
    /// </summary>
    static class ShortestPathBuilder
    {
        public static List<Node> ReconstructPath(Node end)
        {
            var path = new LinkedList<Node>();
            var currentNode = end;

            while (currentNode != null)
            {
                path.AddFirst(currentNode);
                currentNode = currentNode.Parent;
            }

            return new List<Node>(path);
        }
    }
}
