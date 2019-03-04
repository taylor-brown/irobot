using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using HW4.Planning;

namespace HW4.Graph
{
    public class ScaledDrawer
    {
        
        public static void drawBoxedPoint(Graphics g,Pen pen, PointF p,float scale)
        {
            //draw start and end points
            int width = 5;
            Rectangle r = new Rectangle((int)(scalePoint(flipXAxis(p), scale).X - width / 2),
                (int)(scalePoint(flipXAxis(p), scale).Y - (width / 2)), width, width);
            g.DrawRectangle(pen, r);
        }

        public static void drawShortestPath(Graphics g,Pen pen, Node goal, float scale)
        {
            Node current = goal;
            while (current.Previous != null && current.Previous != current)
            {
                g.DrawLine(pen,
                    scalePoint(flipXAxis(current.Previous.Location), scale),
                    scalePoint(flipXAxis(current.Location), scale));
                current = current.Previous;
            }
        }


        protected static PointF scalePoint(PointF p, float scale)
        {
            p.X *= scale;
            p.Y *= scale;
            return p;
        }

        protected static PointF flipXAxis(PointF point)
        {
            point.Y = -point.Y;
            return point;
        }
    }
}
