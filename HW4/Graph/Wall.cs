using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace HW4.Graph
{
    public class Wall : GraphObject
    {
        public Wall()
        {
            _borderPen.Color = Color.Red;
            _borderPen.Width = 1;
        }
        public RectangleF getBounds()
        {
            float xmin = _points[0].X;
            float xmax = _points[0].X;
            float ymin = _points[0].Y;
            float ymax = _points[0].Y;
            foreach (PointF point in _points)
            {
                xmin = Math.Min(point.X, xmin);
                xmax = Math.Max(point.X, xmax);
                ymin = Math.Min(point.Y, ymin);
                ymax = Math.Max(point.Y, ymax);
            }
            return new RectangleF(xmin, ymax, Math.Abs( xmax - xmin),Math.Abs( ymax - ymin));
        }
    }
}