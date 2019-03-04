using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IRobotCreate.Script
{
    public class CommandConverter
    {

        public static byte[] commandWithTwoBytes(int opCode, int one)
        {
            byte[] data = new byte[5];

            data[0] = (byte)opCode;  //Direct Drive command

            data[1] = (byte)((one) >> 8 & 0x00FF);
            data[2] = (byte)((one) & 0x00FF);
            return data;
        }

        public static byte[] integerToBytes(int integer)
        {
            byte[] data = new byte[2];
            data[0] = (byte)(integer >> 8 & 0x00FF);
            data[1] = (byte)(integer & 0x00FF);
            return data;
        }

        public static byte[] commandWithFourBytes(int opCode, int one, int two)
        {
            byte[] data = new byte[5];

            data[0] = (byte)opCode;  //Direct Drive command

            data[1] = (byte)((one) >> 8 & 0x00FF);
            data[2] = (byte)((one) & 0x00FF);
            data[3] = (byte)((two) >> 8 & 0x00FF);
            data[4] = (byte)((two) & 0x00FF);
            return data;
        }

        public static byte[] commandWithThreeBytes(int opCode, int one, int two, int three)
        {
            byte[] data = new byte[4];

            data[0] = (byte)opCode;  //Direct Drive command

            data[1] = (byte)one;
            data[2] = (byte)two;
            data[3] = (byte)three;
            return data;
        }

        public static int bytesToInteger(byte[] b)
        {
            int i = 0;
            i |= b[0] & 0xFF;
            i <<= 8;
            i |= b[1] & 0xFF;
            return i;

        }
    }
}
