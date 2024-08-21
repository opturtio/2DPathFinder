﻿namespace PathFinder.DataStructures
{
    /// <summary>
    /// Represents a node in a graph.
    /// </summary>
    public class Node : IComparable<Node>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Node"/> class.
        /// </summary>
        /// <param name="x">The X-coordinate of the node.</param>
        /// <param name="y">The Y-coordinate of the node.</param>
        /// <param name="isObstacle">Indicates whether the node is an obstacle.</param>
        public Node(int x, int y, bool isObstacle)
        {
            this.X = x;
            this.Y = y;
            this.IsObstacle = isObstacle;
            this.Cost = double.MaxValue;
            this.Parent = null;
            this.Visited = false;
            this.JumpPoint = false;
        }

        /// <summary>
        /// Gets the X-coordinate of the node.
        /// </summary>
        public int X { get; }

        /// <summary>
        /// Gets the Y-coordinate of the node.
        /// </summary>
        public int Y { get; }

        /// <summary>
        /// Gets a value indicating whether the node is an obstacle.
        /// </summary>
        public bool IsObstacle { get; }

        /// <summary>
        /// Gets or sets the cost from the start node to this node.
        /// </summary>
        public double Cost { get; set; }

        /// <summary>
        /// Gets or sets the parent node in the pathfinding process.
        /// </summary>
        public Node? Parent { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the node has been visited.
        /// </summary>
        public bool Visited { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the node is a jump point.
        /// </summary>
        public bool JumpPoint { get; set; }

        /// <summary>
        /// Returns a string that represents the current node.
        /// </summary>
        /// <returns>A string representation of the node e.g. "." or "@".</returns>
        public string GetNodeSymbol()
        {
            return this.IsObstacle ? "@" : ".";
        }

        /// <summary>
        /// Compares the current node with another node based on cost.
        /// </summary>
        /// <param name="other">The other node to compare to.</param>
        /// <returns>An integer that indicates the relative order of the nodes being compared.</returns>
        public int CompareTo(Node other)
        {
            if (other == null) return 1;
            return this.Cost.CompareTo(other.Cost);
        }

        /// <summary>
        /// Returns a string that represents the information of the node.
        /// </summary>
        /// <returns>A string representation of the node's information.</returns>
        public string GetNodeInfo()
        {
            if (this.Parent != null)
                return $"Node position: ({this.X}, {this.Y}). Visited: {this.Visited}. Cost: {this.Cost}. Obstacle: {this.IsObstacle}. Parent: ({this.Parent.X},{this.Parent.Y}). Jump Point: {this.JumpPoint}.";
            else
                return $"Node position: ({this.X}, {this.Y}). Visited: {this.Visited}. Cost: {this.Cost}. Obstacle: {this.IsObstacle}. Parent: Null. Jump Point: {this.JumpPoint}.";
        }
    }
}
