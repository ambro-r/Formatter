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
                    Format flat = FormatHelper.GetFormat(property);
                }

            }
            return expand;
        }
    }
}
