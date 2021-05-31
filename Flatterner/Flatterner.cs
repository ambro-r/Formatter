using Flattener.Attributes;
using Flatterner.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Flatterner
{
    public class Flatterner
    {
        private Flatterner() { }

        public static Flatterner Instance { get; } = new Flatterner();

        public string Flatten<T>(T objectToFlatten)
        {
            SortedDictionary<int, string> lines = new SortedDictionary<int, string>();
            FlattenObject<T>(objectToFlatten, ref lines);
            string flatString = string.Empty;
            for(int i = 1; i <= lines.Keys.Max(); i++)
            {
                if (i > 1) flatString = flatString + Environment.NewLine;
                if (lines.ContainsKey(i))
                {
                    flatString = flatString + lines[i];
                }
            }
            return flatString;
        }

        private void FlattenObject<T>(T objectToFlatten, ref SortedDictionary<int, string> lines)
        {            
            Type type = objectToFlatten.GetType();
            Flatten flatten = (Flatten)type.GetCustomAttribute(typeof(Flatten));
            if (flatten != null)
            {
                if (lines.ContainsKey(flatten.Line))
                {
                    throw new FlatternerException(string.Format("Line {0} already defined, duplicate lines not allowed.", flatten.Line));
                }
                else
                {
                    SortedDictionary<int, string> sortedStrings = new SortedDictionary<int, string>();
                    PropertyInfo[] properties = type.GetProperties();
                    foreach (PropertyInfo property in properties)
                    {
                        if (property.PropertyType.GetCustomAttribute(typeof(Flatten)) != null)
                        {
                            FlattenObject(property.GetValue(objectToFlatten), ref lines);
                        }
                        else
                        {
                            Flat flat = (Flat)property.GetCustomAttribute(typeof(Flat), false);
                            if (flat != null)
                            {
                                string formattedString = FormatString(property.GetValue(objectToFlatten).ToString(), flat);
                                if (sortedStrings.ContainsKey(flat.Offset))
                                {
                                    throw new FlatternerException(string.Format("Offest {0} already defined, duplicate offests not allowed.", flat.Offset));
                                }
                                else
                                {
                                    sortedStrings.Add(flat.Offset, formattedString);
                                }
                            }
                        }

                    }

                    string flatString = string.Empty;
                    foreach (KeyValuePair<int, string> entry in sortedStrings)
                    {
                        int offsetAdjustment = flatten.FromZero ? 0 : 1;
                        if ((entry.Key - offsetAdjustment) > flatString.Length)
                        {
                            throw new FlatternerException(string.Format("Next offest expected at {0}, but {1} specified.", flatString.Length, (entry.Key - offsetAdjustment)));
                        }
                        else if ((entry.Key - offsetAdjustment) < flatString.Length)
                        {
                            throw new FlatternerException(string.Format("Offest {0} specified, but {1} expected.", (entry.Key - offsetAdjustment), flatString.Length));
                        }
                        else
                        {
                            flatString = flatString + entry.Value;
                        }
                    }
                    lines.Add(flatten.Line, flatString);
                }
            }
        }

        private string FormatString(string value, Flat flat)
        {
            string formattedString = string.Empty;
            if(value.Length < flat.Length)
            {
                char fill = flat.Fill.Length > 0 ? flat.Fill[0] : ' ';
                if (flat.Justified == Flattener.Constants.Justified.RIGHT)
                {
                    formattedString = value.PadLeft(flat.Length, fill);
                } else
                {
                    formattedString = value.PadRight(flat.Length, fill);                    
                }                
            } else
            {
                if (flat.Justified == Flattener.Constants.Justified.RIGHT)
                {
                    formattedString = value.Substring(value.Length - flat.Length);
                }
                else
                {
                    formattedString = value.Substring(0, flat.Length);
                }
            }
            return formattedString;
        }

    }
}
