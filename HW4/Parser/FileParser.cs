using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using HW4.Graph;
using System.Drawing;

namespace HW4.Parser
{
    public class FileParser : IParser
    {
        private IFileReader _fileReader;

        public FileParser() : this(new FileReader()) { }

        public FileParser(IFileReader filereader) {
            _fileReader = filereader;
        }

        #region IParser Members

        public IList<HW4.Graph.IGraphObject> parseWorkSpace(string filename)
        {
            try
            {
                IList<string> contents = readFile(filename);
                List<IGraphObject> graphObjects = new List<IGraphObject>();
                int numObstacles = Int32.Parse(contents[0]);
                int location = 1;
                while (numObstacles > 0)
                {
                    IGraphObject obj;
                    if (location == 1)
                    {
                        obj = new Wall();
                    }
                    else
                    {
                        obj = new Obstacle();
                    }
                    int numPoints = Int32.Parse( contents[location]);
                    location++;
                    while (numPoints > 0)
                    {
                        float first = float.Parse( contents[location].Split(' ')[0]);
                        float second = float.Parse( contents[location].Split(' ')[1]);
                        location++;
                        obj.addPoint(new PointF(first, second));
                        numPoints--;
                    }
                    graphObjects.Add(obj);
                    numObstacles--;
                }
                return graphObjects;
            }
            catch (Exception e)
            {
                throw new Exception("Error parsing file:" + e.Message);
            }
        }

        public IList<System.Drawing.PointF> parseStartGoal(string filename)
        {
            IList<System.Drawing.PointF> points = new List<PointF>();
            IList<string> contents = readFile(filename);
            float first = float.Parse(contents[0].Split(' ')[0]);
            float second = float.Parse(contents[0].Split(' ')[1]);
            points.Add(new PointF(first, second));
            first = float.Parse(contents[1].Split(' ')[0]);
            second = float.Parse(contents[1].Split(' ')[1]);
            points.Add(new PointF(first, second));
            return points;
        }

        #endregion

        private IList<string> readFile(string fileName)
        {

            return _fileReader.getFileContents(fileName);
        }
    }
}
