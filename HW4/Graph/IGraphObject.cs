using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;

namespace HW4.Graph
{
    public interface IGraphObject
    {
        void addPoint(PointF p);
        IList<PointF> getPoints();
        void drawBorder(Graphics picture, float scale);
        void setRobotWidth(float robotWidth);
        IList<PointF> getObstacleExpandedPoints();
        void drawObstacleExpandedBorder(Graphics picture, float scale);
        bool withinExpandedObject(PointF p);
    }
}
