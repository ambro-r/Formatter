using Flattener.Attributes;
using Flattener.Constants;

namespace Sample.Models
{
    [Flatten(FromZero = false)]
    public class ComplexPerson : SimplePerson
    {

        public ContactDetails ContactDetails { get; set; }

    }

    [Flatten(FromZero = true, Line = 2)]
    public class ContactDetails
    {
        [Flat(Offset = 0, Length = 25)]
        public string Email { get; set; }
        [Flat(Offset = 25, Length = 15, Justified = Justified.RIGHT)]
        public string MobileNumber { get; set; }

    }


}
