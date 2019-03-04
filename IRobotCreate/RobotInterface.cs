using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Text;
using IRobotCreate.Sensors;

namespace IRobotCreate
{
    public class RobotInterface : IRobotCreate.IRobotInterface
    {
        public enum Mode{
            Off,
            Passive,
            Safe,
            Full
        }
        SerialPort _port;
        Mode _mode = Mode.Off;
        IList<ISensor> _streamSensors;
        SensorParser _parser;

        public RobotInterface(string portName)
        {
            _port = new SerialPort();
            _port.PortName = portName;
            _port.BaudRate = 57600;
            _port.DataBits = 8;
            _port.DtrEnable = false;
            _port.StopBits = StopBits.One;
            _port.Handshake = Handshake.None;
            _port.Parity = Parity.None;
            _port.RtsEnable = false;
            _port.Close();
            _streamSensors = new List<ISensor>();
            _port.DataReceived += this.dataReceived;
        }

        public void open()
        {
            _parser = new SensorParser();
            if (!_port.IsOpen)
            {
                _port.Open();
            }
            setMode(Mode.Passive);
            setMode(Mode.Safe);
        }

        public Mode CurrentMode
        {
            get { return _mode; }
            set { 
                _mode = value;
                setMode(value);
            }
        }

        private void setMode(Mode mode)
        {
            switch(mode)
            {
                case Mode.Full:
                    execute(OpCode.FullMode);
                    break;
                case Mode.Safe:
                    execute(OpCode.SafeMode);
                    break;
                case Mode.Passive:
                    execute(OpCode.Start);
                    break;
            }
        }

        public bool execute(byte opCode)
        {
            return (execute(opCode, new List<Byte>()));
        }

        public bool execute(byte opCode, IList<Byte> data)
        {
            byte[] b = new byte[data.Count+1];
            b[0] = opCode;
            int i = 0;
            foreach (byte curByte in data)
            {
                b[++i] = curByte;
            }
            return execute(b);
        }

        public bool execute(byte[] data)
        {
            try
            {
                _port.RtsEnable = false;
                _port.Write(data, 0, data.Length);
                System.Threading.Thread.Sleep(15);
            }
            catch (Exception e)
            {
                System.Console.WriteLine("error:" + e.Message);
                return false;
            }
            return true;
        }

        public bool Stream(IList<ISensor> sensors)
        {
            _streamSensors = sensors;
            _parser = new SensorParser(sensors);
            _port.DataReceived += this.dataReceived;
            List<byte> data = new List<byte>();
            //number of data packets to receive
            data.Add((byte)sensors.Count);
            //which data packets to receive
            foreach(ISensor sensor in sensors){
                data.Add(sensor.getPacketCode());
            }
            try
            {
                execute(OpCode.Stream, data);
            }
            catch (Exception e)
            {
                System.Console.WriteLine("error:" + e.Message);
                return false;
            }
            return true;
        }

        public void dataReceived(object sender, SerialDataReceivedEventArgs ars)
        {
            byte[] bytes = new byte[_port.BytesToRead];
            _port.Read(bytes, 0, _port.BytesToRead);
            _parser.addStreamData(bytes);
            //_parser.addStreamData(_port.ReadExisting());
            //updateContinuousSensors();
        }

        private void updateContinuousSensors()
        {
            byte[] data;
            try
            {
                data = _parser.getLastSensorPacket();
            }
            catch (InvalidDataStreamException ex)
            {
                return;
            }
            int i = 1;
            foreach (ISensor sensor in _streamSensors)
            {
                if (sensor.getPacketCode() == data[i])
                {
                    if (sensor.isContinuous())
                    {
                        sensor.setValue(
                           bytesToInteger( data.Slice(i + 1, i + sensor.dataSize())));
                    }
                    else
                    {
                        sensor.setValue(data[i + 1]);
                    }
                }
                i+= sensor.dataSize();
            }
        }

        public int bytesToInteger(byte[] b)
        {
            int i = 0;
            i |= b[0] & 0xFF;
            i <<= 8;
            i |= b[1] & 0xFF;
            return i;

        }

        public IList<ISensor> getSensorData()
        {
            return _streamSensors;
        }

        public int readData(byte[] data, int length)
        {
            byte[] buffer = new byte[length];
            //_port.ReadExisting();
            //_parser.stringToByteArr(_port.ReadExisting());
            execute(data);
            //_port.Read(buffer, 0, length);
            //_port.Read(buffer, 0, length);
            System.Threading.Thread.Sleep(60);
            buffer = _parser.getLatestBytes();
            Array.Reverse(buffer);
            return BitConverter.ToInt16(buffer,0);
        }

        public System.IO.Stream getStream()
        {
            return _port.BaseStream;
        }
    }
}
