using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IRobotCreate.Sensors
{
    public class Wall : ISensor
    {
        int _value;
        #region ISensor Members

        public byte getPacketCode()
        {
            return (byte)8;
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
            return "Wall sensor: " + wall();
        }

        public bool wall()
        {
            if (_value == 1)
            {
                return true;
            }
            if (_value == 0)
            {
                return false;
            }
            throw new Exception("Sanity check - Impossible wall value.");
        }
    }
}
