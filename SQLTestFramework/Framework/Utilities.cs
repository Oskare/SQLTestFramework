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
    /// Contains utility functions useful throughout the framework
    /// </summary>
    public static class Utilities
    {
        private const String separator = "|";

        // TODO: Null checks and specify formats. Use string builder instead of concatenation for performance?
        /// <summary>
        /// Extracts a string representation of the results of a SQL query (statement?)
        /// </summary>
        /// <param name="resultSet">The result of an SQL statement execution</param>
        /// <returns>A string representation of the results contained in the input SqlEnumerator</returns>
        public static string GetSingleElementResults(SqlEnumerator<dynamic> resultSet, Boolean usesOrderBy)
        {
            DbTypeCode typeCode;
            List<String> result = new List<String>();
            string headerRow = separator + " " + resultSet.ProjectionTypeCode + " " + separator + Environment.NewLine;
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
                row += " " + separator + Environment.NewLine;
                result.Add(row);
            }
            return GetResultString(headerRow, result, usesOrderBy);
        }


        // TODO: Null checks and specify formats. Use string builder instead of concatenation for performance?
        /// <summary>
        /// Extracts a string representation of the results of a SQL query (statement?). Works on single object projection
        /// </summary>
        /// <param name="resultSet">The result of an SQL statement execution</param>
        /// <returns>A string representation of the results contained in the input SqlEnumerator</returns>
        public static string GetResults(SqlEnumerator<dynamic> resultSet, Boolean usesOrderBy)
        {
            ITypeBinding typeBind;
            IPropertyBinding propBind;
            List<String> result = new List<string>();
            string header = "";
            string row;

            // Extract column names and types
            typeBind = resultSet.TypeBinding;

            for (int i = 0; i < typeBind.PropertyCount; i++)
            {
                propBind = typeBind.GetPropertyBinding(i);
                header += separator + " " + propBind.Name + ":" + propBind.TypeCode.ToString() + " ";
            }
            header += separator + Environment.NewLine;

            // Extract values
            while (resultSet.MoveNext())
            {
                row = "";
                typeBind = resultSet.TypeBinding;
                for (int i = 0; i < typeBind.PropertyCount; i++)
                {
                    row += separator + " ";
                    propBind = typeBind.GetPropertyBinding(i);
                    switch (propBind.TypeCode)
                    {
                        case DbTypeCode.Binary:
                            row += resultSet.Current.GetBinary(i);
                            break;
                        case DbTypeCode.Boolean:
                            row += resultSet.Current.GetBoolean(i);
                            break;
                        case DbTypeCode.Byte:
                            row += resultSet.Current.GetByte(i);
                            break;
                        case DbTypeCode.DateTime:
                            row += resultSet.Current.GetDateTime(i);
                            break;
                        case DbTypeCode.Decimal:
                            row += resultSet.Current.GetDecimal(i);
                            break;
                        case DbTypeCode.Double:
                            row += resultSet.Current.GetDouble(i);
                            break;
                        case DbTypeCode.Int16:
                            row += resultSet.Current.GetInt16(i);
                            break;
                        case DbTypeCode.Int32:
                            row += resultSet.Current.GetInt32(i);
                            break;
                        case DbTypeCode.Int64:
                            row += resultSet.Current.GetInt64(i);
                            break;
                        case DbTypeCode.Object:
                            row += resultSet.Current.GetObject(i);
                            break;
                        case DbTypeCode.SByte:
                            row += resultSet.Current.GetSByte(i);
                            break;
                        case DbTypeCode.Single:
                            row += resultSet.Current.GetSingle(i);
                            break;
                        case DbTypeCode.String:
                            row += resultSet.Current.GetString(i);
                            break;
                        case DbTypeCode.UInt16:
                            row += resultSet.Current.GetUInt16(i);
                            break;
                        case DbTypeCode.UInt32:
                            row += resultSet.Current.GetUInt32(i);
                            break;
                        case DbTypeCode.UInt64:
                            row += resultSet.Current.GetUInt64(i);
                            break;
                        default:
                            throw new ArgumentException("Invalid typeCode in result set");
                    }
                    row += " ";
                }
                row += separator + Environment.NewLine;
                result.Add(row);
            }
            return GetResultString(header, result, usesOrderBy);
        }

        // TODO: Use string builder instead of concatenation for performance?
        /// <summary>
        /// Generate a single string from a list of strings.
        /// </summary>
        /// <param name="resultList">The list of strings to be used for the result</param>
        /// <param name="ignoreSorting">True if the current order of strings should be kept in the result</param>
        /// <returns></returns>
        private static String GetResultString(String header, List<String> resultList, Boolean ignoreSorting)
        {
            string result = "";

            if (!ignoreSorting)
            {
                resultList.Sort(); // TODO: Specify sorting
            }

            foreach (string row in resultList)
            {
                result += row;
            }
            return header + result;
        }
    }
}
