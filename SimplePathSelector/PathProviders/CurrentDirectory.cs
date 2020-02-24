using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimplePathSelectorNamespace.PathProviders
{
    public class CurrentWorkingDirectory
    {
        public override string ToString()
        {
            return Environment.CurrentDirectory;
        }
    }
}
