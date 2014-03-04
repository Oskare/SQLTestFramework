using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLTestFramework.Framework
{
    /// <summary>
    /// Interface to be implemented by component creating SQLTestCase's from some user input
    /// </summary>
    public interface IInputHandler
    {
        List<ISQLTestCase> ReadTests(String filename="");
    }
}
