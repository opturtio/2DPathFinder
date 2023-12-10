namespace PathFinder.DataStructures
{
    public class PathVisualizer
    {
        private Graph graph;
        private string currentMap;
        private HashSet<Node> visitedNodes = new HashSet<Node>();
        private Node currentNode;

        public PathVisualizer(Graph graph, string currentMap)
        {
            this.graph = graph;
            this.currentMap = currentMap;
        }

        public void VisualizePath(Node currentNode)
        {
            this.currentNode = currentNode;
            this.visitedNodes.Add(currentNode);
            this.Visualize();
        }

        private void Visualize()
        {
            Console.Clear();
            string[] rows = this.currentMap.Split('\n');

            for (int y = 0; y < rows.Length; y++)
            {
                // Cleans the row from carriage return if one occurs.
                // Otherwise index out of range occures.
                rows[y] = rows[y].Trim();

                for (int x = 0; x < rows[y].Length; x++)
                {
                    Node node = this.graph.Nodes[y][x];

                    if (node == this.currentNode)
                    {
                        Console.Write("X");
                    }
                    else if (this.visitedNodes.Contains(node))
                    {
                        Console.Write("#");
                    }
                    else
                    {
                        Console.Write(rows[y][x]);
                    }
                }

                Console.WriteLine();
            }

            Thread.Sleep(50);
        }
    }
}
