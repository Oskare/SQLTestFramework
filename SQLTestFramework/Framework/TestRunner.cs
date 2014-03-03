using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLTestFramework.Framework
{
    public static class TestRunner
    {
        private static List<SQLTestCase> testList;

        private static IInputHandler inputHandler;
        private static ITestExecutor testExecutor;
        private static IResultValidator resultValidator;
        private static IOutputHandler outputHandler;

        /// <summary>
        /// Initialize the system by storing the objects to use for different parts of the system.
        /// </summary>
        /// <param name="input">An object implementing the input handler system component.</param>
        /// <param name="executor">An object implementing the test executor system component.</param>
        /// <param name="validator">An object implementing the result validator system component.</param>
        /// <param name="output">An object implementing the output handler system component.</param>
        public static void Initialize(IInputHandler input, ITestExecutor executor, 
            IResultValidator validator, IOutputHandler output)
        {
            inputHandler = input;
            testExecutor = executor;
            resultValidator = validator;
            outputHandler = output;
        }

        public static void RunTest(String filename)
        {
            // TODO: Null check

            // Read tests from input
            testList = inputHandler.ReadTests(filename);

            // Run tests in parallel
            testExecutor.ExecuteSeq(testList);

            // Check results
            int failedTests = resultValidator.EvaluateTests(testList);

            // Output
            outputHandler.Output(testList, failedTests);
        }

    }
}
