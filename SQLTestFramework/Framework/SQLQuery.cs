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
    public class SQLQuery : ISQLTestCase
    {
        public String ExpectedExecutionPlan { get; set; }
        public List<String> ActualExecutionPlan { get; set; }
        public Boolean usesOrderBy { get; set; }

        public SQLQuery()
        {
            DataManipulation = false;
            Description = "";
            Statement = "";
            VariableValues = null;
            ExpectedResults = "";
            ExpectedException = "";
            ActualResults = new List<string>();
            ActualException = new List<string>();

            ExpectedExecutionPlan = "";
            ActualExecutionPlan = new List<string>();
            usesOrderBy = false;
        }

        /// <summary>
        /// Evaluate results by comparing properties of the object
        /// </summary>
        /// <returns>A TestResult value indicating the outcome of the test case run</returns>
        public override TestResult EvaluateResults()
        {
            String expectedResultsNoWhitespace = Regex.Replace(ExpectedResults, "\\s", "");
            String expectedExecutionPlanNoWhitespace = Regex.Replace(ExpectedExecutionPlan, "\\s", "");
            String expectedExceptionNoWhitespace = Regex.Replace(ExpectedException, "\\s", "");

            if (ActualResults.Count == 0 && ActualException.Count == 0)
                throw new Exception("No result/exceptions exists");

            if (expectedResultsNoWhitespace == "GENERATE" || 
                expectedExecutionPlanNoWhitespace == "GENERATE" ||
                ExpectedException == "GENERATE")
            {
                Result = TestResult.Generated;
                return Result;
            }
            else
            {
                for (int i = 0; i < ActualResults.Count; i++)
                {
                    if (expectedResultsNoWhitespace != Regex.Replace(ActualResults[i], "\\s", "") ||
                        expectedExecutionPlanNoWhitespace != Regex.Replace(ActualExecutionPlan[i], "\\s", ""))
                    {
                        Result = TestResult.Failed;
                        return Result;
                    }
                }
                for (int i = 0; i < ActualException.Count; i++)
                {
                    if (expectedExceptionNoWhitespace != Regex.Replace(ActualException[i], "\\s", ""))
                    {
                        Result = TestResult.Failed;
                        return Result;
                    }
                }
            }

            Result = TestResult.Passed;
            return Result;
        }

        /// <summary>
        /// Execute the test case represented by an object. 
        /// </summary>
        public override void Execute()
        {
            try
            {
                SqlEnumerator<dynamic> resultEnumerator;
                try
                {
                    resultEnumerator = Db.SQL(Statement, VariableValues).GetEnumerator() as SqlEnumerator<dynamic>;
                }
                catch (Exception e) // Should catch ScErrUnsupportLiteral, use SlowSQL since the statement contains literals
                {
                    // TODO: Store internal parameter indicating the existence of literals and check this on next execution to avoid exceptions
                    Console.WriteLine("ExecuteSQL: " + e.Message);
                    resultEnumerator = Db.SlowSQL(Statement, VariableValues).GetEnumerator() as SqlEnumerator<dynamic>;
                }       

                string result;
                try
                {
                    result = Utilities.GetResults(resultEnumerator, usesOrderBy);
                }
                catch (Exception e) // Make new exception
                {
                    // TODO: Store internal parameter indicating single object projection
                    Console.WriteLine("GetResults: " + e.Message);
                    result = Utilities.GetSingleElementResults(resultEnumerator, usesOrderBy);
                }

                ActualResults.Add(result);
                ActualExecutionPlan.Add(resultEnumerator.ToString());
            }
            catch (Exception e) // Should catch expected exceptions
            {
                ActualException.Add(e.Message);
            }
        }

        public override string ToString()
        {
            String summary = "Description: " + Description + Environment.NewLine +
                "Statement: " + Statement + Environment.NewLine;

            if (Result == ISQLTestCase.TestResult.Generated)
            {
                for (int i = 0; i < ActualResults.Count; i++)
                    summary += "Generated results " + (i + 1) + ": " + Environment.NewLine + ActualResults[i];

                for (int i = 0; i < ActualExecutionPlan.Count; i++)
                    summary += "Generated execution plan " + (i + 1) + ": " + Environment.NewLine + ActualExecutionPlan[i];

                for (int i = 0; i < ActualException.Count; i++)
                    summary += "Generated exception " + (i + 1) + ": " + Environment.NewLine + ActualException[i];
            }
            else
            {
                summary += "Expected results: " + Environment.NewLine + ExpectedResults + Environment.NewLine;
                for (int i = 0; i < ActualResults.Count; i++)
                    summary += "Actual results " + (i + 1) + ": " + Environment.NewLine + ActualResults[i];

                summary += "Expected execution plan: " + Environment.NewLine + ExpectedExecutionPlan + Environment.NewLine;
                for (int i = 0; i < ActualExecutionPlan.Count; i++)
                    summary += "Actual execution plan " + (i + 1) + ": " + Environment.NewLine + ActualExecutionPlan[i];

                summary += "Expected exeption: " + Environment.NewLine + ExpectedException + Environment.NewLine;
                for (int i = 0; i < ActualException.Count; i++)
                    summary += "Actual exception " + (i + 1) + ": " + Environment.NewLine + ActualException[i] + Environment.NewLine;
            }
            return summary;
        }

        
    }
}
