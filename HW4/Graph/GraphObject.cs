using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using HW4.Graph.Utility;

namespace HW4.Graph
{
    public class GraphObject : IGraphObject
    {
        protected List<PointF> _points;
        protected Pen _borderPen;
        protected Pen _expandedPen;
        protected float _robotWidth;

        public GraphObject()
        {
            _points = new List<PointF>();
            _borderPen = new Pen(Color.Black, 1f);
            _expandedPen = new Pen(Color.Blue, 1f);
            _robotWidth = 0;
        }
        #region IGraphObject Members

        public void addPoint(System.Drawing.PointF p)
        {
            _points.Add(p);
        }

        public IList<System.Drawing.PointF> getPoints()
        {
            return _points;
        }
        public virtual void drawBorder(Graphics picture, float scale)
        {
            picture.DrawPolygon(_borderPen, _points.
                Select(s => scalePoint(flipXAxis(s), scale)).ToArray());
        }
        public IList<PointF> getObstacleExpandedPoints()
        {
            RobotRectangle rr = new RobotRectangle(_robotWidth);
            return rr.expandObstacle(_points);
        }

        public void drawObstacleExpandedBorder(Graphics picture, float scale)
        {
//            RobotRectangle rr = new RobotRectangle(_robotWidth);
            picture.DrawPolygon(_expandedPen, getObstacleExpandedPoints()
                .Select(s => scalePoint(flipXAxis(s), scale)).ToArray());
        }

        public void setRobotWidth(float robotWidth)
        {
            _robotWidth = robotWidth;
        }

        public bool withinExpandedObject(PointF pointToExplore)
        {
            IList<PointF> points = getObstacleExpandedPoints();
            if (points.Contains(pointToExplore))
                return false;
            float maxX = points.Max(s => s.X) + 2;
            int counter = 0;
            PointF last = points.Last();
            foreach (PointF next in points)
            {
                if (LineIntersection.SegmentsIntersect(
                    new LineSegment(pointToExplore, new PointF(maxX, pointToExplore.Y)),
                    new LineSegment(last, next)))
                    counter++;
                last = next;
            }
            if (counter % 2 == 0)
                return false;
            return true;
        }
        #endregion

        protected PointF scalePoint(PointF p, float scale)
        {
            p.X *= scale;
            p.Y *= scale;
            return p;
        }

        protected PointF flipXAxis(PointF point)
        {
            point.Y = -point.Y;
            return point;
        }

    }
}
