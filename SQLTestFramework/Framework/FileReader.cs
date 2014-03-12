using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLTestFramework.Framework
{
    /// <summary>
    /// Input handler implementation that reads test cases from .txt file(s)
    /// </summary>
    class FileReader: IInputHandler
    {
        /// <summary>
        /// STUB!!!
        /// Reads and instantiate an SQLTestCase for every test case in the file filename.
        /// </summary>
        /// <param name="filename">The file to read test from</param>
        /// <returns>A list of SQLTestCase representing every test in the input file</returns>
        public Tuple<List<Tuple<int, string>>, List<SQLTestCase>> ReadTests(string file)
        {
            return stub();
        }

        private Tuple<List<Tuple<int, string>>, List<SQLTestCase>> stub()
        {
            List<SQLTestCase> queryList = new List<SQLTestCase>();
            List<Tuple<int, string>> comments = new List<Tuple<int, string>>();

            SQLQuery query1 = new SQLQuery();
            query1.Identifier = 1;
            query1.Description = "Test 1";
            query1.Statement = "SELECT * FROM Person p ORDER BY Name DESC";
            query1.ExpectedResults =
                "| 0:String | 1:UInt64 | 2:String |" + Environment.NewLine +
                "| Einstein | 1006 | Pu |" + Environment.NewLine +
                "| Albert | 1005 | Pt |";
            query1.ExpectedExecutionPlan = "Tables(" +
                "0 = SQLTestFramework.Person" +
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
                "auto ON SQLTestFramework.Person" +
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

            SQLQuery query2 = new SQLQuery();
            query2.Identifier = 2;
            query2.Description = "Test 2";
            query2.Statement = "SELECT * FROM Company c ORDER BY CompanyName DESC";
            query2.ExpectedResults =
                "| 0:String | 1:UInt64 | 2:String |" + Environment.NewLine +
                "| Starcounter | 1009 | Px |" + Environment.NewLine +
                "| Siba | 1010 | Py |";
            query2.ExpectedExecutionPlan = "Tables(" +
                "0 = SQLTestFramework.Company" +
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
                "auto ON SQLTestFramework.Company" +
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
            query3.Identifier = 3;
            query3.Description = "Test 3";
            query3.Statement = "SELECT * FROM Location l";
            query3.ExpectedResults = 
                "| 0:String | 1:UInt64 | 2:String |" + Environment.NewLine +
                "| Norway | 1008 | Pw |" + Environment.NewLine + 
                "| Sweden | 1007 | Pv |";
            query3.ExpectedExecutionPlan = "Tables(" +
                "0 = SQLTestFramework.Location" +
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
                "auto ON SQLTestFramework.Location" +
                "0" +
                "__id" +
                "UIntegerDynamicRange(" +
                ")" +
                "LogicalValue(TRUE)" +
                "Ascending" +
                ")";

            SQLQuery query4 = new SQLQuery();
            query4.Identifier = 4;
            query4.Description = "Test 4";
            query4.Statement = "SELECT * FROM Person p WHERE Name=?";
            query4.Values = new Object[] { "Albert" };
            query4.ExpectedResults =
                "| 0:String | 1:UInt64 | 2:String |" + Environment.NewLine +
                "| Albert | 1005 | Pt |";
            query4.ExpectedExecutionPlan = "Tables(" +
                "0 = SQLTestFramework.Person" +
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
                "auto ON SQLTestFramework.Person" +
                "0" +
                "ComparisonString(" +
                "Equal" +
                "StringProperty(0, Name)" +
                "StringVariable(Albert)" +
                ")" +
                "Ascending" +
                ")";

            SQLQuery query5 = new SQLQuery();
            query5.Identifier = 5;
            query5.Description = "Test 5";
            query5.Statement = "SELECT Name, ObjectID FROM Person p WHERE Name=?";
            query5.Values = new Object[] { "Albert" };
            query5.ExpectedResults =
                "| 0:String | 1:String |" + Environment.NewLine +
                "| Albert | Pt |";
            query5.ExpectedExecutionPlan = "Tables(" +
                "0 = SQLTestFramework.Person" +
                ")" +
                "Projection(" +
                "0 = " +
                "StringProperty(0, Name)" +
                "1 = " +
                "ObjectIDProperty(0, ObjectID)" +
                ")" +
                "FullTableScan(" +
                "auto ON SQLTestFramework.Person" +
                "0" +
                "ComparisonString(" +
                "Equal" +
                "StringProperty(0, Name)" +
                "StringVariable(Albert)" +
                ")" +
                "Ascending" +
                ")";

            SQLQuery query6 = new SQLQuery();
            query6.Identifier = 6;
            query6.Description = "Test 6";
            query6.Statement = "SELECT Name FROM Person p WHERE Name=?";
            query6.Values = new Object[] { "Albert" };
            query6.ExpectedResults =
                "| String |" + Environment.NewLine +
                "| Albert |";
            query6.ExpectedExecutionPlan = "Tables(" +
                "0 = SQLTestFramework.Person" +
                ")" +
                "Projection(" +
                "0 = " +
                "StringProperty(0, Name)" +
                ")" +
                "FullTableScan(" +
                "auto ON SQLTestFramework.Person" +
                "0" +
                "ComparisonString(" +
                "Equal" +
                "StringProperty(0, Name)" +
                "StringVariable(Albert)" +
                ")" +
                "Ascending" +
                ")";

            SQLQuery query7 = new SQLQuery();
            query7.Identifier = 7;
            query7.Description = "Test 7";
            query7.Statement = "SELECT Name FROM Person p";
            query7.ExpectedResults =
                "| String |" + Environment.NewLine +
                "| Albert |" + Environment.NewLine +
                "| Einstein |";
            query7.ExpectedExecutionPlan = "Tables(" +
                "0 = SQLTestFramework.Person" +
                ")" +
                "Projection(" +
                "0 = " +
                "StringProperty(0, Name)" +
                ")" +
                "IndexScan(" +
                "auto ON SQLTestFramework.Person" +
                "0" +
                "__id" +
                "UIntegerDynamicRange(" +
                ")" +
                "LogicalValue(TRUE)" +
                "Ascending" +
                ")";

            SQLQuery query8 = new SQLQuery();
            query8.Identifier = 8;
            query8.Description = "Test 8";
            query8.Statement = "SELECT Name FROM Person p ORDER BY Name DESC";
            query8.ExpectedResults =
                "| String |" + Environment.NewLine +
                "| Einstein |" + Environment.NewLine +
                "| Albert |";
            query8.ExpectedExecutionPlan = "Tables(" +
                "0 = SQLTestFramework.Person" +
                ")" +
                "Projection(" +
                "0 = " +
                "StringProperty(0, Name)" +
                ")" +
                "Sort(" +
                "IndexScan(" +
                "auto ON SQLTestFramework.Person" +
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
            query9.Identifier = 9;
            query9.Description = "Test 9";

            SQLQuery query10 = new SQLQuery();
            query10.Identifier = 10;
            query10.Description = "Test 10";
            query10.Statement = "SELECT p FROM Person p where p.Name >= object 15";
            query10.Values = new Object[0];
            query10.ExpectedException = "Failed to process query: " + 
                "SELECT p FROM Person p where p.Name >= object 15: " +
                "Incorrect arguments of types string and object(unknown) to operator greaterThanOrEqual.";

            SQLQuery query11= new SQLQuery();
            query11.Identifier = 11;
            query11.Description = "Test 11";
            query11.Statement = "SELECT p FROM Person p where p.Name >= object 15";
            query11.Values = new Object[0];

            SQLQuery query12 = new SQLQuery();
            query12.Identifier = 12;
            query12.Description = "Test 12";
            query12.Statement = "SELECT * FROM Person p ORDER BY Name DESC";
            query12.ExpectedResults =
                "| 0:String | 1:UInt64 | 2:String |" + Environment.NewLine +
                "| Einstein | 1006 | Pu |" + Environment.NewLine +
                "| Albert | 1005 | Pt |";
            query12.ExpectedExecutionPlan = "Tables(" +
                "0 = SQLTestFramework.Person" +
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
                "auto ON SQLTestFramework.Person" +
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


            SQLQuery query13 = new SQLQuery();
            query13.Identifier = 13;
            query13.Description = "Test 13";
            query13.ContainsLiterals = false;
            query13.Statement = "SELECT Name FROM Person p WHERE Name='Albert'";
            query13.ExpectedException = "ScErrUnsupportLiteral (SCERR7029):" +
                "Literals are not supported in the query. Method Starcounter.Db.SQL does not support queries with literals." +
                "Found literal is Albert. Use variable and parameter instead.";

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
            queryList.Add(query13);

            comments.Add(new Tuple<int,string>(2,"First comment at position 2"));
            comments.Add(new Tuple<int,string>(5,"Second comment at position 5"));

            return new Tuple<List<Tuple<int, string>>, List<SQLTestCase>>(comments, queryList);
        }
    }
}
