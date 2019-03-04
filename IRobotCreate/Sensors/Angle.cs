using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IRobotCreate.Sensors
{
    public class Angle : ISensor
    {
        int _value;

        #region ISensor Members

        public byte getPacketCode()
        {
            return (byte)20;
        }

        public int dataSize()
        {
            return 3;
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
            return true;
        }
        #endregion

        public override string ToString()
        {
            return "Angle: " + _value;
        }
    }
}
