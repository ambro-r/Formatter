using Flattener.Attributes;
using Formatter.Exceptions;
using Formatter.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Formatter
{
    public class Flattener
    {
        private Flattener() { }

        public static Flattener Instance { get; } = new Flattener();

        public string Flatten<T>(T objectToFlatten)
        {
            return GetFormattedString(GetSortedLines<T>(objectToFlatten));
        }

        private string GetFormattedString(SortedDictionary<int, Line> lines)
        {
            string returnString = string.Empty;

            for (int i = 1; i <= lines.Keys.Max(); i++)
            {
                if (i > 1) returnString = returnString + Environment.NewLine;
                if (lines.ContainsKey(i))
                {
                    string flatString = string.Empty;

                    if (lines[i].SortedLines?.Count > 0)
                    {
                        flatString = GetFormattedString(lines[i].SortedLines);
                    }
                    else
                    {
                        foreach (KeyValuePair<int, string> entry in lines[i].SortedStrings)
                        {
                            int offsetAdjustment = lines[i].FromZero ? 0 : 1;
                            if ((entry.Key - offsetAdjustment) > flatString.Length)
                            {
                                throw new Exceptions.FlattenerException(string.Format("Next offest expected at {0}, but {1} specified.", flatString.Length, (entry.Key - offsetAdjustment)));
                            }
                            else if ((entry.Key - offsetAdjustment) < flatString.Length)
                            {
                                throw new Exceptions.FlattenerException(string.Format("Offest {0} specified, but {1} expected.", (entry.Key - offsetAdjustment), flatString.Length));
                            }
                            else
                            {
                                flatString = flatString + entry.Value;
                            }
                        }
                    }
                    returnString = returnString + flatString;
                }
            }
            return returnString;
        }

        private SortedDictionary<int, Line> GetSortedLines<T>(T objectToFlatten)
        {
            SortedDictionary<int, Line> lines = new SortedDictionary<int, Line>();
            Type type = objectToFlatten.GetType();
            Formatted flatten = (Formatted)type.GetCustomAttribute(typeof(Formatted));
            if (flatten != null)
            {                
                PropertyInfo[] properties = type.GetProperties();
                foreach (PropertyInfo property in properties)
                {
                    Format flat = (Format)property.GetCustomAttribute(typeof(Format), false);
                    if (flat != null)
                    {
                        string formattedString = FormatString(property.GetValue(objectToFlatten).ToString(), flat);
                        int lineNumber = flat.Line > 0 ? flat.Line : 1;

                        if (!lines.ContainsKey(lineNumber))
                        {
                            Line line = new Line()
                            {
                                LineNumber = lineNumber,
                                FromZero = flatten.FromZero
                            };
                            lines.Add(lineNumber, line);
                        }

                        if (property.PropertyType.GetCustomAttribute(typeof(Formatted)) != null)
                        {
                            lines[lineNumber].SortedLines = GetSortedLines(property.GetValue(objectToFlatten));
                        }
                        else
                        {
                            lines[lineNumber].AddFormattedString(flat.Offset, formattedString);
                        }                        
                    }

                                  
                }
            }
            return lines;
        }

        private string FormatString(string value, Format flat)
        {
            string formattedString = string.Empty;
            if(value.Length < flat.Length)
            {
                char fill = flat.Fill.Length > 0 ? flat.Fill[0] : ' ';
                if (flat.Justified == global::Flattener.Constants.Justified.RIGHT)
                {
                    formattedString = value.PadLeft(flat.Length, fill);
                } else
                {
                    formattedString = value.PadRight(flat.Length, fill);                    
                }                
            } else
            {
                if (flat.Justified == global::Flattener.Constants.Justified.RIGHT)
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
