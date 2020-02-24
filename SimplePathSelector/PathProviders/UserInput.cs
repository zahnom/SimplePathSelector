using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimplePathSelectorNamespace.PathProviders
{
    public class UserInput
    {
        private string Input;

        public UserInput(string input)
        {
            Input = input;
        }

        public override string ToString()
        {
            return Input;
        }
    }
}
