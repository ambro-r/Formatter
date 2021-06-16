using Formatter.Attributes;
using Formatter.Helper;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Formatter
{
    public class Flattener
    {
        private Flattener() { }

        public static Flattener Instance { get; } = new Flattener();

        public string Flatten<T>(T objectToFlatten)
        {
            return GetFlattenedString(GetSortedLines(objectToFlatten));
        }

        //TODO: Need to cater for "skipped" line numbers (i.e. 01 - Text, 02 - Blank, 03 - Text)
        private string GetFlattenedString(SortedDictionary<int, string> lines)
        {
            string flattenedString = string.Empty;
            for (int i = 1; i <= lines.Keys.Max(); i++)
            {
                if (i > 1) flattenedString = flattenedString + Environment.NewLine;
                flattenedString = flattenedString + lines[i];
            }
            return flattenedString;
        }

        private SortedDictionary<int, string> GetSortedLines<T>(T objectToFlatten)
        {
            SortedDictionary<int, string> lines = new SortedDictionary<int, string>();
            Type type = objectToFlatten.GetType();
            Formatted flatten = (Formatted)type.GetCustomAttribute(typeof(Formatted));
            int offsetAdjustment = flatten.FromZero ? 0 : 1;
            if (flatten != null)
            {
                IOrderedEnumerable<PropertyInfo> properties = FormatHelper.GetOrderedFormats(type);
                foreach (PropertyInfo property in properties)
                {
                    Format flat = FormatHelper.GetFormat(property);
                    if (!lines.ContainsKey(flat.Line)) lines.Add(flat.Line, string.Empty);
                    if (flat != null)
                    {
                        if (FormatHelper.isFormattedType(property))
                        {
                            lines[flat.Line] = lines[flat.Line] + GetFlattenedString(GetSortedLines(property.GetValue(objectToFlatten)));
                        }
                        else if (FormatHelper.isIEnumerable(property))
                        {
                            IEnumerable? items = (IEnumerable)property.GetValue(objectToFlatten);
                            int lineCounter = 1;
                            foreach (var item in items)
                            {
                                if (lineCounter > 1) lines[flat.Line] = lines[flat.Line] + Environment.NewLine;
                                lines[flat.Line] = lines[flat.Line] + GetFlattenedString(GetSortedLines(item));
                                lineCounter++;
                            }
                        }
                        else
                        {
                            if ((flat.Offset - offsetAdjustment) > lines[flat.Line].Length)
                            {
                                throw new Exceptions.FlattenerException(string.Format("Next offest expected at {0}, but {1} specified.", lines[flat.Line].Length, (flat.Offset - offsetAdjustment)));
                            }
                            else if ((flat.Offset - offsetAdjustment) < lines[flat.Line].Length)
                            {
                                throw new Exceptions.FlattenerException(string.Format("Offest {0} specified, but {1} expected.", (flat.Offset - offsetAdjustment), lines[flat.Line].Length));
                            }
                            else
                            {
                                lines[flat.Line] = lines[flat.Line] + FormatString(property.GetValue(objectToFlatten).ToString(), flat);
                            }
                        }
                    }
                }
            }
            return lines;
        }

        private string FormatString(string value, Format format)
        {
            string formattedString = string.Empty;
            if (value.Length < format.Length)
            {
                char fill = format.Fill.Length > 0 ? format.Fill[0] : ' ';
                if (format.Justified == Constants.Justified.RIGHT)
                {
                    formattedString = value.PadLeft(format.Length, fill);
                }
                else
                {
                    formattedString = value.PadRight(format.Length, fill);
                }
            }
            else
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
            if (format.Case == Constants.Case.UPPER)
            {
                formattedString = formattedString.ToUpper();
            }
            else if (format.Case == Constants.Case.LOWER)
            {
                formattedString = formattedString.ToLower();
            }
            return formattedString;
        }

    }
}
