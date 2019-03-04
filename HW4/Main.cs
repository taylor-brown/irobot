using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HW4.Parser;
using HW4.Graph;
using HW4.Graph.Utility;
using HW4.Planning;
using HW4.Execution;

namespace HW4
{
    public partial class Main : Form
    {
        public float _scale = 50;
        public IList<IGraphObject> _graphObjects;
        public Pen _positionPen = new Pen(Color.Green, .1f);
        public Pen _shortestPathPen = new Pen(Color.Purple, .1f);
        /// <summary>
        /// crete width, in meters...
        /// </summary>
        public float _createWidth = .33f;
        public PointF _start;
        public PointF _goal;

        public Main()
        {
            InitializeComponent();
            _graphObjects = new List<IGraphObject>();
            //IParser parser = new FileParser();
            //_graphObjects = parser.parseWorkSpace(@"C:\Documents and Settings\fv046c\Desktop\obstacles.txt");
            //IList<PointF> points = parser.parseStartGoal(@"C:\Documents and Settings\fv046c\Desktop\start goal.txt");
            //_start = points[0];
            //_goal = points[1];
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void viewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void obstacleCourseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog fd = new OpenFileDialog();
            DialogResult r = fd.ShowDialog();
            if (r == DialogResult.OK)
            {
                IParser parser = new FileParser();
                _graphObjects = parser.parseWorkSpace(fd.FileName);
            }
        }

        public void obstaclesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Wall wall = (Wall)_graphObjects.Where(s => s is Wall).First();
            RectangleF bounds = wall.getBounds();
            pictureBoxMain.SizeMode = PictureBoxSizeMode.AutoSize;
            Bitmap bitmap = new Bitmap((int)Math.Ceiling((bounds.Width + Math.Abs(bounds.X)) * _scale),
                (int)Math.Ceiling((bounds.Height + Math.Abs(bounds.Y)) * _scale));
            Graphics g =  setupGraphic(bitmap, bounds);
            g.Clear(Color.White);
            foreach (IGraphObject obj in _graphObjects)
            {
                obj.drawBorder(g, _scale);
            }
            pictureBoxMain.Image = bitmap;
        }

        private Graphics setupGraphic(Bitmap bitmap, RectangleF bounds)
        {
            Graphics g = Graphics.FromImage(bitmap);
            
            float xtrans = 0;
            float ytrans = Math.Abs(bounds.Y);
            if (bounds.X < 0)
                xtrans = -bounds.X;
            g.TranslateTransform(xtrans * _scale, ytrans * _scale);
            return g;
        }

        public void grownObstaclesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            obstaclesToolStripMenuItem_Click(null, null);
            Wall wall = (Wall)_graphObjects.Where(s => s is Wall).First();
            RectangleF bounds = wall.getBounds();
            Bitmap bitmap = pictureBoxMain.Image as Bitmap;
            Graphics g = setupGraphic(bitmap, bounds);
            foreach(IGraphObject obj in _graphObjects){
                if (obj is Obstacle)
                {
                    obj.setRobotWidth(_createWidth);
                    obj.drawObstacleExpandedBorder(g, _scale);
                }
            }
            pictureBoxMain.Image = bitmap;
        }

        public void visibilityGraphToolStripMenuItem_Click(object sender, EventArgs e)
        {
            grownObstaclesToolStripMenuItem_Click(null, null);
            Wall wall = (Wall)_graphObjects.Where(s => s is Wall).First();
            RectangleF bounds = wall.getBounds();
            Bitmap bitmap = pictureBoxMain.Image as Bitmap;
            Graphics g = setupGraphic(bitmap, bounds);
            ScaledDrawer.drawBoxedPoint(g, _positionPen, _start, _scale);

            ScaledDrawer.drawBoxedPoint(g, _positionPen, _goal, _scale);

            VisibilityGraph vg = new VisibilityGraph();
            vg.addObjects(_graphObjects);
            vg.Goal = _goal;
            vg.Start = _start;
            try
            {
                vg.drawVisibilityGraph(g, _scale);
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
            pictureBoxMain.Image = bitmap;
        }

        public void solutionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            visibilityGraphToolStripMenuItem_Click(null, null);
            Wall wall = (Wall)_graphObjects.Where(s => s is Wall).First();
            RectangleF bounds = wall.getBounds();
            Bitmap bitmap = pictureBoxMain.Image as Bitmap;
            Graphics g = setupGraphic(bitmap, bounds);

            VisibilityGraph vg = new VisibilityGraph();
            vg.addObjects(_graphObjects);
            vg.Goal = _goal;
            vg.Start = _start;
            NodeGraph ng = new NodeGraph();
            foreach (LineSegment line in vg.getVisibilityGraph())
            {
                ng.addLine(line);
            }
            Node start = new Node();
            start.Location = _start;
            Node goal = new Node();
            goal.Location = _goal;
            Node solution = Djikstra.ShortestPath(ng, start, goal);
            ScaledDrawer.drawShortestPath(g, _shortestPathPen, solution, _scale);
            pictureBoxMain.Image = bitmap;
        }



        private void startAndGoalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog fd = new OpenFileDialog();
            DialogResult r = fd.ShowDialog();
            if (r == DialogResult.OK)
            {
                IParser parser = new FileParser();
                IList<PointF> points = parser.parseStartGoal(fd.FileName);
                _start = points.First();
                _goal = points.Last();
            }
        }

        private void sToolStripMenuItem_Click(object sender, EventArgs e)
        {
            visibilityGraphToolStripMenuItem_Click(null, null);
            Wall wall = (Wall)_graphObjects.Where(s => s is Wall).First();
            RectangleF bounds = wall.getBounds();
            Bitmap bitmap = pictureBoxMain.Image as Bitmap;
            Graphics g = setupGraphic(bitmap, bounds);

            VisibilityGraph vg = new VisibilityGraph();
            vg.addObjects(_graphObjects);
            vg.Goal = _goal;
            vg.Start = _start;
            NodeGraph ng = new NodeGraph();
            foreach (LineSegment line in vg.getVisibilityGraph())
            {
                ng.addLine(line);
            }
            Node start = new Node();
            start.Location = _start;
            Node goal = new Node();
            goal.Location = _goal;
            Node solution = Djikstra.ShortestPath(ng, start, goal);
            ScaledDrawer.drawShortestPath(g, _shortestPathPen, solution, _scale);
            pictureBoxMain.Image = bitmap;
            Driver driver = new Driver(solution);
            System.Threading.Thread tr = new System.Threading.Thread(driver.followPathToGoal);
            tr.Start();
        }
    }
}
