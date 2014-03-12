using System;
using SQLTestFramework.Framework;

namespace SQLTestFramework
{
    class Program
    {

        enum DataProperties { INDICES = 0, 
                                     NO_INDICES = 1, 
                                     OTHER_PROPERTIES = 2};

        /// <summary>
        /// Initialize the framework with some components and do a test run
        /// </summary>
        static void Main()
        {
            // (Re)populate the test database
            // TestPopulation.ClearTestData();
            // TestPopulation.PopulateTestData();

            // Initialize components
            TestRunner.Initialize(input: new FileReader(), output: new FileWriter());

            // Do test run with small database without indices
            TestRunner.ChecksumVerification = false; // For big database test run. Can be assigned automatically by examining 
            TestRunner.RunTest(
                "C:\\Users\\starcounter\\Documents\\GitHub\\SQLTestFramework\\SQLTestFramework\\TestFile1.txt",
                (int)DataProperties.NO_INDICES);

            // Do test run with big database with indices
            /*TestRunner.ChecksumVerification = true; // For big database test run. Can be assigned automatically by examining db
            TestRunner.RunTest(
                "C:\\Users\\starcounter\\Documents\\GitHub\\SQLTestFramework\\SQLTestFramework\\TestFile1.txt",
                (int)DataProperties.INDICES);
            */
            
        }
    }
}