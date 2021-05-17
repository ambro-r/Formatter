using System;
using System.Collections.Generic;
using System.Text;

namespace Flatterner
{
    public class Flatterner
    {
        private Flatterner() { }

        public static Flatterner Instance { get; } = new Flatterner();

        public string Flattern<T>(T flatten)
        {
            return string.Empty;
        }

    }
}
