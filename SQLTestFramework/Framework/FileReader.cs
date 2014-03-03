﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLTestFramework.Framework
{
    /// <summary>
    /// Input implementation which reads all test cases from .txt files
    /// </summary>
    class FileReader: IInputHandler
    {
        /// <summary>
        /// STUB!!!
        /// Reads and instantiate an SQLTestCase for every test case in the file filename.
        /// </summary>
        /// <param name="filename">The file to read test from</param>
        /// <returns>A list of SQLTestCase representing every test in the input file</returns>
        public List<SQLTestCase> ReadTests(string filename)
        {
            List<SQLTestCase> inputList = new List<SQLTestCase>();

            // TODO: Factory should instantiate the correct class implementing SQLTestCase
            SQLTestCase query1 = new SQLQuery();
            query1.description = "Test 1";
            query1.statement = "SELECT * FROM Person p";
            query1.expectedResults = 
                "| 0:String | 1:UInt64 | 2:String |" + Environment.NewLine +
                "| Albert | 987 | Pb |" + Environment.NewLine + 
                "| Einstein | 988 | Pc |";

            SQLTestCase query2 = new SQLQuery();
            query2.description = "Test 2";
            query2.statement = "SELECT * FROM Company c";
            query2.expectedResults = 
                "| 0:String | 1:UInt64 | 2:String |" + Environment.NewLine +
                "| Starcounter | 991 | Pf |" + Environment.NewLine + 
                "| Siba | 992 | Pg |";

            SQLTestCase query3 = new SQLQuery();
            query3.description = "Test 3";
            query3.statement = "SELECT * FROM Location l";
            query3.expectedResults = 
                "| 0:String | 1:UInt64 | 2:String |" + Environment.NewLine +
                "| Sweden | 989 | Pd |" + Environment.NewLine + 
                "| Norway | 990 | Pe |";

            inputList.Add(query1);
            inputList.Add(query2);
            inputList.Add(query3);

            return inputList;
        }
    }
}