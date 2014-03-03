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

            TestRunner.Initialize(new FileReader(), new TestExecutor(), new ResultValidator(), new FileWriter());
            TestRunner.RunTest("testfile");
        }
    }
}