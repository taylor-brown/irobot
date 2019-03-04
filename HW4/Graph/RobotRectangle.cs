using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace HW4.Graph
{
    public class RobotRectangle
    {
        float _width;
        public RobotRectangle(float width)
        {
            _width = width;
        }

        public IList<PointF> expandObstacle(IList<PointF> obstacleVertices)
        {
            ConvexHull ch = new ConvexHull();
            foreach(PointF point in obstacleVertices){
                ch.addPointRange(reflectAboutPoint(point));
            }
            return ch.getConvexHullPoints();
        }

        public IList<PointF> reflectAboutPoint(PointF point)
        {
            List<PointF> expansion = new List<PointF>();
            List<int> sign = new List<int>();
            sign.Add(-1);
            sign.Add(1);
            foreach(int xSign in sign){
                foreach (int ySign in sign)
                {
                    expansion.Add( new PointF(point.X + _width * xSign,point.Y + _width * ySign));
                }
            }
            return expansion;
        }
    }
}
