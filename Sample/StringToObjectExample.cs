using Newtonsoft.Json;
using Sample.Models;
using System;

namespace Sample
{
    public class StringToObjectExample
    {

        private string SimplePerson = "John                Simple              000000000A123-SIMPLE";

        private string ComplexPerson = 
            "John                Complex             00000000A123-COMPLEX\r\n" +
            "EMAIL ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~my.email@mydomain.com\r\n" +
            "ADDRESS__________________________________________________________MY address\r\n" + 
            "MOBILENUMBER................+88-123-1234";

        public StringToObjectExample()
        {
        }

        public void RunExample()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(string.Format("{0}Simple Person Example:{1}", Environment.NewLine, Environment.NewLine));
            Person Person = Formatter.Expander.Instance.Expand(new Person(), SimplePerson);
            Console.WriteLine(string.Format("Direct Serializing:{0}{1}", Environment.NewLine, JsonConvert.SerializeObject(Person)));

            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(string.Format("{0}Complex Person Example:{1}", Environment.NewLine, Environment.NewLine));
            ComplexPerson complexPerson = Formatter.Expander.Instance.Expand(new ComplexPerson(), ComplexPerson);
            Console.WriteLine(string.Format("Direct Serializing:{0}{1}", Environment.NewLine, JsonConvert.SerializeObject(complexPerson)));
        }

    }
}
