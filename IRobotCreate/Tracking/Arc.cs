using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace IRobotCreate.Tracking
{
    public class Arc
    {
        Point _start = new Point(0,0);
        Point _end;
        int _radius;
        int _velocity;
        TimeSpan _span;

        #region IMove Members

        public void setStartPoint(Point p)
        {
            _start = p;
        }
        public void setEndPoint(Point p)
        {
            
        }
        public Point getStartPoint()
        {
            return _start;
        }

        public Point getEndPoint()
        {
            Point p = _start;
            p.Offset(new Point((int)getXDistance(), (int)getYDistance()));
            return p;
        }

        public int getRadius()
        {
            return _radius;
        }

        public double getArcAngleInDegrees()
        {
            return getDistanceTravelled() * 360 / (2 * Math.PI * _radius);
        }

        public double getArcAngle()
        {
            return getDistanceTravelled() / (_radius);
        }

        public  double getXDistance()
        {
            return _radius - _radius * Math.Cos(getArcAngle());
        }

        public double getYDistance()
        {
            return _radius * Math.Sin(getArcAngle());
        }

        public double getStraightLineDistance()
        {
            return Math.Sqrt(Math.Pow(getXDistance(), 2) + Math.Pow(getYDistance(), 2));
        }

        public void setRadius(int mm)
        {
            _radius = mm;
        }

        public void setVelocity(int mms)
        {
            _velocity = mms;
        }

        public void setTimeElapsed(TimeSpan span)
        {
            _span = span;
        }

        public void setArcLength(double length)
        {
            _velocity = 1;
            _span = new TimeSpan(0, 0,(int) length);
        }
        public double getDistanceTravelled()
        {
            return _velocity * _span.TotalSeconds;
        }

        #endregion    
    }
}
