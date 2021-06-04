using Formatter.Exceptions;
using Newtonsoft.Json;
using Sample.Models;
using System;

namespace Sample
{

    public class ObjectToStringExample
    {
        private Person Person;
        private ComplexPerson DetailedPerson;
        private People People;

        public ObjectToStringExample()
        {
            Person = new Person()
            {
                Name = "John",
                Surname = "Simple",
                IdentityNumber = "A123-SIMPLE",
                Age = 40
            };

            DetailedPerson = new ComplexPerson()
            {
                Name = "John",
                Surname = "Complex",
                IdentityNumber = "A123-COMPLEX",
                Age = 40,
                ContactDetails = new ContactDetails()
                {
                    Email = "my.email@mydomain.com",                  
                    AddressDetails = new AddressDetails()
                    {
                        Address = "My Address"
                    },
                    PhoneDetails = new PhoneDetails()
                    {
                        MobileNumber = "+88-123-1234"
                    }
                }
            };

            People = new People();
            People.Persons.Add(Person);
            People.Persons.Add(DetailedPerson);
        }

        public void RunExample()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(string.Format("{0}Simple Person Example:{1}", Environment.NewLine, Environment.NewLine));
            Console.WriteLine(string.Format("Direct Serializing:{0}{1}", Environment.NewLine, JsonConvert.SerializeObject(Person)));
            Console.WriteLine(string.Format("Applying the Flattener:{0}{1}", Environment.NewLine, Formatter.Flattener.Instance.Flatten(Person)));

            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(string.Format("{0}Complex Person Example:{1}", Environment.NewLine, Environment.NewLine));
            Console.WriteLine(string.Format("Direct Serializing:{0}{1}", Environment.NewLine, JsonConvert.SerializeObject(DetailedPerson)));
            Console.WriteLine(string.Format("Applying the Flattener:{0}{1}", Environment.NewLine, Formatter.Flattener.Instance.Flatten(DetailedPerson)));

            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine(string.Format("{0}People List Example:{1}", Environment.NewLine, Environment.NewLine));
            Console.WriteLine(string.Format("Direct Serializing:{0}{1}", Environment.NewLine, JsonConvert.SerializeObject(People)));
            Console.WriteLine(string.Format("Applying the Flattener:{0}{1}", Environment.NewLine, Formatter.Flattener.Instance.Flatten(People)));

            Console.ForegroundColor = ConsoleColor.Gray;
            try
            {
                DuplicateOffset DuplicateOffset = new DuplicateOffset();
                Console.WriteLine(string.Format("{0}Duplicate Offset Example:{1}", Environment.NewLine, Environment.NewLine));
                Console.WriteLine(string.Format("Direct Serializing:{0}{1}", Environment.NewLine, JsonConvert.SerializeObject(DuplicateOffset)));
                Console.WriteLine(string.Format("Applying the Flattener:{0}{1}", Environment.NewLine, Formatter.Flattener.Instance.Flatten(DuplicateOffset)));
            } catch (Formatter.Exceptions.FlattenerException fe)
            {
                Console.WriteLine(string.Format("Exception: {0}", fe.Message));
            }

            Console.ForegroundColor = ConsoleColor.Red;
            try
            {
                IncorrectLines IncorrectLines = new IncorrectLines();
                Console.WriteLine(string.Format("{0}Incorrect Lines Example:{1}", Environment.NewLine, Environment.NewLine));
                Console.WriteLine(string.Format("Direct Serializing:{0}{1}", Environment.NewLine, JsonConvert.SerializeObject(IncorrectLines)));
                Console.WriteLine(string.Format("Applying the Flattener:{0}{1}", Environment.NewLine, Formatter.Flattener.Instance.Flatten(IncorrectLines)));
            }
            catch (Formatter.Exceptions.FlattenerException fe)
            {
                Console.WriteLine(string.Format("Exception: {0}", fe.Message));
            }

        }

    }
}
