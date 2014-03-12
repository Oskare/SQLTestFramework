using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLTestFramework.Framework
{
    /// <summary>
    /// Abstract class to be inherited by all test case classes
    /// </summary>
    public abstract class SQLTestCase
    {
        /// <summary>
        /// The possible test case outcomes
        /// </summary>
        public enum TestResult { Passed, Failed, Generated };

        public int Identifier { get; set; }
        public TestCaseParameters InternalParam { get; set; }

        // Test case information read from input
        public String Description { get; set; }
        public String Statement { get; set; }
        public Object[] Values { get; set; }
        public Boolean DataManipulation { get; set; }
        public String ExpectedResults { get; set; }
        public String ExpectedException { get; set; }
        public TestResult Result { get; set; }

        // Test case information gathered from execution
        protected List<String> ActualResults;
        protected List<String> ActualException;
        // protected List<String> ActualFullExceptions;

        public abstract TestResult EvaluateResults();
        public abstract void Execute();
    }
}
