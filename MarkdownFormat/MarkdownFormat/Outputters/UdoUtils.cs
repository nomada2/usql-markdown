using System;
using Microsoft.Analytics.Interfaces;
using Microsoft.Analytics.Types.Sql;

namespace MarkdownFormat
{
    public static class UdoUtils
    {
        public static TypeNameCache TypeNameDic;
        private static string _LESSTHAN = "<";
        private static string _GREATERTHAN = ">";
        private static string _DOUBLEQUOTE = "\"";
        private static string _SPACE = " ";
        private static string _LEFTCURLYBRACE = "{";
        private static string _RIGHTCURLYBRACE = "}";
        private static string _COMMA = ",";
        private static string _SEMICOLON = ";";
        private static string _NULL = "NULL";

        public static string GetUsqlTypeDisplayName(System.Type type)
        {
            if (TypeNameDic == null)
            {
                TypeNameDic = new TypeNameCache();
            }

            return TypeNameDic.GetDisplayName(type);
        }

        public static string GetValueDisplayString(Microsoft.Analytics.Interfaces.IRow row, Type type, string val, Microsoft.Analytics.Interfaces.IColumn col, TypeDisplayNameOptions opts)
        {
            if (type == typeof(string))
            {
                val = row.Get<string>(col.Name) ?? _NULL;
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
                val = row.Get<int?>(col.Name).ToString();
            }
            else if (type == typeof(long?))
            {
                val = row.Get<long?>(col.Name).ToString();
            }
            else if (type == typeof(float?))
            {
                val = row.Get<float?>(col.Name).ToString();
            }
            else if (type == typeof(double?))
            {
                val = row.Get<double?>(col.Name).ToString();
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
                    sb.Append(_LESSTHAN);
                    sb.Append(UdoUtils.GetUsqlTypeDisplayName(typeof(T)));
                    sb.Append(_GREATERTHAN);
                }
                sb.Append(_LEFTCURLYBRACE);
                sb.Append(_SPACE);

                for (int j = 0; j < arr.Count; j++)
                {
                    if (j > 0)
                    {
                        sb.Append(_COMMA);
                        sb.Append(_SPACE);
                    }

                    sb.Append(_DOUBLEQUOTE);
                    sb.Append(arr[j].ToString());
                    sb.Append(_DOUBLEQUOTE);
                }

                sb.Append(_SPACE);
                sb.Append(_RIGHTCURLYBRACE);

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
                    sb.Append(_LESSTHAN);
                    sb.Append(UdoUtils.GetUsqlTypeDisplayName(typeof(K)));
                    sb.Append(_COMMA);
                    sb.Append(_SPACE);
                    sb.Append(UdoUtils.GetUsqlTypeDisplayName(typeof(V)));
                    sb.Append(_GREATERTHAN);
                }
                sb.Append(_LEFTCURLYBRACE);
                sb.Append(_SPACE);

                int kn = 0;
                foreach (var key in map.Keys)
                {
                    if (kn > 0)
                    {
                        sb.Append(_SEMICOLON);
                        sb.Append(_SPACE);
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
                sb.Append(_SPACE);
                sb.Append(_RIGHTCURLYBRACE);
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