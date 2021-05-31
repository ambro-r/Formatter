using System;

namespace Flattener.Attributes
{

    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class Formatted : Attribute
    {
        public bool FromZero { get; set; } = false;

        public int Line { get; set; } = 1;

    }

}
