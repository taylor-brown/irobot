using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IRobotCreate
{
    public static class OpCode
    {
        public const byte Start = 128;
        public const byte SafeMode = 131;
        public const byte FullMode = 132;
        public const byte Drive = 137;
        public const byte Sensors = 142;
        public const byte DriveDirect = 145;
        public const byte Stream = 148;
        public const byte StreamPauseResume = 150;
    }
}
