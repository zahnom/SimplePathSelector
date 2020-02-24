using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimplePathSelectorNamespace
{
    public class PathProviderExceptions
    {
        public class InvalidPath : Exception
        {
            public InvalidPath() { }
            public InvalidPath(string message) : base(message) { }
            public InvalidPath(string message, Exception inner) : base(message, inner) { }
        }
        public class NoPathAvailable : Exception
        {
            public NoPathAvailable() { }
            public NoPathAvailable(string message) : base(message) { }
            public NoPathAvailable(string message, Exception inner) : base(message, inner) { }
        }
    }
}
