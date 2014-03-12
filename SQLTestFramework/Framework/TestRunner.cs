using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLTestFramework.Framework
{
    public static class TestRunner
    {
        /// <summary>
        /// Indicates if test results verification should use string comparison or checksum value comparison
        /// </summary>
        public static Boolean ChecksumVerification { get; set; }

        /// <summary>
        /// An integer used to identify test runs on different database state. Used to match tests to different result sets.
        /// </summary>
        public static int DatabaseState { get; set; }

        /// <summary>
        /// 
        /// </summary>
        private static String inputFilePath = null;

        /// <summary>
        /// List of all test cases to be executed and evaluated
        /// </summary>
        private static List<SQLTestCase> testList;

        /// <summary>
        /// List of all failed tests
        /// </summary>
        private static List<SQLTestCase> failedTests;

        /// <summary>
        /// List of tests without expected results
        /// </summary>
        private static List<SQLTestCase> generatedTests;

        private static IInputHandler inputHandler;
        private static IInternalParameterHandler parameterHandler;
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
        public static void Initialize(IInputHandler input = null, IInternalParameterHandler parameter = null, 
            ITestExecutor executor = null, IResultValidator validator = null, IOutputHandler output = null)
        {
            inputHandler = input;
            parameterHandler = parameter;
            testExecutor = executor;
            resultValidator = validator;
            outputHandler = output;

            ChecksumVerification = false;
        }

        /// <summary>
        /// Replace any undefined components with the standard component implementations
        /// </summary>
        public static void ComponentNullCheck()
        {
            if (inputHandler == null)
            {
                inputHandler = new FileReader();
                Log("No InputHandler supplied, using standard implementation");
            }

            if (parameterHandler == null)
            {
                parameterHandler = new InternalParameterHandler();
                Log("No ParameterHandler supplied, using standard implementation");
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
        /// Read, execute, evaluate and output results of the execution of all test cases. 
        /// Calls inputHandler, TestExecutor, ResultValidator and OutputHandler components.
        /// </summary>
        /// <param name="filename">The file to read test cases from</param>
        public static void RunTest(String filename, int state)
        {
            ComponentNullCheck();

            DatabaseState = state;

            // Read tests from input
            Log("Reading tests from input");
            testList = inputHandler.ReadTests(filename); // Perhaps use out parameter to count read tests
            Log("Read " + testList.Count + " tests");

            // Fetch internal parameters
            parameterHandler.LoadParameters(filename, testList);

            // Run tests in parallel
            Log("Executing tests");
            testExecutor.ExecuteSeq(testList);
            //testExecutor.ExecutePar(testList);
            Log("Test execution finished");

            // Get failed/generated tests
            Log("Validating tests");
            Tuple<List<SQLTestCase>, List<SQLTestCase>> validationResults = resultValidator.EvaluateTests(testList);
            failedTests = validationResults.Item1;
            generatedTests = validationResults.Item2;
            Log("Test validation finished");

            // Store iternal parameters
            parameterHandler.StoreParameters();

            // Output
            Log("Writing output");
            outputHandler.Output(testList, failedTests, generatedTests);
            Log("Test run finished");
        }
    }
}
