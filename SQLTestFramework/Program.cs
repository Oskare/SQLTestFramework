using System;
using Starcounter;
using SQLTestFramework.Framework;

namespace SQLTestFramework
{
    class Program
    {
        static void Main()
        {
            TestRunner.Initialize(new FileReader(), new TestExecutor(), new TestValidator(), new FileWriter());
            TestRunner.RunTest("testfile");
        }
    }
}