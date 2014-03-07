using System;
using SQLTestFramework.Framework;

namespace SQLTestFramework
{
    class Program
    {
        /// <summary>
        /// Initialize the framework with some components and do a test run
        /// </summary>
        static void Main()
        {
            // (Re)populate the test database
            // TestPopulation.ClearTestData();
            // TestPopulation.PopulateTestData();

            TestRunner.Initialize(input: new FileReader(), output: new FileWriter());
            TestRunner.RunTest("testfile.txt");
        }
    }
}