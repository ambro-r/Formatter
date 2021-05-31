using Flattener.Attributes;
using Flatterner.Exceptions;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Flatterner
{
    public class Flatterner
    {
        private Flatterner() { }

        public static Flatterner Instance { get; } = new Flatterner();

        public string Flattern<T>(T objectToFlatten)
        {
            string flatString = string.Empty;
            Type type = objectToFlatten.GetType();
            Flatten flatten = (Flatten)type.GetCustomAttribute(typeof(Flatten));
            if (flatten != null)
            {
                PropertyInfo[] properties = type.GetProperties();
                SortedDictionary<int, string> sortedStrings = new SortedDictionary<int, string>();
                foreach (PropertyInfo property in properties)
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
                foreach (KeyValuePair<int, string> entry in sortedStrings)
                {
                    int offsetAdjustment = flatten.FromZero ? 0 : 1;
                    if ((entry.Key - offsetAdjustment) > flatString.Length)
                    {
                        throw new FlatternerException(string.Format("Next offest expected at {0}, but {1} specified.", flatString.Length, (entry.Key - offsetAdjustment)));
                    } else if ((entry.Key - offsetAdjustment) < flatString.Length)
                    {
                        throw new FlatternerException(string.Format("Offest {0} specified, but {1} expected.", (entry.Key - offsetAdjustment), flatString.Length));
                    } else
                    {
                        flatString = flatString + entry.Value;
                    }
                 
                }                
            }
            return flatString;
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
