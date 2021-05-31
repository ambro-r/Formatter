using Flattener.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sample.Models
{
    [Formatted(FromZero = true, Line = 1)]
    public class IncorrectLines
    {
        [Format(Offset = 0, Length = 25)]
        public string Offset01 { get; set; }

        public Line02 Line02 { get; set; }

    }

    [Formatted(FromZero = true, Line = 1)]
    public class Line02
    {
        [Format(Offset = 0, Length = 25)]
        public string Offset01 { get; set; }
    }

}
