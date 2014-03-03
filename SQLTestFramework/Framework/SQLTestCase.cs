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
        public String description { get; set; }
        public String statement { get; set; }
        public Object[] variableValues { get; set; }
        public Boolean dataManipulation { get; set; }
        public String expectedResults { get; set; }

        public String actualResults1 { get; set; }
        public String actualResults2 { get; set; }
        // ...

        public abstract Boolean EvaluateResults();

        public abstract void Execute(Boolean firstExecution);
    }
}
