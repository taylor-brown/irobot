using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace HW4.Graph.Utility
{
    public class LineIntersection
    {
        /// <summary>
        /// returns true if the lines interesect between given points.
        /// .00001 is float error factor
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <param name="p3"></param>
        /// <param name="p4"></param>
        /// <returns></returns>
        public static bool SegmentsIntersect(PointF p1, PointF p2, PointF p3, PointF p4)
        {
            if (p1 == p3 || p1 == p4 || p2 == p3 || p2 == p4)
            {
                return false;
            }
            float uaTop = ((p4.X - p3.X) * (p1.Y - p3.Y) - (p4.Y - p3.Y) * (p1.X - p3.X));
            float ubTop = ((p2.X - p1.X) * (p1.Y - p3.Y) - (p2.Y - p1.Y) * (p1.X - p3.X));
            float denominator = ((p4.Y - p3.Y) * (p2.X - p1.X) - (p4.X - p3.X) * (p2.Y - p1.Y));
            //if (Math.Abs(denominator) < .00001)
            if(denominator == 0)
            {
                return false;
            }
            float ua = uaTop / denominator;
            float ub = ubTop/ denominator;

            if((ua <= 1 && ua >= 0) && (ub <=1 && ub >= 0)){
                return true;
            }
            return false;
        }

        public static bool SegmentsIntersect(LineSegment a, LineSegment b)
        {
            return SegmentsIntersect(a.A, a.B, b.A, b.B);
        }
    }
}
