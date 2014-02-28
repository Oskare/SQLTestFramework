using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLTestFramework.Framework
{
    /// <summary>
    /// Abstract class to be inherited by all different test case classes
    /// </summary>
    public abstract class SQLTestCase
    {
        /// <summary>
        /// Test case statement
        /// </summary>
        private String statement = null;

        /// <summary>
        /// Description of test case
        /// </summary>
        private String description = null;

        /// <summary>
        /// Expected result of statement execution
        /// </summary>
        private String expectedResults = null;

        // ...

        public abstract Boolean EvaluateResults();
    }
}
