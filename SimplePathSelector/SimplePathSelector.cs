using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimplePathSelectorNamespace
{
    public class SimplePathSelector
    {
        public bool SilenceNoPathAvailableExceptions = true;
        public bool SilenceInvalidPathExceptions = false;

        public SimplePathSelector(params Type[] orderOfPathProviders)
        {
            OrderOfPathProviders = orderOfPathProviders;
        }

        private Type[] OrderOfPathProviders;
        private List<object> PathProviders = new List<object>();

        public void AddProvider(object pathProvider)
        {
            PathProviders.Add(pathProvider);
        }
        
        public IEnumerable<string> GetPaths()
        {
            var paths = PathProviders
                .Distinct(new ClassEqualityComparer())
                .ToList();

            var selectedPaths = new List<string>();
            foreach (var entry in OrderOfPathProviders)
            {
                var result = paths.FirstOrDefault(x => x.GetType() == entry);
                if (result == null)
                    continue;

                var path = GetPathFromPathProvider(result);
                selectedPaths.Add(path);
            }

            return selectedPaths;
        }
        private string GetPathFromPathProvider(object provider)
        {
            string path;
            try
            {
                path = provider.ToString();
            } catch(Exception e)
            {
                if (e.GetType() == typeof(PathProviderExceptions.NoPathAvailable) && SilenceNoPathAvailableExceptions)
                    return null;

                if (e.GetType() == typeof(PathProviderExceptions.InvalidPath) && SilenceInvalidPathExceptions)
                    return null;

                throw e;
            }

            return path;
        }

        public string SelectPath()
        {
            if (GetPaths().FirstOrDefault() == null)
                throw new SimplePathSelectorExceptions.NoPathsForThisEntry();

            return GetPaths().First();
        }
    }
}
