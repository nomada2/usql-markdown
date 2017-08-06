using System;

namespace MarkdownFormat
{
    public static class UdoUtils
    {
        public static string get_usql_type_name(System.Type coltype)
        {
            if (coltype == typeof(string))
            {
                return "string";
            }
            else if (coltype == typeof(char))
            {
                return "char";
            }
            else if (coltype == typeof(float))
            {
                return "float";
            }
            else if (coltype == typeof(double))
            {
                return "double";
            }
            else if (coltype == typeof(int))
            {
                return "int";
            }
            else if (coltype == typeof(long))
            {
                return "long";
            }
            else if (coltype == typeof(System.Guid))
            {
                return "Guid";
            }
            else if (coltype == typeof(int?))
            {
                return "int?";
            }
            else if (coltype == typeof(long?))
            {
                return "long?";
            }
            else if (coltype == typeof(float?))
            {
                return "float?";
            }
            else if (coltype == typeof(double?))
            {
                return "double?";
            }
            else
            {
                return coltype.Name.Replace("`", "-");
            }
        }

        public static string GetValueDisplayString(Microsoft.Analytics.Interfaces.IRow row, Type coltype, string val, Microsoft.Analytics.Interfaces.IColumn col, bool _ComplexTypeParameters)
        {
            if (coltype == typeof(string))
            {
                val = row.Get<string>(col.Name);
                val = val ?? "NULL";
            }
            else if (coltype == typeof(bool))
            {
                val = row.Get<bool>(col.Name).ToString();
            }
            else if (coltype == typeof(char))
            {
                val = row.Get<char>(col.Name).ToString();
            }
            else if (coltype == typeof(float))
            {
                val = row.Get<float>(col.Name).ToString();
            }
            else if (coltype == typeof(double))
            {
                val = row.Get<double>(col.Name).ToString();
            }
            else if (coltype == typeof(int))
            {
                val = row.Get<int>(col.Name).ToString();
            }
            else if (coltype == typeof(long))
            {
                val = row.Get<long>(col.Name).ToString();
            }
            else if (coltype == typeof(System.Guid))
            {
                val = row.Get<System.Guid>(col.Name).ToString();
            }
            else if (coltype == typeof(int?))
            {
                val = row.Get<int?>(col.Name).ToString();
                val = val ?? "NULL";
            }
            else if (coltype == typeof(long?))
            {
                val = row.Get<long?>(col.Name).ToString();
                val = val ?? "NULL";
            }
            else if (coltype == typeof(float?))
            {
                val = row.Get<float?>(col.Name).ToString();
                val = val ?? "NULL";
            }
            else if (coltype == typeof(double?))
            {
                val = row.Get<double?>(col.Name).ToString();
                val = val ?? "NULL";
            }
            else if (coltype == typeof(Microsoft.Analytics.Types.Sql.SqlArray<string>))
            {
                val = _Get_val_from_usqlarray<string>(row, col, val, _ComplexTypeParameters);
            }
            else if (coltype == typeof(Microsoft.Analytics.Types.Sql.SqlArray<char>))
            {
                val = _Get_val_from_usqlarray<char>(row, col, val, _ComplexTypeParameters);
            }
            else if (coltype == typeof(Microsoft.Analytics.Types.Sql.SqlArray<int>))
            {
                val = _Get_val_from_usqlarray<int>(row, col, val, _ComplexTypeParameters);
            }
            else if (coltype == typeof(Microsoft.Analytics.Types.Sql.SqlArray<long>))
            {
                val = _Get_val_from_usqlarray<long>(row, col, val, _ComplexTypeParameters);
            }
            else if (coltype == typeof(Microsoft.Analytics.Types.Sql.SqlMap<string, string>))
            {
                val = _Get_val_from_usqlmap<string, string>(row, col, val, _ComplexTypeParameters);
            }
            else if (coltype == typeof(Microsoft.Analytics.Types.Sql.SqlMap<string, bool?>))
            {
                val = _Get_val_from_usqlmap<string, bool?>(row, col, val, _ComplexTypeParameters);
            }
            else if (coltype == typeof(Microsoft.Analytics.Types.Sql.SqlMap<string, char?>))
            {
                val = _Get_val_from_usqlmap<string, char?>(row, col, val, _ComplexTypeParameters);
            }
            else if (coltype == typeof(Microsoft.Analytics.Types.Sql.SqlMap<string, int?>))
            {
                val = _Get_val_from_usqlmap<string, int?>(row, col, val, _ComplexTypeParameters);
            }
            else if (coltype == typeof(Microsoft.Analytics.Types.Sql.SqlMap<string, long?>))
            {
                val = _Get_val_from_usqlmap<string, long?>(row, col, val, _ComplexTypeParameters);
            }
            else
            {
                val = "UNKNOWNTYPE:" + UdoUtils.get_usql_type_name(coltype);
            }
            return val;
        }

        public static string _Get_val_from_usqlarray<T>(Microsoft.Analytics.Interfaces.IRow row, Microsoft.Analytics.Interfaces.IColumn col, string val, bool _ComplexTypeParameters)
        {
            var arr = row.Get<Microsoft.Analytics.Types.Sql.SqlArray<T>>(col.Name);

            if (arr != null)
            {
                var sb = new System.Text.StringBuilder();
                sb.Append("SqlArray");
                if (_ComplexTypeParameters)
                {
                    sb.Append("<");
                    sb.Append(UdoUtils.get_usql_type_name(typeof(T)));
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

        public static string _Get_val_from_usqlmap<K, V>(Microsoft.Analytics.Interfaces.IRow row, Microsoft.Analytics.Interfaces.IColumn col, string val, bool _ComplexTypeParameters)
        {
            var map = row.Get<Microsoft.Analytics.Types.Sql.SqlMap<K, V>>(col.Name);

            if (map != null)
            {
                var sb = new System.Text.StringBuilder();
                sb.Append("SqlMap");
                if (_ComplexTypeParameters)
                {
                    sb.Append("<");
                    sb.Append(UdoUtils.get_usql_type_name(typeof(K)));
                    sb.Append(", ");
                    sb.Append(UdoUtils.get_usql_type_name(typeof(V)));
                    sb.Append(">");
                }
                sb.Append("{ ");

                int kn = 0;
                foreach (var key in map.Keys)
                {
                    if (kn > 0)
                    {
                        sb.Append("; ");
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

    }
}