using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLTestFramework.Framework
{
    /// <summary>
    /// Interface to be implemented by components reading test cases from user input
    /// </summary>
    public interface IInputHandler
    {
        /// <summary>
        /// Read comments and test cases from file 
        /// </summary>
        /// <returns>A tuple where the first element is a list of comments (<int,string> tuples)
        /// and the second element is a list of SQLTestCases</returns>
        Tuple<List<Tuple<int,string>>,List<SQLTestCase>> ReadTests(String filename);
    }
}
