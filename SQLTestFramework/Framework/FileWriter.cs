using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLTestFramework.Framework
{
    /// <summary>
    /// Output handler implementation that writes all output to .txt files
    /// </summary>
    class FileWriter: IOutputHandler
    {
        /// <summary>
        /// STUB!!! (Prints all failed tests + summary)
        /// Writes a test run result summary and failed tests to file.
        /// </summary>
        /// <param name="tests">List of all tests run</param>
        /// <param name="failedTests">List of all failed tests</param>
        public void Output(List<ISQLTestCase> tests, List<ISQLTestCase> failedTests, List<ISQLTestCase> generatedTests)
        {
            Console.WriteLine(Environment.NewLine + "OUTPUT:");

            foreach (ISQLTestCase test in failedTests)
            {
                Console.WriteLine(test);
            }
            Console.WriteLine("");

            foreach (ISQLTestCase test in generatedTests)
            {
                Console.WriteLine(test);
            }

            int total = tests.Count;
            int failed = failedTests.Count;
            int generated = generatedTests.Count;
            int passed = total - failed - generated;

            Console.WriteLine(total + " test cases executed: " + Environment.NewLine +
                passed + " test cases passed, " + Environment.NewLine +
                failed + " test cases failed, " + Environment.NewLine + 
                generated + " test case results generated");
        }
    }
}
