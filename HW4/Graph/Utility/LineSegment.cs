using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace HW4.Graph.Utility
{
    public struct LineSegment
    {
        public PointF A, B;
        public LineSegment(PointF a, PointF b)
        {
            A = a;
            B = b;
        }

        public override int GetHashCode()
        {
            PointF first;
            PointF second;
            if (A.X > B.X)
            {
                first = A;
                second = B;
            }
            else
            {
                first = B;
                second = A;
            }
            return (first.ToString() + second.ToString()).GetHashCode();
        }
    }
}
