using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLTestFramework.Framework
{
    /// <summary>
    /// Interface to be implemented by component executing test cases
    /// </summary>
    public interface ITestExecutor
    {
        /// <summary>
        /// Execute test cases in parallel ánd store results in the test case objects
        /// </summary>
        /// <param name="tests"> The tests to be executed </param>
        void ExecutePar(List<ISQLTestCase> tests);

        /// <summary>
        /// Execute test cases sequentially, and store results in the test case objects
        /// </summary>
        /// <param name="tests"> The tests to be executed </param>
        void ExecuteSeq(List<ISQLTestCase> tests);
    }
}
