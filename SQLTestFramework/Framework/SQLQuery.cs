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
    public class SQLQuery : SQLTestCase
    {
        // Some of these should perhaps be moved into the interface
        public String ExpectedExecutionPlan { get; set; }
        public List<String> ActualExecutionPlan { get; set; }
        public Boolean UsesOrderBy { get; set; }
        public Boolean UsesBisonParser { get; set; }
        public List<Boolean> ActuallyUsesBisonParser { get; set; }

        public SQLQuery()
        {
            Description = "";
            Statement = "";
            VariableValues = null;
            DataManipulation = false;
            UsesOrderBy = false;
            UsesBisonParser = false;
            ExpectedResults = "";
            ExpectedException = "";
            ExpectedExecutionPlan = "";

            ActuallyUsesBisonParser = new List<Boolean>();
            ActualResults = new List<string>();
            ActualException = new List<string>();
            ActualExecutionPlan = new List<string>();
        }

        /// <summary>
        /// Evaluate results by comparing results stored in the object
        /// </summary>
        /// <returns>A TestResult value indicating the outcome of the test case run</returns>
        public override TestResult EvaluateResults()
        {
            String expectedResultsNoWhitespace = Regex.Replace(ExpectedResults, "\\s", "");
            String expectedExecutionPlanNoWhitespace = Regex.Replace(ExpectedExecutionPlan, "\\s", "");
            String expectedExceptionNoWhitespace = Regex.Replace(ExpectedException, "\\s", "");

            if (ActualResults.Count == 0 && ActualException.Count == 0)
                throw new ArgumentException("No results or exceptions from statement execution exists");

            if (expectedResultsNoWhitespace == "GENERATE" || 
                expectedExecutionPlanNoWhitespace == "GENERATE" ||
                ExpectedException == "GENERATE")
            {
                // Store use of bison parser internally
                Result = TestResult.Generated;
                return Result;
            }
            else
            {
                for (int i = 0; i < ActualResults.Count; i++)
                {
                    if (expectedResultsNoWhitespace != Regex.Replace(ActualResults[i], "\\s", "") ||
                        expectedExecutionPlanNoWhitespace != Regex.Replace(ActualExecutionPlan[i], "\\s", "") ||
                        UsesBisonParser != ActuallyUsesBisonParser[i])
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
            SqlEnumerator<dynamic> resultEnumerator = null;
            try
            {
                try
                {
                    resultEnumerator = Db.SQL(Statement, VariableValues).GetEnumerator() as SqlEnumerator<dynamic>;
                }
                catch (SqlException e) // Catches ScErrUnsupportLiteral error and use SlowSQL since the statement contains literals
                {
                    // TODO: Store internal parameter indicating the existence of literals and check this on next execution to avoid exceptions
                    //Console.WriteLine("Execute statement: " + e.Message);
                    resultEnumerator = Db.SlowSQL(Statement, VariableValues).GetEnumerator() as SqlEnumerator<dynamic>;
                }       

                string result;
                try
                {
                    result = Utilities.GetResults(resultEnumerator, UsesOrderBy);
                }
                catch (NullReferenceException e) // Catches exception indicating that the result set contains single element rows
                {
                    // TODO: Store internal parameter indicating single object projection
                    //Console.WriteLine("GetResults: " + e.Message);
                    result = Utilities.GetSingleElementResults(resultEnumerator, UsesOrderBy);
                }

                ActualResults.Add(result);
                ActualExecutionPlan.Add(resultEnumerator.ToString());
                ActuallyUsesBisonParser.Add(resultEnumerator.IsBisonParserUsed);
            }
            catch (SqlException exception) // Catch actual exceptions when executing statements
            {
                //Console.WriteLine("Actual exception: " + exception.Message);
                ActualException.Add(exception.Message);
            }
            finally
            {
                if (resultEnumerator != null)
                    resultEnumerator.Dispose();
            }
        }

        public override string ToString()
        {
            String summary = "Description: " + Description + Environment.NewLine +
                "Statement: " + Statement + Environment.NewLine;

            if (Result == SQLTestCase.TestResult.Generated)
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
                summary += "Expected use of bison parser: " + Environment.NewLine + UsesBisonParser + Environment.NewLine;
                for (int i = 0; i < ActuallyUsesBisonParser.Count; i++)
                    summary += "Actual use of bison parser " + (i + 1) + ": " + Environment.NewLine + ActuallyUsesBisonParser[i] + Environment.NewLine;

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
