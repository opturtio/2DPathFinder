using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using PathFinder.Algorithms;
using System.Windows.Shapes;
using PathFinder.DataStructures;
using PathFinder.Managers;
using PathFinder.Services;

namespace PathFinder2D.UI
{
    public partial class MainWindow : Window
    {
        private Graph graph;
        private Node startNode, endNode;
        private bool isStartNodeSelected = false;

        public MainWindow()
        {
            //InitializeComponent();
            InitializeGraph();
            DrawGrid();
        }

        // Initialize the graph
        private void InitializeGraph()
        {
            string mapString = this.fileManager.LoadMap(input);
            graph = GraphBuilder.CreateGraphFromString(mapString);
        }

        // Draw the grid on the canvas
        private void DrawGrid()
        {
            for (int i = 0; i <= graph.Nodes.Count; i++)
            {
                Line horizontalLine = new Line
                {
                    X1 = 0,
                    Y1 = i * nodeSize,
                    X2 = PathCanvas.Width,
                    Y2 = i * nodeSize,
                    Stroke = Brushes.LightGray
                };
                PathCanvas.Children.Add(horizontalLine);
            }

            for (int j = 0; j <= graph.Nodes[0].Count; j++)
            {
                Line verticalLine = new Line
                {
                    X1 = j * nodeSize,
                    X2 = j * nodeSize,
                    Y1 = 0,
                    Y2 = PathCanvas.Height,
                    Stroke = Brushes.LightGray
                };
                PathCanvas.Children.Add(verticalLine);
            }
        }

        // Handle mouse clicks on the canvas
        private void Canvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Point clickPosition = e.GetPosition(PathCanvas);
            int x = (int)(clickPosition.X / nodeSize);
            int y = (int)(clickPosition.Y / nodeSize);

            if (!isStartNodeSelected)
            {
                startNode = graph.Nodes[y][x];
                isStartNodeSelected = true;
                DrawNode(x, y, Brushes.Green);
            }
            else
            {
                endNode = graph.Nodes[y][x];
                isStartNodeSelected = false;
                DrawNode(x, y, Brushes.Red);
            }
        }

        private void DrawNode(int x, int y, Brush color)
        {
            Rectangle rect = new Rectangle
            {
                Width = nodeSize,
                Height = nodeSize,
                Fill = color
            };
            Canvas.SetLeft(rect, x * nodeSize);
            Canvas.SetTop(rect, y * nodeSize);
            PathCanvas.Children.Add(rect);
        }

        private void RunDijkstra_Click(object sender, RoutedEventArgs e)
        {
            if (startNode == null || endNode == null) return;

            var visualizer = new PathVisualizerWPF(PathCanvas, graph);
            var dijkstra = new Dijkstra(graph, visualizer);
            dijkstra.FindShortestPath(startNode, endNode);

            ResultLabel.Content = $"Dijkstra Path Cost: {dijkstra.GetShortestPathCost()}";
        }

        private void RunAStar_Click(object sender, RoutedEventArgs e)
        {
            if (startNode == null || endNode == null) return;

            var visualizer = new PathVisualizerWPF(PathCanvas, graph);
            var aStar = new Astar(graph, visualizer);
            aStar.FindShortestPath(startNode, endNode);

            ResultLabel.Content = $"A* Path Cost: {aStar.GetShortestPathCost()}";
        }

        private void RunJPS_Click(object sender, RoutedEventArgs e)
        {
            if (startNode == null || endNode == null) return;

            var visualizer = new PathVisualizerWPF(PathCanvas, graph);
            var jps = new JPS(graph, visualizer);
            jps.FindShortestPath(startNode, endNode);

            ResultLabel.Content = $"JPS Path Cost: {jps.GetShortestPathLength()}";
        }
    }
}
