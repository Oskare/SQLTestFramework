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
        public List<Boolean> ActuallyUsesBisonParser { get; set; }
        public int LineNumber { get; set; }

        // Override of internal parameters
        public Nullable<Boolean> UsesBisonParser { get; set; }
        public Nullable<Boolean> UsesOrderBy { get; set; }
        public Nullable<Boolean> SingleObjectProjection { get; set; }
        public Nullable<Boolean> ContainsLiterals { get; set; }

        public SQLQuery()
        {
            Description = "";
            Statement = "";
            Values = null;
            DataManipulation = false;
            ExpectedResults = null;
            ExpectedException = null;
            ExpectedExecutionPlan = null;
            LineNumber = -1;

            UsesOrderBy = null;
            UsesBisonParser = null;
            SingleObjectProjection = null;
            ContainsLiterals = null;

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
            if (ExpectedResults == null && ExpectedExecutionPlan == null && ExpectedException == null)
            {
                InternalParam = new TestCaseParameters(Identifier);
                if (ActuallyUsesBisonParser.Count > 0)
                    InternalParam.UsesBisonParser = ActuallyUsesBisonParser[0];
                // Else, the test result is an exception, and bison parser will not be used

                Result = TestResult.Generated;
                return Result;
            }

            if (ExpectedException == null)
                ExpectedException = "";

            if (ExpectedExecutionPlan == null)
                ExpectedExecutionPlan = "";

            if (ExpectedResults == null)
                ExpectedResults = "";

            if (!InternalParam.UsesBisonParser.HasValue && ActuallyUsesBisonParser.Count > 0)
                InternalParam.UsesBisonParser = ActuallyUsesBisonParser[0];

            String expectedResultsNoWhitespace = Regex.Replace(ExpectedResults, "\\s", "");
            String expectedExecutionPlanNoWhitespace = Regex.Replace(ExpectedExecutionPlan, "\\s", "");
            String expectedExceptionNoWhitespace = Regex.Replace(ExpectedException, "\\s", "");
            Boolean usesBisonParser = UsesBisonParser.HasValue && UsesBisonParser.Value ||
                InternalParam.UsesBisonParser.HasValue && InternalParam.UsesBisonParser.Value && !UsesBisonParser.HasValue;

            if (ActualResults.Count == 0 && ActualException.Count == 0)
                throw new ArgumentException("No results or exceptions from statement execution exists");

            for (int i = 0; i < ActualResults.Count; i++)
            {
                if (expectedResultsNoWhitespace != Regex.Replace(ActualResults[i], "\\s", "") ||
                    expectedExecutionPlanNoWhitespace != Regex.Replace(ActualExecutionPlan[i], "\\s", "") ||
                    (usesBisonParser != ActuallyUsesBisonParser[i]))
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

            Result = TestResult.Passed;
            return Result;
        }

        /// <summary>
        /// Execute the test case represented by the object, and store execution results and/or outcome.
        /// </summary>
        public override void Execute()
        {
            // User supplied parameters will override internal parameters
            Boolean containsLiterals = ContainsLiterals.HasValue && ContainsLiterals.Value ||
                InternalParam.ContainsLiterals.HasValue && InternalParam.ContainsLiterals.Value && !ContainsLiterals.HasValue;
            Boolean singleObjectProj = SingleObjectProjection.HasValue && SingleObjectProjection.Value ||
                InternalParam.SingleObjectProjection.HasValue && InternalParam.SingleObjectProjection.Value && !SingleObjectProjection.HasValue;

            SqlEnumerator<dynamic> resultEnumerator = null;
            try
            {
                if (!InternalParam.ContainsLiterals.HasValue && !ContainsLiterals.HasValue)
                {
                    try
                    {
                        resultEnumerator = Db.SQL(Statement, Values).GetEnumerator() as SqlEnumerator<dynamic>;
                        InternalParam.ContainsLiterals = false;
                    }
                    catch (SqlException e)
                    {
                        throw e;
                    }
                    catch (Exception e) // Catches ScErrUnsupportLiteral error
                    {
                        //Console.WriteLine("Execute statement: " + e.Message);
                        // TODO: Match unsupported literal exception, throw otherwise. 
                        //       Also removes the need for the SqlException above
                        resultEnumerator = Db.SlowSQL(Statement, Values).GetEnumerator() as SqlEnumerator<dynamic>;
                        InternalParam.ContainsLiterals = true;
                    }
                }
                else if (containsLiterals)
                    resultEnumerator = Db.SlowSQL(Statement, Values).GetEnumerator() as SqlEnumerator<dynamic>;
                else
                    resultEnumerator = Db.SQL(Statement, Values).GetEnumerator() as SqlEnumerator<dynamic>;

                string result;
                if (!InternalParam.SingleObjectProjection.HasValue && !SingleObjectProjection.HasValue)
                {
                    try
                    {
                        result = Utilities.GetResults(resultEnumerator, hasOrderByClause());
                        InternalParam.SingleObjectProjection = false;
                    }
                    catch (NullReferenceException e) // Exception indicating that the result set contains single element rows
                    {
                        //Console.WriteLine("GetResults: " + e.Message);
                        result = Utilities.GetSingleElementResults(resultEnumerator, hasOrderByClause());
                        InternalParam.SingleObjectProjection = true;
                    }
                }
                else if (singleObjectProj)
                    result = Utilities.GetSingleElementResults(resultEnumerator, hasOrderByClause());
                else
                    result = Utilities.GetResults(resultEnumerator, hasOrderByClause());

                ActualResults.Add(result);
                ActualExecutionPlan.Add(resultEnumerator.ToString());
                ActuallyUsesBisonParser.Add(resultEnumerator.IsBisonParserUsed);
            }
            catch (Exception exception) // Catch actual exceptions when executing queries
            {
                //Console.WriteLine("Actual exception: " + exception.Message);
                String exceptionMessage = exception.Message;
                if (exceptionMessage.Contains("\r"))
                    exceptionMessage = exceptionMessage.Substring(0, exceptionMessage.IndexOf("\r"));
                if (exceptionMessage.Contains("\n"))
                    exceptionMessage = exceptionMessage.Substring(0, exceptionMessage.IndexOf("\n"));

                ActualException.Add(exceptionMessage);
            }

            finally
            {
                if (resultEnumerator != null)
                    resultEnumerator.Dispose();
            }
        }

        /// <summary>
        /// Check if internal parameters specify the use of ORDER BY clause in the query.
        /// Otherwise, parse the query to find out.
        /// </summary>
        /// <returns>True if the query contains an ORDER BY clause, otherwise false.</returns>
        private Boolean hasOrderByClause()
        {
            if (!InternalParam.UsesOrderBy.HasValue)
            {
                Boolean containsOrderBy = (Statement.IndexOf("order by", StringComparison.OrdinalIgnoreCase) >= 0);
                InternalParam.UsesOrderBy = containsOrderBy;
            }

            return UsesOrderBy.HasValue && UsesOrderBy.Value || 
                InternalParam.UsesOrderBy.HasValue && InternalParam.UsesOrderBy.Value && !UsesOrderBy.HasValue;
        }

        public override string ToString()
        {
            StringBuilder summary = new StringBuilder();
            
            summary.AppendLine("Description: " + Description);
            summary.AppendLine("Line number: " + LineNumber);
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
                summary.AppendLine("Expected use of bison parser (internal): " + InternalParam.UsesBisonParser);
                for (int i = 0; i < ActuallyUsesBisonParser.Count; i++)
                    summary.AppendLine("Actual use of bison parser " + (i + 1) + ": " + ActuallyUsesBisonParser[i]);

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

            if (InternalParam != null)
            {
                summary.AppendLine("Parameters: ");
                summary.AppendLine("ID: " + Identifier);
                summary.AppendLine("UsesBisonParser: " + UsesBisonParser);
                summary.AppendLine("UsesOrderBy: " + UsesOrderBy);
                summary.AppendLine("SingleObjectProjection: " + SingleObjectProjection);
                summary.AppendLine("ContainsLiteral: " + ContainsLiterals);
                summary.AppendLine("InternalParameters: ");
                summary.AppendLine("ID: " + InternalParam.Identifier);
                summary.AppendLine("UsesBisonParser: " + InternalParam.UsesBisonParser);
                summary.AppendLine("UsesOrderBy: " + InternalParam.UsesOrderBy);
                summary.AppendLine("SingleObjectProjection: " + InternalParam.SingleObjectProjection);
                summary.AppendLine("ContainsLiteral: " + InternalParam.ContainsLiterals);
            }

            return summary.ToString();
        }

    }
}
