using System;
using System.Collections.Generic;
using System.Text;

namespace Flatterner.Exceptions
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
