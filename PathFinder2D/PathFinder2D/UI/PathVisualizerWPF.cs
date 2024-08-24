namespace PathFinder2D.UI
{
    using PathFinder2D.DataStructures;
    using PathFinder2D.Managers;
    using System.Diagnostics;
    using System.Security.Cryptography;
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

        // Override to pathvisualizer
        public override void VisualizePath(Node currentNode, Node start, Node end, bool jps = false)
        {
            if (!VisualizationEnabled) { return; }

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

        // Method to draw a node on the canvas
        private void DrawNode(int x, int y, Brush color, string tag)
        {
            int rectSize = 18; // Slightly larger than before to fill more of the grid cell
            int offset = (nodeSize - rectSize) / 2; // Adjust position to keep it centered

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

        // Method to process all pending UI events
        private void DoEvents()
        {
            Application.Current.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Background,
                new Action(delegate { }));
        }

        // Method to clear temporary nodes from the canvas except grid lines and obstacles
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
