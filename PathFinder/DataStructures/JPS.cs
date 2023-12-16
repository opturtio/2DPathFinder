namespace PathFinder.DataStructures
{
    public class JPS
    {
        private readonly Graph graph;
        private readonly PathVisualizer pathVisualizer;

        public JPS(Graph graph, PathVisualizer visualizer)
        {
            this.graph = graph;
            this.pathVisualizer = visualizer;
        }

        public List<Node> FindShortestPath(Node start, Node end)
        {
            start.Cost = 0;

            var priorityQueue = new PriorityQueue<Node, double>();

            return ShortestPathBuilder.ShortestPath(end);
        }
    }
}
