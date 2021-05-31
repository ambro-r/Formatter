using System;
using System.Collections.Generic;
using System.Text;

namespace Flatterner.Exceptions
{
    [Serializable]
    public class FormatException : Exception
    {
        private FormatException()
        {
        }

        public FormatException(string message) : base(message) { }
    }
}
