using Formatter.Attributes;
using Formatter.Constants;

namespace Sample.Models
{
    [Formatted]
    public class DuplicateOffset
    {
        [Format(Offset = 1, Length = 25)]
        public string Offset01 { get; set; } = "Offset 01";
        [Format(Offset = 1, Length = 15, Justified = Justified.RIGHT)]
        public string DuplicateOffset01 { get; set; } = "Duplicate Offset 01";
    }
}
