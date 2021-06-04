using Flattener.Attributes;
using Flattener.Constants;

namespace Sample.Models
{
    [Formatted(FromZero = false)]
    public class ComplexPerson : SimplePerson
    {
        [Format(Line = 3)]
        public ContactDetails ContactDetails { get; set; }

    }

    [Formatted(FromZero = true)]
    public class ContactDetails
    {
        [Format(Offset = 0, Length = 30, Fill = "#", Justified = Justified.RIGHT)]
        public string Email { get; set; }
        [Format(Offset = 30, Length = 15, Justified = Justified.RIGHT)]
        public string MobileNumber { get; set; }
    }


}
