using System;

namespace Formatter.Exceptions
{
    [Serializable]
    public class FlattenerException : Exception
    {
        private FlattenerException()
        {
        }

        public FlattenerException(string message) : base(message) { }
    }
}
