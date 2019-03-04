using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using HW4.Graph;

namespace HW4.Parser
{
    public interface IParser
    {
        IList<IGraphObject> parseWorkSpace(string filename);
        IList<PointF> parseStartGoal(string filename);
    }
}
