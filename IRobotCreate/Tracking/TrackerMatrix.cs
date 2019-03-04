using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing.Drawing2D;
using System.Drawing;

namespace IRobotCreate.Tracking
{
    public class TrackerMatrix
    {
        Matrix _position;
        float _angle;

        private float Angle
        {
            get { return _angle; }
            set
            {
                _angle = value;
                while (_angle >= 360)
                {
                    _angle -= 360;
                }
                while (_angle < 0)
                {
                    _angle += 360;
                }
            }
        }

        public TrackerMatrix(float X, float Y)
        {
            _position = new Matrix();
            _position.Translate(X, Y);
            _angle = 0;
        }

        public TrackerMatrix()
            :this(0,0) { }

        public void rotateArc(float angle, float distance)
        {
            _position.Rotate(angle);
            Angle += angle;
            _position.Translate(0, distance);
        }

        public void rotate(float angle)
        {
            _position.Rotate(angle);
            Angle += angle;
        }
        public void goForward(float distance)
        {
            _position.Translate(0, distance);
        }

        public float getX()
        {
            return _position.OffsetX;
        }
        public float getY()
        {
            return _position.OffsetY;
        }

        public PointF getPoint()
        {
            return new PointF(getX(), getY());
        }

        public float getAngle()
        {
            double result = Math.Asin(_position.Elements[1])*180/Math.PI;
            if (result < 0)
            {
                result += 360;
            }
            return (float)result;
        }
    }
}
