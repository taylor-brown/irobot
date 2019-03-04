using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;


namespace HW4.Planning
{
    public class Node
    {
        public bool Expanded { get; set; }
        public PointF Location {get; set;}
        public float Distance { get; set; }
        public Node Previous { get; set; }

        private List<Node> _neighbors;
        public Node()
        {
            Distance = float.PositiveInfinity;
            _neighbors = new List<Node>();
        }
        public void AddNeighbor(Node n)
        {
            _neighbors.Add(n);
        }

        public float distanceToNode(Node node)
        {
            return (float) Math.Sqrt(Math.Pow(node.Location.X - Location.X, 2) + Math.Pow(node.Location.Y - Location.Y, 2));
        }
        public IEnumerable<Node> getNeighbors()
        {
            return _neighbors;
        }
        public override bool Equals(object obj)
        {
            if(obj is Node)
                return Location.Equals((obj as Node).Location);
            return false;
        }

        public override int GetHashCode()
        {
            return Location.GetHashCode();
        }
    }
}
