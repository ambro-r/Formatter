using Formatter.Constants;
using System;

namespace Formatter.Attributes
{
    public class Format : Attribute
    {
        public Justified Justified { get; set; } = Justified.LEFT;

        public Case Case { get; set; } = Case.DEFAULT;

        public string Fill { get; set; } = string.Empty;

        public int Offset { get; set; } = 1;

        public int Length { get; set; }

        public int Line { get; set; } = 1;

    }

}
