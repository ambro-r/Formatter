using Formatter.Attributes;
using Formatter.Constants;
using System;
using System.Collections.Generic;

namespace Sample.Models
{
    [Formatted(FromZero = false)]
    public class People
    {
        [Format(Line = 1, Length = 25, Justified = Justified.LEFT, Case = Case.UPPER, Fill = ".")]
        public String Header { get; set; } = "Header Line";

        [Format(Line = 2)]
        public List<Person> Persons { get; set; } = new List<Person>();

        [Format(Line = 3, Length = 25, Justified = Justified.RIGHT, Case = Case.UPPER, Fill = ".")]
        public String Footer { get; set; } = "Footer Line";
    }
}
