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
        /// Returns all failed tests
        /// </summary>
        /// <param name="tests"></param>
        /// <returns>A list of failed tests</returns>
        public List<SQLTestCase> EvaluateTests(List<SQLTestCase> tests)
        {
            List<SQLTestCase> failedTests = new List<SQLTestCase>();
            foreach (SQLTestCase test in tests)
            {
                if (!test.EvaluateResults())
                {
                    failedTests.Add(test);
                }
            }
            return failedTests;
        }
    }
}
