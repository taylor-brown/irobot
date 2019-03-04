using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace HW4.Planning
{
    public class Djikstra
    {

        public static Node ShortestPath(NodeGraph graph, Node start, Node goal)
        {
            List<Node> Q = graph.getNodes().ToList();
            Q.First(n => n.Equals(start)).Distance = 0;
            while (Q.Count > 0)
            {
                Node current = min(Q);
                Q.Remove(current);
                if (current.Distance == float.PositiveInfinity)
                    break;
                if (goal.Equals(current))
                    return current;
                foreach (Node neighbor in current.getNeighbors())
                {
                    float alt = current.Distance + current.distanceToNode(neighbor);
                    if (alt < neighbor.Distance)
                    {
                        neighbor.Distance = alt;
                        neighbor.Previous = current;
                    }
                }
            }
            return null;
        }

        private static Node min(List<Node> nodes)
        {
            Node min = nodes.FirstOrDefault();
            foreach (Node node in nodes)
            {
                if (node.Distance < min.Distance)
                    min = node;
            }
            return min;
        }
    }
}
