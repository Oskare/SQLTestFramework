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
            List<ISQLTestCase> queryList = new List<ISQLTestCase>();

            SQLQuery query1 = new SQLQuery();
            query1.Description = "Test 1";
            query1.Statement = "SELECT * FROM Person p ORDER BY Name DESC";
            query1.UsesOrderBy = true;
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
            query2.UsesOrderBy = true;
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
            query3.UsesOrderBy = false;
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

            SQLQuery query4 = new SQLQuery();
            query4.Description = "Test 4";
            query4.Statement = "SELECT * FROM Person p WHERE Name=?";
            query4.VariableValues = new Object[] { "Albert" };
            query4.UsesOrderBy = false;
            query4.ExpectedResults =
                "| 0:String | 1:UInt64 | 2:String |" +
                "| Albert | 987 | Pb |";
            query4.ExpectedExecutionPlan = "Tables(" +
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
                "FullTableScan(" +
                "auto ON SQLTestFramework.Framework.Person" +
                "0" +
                "ComparisonString(" +
                "Equal" +
                "StringProperty(0, Name)" +
                "StringVariable(Albert)" +
                ")" +
                "Ascending" +
                ")";

            SQLQuery query5 = new SQLQuery();
            query5.Description = "Test 5";
            query5.Statement = "SELECT Name, ObjectID FROM Person p WHERE Name=?";
            query5.VariableValues = new Object[] { "Albert" };
            query5.UsesOrderBy = false;
            query5.ExpectedResults =
                "| 0:String | 1:String |" +
                "| Albert | Pb |";
            query5.ExpectedExecutionPlan = "Tables(" +
                "0 = SQLTestFramework.Framework.Person" +
                ")" +
                "Projection(" +
                "0 = " +
                "StringProperty(0, Name)" +
                "1 = " +
                "ObjectIDProperty(0, ObjectID)" +
                ")" +
                "FullTableScan(" +
                "auto ON SQLTestFramework.Framework.Person" +
                "0" +
                "ComparisonString(" +
                "Equal" +
                "StringProperty(0, Name)" +
                "StringVariable(Albert)" +
                ")" +
                "Ascending" +
                ")";

            SQLQuery query6 = new SQLQuery();
            query6.Description = "Test 6";
            query6.Statement = "SELECT Name FROM Person p WHERE Name=?";
            query6.VariableValues = new Object[] { "Albert" };
            query6.UsesOrderBy = false;
            query6.ExpectedResults =
                "| String |" + Environment.NewLine +
                "| Albert |";
            query6.ExpectedExecutionPlan = "Tables(" +
                "0 = SQLTestFramework.Framework.Person" +
                ")" +
                "Projection(" +
                "0 = " +
                "StringProperty(0, Name)" +
                ")" +
                "FullTableScan(" +
                "auto ON SQLTestFramework.Framework.Person" +
                "0" +
                "ComparisonString(" +
                "Equal" +
                "StringProperty(0, Name)" +
                "StringVariable(Albert)" +
                ")" +
                "Ascending" +
                ")";

            SQLQuery query7 = new SQLQuery();
            query7.Description = "Test 7";
            query7.Statement = "SELECT Name FROM Person p";
            query7.UsesOrderBy = false;
            query7.ExpectedResults =
                "| String |" + Environment.NewLine +
                "| Albert |" + Environment.NewLine +
                "| Einstein |";
            query7.ExpectedExecutionPlan = "Tables(" +
                "0 = SQLTestFramework.Framework.Person" +
                ")" +
                "Projection(" +
                "0 = " +
                "StringProperty(0, Name)" +
                ")" +
                "IndexScan(" +
                "auto ON SQLTestFramework.Framework.Person" +
                "0" +
                "__id" +
                "UIntegerDynamicRange(" +
                ")" +
                "LogicalValue(TRUE)" +
                "Ascending" +
                ")";

            SQLQuery query8 = new SQLQuery();
            query8.Description = "Test 8";
            query8.Statement = "SELECT Name FROM Person p ORDER BY Name DESC";
            query8.UsesOrderBy = true;
            query8.ExpectedResults =
                "| String |" + Environment.NewLine +
                "| Einstein |" + Environment.NewLine +
                "| Albert |";
            query8.ExpectedExecutionPlan = "Tables(" +
                "0 = SQLTestFramework.Framework.Person" +
                ")" +
                "Projection(" +
                "0 = " +
                "StringProperty(0, Name)" +
                ")" +
                "Sort(" +
                "IndexScan(" +
                "auto ON SQLTestFramework.Framework.Person" +
                "0" +
                "__id" +
                "UIntegerDynamicRange(" +
                ")" +
                "LogicalValue(TRUE)" +
                "Ascending" +
                ")" +
                "StringComparer(" +
                "StringProperty(0, Name)" +
                "Descending" +
                ")" +
                ")";

            SQLQuery query9 = new SQLQuery();
            query9.Description = "Test 9";
            query9.Statement = "SELECT * FROM Person p";
            query9.UsesOrderBy = false;
            query9.ExpectedResults = "GENERATE ";
            query9.ExpectedExecutionPlan = " GENERATE";

            SQLQuery query10 = new SQLQuery();
            query10.Description = "Test 10";
            query10.Statement = "SELECT p FROM Person p where p.Name >= object 15";
            query10.VariableValues = new Object[0];
            query10.UsesOrderBy = false;
            query10.ExpectedException = "Failed to process query: SELECT p FROM Person p where p.Name >= object 15: Incorrect arguments of types string and object(unknown) to operator greaterThanOrEqual.";

            SQLQuery query11= new SQLQuery();
            query11.Description = "Test 11";
            query11.Statement = "SELECT p FROM Person p where p.Name >= object 15";
            query11.VariableValues = new Object[0];
            query11.UsesOrderBy = false;
            query11.ExpectedException = "GENERATE";

            SQLQuery query12 = new SQLQuery();
            query12.Description = "Test 12";
            query12.Statement = "SELECT * FROM Person p ORDER BY Name DESC";
            query12.UsesOrderBy = true;
            query12.UsesBisonParser = false; // This should be read from the internally stored parameters when that functionality is implemented.
            query12.ExpectedResults =
                "| 0:String | 1:UInt64 | 2:String |" + Environment.NewLine +
                "| Einstein | 988 | Pc |" + Environment.NewLine +
                "| Albert | 987 | Pb |";
            query12.ExpectedExecutionPlan = "Tables(" +
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

            queryList.Add(query1);
            queryList.Add(query2);
            queryList.Add(query3);
            queryList.Add(query4);
            queryList.Add(query5);
            queryList.Add(query6);
            queryList.Add(query7);
            queryList.Add(query8);
            queryList.Add(query9);
            queryList.Add(query10);
            queryList.Add(query11);
            queryList.Add(query12);

            return queryList;
        }
    }
}
