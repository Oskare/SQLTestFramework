using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace SQLTestFramework.Framework
{
    class InternalParameterHandler : IInternalParameterHandler
    {
        private const String fileSuffix = "_param.bin";

        /// <summary>
        /// The formatter used to serialize the TestCaseParameters
        /// </summary>
        private IFormatter formatter = new BinaryFormatter();

        public Dictionary<int, TestCaseParameters> loadedParameters = new Dictionary<int, TestCaseParameters>();
        private string parameterFile;

        /// <summary>
        /// Read serialized parameter objects from file and store them in corresponding test objects.
        /// If no matching parameter objects is found for a test case object, a new parameter object is created
        /// </summary>
        /// <param name="filename">The file containing the tests to read parameters to</param>
        /// <param name="testList">The list of all test read from filename</param>
        public void LoadParameters(string filename, List<SQLTestCase> testList)
        {
            Stream stream = null;
            loadedParameters = new Dictionary<int, TestCaseParameters>();

            try
            {
                parameterFile = filename.Substring(0, filename.IndexOf(".txt")) + fileSuffix;
                stream = new FileStream(parameterFile, FileMode.Open, FileAccess.Read, FileShare.Read);
                loadedParameters = (Dictionary<int, TestCaseParameters>) formatter.Deserialize(stream);
                Console.WriteLine("InternalParameterHandler: Loaded parameters");
                stream.Close();

                foreach (SQLTestCase test in testList)
                {
                    try
                    {
                        test.InternalParam = loadedParameters[test.Identifier];
                    }
                    catch (KeyNotFoundException ex) // No parameters exists for a test case, indicative of a new test case
                    {
                        createParameters(test.Identifier, test);
                        continue;
                    }
                }
            }
            catch (FileNotFoundException ex) // No parameter file exists, indicative of first test run
            {
                foreach (SQLTestCase test in testList)
                    createParameters(test.Identifier, test);
            }
            finally
            {
                if (stream != null)
                    stream.Close();
            } 
        }

        /// <summary>
        /// Creates a new parameter object and add it to the corresponding test case object
        /// </summary>
        /// <param name="identifier">The test object identifier to be used for the parameter object</param>
        /// <param name="test">The test object to assign the new parameter object to</param>
        private void createParameters(int identifier, SQLTestCase test)
        {
            TestCaseParameters newParam = new TestCaseParameters(identifier);
            loadedParameters.Add(identifier, newParam);
            test.InternalParam = newParam;
            Console.WriteLine("InternalParameterHandler: Created new parameter with id: " + identifier);
        }

        /// <summary>
        /// Serialize all loaded parameters to file. 
        /// This will also store any changes made to the parameters elsewhere in the framework
        /// </summary>
        public void StoreParameters()
        {    
            Stream stream = new FileStream(parameterFile, FileMode.Create, FileAccess.Write, FileShare.None);
            formatter.Serialize(stream, loadedParameters);
            Console.WriteLine("InternalParameterHandler: Wrote parameters");
            stream.Close();

            parameterFile = null;
            loadedParameters = null;
        }
    }
}
