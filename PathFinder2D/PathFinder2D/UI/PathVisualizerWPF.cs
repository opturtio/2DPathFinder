
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

        public PathVisualizerWPF(Canvas canvas, Graph graph) : base(graph, "")
        {
            this.canvas = canvas;
        }

        // Override to pathvisualizer
        public override void VisualizePath(Node currentNode, Node start, Node end, bool jps = false)
        {
            // Draw the current node on the canvas
            DrawNode(currentNode.X, currentNode.Y, Brushes.LightGray, "TempNode");

            if (jps && currentNode.JumpPoint)
            {
                Console.WriteLine($"Jump Point detected at ({currentNode.X}, {currentNode.Y})");
                DrawNode(currentNode.X, currentNode.Y, Brushes.Yellow, "TempNode");
            }
            else if (currentNode == start)
            {
                DrawNode(currentNode.X, currentNode.Y, Brushes.Green, "StartNode");
            }
            else if (currentNode == end)
            {
                DrawNode(currentNode.X, currentNode.Y, Brushes.Red, "EndNode");
            }
        }

        // Method to draw a node on the canvas
        private void DrawNode(int x, int y, Brush color, string tag)
        {
            Rectangle rect = new Rectangle
            {
                Width = nodeSize,
                Height = nodeSize,
                Fill = color,
                Tag = tag
            };
            Canvas.SetLeft(rect, x * nodeSize);
            Canvas.SetTop(rect, y * nodeSize);
            canvas.Children.Add(rect);
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
