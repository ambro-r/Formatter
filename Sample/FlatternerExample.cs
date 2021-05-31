using Flatterner.Exceptions;
using Newtonsoft.Json;
using Sample.Models;
using System;

namespace Sample
{

    public class FlatternerExample
    {
        private SimplePerson SimplePerson;
        private ComplexPerson ComplexPerson;
        private DuplicateOffset DuplicateOffset;
        private IncorrectLines IncorrectLines;

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

            DuplicateOffset = new DuplicateOffset()
            {
                Offset01 = "Not Important",
                Offset02 = "Also Not Important",
            };

            IncorrectLines = new IncorrectLines()
            {
                Offset01 = "Not Important",
                Line02 = new Line02()
                {
                    Offset01 = "Also Not Important"
                }
            };
        }

        public void RunSimpleExample()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(string.Format("{0}Simple Person Example:{1}", Environment.NewLine, Environment.NewLine));
            Console.WriteLine(string.Format("Direct Serializing:{0}{1}", Environment.NewLine, JsonConvert.SerializeObject(SimplePerson)));
            Console.WriteLine(string.Format("Applying the Flattener:{0}{1}", Environment.NewLine, Flatterner.Flatterner.Instance.Flatten(SimplePerson)));

            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(string.Format("{0}Complex Person Example:{1}", Environment.NewLine, Environment.NewLine));
            Console.WriteLine(string.Format("Direct Serializing:{0}{1}", Environment.NewLine, JsonConvert.SerializeObject(ComplexPerson)));
            Console.WriteLine(string.Format("Applying the Flattener:{0}{1}", Environment.NewLine, Flatterner.Flatterner.Instance.Flatten(ComplexPerson)));

            Console.ForegroundColor = ConsoleColor.Gray;
            try
            {
                Console.WriteLine(string.Format("{0}Duplicate Offset Example:{1}", Environment.NewLine, Environment.NewLine));
                Console.WriteLine(string.Format("Direct Serializing:{0}{1}", Environment.NewLine, JsonConvert.SerializeObject(DuplicateOffset)));
                Console.WriteLine(string.Format("Applying the Flattener:{0}{1}", Environment.NewLine, Flatterner.Flatterner.Instance.Flatten(DuplicateOffset)));
            } catch (FlatternerException fe)
            {
                Console.WriteLine(string.Format("Exception:{0}{1}", Environment.NewLine, fe.Message));
            }

            Console.ForegroundColor = ConsoleColor.Red;
            try
            {
                Console.WriteLine(string.Format("{0}Incorrect Lines Example:{1}", Environment.NewLine, Environment.NewLine));
                Console.WriteLine(string.Format("Direct Serializing:{0}{1}", Environment.NewLine, JsonConvert.SerializeObject(IncorrectLines)));
                Console.WriteLine(string.Format("Applying the Flattener:{0}{1}", Environment.NewLine, Flatterner.Flatterner.Instance.Flatten(IncorrectLines)));
            }
            catch (FlatternerException fe)
            {
                Console.WriteLine(string.Format("Exception:{0}{1}", Environment.NewLine, fe.Message));
            }

        }

    }
}
