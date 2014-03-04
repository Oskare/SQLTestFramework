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
        public String Description { get; set; }
        public String Statement { get; set; }
        public Object[] VariableValues { get; set; }
        public Boolean DataManipulation { get; set; }
        public String ExpectedResults { get; set; }

        public List<String> ActualResults { get; set; }

        public Boolean Passed { get; set; }
        // ...

        public abstract Boolean EvaluateResults();

        public abstract void Execute();
    }
}
