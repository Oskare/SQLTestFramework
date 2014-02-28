using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLTestFramework.Framework
{
    public static class TestRunner
    {
        static List<SQLTestCase> testList = null;

        static IInputHandler inputHandler = null;
        static ITestExecutor testExecutor = null;
        static ITestValidator testValidator = null;
        static IOutputHandler outputHandler = null;

        public static void Initialize(IInputHandler input, ITestExecutor executor, 
            ITestValidator validator, IOutputHandler output)
        {
            inputHandler = input;
            testExecutor = executor;
            testValidator = validator;
            outputHandler = output;
        }

        public static void RunTest(String filename)
        {
            // Read tests from input
            testList = inputHandler.ReadTests(filename);

            // Run tests in parallel
            testExecutor.ExecutePar(testList);

            // Validate results
            testValidator.EvaluateTests(testList);

            // Output
            outputHandler.Output(testList);
        }

    }
}
