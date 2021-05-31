using Flattener.Attributes;
using Flattener.Constants;

namespace Sample.Models
{
    [Formatted(FromZero = false)]
    public class SimplePerson
    {

        [Format(Offset = 1, Length = 20, Justified = Justified.LEFT)]
        public string Name { get; set; }

        [Format(Offset = 21, Length = 20)]
        public string Surname { get; set; }

        [Format(Offset = 41, Length = 20, Justified = Justified.RIGHT, Fill = "0")]
        public string IdentityNumber { get; set; }

        public int Age { get; set; }

    }
}
