using Newtonsoft.Json;
using Sample.Models;
using System;

namespace Sample
{

    public class FlatternerExample
    {
        private SimplePerson SimplePerson;
        private ComplexPerson ComplexPerson;

        public FlatternerExample()
        {
            SimplePerson = new SimplePerson()
            {
                Name = "John",
                Surname = "Simple",
                IdentityNumber = "A123-SIMPLE",
                Age = 40
            };

            ComplexPerson = new ComplexPerson()
            {
                Name = "John",
                Surname = "Complex",
                IdentityNumber = "A123-COMPLEX",
                Age = 40,
                ContactDetails = new ContactDetails()
                {
                    Email = "my.email@simpleperson.co.za",
                    MobileNumber = "+88-123-1234"
                }
            };
        }

        public void RunSimpleExample()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(string.Format("{0}Simple Person Example:{1}", Environment.NewLine, Environment.NewLine));
            Console.WriteLine(string.Format("Direct Serializing:{0}{1}", Environment.NewLine, JsonConvert.SerializeObject(SimplePerson)));
            Console.WriteLine(string.Format("Applying the Flattener:{0}{1}", Environment.NewLine, Flatterner.Flatterner.Instance.Flattern(SimplePerson)));

            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(string.Format("{0}Complex Person Example:{1}", Environment.NewLine, Environment.NewLine));
            Console.WriteLine(string.Format("Direct Serializing:{0}{1}", Environment.NewLine, JsonConvert.SerializeObject(ComplexPerson)));
            Console.WriteLine(string.Format("Applying the Flattener:{0}{1}", Environment.NewLine, Flatterner.Flatterner.Instance.Flattern(ComplexPerson)));
        }

    }
}
