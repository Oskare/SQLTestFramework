using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Starcounter;

namespace SQLTestFramework.Framework
{
    /// <summary>
    /// Execute test cases and store results
    /// </summary>
    // TODO: Read/Write internal parameters + parameter validator
    class TestExecutor: ITestExecutor
    {
        /// <summary>
        /// The number of executions to do for each test case
        /// </summary>
        const int ExecutionIterations = 2;

        /// <summary>
        /// Execute tests in parallel and store the results
        /// </summary>
        /// <param name="tests">List of tests to be executed.</param>
        public void ExecutePar(List<SQLTestCase> tests)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Execute tests sequentially and store the results.
        /// </summary>
        /// <param name="tests">List of tests to be executed.</param>
        public void ExecuteSeq(List<SQLTestCase> tests)
        {
            Db.Transaction(delegate
            {
                for (int i = 0; i < ExecutionIterations; i++)
                {
                    Console.WriteLine("TestExecutor: Execution run " + i + " starting");
                    foreach (SQLTestCase test in tests){
                        // TODO: Fetch internal parameters and add to test case object
                        test.Execute();
                    }
                    Console.WriteLine("TestExecutor: Execution run " + i + " finished");
                }
            });
        }
    }
}
