using System;
using System.IO;
namespace IRobotCreate
{
    public interface IRobotInterface
    {
        void dataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs ars);
        bool execute(byte opCode, System.Collections.Generic.IList<byte> data);
        bool execute(byte opCode);
        bool execute(byte[] data);
        int readData(byte[] data, int length);
        System.Collections.Generic.IList<IRobotCreate.Sensors.ISensor> getSensorData();
        void open();
        bool Stream(System.Collections.Generic.IList<IRobotCreate.Sensors.ISensor> sensors);
        Stream getStream();
    }
}
