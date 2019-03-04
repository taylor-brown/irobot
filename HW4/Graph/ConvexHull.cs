using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace HW4.Graph
{
    public class ConvexHull
    {
        List<PointF> _points;

        public ConvexHull()
        {
            _points = new List<PointF>();
        }

        public void addPoint(PointF point)
        {
            _points.Add(point);
        }
        public void addPointRange(IEnumerable<PointF> pointList)
        {
            _points.AddRange(pointList);
        }

        public IList<PointF> getConvexHullPoints()
        {
            List<PointF> points;
            //remove any duplicate points...
            points = new HashSet<PointF>(_points).ToList();
            PointF lowest = findLowest(points);
            points.Remove(lowest);
            points = points.OrderBy(s => Math.Atan2((lowest.Y - s.Y) , (lowest.X - s.X)))
                .ThenBy(s => distance(lowest, s)).ToList();
            List<PointF> stack = new List<PointF>();
            stack.Add(points.Last());
            stack.Add(lowest);
            foreach (PointF p in points)
            {
                float result = counterClockwiseTurn(stack[stack.Count - 2],stack.Last(), p);
                //if (result == 0)
                //{
                //    if (distance(lowest, p) > distance(lowest, stack.Last()))
                //    {
                //        stack.Remove(stack.Last());
                //        stack.Add(p);
                //    }
                //    continue;
                //}
                try
                {
                    while (counterClockwiseTurn(stack[stack.Count - 2], stack.Last(), p) <= 0)
                    {
                        result = counterClockwiseTurn(stack[stack.Count - 2], stack.Last(), p);
                        stack.Remove(stack.Last());
                    }
                }
                catch (Exception e)
                {
                    int none = 0;
                }
                stack.Add(p);
            }
            return new HashSet<PointF>(stack).ToList();
        }

        public float distance(PointF one, PointF two)
        {
            return (float)(Math.Pow((one.X - two.X), 2) + Math.Pow((one.Y - two.Y), 2));
        }

        public PointF findLowest(IEnumerable<PointF> points)
        {
            PointF lowest = points.First();
            foreach (PointF p in points)
            {
                if (p.Y <= lowest.Y)
                {
                    if (p.Y == lowest.Y)
                    {
                        if (p.X < lowest.X)
                        {
                            continue;
                        }
                    }
                    lowest = p;
                }
            }
            return lowest;
        }

        private float counterClockwiseTurn(PointF p1, PointF p2, PointF p3)
        {
            return (p2.X - p1.X) * (p3.Y - p1.Y) - (p2.Y - p1.Y) * (p3.X - p1.X);
        }
    }
}
