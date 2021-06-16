using Formatter.Attributes;
using Formatter.Constants;

namespace Sample.Models
{
    [Formatted(FromZero = false)]
    public class ComplexPerson : Person
    {
        [Format(Line = 2)]
        public ContactDetails ContactDetails { get; set; }

    }

    [Formatted(FromZero = true)]
    public class ContactDetails
    {
        [Format(Offset = 0, Length = 25, Justified = Justified.LEFT, Case = Case.UPPER, Fill = "~")]
        public string Label { get; } = "Email";

        [Format(Offset = 25, Length = 30, Justified = Justified.RIGHT, Case = Case.LOWER, Fill = "~")]
        public string Email { get; set; }

        [Format(Line = 2)]
        public AddressDetails AddressDetails { get; set; }

        [Format(Line = 3)]
        public PhoneDetails PhoneDetails { get; set; }
    }

    [Formatted(FromZero = true)]
    public class AddressDetails
    {
        [Format(Offset = 0, Length = 25, Justified = Justified.LEFT, Case = Case.UPPER, Fill = "_")]
        public string Label { get; } = "Address";

        [Format(Offset = 25, Length = 50, Justified = Justified.RIGHT, Case = Case.LOWER, Fill = "_")]
        public string Address { get; set; }
    }

    [Formatted(FromZero = true)]
    public class PhoneDetails
    {
        [Format(Offset = 0, Length = 25, Justified = Justified.LEFT, Case = Case.UPPER, Fill = ".")]
        public string Label { get; } = "MobileNumber";

        [Format(Offset = 25, Length = 15, Justified = Justified.RIGHT, Case = Case.LOWER, Fill = ".")]
        public string MobileNumber { get; set; }
    }

}
