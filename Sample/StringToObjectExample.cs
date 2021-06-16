using Newtonsoft.Json;
using Sample.Models;
using System;

namespace Sample
{
    public class StringToObjectExample
    {

        private string SimpleString = "John                Simple              000000000A123-SIMPLE";

        public StringToObjectExample()
        {
        }

        public void RunExample()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(string.Format("{0}Simple Person Example:{1}", Environment.NewLine, Environment.NewLine));
            Person Person = Formatter.Expander.Instance.Expand(new Person(), SimpleString);
            Console.WriteLine(string.Format("Direct Serializing:{0}{1}", Environment.NewLine, JsonConvert.SerializeObject(Person)));
        }

    }
}
