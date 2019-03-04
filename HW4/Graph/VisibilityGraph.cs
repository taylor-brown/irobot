using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using HW4.Graph.Utility;

namespace HW4.Graph
{
    public class VisibilityGraph
    {
        Pen _visibilityPen = new Pen(Color.Aquamarine, .1f);
        private PointF _start;

        public PointF Start
        {
            get { return _start; }
            set { _start = value; }
        }
        private PointF _goal;

        public PointF Goal
        {
            get { return _goal; }
            set { _goal = value; }
        }
        private List<IGraphObject> _polygons;

        public VisibilityGraph()
        {
            _polygons = new List<IGraphObject>();
        }

        public void addObjects(IEnumerable<IGraphObject> objects)
        {
            _polygons.AddRange(objects);
        }

        private void addWallLines(List<LineSegment> lines, List<PointF> points)
        {
            PointF last = points.Last();
            foreach (PointF p in points)
            {
                lines.Add(new LineSegment(last, p));
                last = p;
            }
        }

        private void addObstacleLines(List<LineSegment> lines, List<PointF> points)
        {
            foreach (PointF point1 in points)
                {
                    foreach (PointF point2 in points)
                    {
                        lines.Add(new LineSegment(point1, point2));
                    }
                }
        }

        public IEnumerable<LineSegment> getVisibilityGraph()
        {
            List<LineSegment> collisionLines = new List<LineSegment>();
            List<PointF> potentialVertices = new List<PointF>();
            HashSet<LineSegment> validPaths = new HashSet<LineSegment>();
            // build list of obstacles/potential vertices
            foreach (IGraphObject gobj in _polygons)
            {
                if (gobj is Wall)
                {
                    addWallLines(collisionLines, gobj.getPoints().ToList());
                }
                else
                {
                    IList<PointF> expandedPoints = gobj.getObstacleExpandedPoints();
                    addObstacleLines(collisionLines, expandedPoints.ToList());
                    potentialVertices.AddRange(expandedPoints);
                }
            }
            // check for vertices within objects...
            foreach (Obstacle gobj in _polygons.Where(s => !(s is Wall)))
            {
                potentialVertices = potentialVertices.Where(s => !gobj.withinExpandedObject(s)).ToList();
            }
            potentialVertices.Add(_start);
            potentialVertices.Add(_goal);
            // compare all potential lines for collisions with obstacles
            foreach (PointF i in potentialVertices)
            {
                foreach (PointF j in potentialVertices)
                {
                    if (i == j)
                        continue;
                    LineSegment path = new LineSegment(i, j);
                    bool collision = false;
                    foreach(LineSegment line in collisionLines){
                        if (LineIntersection.SegmentsIntersect(path, line))
                        {
                            collision = true;
                            break;
                        }
                    }
                    if(!collision)
                        validPaths.Add(path);
                }
            }
            return validPaths;
        }

        public void drawVisibilityGraph(Graphics picture, float scale)
        {
            IEnumerable<LineSegment> lines = getVisibilityGraph();
            if (lines == null)
            {
                throw new Exception("No valid path found.");
            }
            foreach (LineSegment line in lines)
            {
                picture.DrawLine(_visibilityPen, 
                    scalePoint(flipXAxis(line.A), scale), 
                    scalePoint(flipXAxis(line.B), scale));
            }
        }

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
