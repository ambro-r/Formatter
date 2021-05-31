using Flattener.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sample.Models
{
    [Flatten(FromZero = true, Line = 1)]
    public class IncorrectLines
    {
        [Flat(Offset = 0, Length = 25)]
        public string Offset01 { get; set; }

        public Line02 Line02 { get; set; }

    }

    [Flatten(FromZero = true, Line = 1)]
    public class Line02
    {
        [Flat(Offset = 0, Length = 25)]
        public string Offset01 { get; set; }
    }

}
