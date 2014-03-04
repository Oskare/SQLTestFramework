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
        /// STUB! (Prints all failed tests + summary)
        /// Writes a test run result summary and failed tests to file.
        /// </summary>
        /// <param name="tests">List of all tests run</param>
        /// <param name="failedTests">List of all failed tests</param>
        public void Output(List<SQLTestCase> tests, List<SQLTestCase> failedTests)
        {
            Console.WriteLine(Environment.NewLine + "OUTPUT:");

            foreach (SQLTestCase test in failedTests)
            {
                Console.WriteLine(test);
            }

            if (failedTests.Count > 0)
            {
                Console.WriteLine(failedTests.Count + " test case(s) failed");
            }
            else
            {
                Console.WriteLine("All tests passed");
            }
        }
    }
}
