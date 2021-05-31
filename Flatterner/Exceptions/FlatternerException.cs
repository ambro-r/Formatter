using System;
using System.Collections.Generic;
using System.Text;

namespace Flatterner.Exceptions
{
    [Serializable]
    public class FlatternerException : Exception
    {
        private FlatternerException()
        {
        }

        public FlatternerException(string message) : base(message) { }
    }
}
