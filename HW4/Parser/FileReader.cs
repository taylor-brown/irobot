using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace HW4.Parser
{
    public class FileReader : IFileReader
    {
        #region IFileReader Members

        public IList<string> getFileContents(string fileName)
        {
            FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
            StreamReader sr = new StreamReader(fs);
            List<string> fileContents = new List<string>();
            while (!sr.EndOfStream)
            {
                fileContents.Add(sr.ReadLine().Trim());
            }
            return fileContents;
        }

        #endregion
    }
}
