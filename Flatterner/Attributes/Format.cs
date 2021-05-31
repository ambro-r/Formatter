using Flattener.Constants;
using System;

namespace Flattener.Attributes
{
    public class Format : Attribute
    {

        public Justified Justified { get; set; } = Justified.LEFT;

        public string Fill { get; set; } = string.Empty;

        public int Offset { get; set; }

        public int Length { get; set; }

    }

}
