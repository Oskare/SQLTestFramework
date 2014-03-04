using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLTestFramework.Framework
{
    /// <summary>
    /// Interface to be implemented by component handling test run output
    /// </summary>
    public interface IOutputHandler
    {
        void Output(List<ISQLTestCase> tests, List<ISQLTestCase> failedTests);
    }
}
