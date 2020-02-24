using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimplePathSelectorNamespace.PathProviders
{
    public class DefaultValue
    {
        private string Value;

        public DefaultValue(string value)
        {
            Value = value;
        }

        public override string ToString()
        {
            return Value;
        }
    }
}
