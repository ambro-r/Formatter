using System;
using System.Collections.Generic;
using System.Text;

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
