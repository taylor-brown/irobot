using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IRobotCreate.Script
{
    public class CommandScript
    {
        private List<Byte> byteList;


        public CommandScript()
        {
            byteList = new List<Byte>();
        }

        public void addScript(byte[] data)
        {
            for (int i = 0; i < data[1]; i++)
            {
                byteList.Add(data[i + 2]);
            }
        }

        public void addCommand(byte cmd)
        {
            byteList.Add(cmd);
        }
        public void addCommand(int cmd)
        {
            byteList.Add((byte)cmd);
        }
        public void addBytes(byte[] cmds)
        {
            foreach (byte b in cmds)
            {
                addCommand(b);
            }
        }
        public byte[] getScript()
        {
            byte[] data = new byte[byteList.Count() + 3];
            data[0] = (byte)152;
            data[1] = (byte)(byteList.Count());
            for (int i = 2; i < data.Length - 1; i++)
            {
                data[i] = byteList[i - 2];
            }
            data[data.Length - 1] = (byte)153;
            return data;
        }

        public byte[] executeScript()
        {
            byte[] data = new byte[1];
            data[0] = (byte)153;
            return data;

        }
    }
}
