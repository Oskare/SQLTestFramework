using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLTestFramework.Framework
{
    /// <summary>
    /// Output implementation which writes all output to .txt files
    /// </summary>
    class FileWriter: IOutputHandler
    {
        /// <summary>
        /// STUB! (Prints all tests + summary)
        /// Writes a test run result summary and failed tests to file.
        /// </summary>
        /// <param name="tests">List of all tests run</param>
        /// <param name="failedTests">Number of failed tests</param>
        public void Output(List<SQLTestCase> tests, int failedTests)
        {
            Console.WriteLine(Environment.NewLine + "OUTPUT:");

            if (failedTests > 0)
            {
                foreach (SQLTestCase test in tests)
                {
                    if (!test.Passed)
                    {
                        Console.WriteLine(test);
                    }
                }
                Console.WriteLine(failedTests + " test case(s) failed");
            }
            else
            {
                Console.WriteLine("All tests passed");
            }
        }
    }
}
