using System;
using SQLTestFramework.Framework;

namespace SQLTestFramework
{
    class Program
    {
        static void Main()
        {
            //TestPopulation.Clear();
            //TestPopulation.Populate();

            TestRunner.Initialize(input: new FileReader(), output: new FileWriter());
            TestRunner.RunTest("testfile");
        }
    }
}