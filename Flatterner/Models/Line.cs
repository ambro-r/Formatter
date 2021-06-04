using System;
using System.Collections.Generic;
using System.Text;

namespace Formatter.Models
{
    public class Line
    {
        public int LineNumber { get; set; }

        public bool FromZero { get; set; } = false;

        public SortedDictionary<int, string> SortedStrings { get; } = new SortedDictionary<int, string>();

        public SortedDictionary<int, Line> SortedLines { get; set;  } = new SortedDictionary<int, Line>();

        public void AddFormattedString(int offset, string formattedString)
        {
            if (SortedStrings.ContainsKey(offset))
            {
                throw new Exceptions.FlattenerException(string.Format("Offest {0} already defined, duplicate offests not allowed.", offset));
            }
            else
            {
                SortedStrings.Add(offset, formattedString);
            }
        }
        
    }
}
