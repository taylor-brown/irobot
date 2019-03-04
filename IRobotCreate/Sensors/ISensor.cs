using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IRobotCreate.Sensors
{
    public interface ISensor
    {
         byte getPacketCode();
         int dataSize();
         int getValue();
         void setValue(int value);
         void setValue(byte value);
         bool isContinuous();
    }
}
