using Formatter.Attributes;
using Formatter.Exceptions;
using Formatter.Models;
using System;
using System.Collections;
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
            if (lines.Count > 0)
            {
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
                        else if (property.PropertyType.IsGenericType && typeof(IEnumerable).IsAssignableFrom(property.PropertyType))
                        {
                            IEnumerable? items = (IEnumerable) property.GetValue(objectToFlatten);                            
                            foreach(var item in items)
                            {
                                int startingPosition = (lines[lineNumber].SortedLines.Keys.Count > 0 ? lines[lineNumber].SortedLines.Keys.Max() : 0);
                                foreach (KeyValuePair<int, Line> entry in GetSortedLines(item))
                                {                                    
                                    lines[lineNumber].SortedLines.Add(startingPosition + entry.Key, entry.Value);
                                }
                            }                            
                        }
                        else
                        {
                            string formattedString = FormatString(property.GetValue(objectToFlatten).ToString(), flat);
                            int offSet = flat.Offset == 0 && !flatten.FromZero ? 1 : flat.Offset;
                            lines[lineNumber].AddFormattedString(offSet, formattedString);
                        }                        
                    }

                                  
                }
            }
            return lines;
        }

        private string FormatString(string value, Format format)
        {
            string formattedString = string.Empty;
            if(value.Length < format.Length)
            {
                char fill = format.Fill.Length > 0 ? format.Fill[0] : ' ';
                if (format.Justified == Constants.Justified.RIGHT)
                {
                    formattedString = value.PadLeft(format.Length, fill);
                } else
                {
                    formattedString = value.PadRight(format.Length, fill);                    
                }                
            } else
            {
                if (format.Justified == Constants.Justified.RIGHT)
                {
                    formattedString = value.Substring(value.Length - format.Length);
                }
                else
                {
                    formattedString = value.Substring(0, format.Length);
                }
            }
            if(format.Case == Constants.Case.UPPER)
            {
                formattedString = formattedString.ToUpper();
            } else if (format.Case == Constants.Case.LOWER)
            {
                formattedString = formattedString.ToLower();
            }
            return formattedString;
        }

    }
}
