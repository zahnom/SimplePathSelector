using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimplePathSelectorNamespace
{
    public class ClassEqualityComparer : IEqualityComparer<object>
    {
        public new bool Equals(object x, object y)
        {
            return x.GetType() == y.GetType();
        }

        public int GetHashCode(object obj)
        {
            return obj.GetType().GetHashCode();
        }
    }
}
