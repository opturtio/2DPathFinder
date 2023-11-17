using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PathFinder.DataStructures
{
    internal class Node
    {
        // X-coordinate of the node
        public int X { get; }
        // Y-coordinate of the node
        public int Y { get; }
        // Boolean value declearing whether the node is an obstacle
        public bool isObstacle {  get; }
        // Cost from the start to this value
        public double cost { get; set; }
        // Parent node in the path
        public Node Parent { get; set; }

        public Node(int x, int y, bool isObstacle)
        {
            X = x;
            Y = y;
            this.isObstacle = isObstacle;
            this.cost = double.MaxValue;
            Parent = null;
        }
    }
}
