using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Starcounter;
using System.Text.RegularExpressions;

namespace SQLTestFramework.Framework
{
    /// <summary>
    /// Class representing SQL test queries
    /// </summary>
    class SQLQuery : SQLTestCase
    {
        public SQLQuery()
        {
            DataManipulation = false;
            Description = null;
            Statement = null;
            VariableValues = null;
            ExpectedResults = null;
            ActualResults = new List<string>();
        }

        public override Boolean EvaluateResults()
        {
            String expectedResultsNoWhitespace = Regex.Replace(ExpectedResults, "\\s", "");

            for (int i = 0; i < ActualResults.Count; i++)
            {
                if (expectedResultsNoWhitespace != Regex.Replace(ActualResults[i], "\\s", "")) 
                {
                    Passed = false;
                    return Passed;
                }
            }

            Passed = true;
            return Passed;
        }

        public override void Execute(int iteration)
        {
            try
            {
                SqlEnumerator<dynamic> resultEnumerator = Db.SQL(Statement, VariableValues).GetEnumerator() as SqlEnumerator<dynamic>;
               
                ActualResults.Add(Utilities.GetResultString(resultEnumerator));
                // executionPlan = resultEnumerator.ToString();
            }
            catch (Exception e) // Should catch ScErrUnsupportLiteral, use SlowSQL since the statement contains literals
            {
                // TODO: Store internal parameter indicating the existence of literals and check this on next execution to avoid exceptions
                Console.WriteLine(e.Message);
                SqlEnumerator<dynamic> resultEnumerator = Db.SlowSQL(Statement, VariableValues).GetEnumerator() as SqlEnumerator<dynamic>;

                ActualResults.Add(Utilities.GetResultString(resultEnumerator));
            }
        }

        public override string ToString()
        {
            String summary = "Description: " + Description + Environment.NewLine +
                "Statement: " + Statement + Environment.NewLine +
                "Expected: " + Environment.NewLine + ExpectedResults + Environment.NewLine;

            for (int i = 0; i < ActualResults.Count; i++)
            {
                summary += "Actual " + (i+1) + ": " + Environment.NewLine + ActualResults[i];
            }

            return summary;
        }

        
    }
}
