using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimplePathSelectorNamespace
{
    public class SimplePathSelectorExceptions
    {
        public class NoPathsForThisEntry : Exception
        {
            public NoPathsForThisEntry() { }
            public NoPathsForThisEntry(string message) : base(message) { }
            public NoPathsForThisEntry(string message, Exception inner) : base(message, inner) { }
        }
    }
}
