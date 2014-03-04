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
        // TODO: Null checks and specify formats
        /// <summary>
        /// Extracts a string representation of the results of a SQL query (statement?)
        /// </summary>
        /// <param name="resultSet"></param>
        /// <returns></returns>
        public static String GetResultString(SqlEnumerator<dynamic> resultSet)
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
