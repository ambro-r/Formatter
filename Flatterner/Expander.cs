using Formatter.Attributes;
using Formatter.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Formatter
{
    public class Expander
    {
        private Expander() { }

        public static Expander Instance { get; } = new Expander();

        public U Expand<U>(U expand, string flatString)
        {
            int lineNumber = 0;
            return Expand(expand, GetLines(flatString), ref lineNumber);
        }

        private U Expand<U>(U expand, string[] lines, ref int lineNumber)
        {
            int internalLineCounter = 0; 
            Type type = expand.GetType();
            Formatted flatten = (Formatted)type.GetCustomAttribute(typeof(Formatted));
        
            int offsetAdjustment = flatten.FromZero ? 0 : 1;
            if (flatten != null)
            {          
                IOrderedEnumerable<PropertyInfo> properties = FormatHelper.GetOrderedFormats(type);
                foreach (PropertyInfo property in properties)
                {
                    if (FormatHelper.isFormattedType(property))
                    {
                        lineNumber++;
                        property.SetValue(expand, Expand(Activator.CreateInstance(property.PropertyType), lines, ref lineNumber), null);                        
                    }
                    else
                    {
                        Format format = FormatHelper.GetFormat(property);
                        string piece = GetPiece(format, lines[lineNumber], offsetAdjustment);
                        if (property.CanWrite)
                        {
                            property.SetValue(expand, Convert.ChangeType(piece, property.PropertyType), null);
                        }
                    }
                }

            }
            return expand;
        }

        private string[] GetLines(string flatString)
        {
            string[] lines = flatString.Split(new[] { "\r\n", "\r", "\n", Environment.NewLine }, StringSplitOptions.None);
            return lines;
        }

        private string GetPiece(Format format, string flatString, int offsetAdjustment)
        {
            string piece = flatString.Substring(format.Offset - offsetAdjustment, format.Length);
            char fill = format.Fill.Length > 0 ? format.Fill[0] : ' ';
            if (format.Justified == Constants.Justified.LEFT)
            {
                piece = piece.TrimEnd(fill);
            }
            else if (format.Justified == Constants.Justified.RIGHT)
            {
                piece = piece.TrimStart(fill);
            }
            return piece.Trim();
        }

    }
}
