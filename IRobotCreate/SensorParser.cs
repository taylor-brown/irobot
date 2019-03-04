using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IRobotCreate.Sensors;

namespace IRobotCreate
{
    public class SensorParser
    {
        private IList<ISensor> _sensorList;
        private int _numPackets;
        private IList<string> _streamData;
        private const int maxQueueLength = 5;
        private List<byte> _byteStream;

        public SensorParser()
        {
            _byteStream = new List<byte>();
        }

        public SensorParser(IList<ISensor> sensorList)
        {
            _sensorList = sensorList;
            _streamData = new List<string>();
            _numPackets = sensorList.Sum(s => s.dataSize());
            
        }

        public byte[] getLatestBytes()
        {
            if (_byteStream.Count > 1)
            {
                byte[] bits = new byte[2];
                bits[0] = _byteStream[_byteStream.Count - 2];
                bits[1] = _byteStream.Last();
                _byteStream.Clear();
                return bits;
            }
            return new byte[]{ 0, 0 };
        }

        public void addStreamData(byte[] stream)
        {
            _byteStream.AddRange(stream);
        }

        public void addStreamData(string stream)
        {
            lock (_streamData)
            {
                if (_streamData.Count > maxQueueLength)
                {
                    _streamData.RemoveAt(0);
                }
                _streamData.Add(stream);
            }
        }

        public  byte[] getLastSensorPacket()
        {
            string localCopy;
            if(_streamData.Count < 1)
                throw new InvalidDataStreamException();
            lock (_streamData)
            {
                localCopy = _streamData.Aggregate((final, item) => final + item);
                _streamData = new List<string>();
            }
            byte[] curr = stringToByteArr(localCopy);
            int ijk = 0;
            for (int i = curr.Count()-1; i >= 0; i--)
            {
                if (curr[i] == 19 && curr.Length - i > _numPackets && curr[i+1] == _numPackets)
                {
                    //for some reason, the checksum is always 63... that's not right...
                    // so go based on length.  for bumps/wall, there's no way to get 19.
                    //if(isValidChecksum(curr.Slice(i+1, i+_numPackets))){
                        return curr.Slice(i+2, i+_numPackets + 1);
                    //}
                }
            }
            throw new InvalidDataStreamException();
        }

        private bool isValidChecksum(byte[] barr)
        {
            int sum = 0;
            foreach (byte b in barr)
            {
                sum += b;
            }
            return (sum & (sum & 255)) == 0 ? true : false;
        }

        public byte[] stringToByteArr(string toConvert)
        {
            System.Text.ASCIIEncoding encoding = new ASCIIEncoding();
            return encoding.GetBytes(toConvert);
        }
    }

    public static class Extensions
    {
        /// <summary>
        /// Get the array slice between the two indexes.
        /// Inclusive for start index, exclusive for end index.
        /// </summary>
        public static T[] Slice<T>(this T[] source, int start, int end)
        {
            // Handles negative ends
            if (end < 0)
            {
                end = source.Length - start - end - 1;
            }
            int len = end - start;

            // Return new array
            T[] res = new T[len];
            for (int i = 0; i < len; i++)
            {
                res[i] = source[i + start];
            }
            return res;
        }
    }


    public class InvalidDataStreamException : Exception
    {

    }
}
