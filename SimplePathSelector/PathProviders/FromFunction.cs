using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimplePathSelector.PathProviders
{
    public class FromFunction
    {
        private Func<string> MyFunction;

        public FromFunction(Func<string> function)
        {
            MyFunction = function;
        }
        public override string ToString()
        {
            return MyFunction();
        }
    }
}
