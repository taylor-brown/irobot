using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IRobotCreate.Sensors
{
    public class Bumps : ISensor
    {
        int _value;
        #region ISensor Members

        public byte getPacketCode()
        {
            return (byte)7;
        }

        public int dataSize()
        {
            return 2;
        }

        public int getValue()
        {
            return _value;
        }
        public void setValue(int value)
        {
            _value = value;
        }

        public void setValue(byte value)
        {
            _value = value;
        }

        public bool isContinuous()
        {
            return false;
        }

        #endregion

        public override string ToString()
        {
            return "Left Bump Sensor: " + getLeftBump() + "\n"
                + "Right Bump Sensor: " + getRightBump();
        }
        public bool getLeftBump()
        {
            if ((_value & 2) == 2)
                return true;
            return false;
        }
        public bool getRightBump()
        {
            if ((_value & 1) == 1)
                return true;
            return false;
        }
    }
}
