using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Starcounter;
using Starcounter.Binding;
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
            dataManipulation = false;
            description = null;
            statement = null;
            variableValues = null;
            expectedResults = null;
            
            // Make actual results an array/list
            actualResults1 = null;
            actualResults2 = null;
        }

        public override Boolean EvaluateResults()
        {
            String expectedNoWhitespace = Regex.Replace(expectedResults, "\\s", "");
            String actual1NoWhitespace = Regex.Replace(actualResults1, "\\s", "");
            String actual2NoWhitespace = Regex.Replace(actualResults2, "\\s", "");

            return (expectedNoWhitespace == actual1NoWhitespace && expectedNoWhitespace == actual2NoWhitespace);
        }

        public override void Execute(Boolean firstExecution)
        {
            try
            {
                SqlEnumerator<dynamic> resultEnumerator = Db.SQL(statement, variableValues).GetEnumerator() as SqlEnumerator<dynamic>;
               
                if (firstExecution)
                {
                    actualResults1 = GetResultString(resultEnumerator);
                    // executionPlan = resultEnumerator.ToString();
                }
                else
                {
                    actualResults2 = GetResultString(resultEnumerator);
                    // executionPlan = resultEnumerator.ToString();
                }
            }
            catch (Exception e) // Should catch ScErrUnsupportLiteral, use SlowSQL since the statement contains literals
            {
                Console.WriteLine(e.Message);
                SqlEnumerator<dynamic> resultEnumerator = Db.SlowSQL(statement, variableValues).GetEnumerator() as SqlEnumerator<dynamic>;

                if (firstExecution)
                {
                    actualResults1 = GetResultString(resultEnumerator);
                }
                else
                {
                    actualResults2 = GetResultString(resultEnumerator);
                }
            }
        }

        public override string ToString()
        {
            return "Description: " + description + Environment.NewLine +
                "Statement: " + statement + Environment.NewLine +
                "Expected: " + Environment.NewLine + expectedResults + Environment.NewLine +
                "Actual 1: " + Environment.NewLine + actualResults1 +
                "Actual 2: " + Environment.NewLine + actualResults2;
        }

        // TODO: Null checks and specify formats
        public String GetResultString(SqlEnumerator<dynamic> resultSet)
        {
            ITypeBinding typeBind;
            IPropertyBinding propBind;
            String result = "";

            typeBind = resultSet.TypeBinding;
            for (int i = 0; i < typeBind.PropertyCount; i++)
            {
                propBind = typeBind.GetPropertyBinding(i);
                result += "| " + propBind.Name + ":" + propBind.TypeCode.ToString() + " ";
            }
            result += "|" + Environment.NewLine;

            while (resultSet.MoveNext())
            {
                typeBind = resultSet.TypeBinding;
                for (int i = 0; i < typeBind.PropertyCount; i++)
                {
                    result += "| ";
                    propBind = typeBind.GetPropertyBinding(i);
                    switch (propBind.TypeCode)
                    {
                        case DbTypeCode.Binary:
                            result += resultSet.Current.GetBinary(i);
                            break;
                        case DbTypeCode.Boolean:
                            result += resultSet.Current.GetBoolean(i);
                            break;
                        case DbTypeCode.Byte:
                            result += resultSet.Current.GetByte(i);
                            break;
                        case DbTypeCode.DateTime:
                            result += resultSet.Current.GetDateTime(i);
                            break;
                        case DbTypeCode.Decimal:
                            result += resultSet.Current.GetDecimal(i);
                            break;
                        case DbTypeCode.Double:
                            result += resultSet.Current.GetDouble(i);
                            break;
                        case DbTypeCode.Int16:
                            result += resultSet.Current.GetInt16(i);
                            break;
                        case DbTypeCode.Int32:
                            result += resultSet.Current.GetInt32(i);
                            break;
                        case DbTypeCode.Int64:
                            result += resultSet.Current.GetInt64(i);
                            break;
                        case DbTypeCode.Object:
                            result += resultSet.Current.GetObject(i);
                            break;
                        case DbTypeCode.SByte:
                            result += resultSet.Current.GetSByte(i);
                            break;
                        case DbTypeCode.Single:
                            result += resultSet.Current.GetSingle(i);
                            break;
                        case DbTypeCode.String:
                            result += resultSet.Current.GetString(i);
                            break;
                        case DbTypeCode.UInt16:
                            result += resultSet.Current.GetUInt16(i);
                            break;
                        case DbTypeCode.UInt32:
                            result += resultSet.Current.GetUInt32(i);
                            break;
                        case DbTypeCode.UInt64:
                            result += resultSet.Current.GetUInt64(i);
                            break;
                        default:
                            break;
                    }
                    result += " ";
                }
                result += "|" + Environment.NewLine;
            }
            return result;
        }
    }
}
