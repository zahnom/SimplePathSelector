using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimplePathSelectorNamespace
{
    public class SimplePathSelectorExceptions
    {
        public class InvalidPath : Exception
        {
            public InvalidPath() { }
            public InvalidPath(string message) : base(message) { }
            public InvalidPath(string message, Exception inner) : base(message, inner) { }
        }

        public class NoPathsForThisEntry : Exception
        {
            public NoPathsForThisEntry() { }
            public NoPathsForThisEntry(string message) : base(message) { }
            public NoPathsForThisEntry(string message, Exception inner) : base(message, inner) { }
        }
    }
}
