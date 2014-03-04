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
        public String ExpectedExecutionPlan { get; set; }
        public List<String> ActualExecutionPlan { get; set; }

        public SQLQuery()
        {
            DataManipulation = false;
            Description = "";
            Statement = "";
            VariableValues = null;
            ExpectedResults = "";
            ActualResults = new List<string>();

            ExpectedExecutionPlan = "";
            ActualExecutionPlan = new List<string>();
        }

        public override Boolean EvaluateResults()
        {
            String expectedResultsNoWhitespace = Regex.Replace(ExpectedResults, "\\s", "");
            String expectedExecutionPlanNoWhitespace = Regex.Replace(ExpectedExecutionPlan, "\\s", "");

            for (int i = 0; i < ActualResults.Count; i++)
            {
                if (expectedResultsNoWhitespace != Regex.Replace(ActualResults[i], "\\s", "") ||
                    expectedExecutionPlanNoWhitespace != Regex.Replace(ActualExecutionPlan[i], "\\s", "")) 
                {
                    Passed = false;
                    return Passed;
                }
            }

            Passed = true;
            return Passed;
        }

        public override void Execute()
        {
            try
            {
                SqlEnumerator<dynamic> resultEnumerator = Db.SQL(Statement, VariableValues).GetEnumerator() as SqlEnumerator<dynamic>;
               
                ActualResults.Add(Utilities.GetResultString(resultEnumerator));
                ActualExecutionPlan.Add(resultEnumerator.ToString());
            }
            catch (Exception e) // Should catch ScErrUnsupportLiteral, use SlowSQL since the statement contains literals
            {
                // TODO: Store internal parameter indicating the existence of literals and check this on next execution to avoid exceptions
                Console.WriteLine(e.Message);
                SqlEnumerator<dynamic> resultEnumerator = Db.SlowSQL(Statement, VariableValues).GetEnumerator() as SqlEnumerator<dynamic>;

                ActualResults.Add(Utilities.GetResultString(resultEnumerator));
                ActualExecutionPlan.Add(resultEnumerator.ToString());
            }
        }

        public override string ToString()
        {
            String summary = "Description: " + Description + Environment.NewLine +
                "Statement: " + Statement + Environment.NewLine +
                "Expected results: " + Environment.NewLine + ExpectedResults + Environment.NewLine;

            for (int i = 0; i < ActualResults.Count; i++)
            {
                summary += "Actual results " + (i+1) + ": " + Environment.NewLine + ActualResults[i];
            }

            summary += "Expected execution plan: " + Environment.NewLine + ExpectedExecutionPlan + Environment.NewLine;
            for (int i = 0; i < ActualExecutionPlan.Count; i++)
            {
                summary += "Actual execution plan " + (i+1) + ": " + Environment.NewLine + ActualExecutionPlan[i];
            }

            return summary;
        }

        
    }
}
