namespace PathFinder2D.UI
{
    using PathFinder2D.DataStructures;
    using PathFinder2D.Managers;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media;
    using System.Windows.Shapes;

    /// <summary>
    /// Visualizes the pathfinding process on a WPF canvas.
    /// </summary>
    public class PathVisualizerWPF : PathVisualizer
    {
        private readonly Canvas canvas;
        private readonly int nodeSize = 20;
        private Node lastNode;

        /// <summary>
        /// Gets or sets the delay between visualization steps.
        /// </summary>
        public int Delay { get; set; } = 20;

        /// <summary>
        /// Gets or sets a value indicating whether visualization is enabled.
        /// </summary>
        public bool VisualizationEnabled { get; set; } = false;

        /// <summary>
        /// Initializes a new instance of the <see cref="PathVisualizerWPF"/> class.
        /// </summary>
        /// <param name="canvas">The WPF canvas where the visualization will be rendered.</param>
        /// <param name="graph">The graph representing the map.</param>
        public PathVisualizerWPF(Canvas canvas, Graph graph) : base(graph, "")
        {
            this.canvas = canvas;
            this.lastNode = null;
        }

        /// <summary>
        /// Visualizes the current step in the pathfinding process on the canvas.
        /// </summary>
        /// <param name="currentNode">The current node being processed.</param>
        /// <param name="start">The start node of the path.</param>
        /// <param name="end">The end node of the path.</param>
        /// <param name="jps">Indicates whether Jump Point Search (JPS) is used.</param>
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

        /// <summary>
        /// Draws a visual representation of a node on the canvas.
        /// </summary>
        /// <param name="x">The x-coordinate of the node.</param>
        /// <param name="y">The y-coordinate of the node.</param>
        /// <param name="color">The color to fill the node with.</param>
        /// <param name="tag">The tag associated with the node.</param>
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

        /// <summary>
        /// Draws a line between two nodes on the canvas.
        /// </summary>
        /// <param name="fromNode">The starting node of the line.</param>
        /// <param name="toNode">The ending node of the line.</param>
        /// <param name="color">The color of the line.</param>
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

        /// <summary>
        /// Forces the application to process all pending UI events, updating the canvas immediately.
        /// </summary>
        private void DoEvents()
        {
            Application.Current.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Background,
                new System.Action(delegate { }));
        }

        /// <summary>
        /// Clears all temporary nodes from the canvas.
        /// </summary>
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
