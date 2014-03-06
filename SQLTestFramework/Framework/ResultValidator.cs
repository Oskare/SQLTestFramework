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
        public Tuple<List<ISQLTestCase>, List<ISQLTestCase>> EvaluateTests(List<ISQLTestCase> tests)
        {
            List<ISQLTestCase> failedTests = new List<ISQLTestCase>();
            List<ISQLTestCase> generatedTests = new List<ISQLTestCase>();

            foreach (ISQLTestCase test in tests)
            {
                switch (test.EvaluateResults())
                {
                    case ISQLTestCase.TestResult.Failed:
                        failedTests.Add(test);
                        break;
                    case ISQLTestCase.TestResult.Generated:
                        generatedTests.Add(test);
                        break;
                }
            }
            return new Tuple<List<ISQLTestCase>,List<ISQLTestCase>>(failedTests, generatedTests);
        }
    }
}
