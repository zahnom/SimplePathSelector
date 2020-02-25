using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimplePathSelectorNamespace
{
    public class SimplePathSelectorBuilder
    {
        private List<object> PathProviders = new List<object>();
        private List<Type> ProviderOrder = new List<Type>();

        private bool? IgnoreNoPathsAvailableException = null;
        private bool? IgnoreInvalidPathException = null;

        public SimplePathSelectorBuilder FirstChoice(Type provider)
        {
            ProviderOrder.Insert(0, provider);
            return this;
        }
        public SimplePathSelectorBuilder FirstChoice(object provider)
        {
            ProviderOrder.Insert(0, provider.GetType());
            PathProviders.Insert(0, provider);
            return this;
        }
        public SimplePathSelectorBuilder Otherwise(Type provider)
        {
            ProviderOrder.Add(provider);
            return this;
        }
        public SimplePathSelectorBuilder Otherwise(object provider)
        { 
            ProviderOrder.Add(provider.GetType());
            PathProviders.Add(provider);
            return this;
        }
        public SimplePathSelectorBuilder IgnoreNoPathsAvailableExceptions()
        {
            IgnoreNoPathsAvailableException = true;
            return this;
        }
        public SimplePathSelectorBuilder ThrowNoPathsAvailableExceptions()
        {
            IgnoreNoPathsAvailableException = false;
            return this;
        }
        public SimplePathSelectorBuilder IgnoreInvalidPathExceptions()
        {
            IgnoreInvalidPathException = true;
            return this;
        }
        public SimplePathSelectorBuilder ThrowInvalidPathExceptions()
        {
            IgnoreInvalidPathException = false;
            return this;
        }

        public SimplePathSelector Create()
        {
            var newSelector = new SimplePathSelector(ProviderOrder.ToArray());
            PathProviders.ForEach(p => newSelector.AddProvider(p));

            if (IgnoreNoPathsAvailableException != null)
                newSelector.SilenceNoPathAvailableExceptions = IgnoreNoPathsAvailableException.Value;

            if (IgnoreInvalidPathException != null)
                newSelector.SilenceInvalidPathExceptions = IgnoreInvalidPathException.Value;

            return newSelector;
        }
    }
}
