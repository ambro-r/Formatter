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
            Type type = expand.GetType();
            Formatted flatten = (Formatted)type.GetCustomAttribute(typeof(Formatted));
            int offsetAdjustment = flatten.FromZero ? 0 : 1;
            if (flatten != null)
            {
                IOrderedEnumerable<PropertyInfo> properties = FormatHelper.GetOrderedFormats(type);
                foreach (PropertyInfo property in properties)
                {
                    Format format = FormatHelper.GetFormat(property);
                    string piece = GetPiece(format, flatString, offsetAdjustment);
                    property.SetValue(expand, Convert.ChangeType(piece, property.PropertyType), null);
                }

            }
            return expand;
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
