using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLTestFramework.Framework
{
    /// <summary>
    /// Input handler implementation that reads all test cases from .txt files
    /// </summary>
    class FileReader: IInputHandler
    {
        /// <summary>
        /// STUB!!!
        /// Reads and instantiate an SQLTestCase for every test case in the file filename.
        /// </summary>
        /// <param name="filename">The file to read test from</param>
        /// <returns>A list of SQLTestCase representing every test in the input file</returns>
        public List<ISQLTestCase> ReadTests(string filename)
        {
            List<ISQLTestCase> inputList = new List<ISQLTestCase>();

            // TODO: Factory should instantiate the correct class implementing SQLTestCase
            SQLQuery query1 = new SQLQuery();
            query1.Description = "Test 1";
            query1.Statement = "SELECT * FROM Person p ORDER BY Name DESC";
            query1.usesOrderBy = true;
            query1.ExpectedResults = 
                "| 0:String | 1:UInt64 | 2:String |" + Environment.NewLine + 
                "| Einstein | 988 | Pc |" + Environment.NewLine + 
                "| Albert | 987 | Pb |";
            query1.ExpectedExecutionPlan = "Tables(" +
                "0 = SQLTestFramework.Framework.Person" +
                ")" +
                "Projection(" +
                "0 = " +
                "StringProperty(0, Name)" +
                "1 = " +
                "ObjectNoProperty(0, ObjectNo)" +
                "2 = " +
                "ObjectIDProperty(0, ObjectID)" +
                ")" +
                "Sort(" +
                "IndexScan(" +
                "auto ON SQLTestFramework.Framework.Person" +
                "0" +
                "__id" +
                "UIntegerDynamicRange(" +
                ")" +
                "LogicalValue(TRUE)" +
                "Ascending )" +
                "StringComparer(" +
                "StringProperty(0, Name)" +
                "Descending" +
                ")" +
                ")";

            SQLQuery query2 = new SQLQuery();
            query2.Description = "Test 2";
            query2.Statement = "SELECT * FROM Company c ORDER BY CompanyName DESC";
            query2.usesOrderBy = true;
            query2.ExpectedResults = 
                "| 0:String | 1:UInt64 | 2:String |" + Environment.NewLine +
                "| Starcounter | 991 | Pf |" + Environment.NewLine + 
                "| Siba | 992 | Pg |";
            query2.ExpectedExecutionPlan = "Tables(" +
                "0 = SQLTestFramework.Framework.Company" +
                ")" +
                "Projection(" +
                "0 = " +
                "StringProperty(0, CompanyName)" +
                "1 = " +
                "ObjectNoProperty(0, ObjectNo)" +
                "2 = " +
                "ObjectIDProperty(0, ObjectID)" +
                ")" +
                "Sort(" +
                "IndexScan(" +
                "auto ON SQLTestFramework.Framework.Company" +
                "0" +
                "__id" +
                "UIntegerDynamicRange(" +
                ")" +
                "LogicalValue(TRUE)" +
                "Ascending" +
                ")" +
                "StringComparer(" +
                "StringProperty(0, CompanyName)" +
                "Descending" +
                ")" +
                ")";

            SQLQuery query3 = new SQLQuery();
            query3.Description = "Test 3";
            query3.Statement = "SELECT * FROM Location l";
            query3.usesOrderBy = false;
            query3.ExpectedResults = 
                "| 0:String | 1:UInt64 | 2:String |" + Environment.NewLine +
                "| Norway | 990 | Pe |" + Environment.NewLine + 
                "| Sweden | 989 | Pd |";
            query3.ExpectedExecutionPlan = "Tables(" +
                "0 = SQLTestFramework.Framework.Location" +
                ")" +
                "Projection(" +
                "0 = " +
                "StringProperty(0, Country)" +
                "1 = " +
                "ObjectNoProperty(0, ObjectNo)" +
                "2 = " +
                "ObjectIDProperty(0, ObjectID)" +
                ")" +
                "IndexScan(" +
                "auto ON SQLTestFramework.Framework.Location" +
                "0" +
                "__id" +
                "UIntegerDynamicRange(" +
                ")" +
                "LogicalValue(TRUE)" +
                "Ascending" +
                ")";


            inputList.Add(query1);
            inputList.Add(query2);
            inputList.Add(query3);

            return inputList;
        }
    }
}
