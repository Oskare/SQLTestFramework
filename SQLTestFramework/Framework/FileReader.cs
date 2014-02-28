using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLTestFramework.Framework
{
    /// <summary>
    /// Input implementation which reads all input from .txt files
    /// </summary>
    class FileReader: IInputHandler
    {
        public List<SQLTestCase> ReadTests(string filename = "")
        {
            throw new NotImplementedException();
        }
    }
}
