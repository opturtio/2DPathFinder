using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using PathFinder.DataStructures;
using PathFinder.Managers;

namespace PathFinder2D.UI
{
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
            DrawNode(currentNode.X, currentNode.Y, Brushes.Blue);

            if (jps && currentNode.JumpPoint)
            {
                DrawNode(currentNode.X, currentNode.Y, Brushes.Yellow);
            }
            else if (currentNode == start)
            {
                DrawNode(currentNode.X, currentNode.Y, Brushes.Green);
            }
            else if (currentNode == end)
            {
                DrawNode(currentNode.X, currentNode.Y, Brushes.Red);
            }
        }

        // Method to draw a node on the canvas
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
            canvas.Children.Add(rect);
        }

        // Method to clear the visualization
        public void ClearCanvas()
        {
            canvas.Children.Clear();
        }
    }
}
