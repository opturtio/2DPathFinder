namespace PathFinder2D.UI
{
    using PathFinder2D.DataStructures;
    using PathFinder2D.Managers;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media;
    using System.Windows.Shapes;

    public class PathVisualizerWPF : PathVisualizer
    {
        private readonly Canvas canvas;
        private readonly int nodeSize = 20;
        private Node lastNode;

        public int Delay { get; set; } = 20;
        public bool VisualizationEnabled { get; set; } = false;

        public PathVisualizerWPF(Canvas canvas, Graph graph) : base(graph, "")
        {
            this.canvas = canvas;
            this.lastNode = null;
        }

        public override void VisualizePath(Node currentNode, Node start, Node end, bool jps = false)
        {
            if (!VisualizationEnabled)
            {
                return;
            }

            DrawNode(currentNode.X, currentNode.Y, Brushes.Gray, "TempNode");

            if (lastNode != null && !lastNode.JumpPoint && lastNode != start && lastNode != end)
            {
                DrawNode(this.lastNode.X, this.lastNode.Y, Brushes.LightGray, "TempNode");
            }

            if (jps && currentNode.JumpPoint)
            {
                DrawNode(currentNode.X, currentNode.Y, Brushes.Yellow, "TempNode");
            }
            if (currentNode == start)
            {
                DrawNode(currentNode.X, currentNode.Y, Brushes.Green, "StartNode");
            }
            if (currentNode == end)
            {
                DrawNode(currentNode.X, currentNode.Y, Brushes.Red, "EndNode");
            }

            this.lastNode = currentNode;

            DoEvents();
            Thread.Sleep(Delay);
        }


        private void DrawNode(int x, int y, Brush color, string tag)
        {
            int rectSize = 18;
            int offset = (nodeSize - rectSize) / 2;

            Rectangle rect = new Rectangle
            {
                Width = rectSize,
                Height = rectSize,
                Fill = color,
                Opacity = 0.7,
                Tag = tag
            };
            Canvas.SetLeft(rect, x * nodeSize + offset);
            Canvas.SetTop(rect, y * nodeSize + offset);
            canvas.Children.Add(rect);
        }

        public void DrawLineBetweenNodes(Node fromNode, Node toNode, Brush color)
        {
            Line line = new Line
            {
                X1 = fromNode.X * nodeSize + nodeSize / 2,
                Y1 = fromNode.Y * nodeSize + nodeSize / 2,
                X2 = toNode.X * nodeSize + nodeSize / 2,
                Y2 = toNode.Y * nodeSize + nodeSize / 2,
                Stroke = color,
                StrokeThickness = 5
            };

            canvas.Children.Add(line);
        }

        private void DoEvents()
        {
            Application.Current.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Background,
                new System.Action(delegate { }));
        }

        public void ClearTemporaryNodes()
        {
            for (int i = canvas.Children.Count - 1; i >= 0; i--)
            {
                UIElement child = canvas.Children[i];
                if (child is FrameworkElement element && element.Tag?.ToString() == "TempNode")
                {
                    canvas.Children.RemoveAt(i);
                }
            }
        }
    }
}
