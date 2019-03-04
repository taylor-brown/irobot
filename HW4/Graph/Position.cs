using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace HW4.Graph
{
    public class Position : GraphObject
    {
        public Position()
        {
            _borderPen = new Pen(Color.Red, 1);
        }

        public override void drawBorder(Graphics picture, float scale)
        {
            _points.Add(_points.First());
            picture.DrawPolygon(_borderPen, _points.
                Select(s => scalePoint(flipXAxis(s), scale)).ToArray());
        }
    }
}
