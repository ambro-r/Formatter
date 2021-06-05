using Formatter.Attributes;

namespace Sample.Models
{
    [Formatted(FromZero = true)]
    public class IncorrectLines
    {
        [Format(Line = 1, Offset = 0, Length = 25)]
        public string Line01 { get; set; } = "Line 01";

        [Format(Line = 1)]
        public string DuplicateLine01 { get; set; } = "Duplicate Line 01";

    }

}
