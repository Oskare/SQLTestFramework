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
        private static List<SQLTestCase> failedTests;
        private static List<SQLTestCase> generatedTests;
        private static Tuple<List<SQLTestCase>, List<SQLTestCase>> validationResults;

        private static IInputHandler inputHandler;
        private static ITestExecutor testExecutor;
        private static IResultValidator resultValidator;
        private static IOutputHandler outputHandler;

        // TODO: Add proper logging
        public static void Log(String text)
        {
            Console.WriteLine("TestRunner: " + text);
        }

        /// <summary>
        /// Initialize the system by storing the objects to use for different parts of the system.
        /// </summary>
        /// <param name="input">An object implementing the input handler system component.</param>
        /// <param name="executor">An object implementing the test executor system component.</param>
        /// <param name="validator">An object implementing the result validator system component.</param>
        /// <param name="output">An object implementing the output handler system component.</param>
        public static void Initialize(IInputHandler input = null, ITestExecutor executor = null, 
            IResultValidator validator = null, IOutputHandler output = null)
        {
            inputHandler = input;
            testExecutor = executor;
            resultValidator = validator;
            outputHandler = output;
        }

        /// <summary>
        /// Replace any null components with the standard components
        /// </summary>
        public static void ComponentNullCheck()
        {
            if (inputHandler == null)
            {
                inputHandler = new FileReader();
                Log("No InputHandler supplied, using standard implementation");
            }

            if (testExecutor == null)
            {
                testExecutor = new TestExecutor();
                Log("No TestExecutor supplied, using standard implementation");
            }

            if (resultValidator == null)
            {
                resultValidator = new ResultValidator();
                Log("No ResultValidator supplied, using standard implementation");
            }

            if (outputHandler == null)
            {
                outputHandler = new FileWriter();
                Log("No OutputHandler supplied, using standard implementation");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filename"></param>
        public static void RunTest(String filename)
        {
            ComponentNullCheck();

            // Read tests from input
            Log("Reading tests from input");
            testList = inputHandler.ReadTests(filename);
            Log("Read " + testList.Count + " tests");

            // Run tests in parallel
            Log("Executing tests");
            testExecutor.ExecuteSeq(testList);
            //testExecutor.ExecutePar(testList);
            Log("Test execution finished");

            // Get failed/generated tests
            Log("Validating tests");
            validationResults = resultValidator.EvaluateTests(testList);
            failedTests = validationResults.Item1;
            generatedTests = validationResults.Item2;
            Log("Test validation finished");

            // Output
            Log("Writing output");
            outputHandler.Output(testList, failedTests, generatedTests);
            Log("Test run finished");
        }
    }
}
