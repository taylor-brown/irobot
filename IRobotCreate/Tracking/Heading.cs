using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace IRobotCreate.Tracking
{
    class Heading
    {
        double _headingAngle;

        public double HeadingAngle
        {
            get { return _headingAngle; }
            set
            {
                _headingAngle = value;
                while (_headingAngle > 2 * Math.PI)
                {
                    _headingAngle = _headingAngle - Math.PI;
                }
                while (_headingAngle < 0)
                {
                    _headingAngle = _headingAngle + Math.PI;
                }
            }
        }
        Point _currentPoint = new Point(0, 0);

        public Heading()
        {
            _headingAngle = Math.PI / 2;
            _headingAngle = 0;
        }
        public void updateHeading(Arc move)
        {
            if (move is Arc)
            {
                _headingAngle += ((Arc)move).getArcAngle();
            }
            Point p = new Point((int)(move.getDistanceTravelled() * Math.Cos(_headingAngle)),
                    (int)(move.getDistanceTravelled() * Math.Sin(_headingAngle)));

            if (_headingAngle < Math.PI / 2)
            {
                if (move.getEndPoint().X < 0)
                {
                    p.X = p.X * -1;
                }
                if (move.getEndPoint().Y < 0)
                {
                    p.Y = p.Y * -1;
                }
                
            }
            else if (_headingAngle < Math.PI)
            {
                if (move.getEndPoint().X > 0)
                {
                    p.X = p.X * -1;
                }
                if (move.getEndPoint().Y < 0)
                {
                    p.Y = p.Y * -1;
                }
            }
            else if (_headingAngle < 3* Math.PI / 2)
            {
                if (move.getEndPoint().X > 0)
                {
                    p.X = p.X * -1;
                }
                if (move.getEndPoint().Y > 0)
                {
                    p.Y = p.Y * -1;
                }
            }
            else 
            {
                if (move.getEndPoint().X < 0)
                {
                    p.X = p.X * -1;
                }
                if (move.getEndPoint().Y > 0)
                {
                    p.Y = p.Y * -1;
                }
            }
            _currentPoint.Offset(p);
        }

        public int getXoffset()
        {
            return _currentPoint.X;
        }
        public int getYoffset()
        {
            return _currentPoint.Y;
        }

        public Point getCurrentPoint()
        {
            return _currentPoint;
        }
    }
}
