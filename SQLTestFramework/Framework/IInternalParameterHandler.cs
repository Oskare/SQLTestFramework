using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLTestFramework.Framework
{
    /// <summary>
    /// Interface to be implemented by component handling loading and storing of internal parameters
    /// </summary>
    public interface IInternalParameterHandler
    {
        /// <summary>
        /// Read serialized parameter objects from file and store them in corresponding test objects.
        /// If no matching parameter objects is found for a test case object, a new parameter object should be created
        /// </summary>
        /// <param name="filename">The file containing the tests to read parameters to</param>
        /// <param name="testList">The list of all test read from filename</param>
        void LoadParameters(string filename, List<SQLTestCase> testList);

        /// <summary>
        /// Serialize all loaded parameters to file. 
        /// </summary>
        void StoreParameters();
    }
}
