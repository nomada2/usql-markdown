using System;
using System.Collections.Generic;

namespace MarkdownFormat
{
    public class TypeNameCache
    {
        private Dictionary<Type, string> Dictionary;

        public TypeNameCache()
        {
            this.Dictionary = new Dictionary<Type, string>();
            this.Dictionary = new Dictionary<Type, string>();
            this.Dictionary[typeof(string)] = "string";
            this.Dictionary[typeof(char)] = "char";
            this.Dictionary[typeof(char?)] = "char?";
            this.Dictionary[typeof(float)] = "float";
            this.Dictionary[typeof(float?)] = "float?";
            this.Dictionary[typeof(double)] = "double";
            this.Dictionary[typeof(double?)] = "double?";
            this.Dictionary[typeof(decimal)] = "decimal";
            this.Dictionary[typeof(decimal?)] = "decimal?";
            this.Dictionary[typeof(int)] = "int";
            this.Dictionary[typeof(int?)] = "int?";
            this.Dictionary[typeof(short)] = "short";
            this.Dictionary[typeof(short?)] = "short?";
            this.Dictionary[typeof(byte)] = "byte";
            this.Dictionary[typeof(byte?)] = "byte?";
            this.Dictionary[typeof(sbyte)] = "sbyte";
            this.Dictionary[typeof(sbyte?)] = "sbyte?";
            this.Dictionary[typeof(uint)] = "uint";
            this.Dictionary[typeof(uint?)] = "uint?";
            this.Dictionary[typeof(ulong)] = "ulong";
            this.Dictionary[typeof(ulong?)] = "ulong?";
            this.Dictionary[typeof(ushort)] = "ushort";
            this.Dictionary[typeof(ushort?)] = "ushort?";
            this.Dictionary[typeof(bool)] = "bool";
            this.Dictionary[typeof(bool?)] = "bool?";
            this.Dictionary[typeof(byte[])] = "byte[]";
            this.Dictionary[typeof(System.Guid)] = "Guid";
            this.Dictionary[typeof(System.DateTime)] = "DateTime";
        }

        public string GetDisplayName(System.Type type)
        {

            // If it is already cached, return it
            if (this.Dictionary.ContainsKey(type))
            {
                string dn = this.Dictionary[type];
                return dn;
            }

            // it's not cached

            // Check if it is one of the known U-SQL complex types

            if (type.IsGenericType)
            {
                var tokens = type.FullName.Split('`');

                if (tokens[0].StartsWith("Microsoft.Analytics.Types.Sql."))
                {
                    var tokens2 = tokens[0].Split('.');
                    string name = tokens2[tokens2.Length - 1];

                    if (name == "SqlArray" || name == "SqlMap")
                    {
                        this.Dictionary[type] = name;
                        return name;
                    }
                }
            }

            // Give up: use the full name
            return type.FullName;
        }
    }
}