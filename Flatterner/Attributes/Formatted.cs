using System;

namespace Formatter.Attributes
{

    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class Formatted : Attribute
    {
        public bool FromZero { get; set; } = false;        

    }

}
