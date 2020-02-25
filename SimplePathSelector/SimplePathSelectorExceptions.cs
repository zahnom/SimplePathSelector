using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimplePathSelectorNamespace
{
    public class SimplePathSelectorExceptions
    {
        public class NoPathsAvailable : Exception
        {
            public NoPathsAvailable() { }
            public NoPathsAvailable(string message) : base(message) { }
            public NoPathsAvailable(string message, Exception inner) : base(message, inner) { }
        }
    }
}
