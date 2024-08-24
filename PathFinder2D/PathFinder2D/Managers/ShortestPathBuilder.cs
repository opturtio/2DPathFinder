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
            var path = new List<Node>();
            var currentNode = end;

            while (currentNode != null)
            {
                path.Add(currentNode);
                currentNode = currentNode.Parent;
            }

            path.Reverse(); // Optional: Reverse to start from the start node
            return path;
        }
    }
}
