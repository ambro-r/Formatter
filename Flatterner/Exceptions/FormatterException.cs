using System;

namespace Formatter.Exceptions
{
    [Serializable]
    public class FormatterException : Exception
    {
        private FormatterException()
        {
        }

        public FormatterException(string message) : base(message) { }
    }
}
