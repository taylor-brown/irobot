using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using HW4.Graph;
using HW4.Graph.Utility;

namespace HW4.Planning
{
    public class NodeGraph : IEnumerable<Node>
    {
        private HashSet<Node> _nodes;
        public NodeGraph()
        {
            _nodes = new HashSet<Node>();
        }
        public void addLine(LineSegment line)
        {
            Node first = new Node();
            first.Location = line.A;
            Node second = new Node();
            second.Location = line.B;
            _nodes.Add(first);
            _nodes.Add(second);
            first = _nodes.First(s => s.Equals(first));
            second = _nodes.First(s => s.Equals(second));
            first.AddNeighbor(second);
            second.AddNeighbor(first);
        }

        public IEnumerable<Node> getNodes()
        {
            return _nodes;
        }

        #region IEnumerable<Node> Members

        public IEnumerator<Node> GetEnumerator()
        {
            return getNodes().GetEnumerator();
        }

        #endregion

        #region IEnumerable Members

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion
    }
}
