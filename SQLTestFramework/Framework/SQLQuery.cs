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
            Values = null;
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
        /// Evaluate results by comparing expected values to execution results stored in the object
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
        /// Execute the test case represented by the object, and store execution results and/or outcome.
        /// </summary>
        public override void Execute()
        {
            SqlEnumerator<dynamic> resultEnumerator = null;
            try
            {
                try
                {
                    resultEnumerator = Db.SQL(Statement, Values).GetEnumerator() as SqlEnumerator<dynamic>;
                }
                catch (SqlException e) // Catches ScErrUnsupportLiteral error to use SlowSQL since the statement contains literals
                {
                    // TODO: Store internal parameter indicating the existence of literals and check this on next execution to avoid exceptions
                    //Console.WriteLine("Execute statement: " + e.Message);
                    resultEnumerator = Db.SlowSQL(Statement, Values).GetEnumerator() as SqlEnumerator<dynamic>;
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
            catch (SqlException exception) // Catch actual exceptions when executing queries
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
            StringBuilder summary = new StringBuilder();
            
            summary.AppendLine("Description: " + Description);
            summary.AppendLine("Statement: " + Statement);

            if (Result == SQLTestCase.TestResult.Generated)
            {
                for (int i = 0; i < ActualResults.Count; i++)
                    summary.Append("Generated results " + (i + 1) + ": " + Environment.NewLine + ActualResults[i]);

                for (int i = 0; i < ActualExecutionPlan.Count; i++)
                    summary.Append("Generated execution plan " + (i + 1) + ": " + Environment.NewLine + ActualExecutionPlan[i]);

                for (int i = 0; i < ActualException.Count; i++)
                    summary.AppendLine("Generated exception " + (i + 1) + ": " + Environment.NewLine + ActualException[i]);
            }
            else
            {
                summary.AppendLine("Expected use of bison parser: " + UsesBisonParser);
                for (int i = 0; i < ActuallyUsesBisonParser.Count; i++)
                    summary.AppendLine("Actual use of bison parser " + (i + 1) + ": " + ActuallyUsesBisonParser[i]);

                summary.AppendLine("Expected results: " + Environment.NewLine + ExpectedResults);
                for (int i = 0; i < ActualResults.Count; i++)
                    summary.Append("Actual results " + (i + 1) + ": " + Environment.NewLine + ActualResults[i]);

                summary.AppendLine("Expected execution plan: " + Environment.NewLine + ExpectedExecutionPlan);
                for (int i = 0; i < ActualExecutionPlan.Count; i++)
                    summary.Append("Actual execution plan " + (i + 1) + ": " + Environment.NewLine + ActualExecutionPlan[i]);

                summary.AppendLine("Expected exeption: " + Environment.NewLine + ExpectedException);
                for (int i = 0; i < ActualException.Count; i++)
                    summary.Append("Actual exception " + (i + 1) + ": " + Environment.NewLine + ActualException[i]);
            }
            return summary.ToString();
        }

    }
}
