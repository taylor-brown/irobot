using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IRobotCreate;
using IRobotCreate.Script;
using HW4.Planning;
using System.Drawing;
using IRobotCreate.Tracking;

namespace HW4.Execution
{
    public class Driver
    {
        //public float CurrentHeading = 0;
        //public PointF Location { get; set; }
        TrackerMatrix _location;
        public Robot _robot;
        public CommandScript _cs;
        public Node _goal;
        public Driver(Node goal)        
        {
            _cs = new CommandScript();
            _location = new TrackerMatrix();
            _robot = new Robot("COM19");
            _robot.Open();
            _goal = goal;
        }

        public void followPathToGoal()
        {
            List<PointF> plan = getLocationSequence(_goal).ToList();
            //_robot.setLocation(plan.First());
            _location = new TrackerMatrix(plan.First().X, plan.First().Y);
            //float totalDistance = 0;
            foreach (PointF next in plan)
            {
                if (Math.Abs(_location.getPoint().X - next.X) < .001 &&
                    Math.Abs(_location.getPoint().Y - next.Y) < .001)
                    continue;
                _location.goForward(1);
                PointF calc = _location.getPoint();
                _location.goForward(-1);
                int angle = (int)getAngleC(calc, next, _location.getPoint());
                if (counterClockwiseTurn(calc, _location.getPoint(), next) > 0)
                {
                    angle = angle * -1;
                }
                int distance = (int)(getDistance(next, _location.getPoint())*1000);
                _location.rotate(turnAngle(angle));
                _location.goForward(goDistance(distance)/1000);
            }
        }

        private int goDistance(int totalDistance)
        {
            
            byte[] data = new byte[2];
            data[0] = OpCode.Sensors;
            data[1] = 19;
            _robot.sendBytes(data, 2);
            _robot.DriveStraight();
            int result = 0;
            while (result < totalDistance)
            {
                result += _robot.sendBytes(data, 2);
                System.Threading.Thread.Sleep(50);
            }
            _robot.DriveStop();
            return result;
        }

        private int turnAngle(int angle)
        {
           
            byte[] data = new byte[2];
            data[0] = OpCode.Sensors;
            data[1] = 20; 
            _robot.sendBytes(data, 2);
            if (angle > 0)
                _robot.TurnLeft();
            else
                _robot.TurnRight();
            int result = 0;
            while (Math.Abs(result) < Math.Abs(angle))
            {
                result += _robot.sendBytes(data, 2);
                System.Threading.Thread.Sleep(50);
            }
            _robot.DriveStop();
            return result;
        }

        public float getDistance(PointF one, PointF two)
        {
            return (float)Math.Sqrt(Math.Pow(one.X - two.X, 2) + Math.Pow(one.Y-two.Y, 2));
        }

        public float getDistance(Point one, Point two)
        {
            return (float)Math.Sqrt(Math.Pow(one.X - two.X, 2) + Math.Pow(one.Y - two.Y, 2));
        }

        public float getAngleC(PointF A, PointF B, PointF C)
        {
            float a = getDistance(C, B);
            float b = getDistance(A, C);
            float c = getDistance(A, B);
            return (float)(180/Math.PI*Math.Acos((Math.Pow(a,2) + Math.Pow(b, 2) - Math.Pow(c, 2))/
                (2*a*b)));
        }

        public IEnumerable<PointF> getLocationSequence(Node node)
        {
            Stack<PointF> plan = new Stack<PointF>();
            Node current = node;
            while (current != null && current != current.Previous)
            {
                plan.Push(current.Location);
                current = current.Previous;
            }
            return plan;
        }

        private float counterClockwiseTurn(PointF p1, PointF p2, PointF p3)
        {
            return (p2.X - p1.X) * (p3.Y - p1.Y) - (p2.Y - p1.Y) * (p3.X - p1.X);
        }
    }
}
