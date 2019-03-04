using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IRobotCreate.Sensors;
using IRobotCreate.Tracking;
using System.Drawing;

namespace IRobotCreate
{
    public class Robot : IRobotCreate.IRobot
    {
        private enum currentState
        {
            Stop,
            Forward,
            Back,
            Right,
            Left,
            ArcRight,
            ArcLeft
        }
        IRobotInterface _interface;
        Tracking.TrackerMatrix _tracker;
        currentState _state;
        DateTime _lastUpdate;
        int _velocity = 150;
        int _radius = 195;
        float _forwardError = .9333333F;
        float _angleError = 1.08F;
        float _arcError = .823F;


        public Robot(IRobotInterface rint)
        {
            _interface = rint;
            _state = currentState.Stop;
            _lastUpdate = DateTime.Now;
        }
        public Robot(string comport)
        {
            _interface = new RobotInterface(comport);
            _state = currentState.Stop;
            _lastUpdate = DateTime.Now;
        }

        public bool Open()
        { 
            _tracker = new IRobotCreate.Tracking.TrackerMatrix();
            _interface.open();
            //List<ISensor> sensorList = new List<ISensor>();
            //sensorList.Add(new Wall());
            //sensorList.Add(new Bumps());
            //sensorList.Add(new Angle());
            //sensorList.Add(new Distance());
            //_interface.Stream(sensorList);
            return true;
        }

        private void updateTracker()
        {
            Arc arc = new Arc();
            switch (_state)
            {
                case currentState.Stop:
                    break;
                case currentState.Right:
                    arc.setRadius(140);
                    arc.setVelocity(_velocity);
                    arc.setTimeElapsed(DateTime.Now - _lastUpdate);
                    _tracker.rotate(-(float)arc.getArcAngleInDegrees() * _angleError);
                    break;
                case currentState.Left:
                    arc.setRadius(140);
                    arc.setVelocity(_velocity);
                    arc.setTimeElapsed(DateTime.Now - _lastUpdate);
                    _tracker.rotate((float)arc.getArcAngleInDegrees() * _angleError);
                    break;
                case currentState.Forward:
                    _tracker.goForward((float)(_velocity * (DateTime.Now - _lastUpdate).TotalSeconds) * _forwardError);
                    break;
                case currentState.Back:
                    _tracker.goForward((float)(-50 * (DateTime.Now - _lastUpdate).TotalSeconds));
                    break;
                case currentState.ArcRight:
                    arc.setRadius(_radius);
                    arc.setVelocity(_velocity);
                    arc.setTimeElapsed(DateTime.Now - _lastUpdate);
                    _tracker.rotateArc(-(float)arc.getArcAngleInDegrees() * _arcError, (float)arc.getStraightLineDistance() * _forwardError);
                    break;
                case currentState.ArcLeft:
                    arc.setRadius(_radius);
                    arc.setVelocity(_velocity);
                    arc.setTimeElapsed(DateTime.Now - _lastUpdate);
                    _tracker.rotateArc((float)arc.getArcAngleInDegrees() * _arcError, (float)arc.getStraightLineDistance() * _forwardError);
                    break;
            }
        }

        public Point getCurrentLocation()
        {
            updateTracker();
            _lastUpdate = DateTime.Now;
            return new Point((int)_tracker.getX(), (int)_tracker.getY());
        }

        public float getCurrentHeading()
        {
            return _tracker.getAngle();
        }
        /// <summary>
        /// straight at 150mm/s
        /// </summary>
        /// <returns></returns>
        public void DriveStraight()
        {
            List<byte> bytes = new List<byte>();
            foreach (byte b in integerToBytes(_velocity))
            {
                bytes.Add(b);
            }
            foreach (byte b in integerToBytes(_velocity))
            {
                bytes.Add(b);
            }

            _interface.execute(OpCode.DriveDirect, bytes);
            updateTracker();
            _lastUpdate = DateTime.Now;
            _state = currentState.Forward;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="angle"></param>
        public void TurnRight()
        {
            List<byte> bytes = new List<byte>();
            foreach (byte b in integerToBytes(_velocity))
            {
                bytes.Add(b);
            }
            foreach (byte b in integerToBytes(65535))
            {
                bytes.Add(b);
            }

            _interface.execute(OpCode.Drive, bytes);
            updateTracker();
            _lastUpdate = DateTime.Now;
            _state = currentState.Right;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="angle"></param>
        public void TurnLeft()
        {
            List<byte> bytes = new List<byte>();
            foreach (byte b in integerToBytes(_velocity))
            {
                bytes.Add(b);
            }
            foreach (byte b in integerToBytes(1))
            {
                bytes.Add(b);
            }

            _interface.execute(OpCode.Drive, bytes);
            updateTracker();
            _lastUpdate = DateTime.Now;
            _state = currentState.Left;
        }
        /// <summary>
        /// 
        /// </summary>
        public void DriveRight()
        {
            List<byte> bytes = new List<byte>();
            foreach (byte b in integerToBytes(_velocity))
            {
                bytes.Add(b);
            }
            foreach (byte b in integerToBytes(-_radius))
            {
                bytes.Add(b);
            }

            _interface.execute(OpCode.Drive, bytes);
            updateTracker();
            _lastUpdate = DateTime.Now;
            _state = currentState.ArcRight;
        }
        /// <summary>
        /// 
        /// </summary>
        public void DriveLeft()
        {
            List<byte> bytes = new List<byte>();
            foreach (byte b in integerToBytes(_velocity))
            {
                bytes.Add(b);
            }
            foreach (byte b in integerToBytes(_radius))
            {
                bytes.Add(b);
            }

            _interface.execute(OpCode.Drive, bytes);
            updateTracker();
            _lastUpdate = DateTime.Now;
            _state = currentState.ArcLeft;
        }
        /// <summary>
        /// back at -50 mm/s
        /// </summary>
        public void DriveBack()
        {
            List<byte> bytes = new List<byte>();
            foreach (byte b in integerToBytes(-50))
            {
                bytes.Add(b);
            }
            foreach (byte b in integerToBytes(-50))
            {
                bytes.Add(b);
            }

            _interface.execute(OpCode.DriveDirect, bytes);
            updateTracker();
            _lastUpdate = DateTime.Now;
            _state = currentState.Back;
        }

        public void DriveStop()
        {
            List<byte> bytes = new List<byte>();
            foreach (byte b in integerToBytes(0))
            {
                bytes.Add(b);
            }
            foreach (byte b in integerToBytes(0))
            {
                bytes.Add(b);
            }

            _interface.execute(OpCode.DriveDirect, bytes);
            updateTracker();
            _lastUpdate = DateTime.Now;
            _state = currentState.Stop;
        }

        public IList<ISensor> getSensorData()
        {
            return _interface.getSensorData();
        }

        public static byte[] integerToBytes(int integer)
        {
            byte[] data = new byte[2];
            data[0] = (byte)(integer >> 8 & 0x00FF);
            data[1] = (byte)(integer & 0x00FF);
            return data;
        }

        public void Close()
        {
            
        }

        public int sendBytes(byte[] bytes, int length)
        {
            return _interface.readData(bytes, length);
        }

        public System.IO.Stream getStream()
        {
            return _interface.getStream();
        }
    }
}
