using System;
namespace IRobotCreate
{
    public interface IRobot
    {
        void Close();
        void DriveBack();
        void DriveLeft();
        void DriveRight();
        void DriveStop();
        void DriveStraight();
        float getCurrentHeading();
        System.Drawing.Point getCurrentLocation();
        System.Collections.Generic.IList<IRobotCreate.Sensors.ISensor> getSensorData();
        bool Open();
        void TurnLeft();
        void TurnRight();
        int sendBytes(byte[] bytes, int length);
        System.IO.Stream getStream();
    }
}
