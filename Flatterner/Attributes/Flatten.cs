using System;

namespace Flattener.Attributes
{

    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class Flatten : Attribute
    {
        public bool FromZero { get; set; } = false;

    }

}
