﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLTestFramework.Framework
{
    /// <summary>
    /// Interface to be implemented by component handling test run output
    /// </summary>
    public interface IOutputHandler
    {
        void Output(List<SQLTestCase> tests, List<SQLTestCase> failedTests, 
            List<SQLTestCase> generatedTests, List<Tuple<int,string>> comments);
    }
}
