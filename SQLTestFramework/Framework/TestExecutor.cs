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
        const int ExecutionIterations = 2;

        /// <summary>
        /// Execute tests in parallel and store the results
        /// </summary>
        /// <param name="tests">List of tests to be executed.</param>
        public void ExecutePar(List<ISQLTestCase> tests)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Execute tests sequentially and store the results.
        /// </summary>
        /// <param name="tests">List of tests to be executed.</param>
        public void ExecuteSeq(List<ISQLTestCase> tests)
        {
            Db.Transaction(delegate
            {
                // temporary loggging
                for (int i = 0; i < ExecutionIterations; i++)
                {
                    Console.WriteLine("Starting execution run " + i);
                    foreach (ISQLTestCase test in tests){
                        test.Execute();
                    }
                    Console.WriteLine("Execution run " + i + " finished");
                }
            });
        }
    }
}
