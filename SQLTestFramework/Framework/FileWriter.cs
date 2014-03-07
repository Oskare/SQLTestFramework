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
        /// Writes a test run result summary and failed and generated tests to file.
        /// </summary>
        /// <param name="tests">List of all run tests</param>
        /// <param name="failedTests">List of all failed tests</param>
        /// <param name="generatedTests">List of all generated tests</param>
        public void Output(List<SQLTestCase> tests, List<SQLTestCase> failedTests, List<SQLTestCase> generatedTests)
        {
            StringBuilder output = new StringBuilder();

            output.AppendLine(Environment.NewLine + "OUTPUT:" + Environment.NewLine);
            output.AppendLine("FAILED TEST CASES: ");
            foreach (SQLTestCase test in failedTests)
                output.AppendLine(test.ToString());

            output.AppendLine(Environment.NewLine + " GENERATED TEST CASES: ");
            foreach (SQLTestCase test in generatedTests)
                output.AppendLine(test.ToString());

            int total = tests.Count;
            int failed = failedTests.Count;
            int generated = generatedTests.Count;
            int passed = total - failed - generated;

            output.AppendLine(Environment.NewLine + total + " test cases executed: " + Environment.NewLine +
                passed + " test cases passed, " + Environment.NewLine +
                failed + " test cases failed, " + Environment.NewLine + 
                generated + " test case results generated");

            Console.Write(output.ToString());
        }
    }
}
