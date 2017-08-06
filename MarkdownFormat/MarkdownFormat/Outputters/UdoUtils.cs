using System;
using System.Collections.Generic;
using Microsoft.Analytics.Interfaces;
using Microsoft.Analytics.Types.Sql;

namespace MarkdownFormat
{
    public static class UdoUtils
    {
        public static string GetUsqlTypeDisplayName(System.Type type)
        {
            if (ScalarTypeNameDic == null)
            {
                ScalarTypeNameDic = new Dictionary<Type, string>();
                ScalarTypeNameDic[typeof(string)] = "string";
                ScalarTypeNameDic[typeof(char)] = "char";
                ScalarTypeNameDic[typeof(char?)] = "char?";
                ScalarTypeNameDic[typeof(float)] = "float";
                ScalarTypeNameDic[typeof(float?)] = "float?";
                ScalarTypeNameDic[typeof(double)] = "double";
                ScalarTypeNameDic[typeof(double?)] = "double?";
                ScalarTypeNameDic[typeof(decimal)] = "decimal";
                ScalarTypeNameDic[typeof(decimal?)] = "decimal?";
                ScalarTypeNameDic[typeof(int)] = "int";
                ScalarTypeNameDic[typeof(int?)] = "int?";
                ScalarTypeNameDic[typeof(short)] = "short";
                ScalarTypeNameDic[typeof(short?)] = "short?";
                ScalarTypeNameDic[typeof(byte)] = "byte";
                ScalarTypeNameDic[typeof(byte?)] = "byte?";
                ScalarTypeNameDic[typeof(sbyte)] = "sbyte";
                ScalarTypeNameDic[typeof(sbyte?)] = "sbyte?";
                ScalarTypeNameDic[typeof(uint)] = "uint";
                ScalarTypeNameDic[typeof(uint?)] = "uint?";
                ScalarTypeNameDic[typeof(ulong)] = "ulong";
                ScalarTypeNameDic[typeof(ulong?)] = "ulong?";
                ScalarTypeNameDic[typeof(ushort)] = "ushort";
                ScalarTypeNameDic[typeof(ushort?)] = "ushort?";
                ScalarTypeNameDic[typeof(bool)] = "bool";
                ScalarTypeNameDic[typeof(bool?)] = "bool?";
                ScalarTypeNameDic[typeof(byte[])] = "byte[]";
                ScalarTypeNameDic[typeof(System.Guid)] = "Guid";
                ScalarTypeNameDic[typeof(System.DateTime)] = "DateTime";
            }

            if (ScalarTypeNameDic.ContainsKey(type))
            {
                string dn = ScalarTypeNameDic[type];
                return dn;
            }

            if (type.IsGenericType)
            {
                var tokens = type.FullName.Split('`');

                if (tokens[0].StartsWith("Microsoft.Analytics.Types.Sql."))
                {
                    var tokens2 = tokens[0].Split('.');
                    string name = tokens2[tokens2.Length - 1];
                    ScalarTypeNameDic[type] = name;
                    return name;
                }
            }

            return type.FullName;
        }

        public static Dictionary<Type, string> ScalarTypeNameDic;
        public static string GetValueDisplayString(Microsoft.Analytics.Interfaces.IRow row, Type type, string val, Microsoft.Analytics.Interfaces.IColumn col, TypeDisplayNameOptions opts)
        {
            if (type == typeof(string))
            {
                val = row.Get<string>(col.Name) ?? "NULL";
            }
            else if (type == typeof(bool))
            {
                val = row.Get<bool>(col.Name).ToString();
            }
            else if (type == typeof(char))
            {
                val = row.Get<char>(col.Name).ToString();
            }
            else if (type == typeof(float))
            {
                val = row.Get<float>(col.Name).ToString();
            }
            else if (type == typeof(double))
            {
                val = row.Get<double>(col.Name).ToString();
            }
            else if (type == typeof(int))
            {
                val = row.Get<int>(col.Name).ToString();
            }
            else if (type == typeof(long))
            {
                val = row.Get<long>(col.Name).ToString();
            }
            else if (type == typeof(System.Guid))
            {
                val = row.Get<System.Guid>(col.Name).ToString();
            }
            else if (type == typeof(int?))
            {
                val = row.Get<int?>(col.Name).ToString() ?? "NULL";
            }
            else if (type == typeof(long?))
            {
                val = row.Get<long?>(col.Name).ToString() ?? "NULL";
            }
            else if (type == typeof(float?))
            {
                val = row.Get<float?>(col.Name).ToString() ?? "NULL";
            }
            else if (type == typeof(double?))
            {
                val = row.Get<double?>(col.Name).ToString() ?? "NULL";
            }
            else if (type.IsGenericType)
            {
                val = GetValueFromUsqlArray_(row, type, col, opts);

                if (val == null)
                {
                    val = GetValueFromUsqlMap_(row, type, col, opts);
                }

                if (val==null)
                {
                    val = UdoUtils.GetUsqlTypeDisplayName(type);
                }
            }
            else
            {
                val = UdoUtils.GetUsqlTypeDisplayName(type);
            }
            return val;
        }

        private static string GetValueFromUsqlMap_(IRow row, Type type, IColumn col, TypeDisplayNameOptions opts)
        {
            string val = null;

            if (type == typeof(Microsoft.Analytics.Types.Sql.SqlMap<string, string>))
            {
                val = GetValueFromUsqlMap<string, string>(row, col, opts);
            }
            else if (type == typeof(Microsoft.Analytics.Types.Sql.SqlMap<string, bool?>))
            {
                val = GetValueFromUsqlMap<string, bool?>(row, col, opts);
            }
            else if (type == typeof(Microsoft.Analytics.Types.Sql.SqlMap<string, char?>))
            {
                val = GetValueFromUsqlMap<string, char?>(row, col, opts);
            }
            else if (type == typeof(Microsoft.Analytics.Types.Sql.SqlMap<string, int?>))
            {
                val = GetValueFromUsqlMap<string, int?>(row, col, opts);
            }
            else if (type == typeof(Microsoft.Analytics.Types.Sql.SqlMap<string, long?>))
            {
                val = GetValueFromUsqlMap<string, long?>(row, col, opts);
            }
            else if (type == typeof(Microsoft.Analytics.Types.Sql.SqlMap<string, float?>))
            {
                val = GetValueFromUsqlMap<string, float?>(row, col, opts);
            }
            else if (type == typeof(Microsoft.Analytics.Types.Sql.SqlMap<string, double?>))
            {
                val = GetValueFromUsqlMap<string, double?>(row, col, opts);
            }
            else if (type == typeof(Microsoft.Analytics.Types.Sql.SqlMap<string, decimal?>))
            {
                val = GetValueFromUsqlMap<string, decimal?>(row, col, opts);
            }
            return val;
        }

        private static string GetValueFromUsqlArray_(IRow row, Type type, IColumn col, TypeDisplayNameOptions opts)
        {
            string val = null;

            if (type == typeof(Microsoft.Analytics.Types.Sql.SqlArray<string>))
            {
                val = GetValueFromUsqlArray<string>(row, col, val, opts);
            }
            else if (type == typeof(Microsoft.Analytics.Types.Sql.SqlArray<char>))
            {
                val = GetValueFromUsqlArray<char>(row, col, val, opts);
            }
            else if (type == typeof(Microsoft.Analytics.Types.Sql.SqlArray<char?>))
            {
                val = GetValueFromUsqlArray<char?>(row, col, val, opts);
            }
            else if (type == typeof(Microsoft.Analytics.Types.Sql.SqlArray<int>))
            {
                val = GetValueFromUsqlArray<int>(row, col, val, opts);
            }
            else if (type == typeof(Microsoft.Analytics.Types.Sql.SqlArray<int?>))
            {
                val = GetValueFromUsqlArray<int?>(row, col, val, opts);
            }
            else if (type == typeof(Microsoft.Analytics.Types.Sql.SqlArray<long>))
            {
                val = GetValueFromUsqlArray<long>(row, col, val, opts);
            }
            else if (type == typeof(Microsoft.Analytics.Types.Sql.SqlArray<long?>))
            {
                val = GetValueFromUsqlArray<long?>(row, col, val, opts);
            }
            else if (type == typeof(Microsoft.Analytics.Types.Sql.SqlArray<float>))
            {
                val = GetValueFromUsqlArray<float>(row, col, val, opts);
            }
            else if (type == typeof(Microsoft.Analytics.Types.Sql.SqlArray<float?>))
            {
                val = GetValueFromUsqlArray<float?>(row, col, val, opts);
            }
            else if (type == typeof(Microsoft.Analytics.Types.Sql.SqlArray<decimal>))
            {
                val = GetValueFromUsqlArray<decimal>(row, col, val, opts);
            }
            else if (type == typeof(Microsoft.Analytics.Types.Sql.SqlArray<decimal?>))
            {
                val = GetValueFromUsqlArray<decimal?>(row, col, val, opts);
            }
            else if (type == typeof(Microsoft.Analytics.Types.Sql.SqlArray<double>))
            {
                val = GetValueFromUsqlArray<double>(row, col, val, opts);
            }
            else if (type == typeof(Microsoft.Analytics.Types.Sql.SqlArray<double?>))
            {
                val = GetValueFromUsqlArray<double?>(row, col, val, opts);
            }
            else if (type == typeof(Microsoft.Analytics.Types.Sql.SqlArray<byte>))
            {
                val = GetValueFromUsqlArray<byte>(row, col, val, opts);
            }
            else if (type == typeof(Microsoft.Analytics.Types.Sql.SqlArray<byte?>))
            {
                val = GetValueFromUsqlArray<byte?>(row, col, val, opts);
            }
            else if (type == typeof(Microsoft.Analytics.Types.Sql.SqlArray<sbyte>))
            {
                val = GetValueFromUsqlArray<sbyte>(row, col, val, opts);
            }
            else if (type == typeof(Microsoft.Analytics.Types.Sql.SqlArray<sbyte?>))
            {
                val = GetValueFromUsqlArray<sbyte?>(row, col, val, opts);
            }
            else if (type == typeof(Microsoft.Analytics.Types.Sql.SqlArray<uint>))
            {
                val = GetValueFromUsqlArray<uint>(row, col, val, opts);
            }
            else if (type == typeof(Microsoft.Analytics.Types.Sql.SqlArray<uint?>))
            {
                val = GetValueFromUsqlArray<uint?>(row, col, val, opts);
            }
            else if (type == typeof(Microsoft.Analytics.Types.Sql.SqlArray<ulong>))
            {
                val = GetValueFromUsqlArray<ulong>(row, col, val, opts);
            }
            else if (type == typeof(Microsoft.Analytics.Types.Sql.SqlArray<ulong?>))
            {
                val = GetValueFromUsqlArray<ulong?>(row, col, val, opts);
            }

            else if (type == typeof(Microsoft.Analytics.Types.Sql.SqlArray<ushort>))
            {
                val = GetValueFromUsqlArray<ushort>(row, col, val, opts);
            }
            else if (type == typeof(Microsoft.Analytics.Types.Sql.SqlArray<ushort?>))
            {
                val = GetValueFromUsqlArray<ushort?>(row, col, val, opts);
            }
            else if (type == typeof(Microsoft.Analytics.Types.Sql.SqlArray<bool>))
            {
                val = GetValueFromUsqlArray<bool>(row, col, val, opts);
            }
            else if (type == typeof(Microsoft.Analytics.Types.Sql.SqlArray<bool?>))
            {
                val = GetValueFromUsqlArray<bool?>(row, col, val, opts);
            }
            else if (type == typeof(Microsoft.Analytics.Types.Sql.SqlArray<DateTime>))
            {
                val = GetValueFromUsqlArray<DateTime>(row, col, val, opts);
            }
            else if (type == typeof(Microsoft.Analytics.Types.Sql.SqlArray<DateTime?>))
            {
                val = GetValueFromUsqlArray<DateTime?>(row, col, val, opts);
            }
            else if (type == typeof(Microsoft.Analytics.Types.Sql.SqlArray<Guid>))
            {
                val = GetValueFromUsqlArray<Guid>(row, col, val, opts);
            }
            else if (type == typeof(Microsoft.Analytics.Types.Sql.SqlArray<Guid?>))
            {
                val = GetValueFromUsqlArray<Guid?>(row, col, val, opts);
            }
            else
            {
            }
            return val;
        }

        public static string GetValueFromUsqlArray<T>(Microsoft.Analytics.Interfaces.IRow row, Microsoft.Analytics.Interfaces.IColumn col, string val, TypeDisplayNameOptions opts)
        {
            var arr = row.Get<Microsoft.Analytics.Types.Sql.SqlArray<T>>(col.Name);

            if (arr != null)
            {
                var sb = new System.Text.StringBuilder();
                sb.Append("SqlArray");
                if (opts.ShowGenericParameters)
                {
                    sb.Append("<");
                    sb.Append(UdoUtils.GetUsqlTypeDisplayName(typeof(T)));
                    sb.Append(">");
                }
                sb.Append("{ ");

                for (int j = 0; j < arr.Count; j++)
                {
                    if (j > 0)
                    {
                        sb.Append(", ");
                    }

                    sb.Append("\"");
                    sb.Append(arr[j].ToString());
                    sb.Append("\"");
                }

                sb.Append(" }");
                val = sb.ToString();
            }
            else
            {
                val = "NULL";
            }
            return val;
        }

        public static string GetValueFromUsqlMap<K, V>(Microsoft.Analytics.Interfaces.IRow row, Microsoft.Analytics.Interfaces.IColumn col, TypeDisplayNameOptions opts)
        {
            string val = null;

            var map = row.Get<Microsoft.Analytics.Types.Sql.SqlMap<K, V>>(col.Name);

            if (map != null)
            {
                var sb = new System.Text.StringBuilder();
                sb.Append("SqlMap");
                if (opts.ShowGenericParameters)
                {
                    sb.Append("<");
                    sb.Append(UdoUtils.GetUsqlTypeDisplayName(typeof(K)));
                    sb.Append(",");
                    sb.Append(" ");
                    sb.Append(UdoUtils.GetUsqlTypeDisplayName(typeof(V)));
                    sb.Append(">");
                }
                sb.Append("{ ");

                int kn = 0;
                foreach (var key in map.Keys)
                {
                    if (kn > 0)
                    {
                        sb.Append(";");
                        sb.Append(" ");
                    }

                    V xval = map[key];
                    string val_str = "NULL";
                    if (xval != null)
                    {
                        val_str = xval.ToString();
                    }

                    var key_str = key.ToString();
                    sb.AppendFormat("{0}={1}", key_str, val_str);

                    kn++;
                }

                sb.Append(" }");
                val = sb.ToString();
            }
            else
            {
                val = "NULL";
            }
            return val;
        }

        private static void test<T>(string left)
        {
            string actual_left = GetUsqlTypeDisplayName(typeof(T));
            if (left != actual_left)
            {
                throw new System.ArgumentOutOfRangeException("Incorrect type display");
            }
        }

        public static void TestTypeNames()
        {
            test<int>("int");
            test<int?>("int?");
            test<SqlArray<int>>("SqlArray");
            test<SqlArray<double>>("SqlArray");
            test<SqlMap<string,string>>("SqlMap");
        }
    }
}