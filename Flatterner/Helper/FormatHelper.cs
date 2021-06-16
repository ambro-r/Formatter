using Formatter.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Formatter.Helper
{
    class FormatHelper
    {

        public static IOrderedEnumerable<PropertyInfo> GetOrderedFormats(Type type) {
            return type.GetProperties().Where(p => ((Format)p.GetCustomAttribute(typeof(Format), false))?.Line > 0).OrderBy(p => ((Format)p.GetCustomAttribute(typeof(Format), false))?.Line).ThenBy(p => ((Format)p.GetCustomAttribute(typeof(Format), false))?.Offset);
        }

        public static Format GetFormat(PropertyInfo property)
        {
            return (Format)property.GetCustomAttribute(typeof(Format), false);
        }

        public static bool isFormattedType(PropertyInfo property)
        {
            return property.PropertyType.GetCustomAttribute(typeof(Formatted)) != null;
        }

    }
}
