using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLTestFramework.Framework
{
    /// <summary>
    /// Interface to be implemented by component validating test execution results
    /// </summary>
    public interface ITestValidator
    {
        void EvaluateTests(List<SQLTestCase> tests);
    }
}
