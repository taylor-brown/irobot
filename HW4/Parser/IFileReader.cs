using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace HW4.Parser
{
    public  interface IFileReader
    {
        IList<string> getFileContents(string filename);
    }
}
