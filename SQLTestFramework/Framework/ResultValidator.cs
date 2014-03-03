using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLTestFramework.Framework
{
    /// <summary>
    /// Validate test case execution results with expected results
    /// </summary>
    class ResultValidator: IResultValidator
    {
        /// <summary>
        /// Returns the number of failed tests
        /// </summary>
        /// <param name="tests"></param>
        /// <returns></returns>
        public int EvaluateTests(List<SQLTestCase> tests)
        {
            int failedTests = 0;
            foreach (SQLTestCase test in tests)
            {
                if (!test.EvaluateResults())
                {
                    failedTests++;
                }
            }
            return failedTests;
        }
    }
}
