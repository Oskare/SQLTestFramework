using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLTestFramework.Framework
{
    /// <summary>
    /// Validates test case execution results
    /// </summary>
    class ResultValidator: IResultValidator
    {
        /// <summary>
        /// Get a list of failed tests and a list of generated tests
        /// </summary>
        /// <param name="tests">A list of all test cases</param>
        /// <returns>A list of failed tests, and a list of generated tests</returns>
        public Tuple<List<SQLTestCase>, List<SQLTestCase>> EvaluateTests(List<SQLTestCase> tests)
        {
            List<SQLTestCase> failedTests = new List<SQLTestCase>();
            List<SQLTestCase> generatedTests = new List<SQLTestCase>();

            foreach (SQLTestCase test in tests)
            {
                switch (test.EvaluateResults())
                {
                    case SQLTestCase.TestResult.Failed:
                        failedTests.Add(test);
                        break;
                    case SQLTestCase.TestResult.Generated:
                        generatedTests.Add(test);
                        break;
                    case SQLTestCase.TestResult.Passed:
                        break;
                    default:
                        throw new ArgumentOutOfRangeException("Undefined test case result");
                }
            }
            return new Tuple<List<SQLTestCase>,List<SQLTestCase>>(failedTests, generatedTests);
        }
    }
}
