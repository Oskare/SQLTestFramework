using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLTestFramework.Framework
{
    /// <summary>
    /// Interface to be implemented by components reading test cases from user input
    /// </summary>
    public interface IInputHandler
    {
        List<SQLTestCase> ReadTests(String filename);
    }
}
