using Flattener.Attributes;
using Flattener.Constants;

namespace Sample.Models
{
    [Flatten(FromZero = false)]
    public class ComplexPerson : SimplePerson
    {

        public ContactDetails ContactDetails { get; set; }

    }

    [Flatten(FromZero = true)]
    public class ContactDetails
    {
        [Flat(Offset = 1, Length = 25)]
        public string Email { get; set; }
        [Flat(Offset = 26, Length = 15, Justified = Justified.RIGHT)]
        public string MobileNumber { get; set; }

    }


}
