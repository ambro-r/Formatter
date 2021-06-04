using Flattener.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sample.Models
{
    [Formatted(FromZero = true)]
    public class IncorrectLines
    {
        [Format(Offset = 0, Length = 25, Line = 1)]
        public string Offset01 { get; set; }

        [Format(Line = 1)]
        public Line02 Line02 { get; set; }

    }

    [Formatted(FromZero = true)]
    public class Line02
    {
        [Format(Offset = 0, Length = 25)]
        public string Offset01 { get; set; }
    }

}
