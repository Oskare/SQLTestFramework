using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Starcounter.Binding;
using Starcounter;

namespace SQLTestFramework.Framework
{
    /// <summary>
    /// Contains utility functions used throughout the framework
    /// </summary>
    public static class Utilities
    {
        private const String separator = "|";

        // TODO: Null checks and specify formats.
        /// <summary>
        /// Extracts a string representation of the results of an SQL query
        /// </summary>
        /// <param name="resultSet">The result of an SQL statement execution</param>
        /// <param name="usesOrderBy">A boolean indicating the use of the ORDER BY keyword in the query</param>
        /// <returns>A string representation of the results contained in the input SqlEnumerator</returns>
        public static string GetSingleElementResults(SqlEnumerator<dynamic> resultSet, Boolean usesOrderBy)
        {
            DbTypeCode typeCode;
            List<String> result = new List<String>();
            string headerRow = separator + " " + resultSet.ProjectionTypeCode + " " + separator;
            string row;

            // Extract values
            while (resultSet.MoveNext())
            {
                row = separator + " ";
                typeCode = (DbTypeCode) resultSet.ProjectionTypeCode;
                
                switch (typeCode)
                { 
                    case DbTypeCode.Binary:
                        row += (Nullable<Binary>) resultSet.Current;
                        break;
                    case DbTypeCode.Boolean:
                        row += (Nullable<Boolean>) resultSet.Current;
                        break;
                    case DbTypeCode.Byte:
                        row += (Nullable<Byte>) resultSet.Current;
                        break;
                    case DbTypeCode.DateTime:
                        row += (Nullable<DateTime>) resultSet.Current;
                        break;
                    case DbTypeCode.Decimal:
                        row += (Nullable<Decimal>) resultSet.Current;
                        break;
                    case DbTypeCode.Double:
                        row += (Nullable<Double>) resultSet.Current;
                        break;
                    case DbTypeCode.Int16:
                        row += (Nullable<Int16>) resultSet.Current;
                        break;
                    case DbTypeCode.Int32:
                        row += (Nullable<Int32>) resultSet.Current;
                        break;
                    case DbTypeCode.Int64:
                        row += (Nullable<Int64>) resultSet.Current;
                        break;
                    case DbTypeCode.Object:
                        row += (IObjectView) resultSet.Current;
                        break;
                    case DbTypeCode.SByte:
                        row += (Nullable<SByte>) resultSet.Current;
                        break;
                    case DbTypeCode.Single:
                        row += (Nullable<Single>) resultSet.Current;
                        break;
                    case DbTypeCode.String:
                        row += (String) resultSet.Current;
                        break;
                    case DbTypeCode.UInt16:
                        row += (Nullable<UInt16>) resultSet.Current;
                        break;
                    case DbTypeCode.UInt32:
                        row += (Nullable<UInt32>) resultSet.Current;
                        break;
                    case DbTypeCode.UInt64:
                        row += (Nullable<UInt64>) resultSet.Current;
                        break;
                    default:
                        throw new ArgumentException("Invalid typeCode in result set") ;
                }
                row += " " + separator;
                result.Add(row);
            }
            return GetResultString(headerRow, result, usesOrderBy);
        }


        // TODO: Null checks and specify formats.
        /// <summary>
        /// Extracts a string representation of the results of an SQL query. Works with single object projection results
        /// </summary>
        /// <param name="resultSet">The result of an SQL statement execution</param>
        /// <param name="usesOrderBy">A boolean indicating the use of the ORDER BY keyword in the query</param>
        /// <returns>A string representation of the results contained in the input SqlEnumerator</returns>
        public static string GetResults(SqlEnumerator<dynamic> resultSet, Boolean usesOrderBy)
        {
            ITypeBinding typeBind;
            IPropertyBinding propBind;
            List<String> result = new List<string>();
            StringBuilder header = new StringBuilder();
            StringBuilder row = new StringBuilder();

            // Extract column names and types
            typeBind = resultSet.TypeBinding;
            for (int i = 0; i < typeBind.PropertyCount; i++)
            {
                propBind = typeBind.GetPropertyBinding(i);
                header.Append(separator + " " + propBind.Name + ":" + propBind.TypeCode.ToString() + " ");
            }
            header.Append(separator);

            // Extract values
            while (resultSet.MoveNext())
            {
                row.Clear();
                typeBind = resultSet.TypeBinding;
                for (int i = 0; i < typeBind.PropertyCount; i++)
                {
                    row.Append(separator + " ");
                    propBind = typeBind.GetPropertyBinding(i);
                    switch (propBind.TypeCode)
                    {
                        case DbTypeCode.Binary:
                            row.Append(resultSet.Current.GetBinary(i));
                            break;
                        case DbTypeCode.Boolean:
                            row.Append(resultSet.Current.GetBoolean(i));
                            break;
                        case DbTypeCode.Byte:
                            row.Append(resultSet.Current.GetByte(i));
                            break;
                        case DbTypeCode.DateTime:
                            row.Append(resultSet.Current.GetDateTime(i));
                            break;
                        case DbTypeCode.Decimal:
                            row.Append(resultSet.Current.GetDecimal(i));
                            break;
                        case DbTypeCode.Double:
                            row.Append(resultSet.Current.GetDouble(i));
                            break;
                        case DbTypeCode.Int16:
                            row.Append(resultSet.Current.GetInt16(i));
                            break;
                        case DbTypeCode.Int32:
                            row.Append(resultSet.Current.GetInt32(i));
                            break;
                        case DbTypeCode.Int64:
                            row.Append(resultSet.Current.GetInt64(i));
                            break;
                        case DbTypeCode.Object:
                            row.Append(resultSet.Current.GetObject(i));
                            break;
                        case DbTypeCode.SByte:
                            row.Append(resultSet.Current.GetSByte(i));
                            break;
                        case DbTypeCode.Single:
                            row.Append(resultSet.Current.GetSingle(i));
                            break;
                        case DbTypeCode.String:
                            row.Append(resultSet.Current.GetString(i));
                            break;
                        case DbTypeCode.UInt16:
                            row.Append(resultSet.Current.GetUInt16(i));
                            break;
                        case DbTypeCode.UInt32:
                            row.Append(resultSet.Current.GetUInt32(i));
                            break;
                        case DbTypeCode.UInt64:
                            row.Append(resultSet.Current.GetUInt64(i));
                            break;
                        default:
                            throw new ArgumentException("Invalid typeCode in result set");
                    }
                    row.Append(" ");
                }
                row.Append(separator);
                result.Add(row.ToString());
            }
            return GetResultString(header.ToString(), result, usesOrderBy);
        }

        /// <summary>
        /// Generate a single string from a list of strings.
        /// </summary>
        /// <param name="header">A header string to be added to the head of the result string, and excluded from any sorting</param>
        /// <param name="resultList">The list of strings to be used for the result</param>
        /// <param name="ignoreSorting">True if the current order of strings should be kept in the result</param>
        /// <returns>A string containing the contents of the header and resultList arguments. 
        /// The contents of the resultList will be sorted if ignoreSorting is false</returns>
        private static String GetResultString(String header, List<String> resultList, Boolean ignoreSorting)
        {
            StringBuilder result = new StringBuilder();

            if (!ignoreSorting)
            {
                resultList.Sort();
            }

            result.AppendLine(header);
            foreach (string row in resultList)
            {
                result.AppendLine(row);
            }

            return result.ToString();
        }
    }
}
