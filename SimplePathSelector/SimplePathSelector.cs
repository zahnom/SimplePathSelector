using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimplePathSelectorNamespace
{
    public class SimplePathSelector
    {
        private Type[] OrderOfPathProviders;
        private Dictionary<string, List<object>> Paths = new Dictionary<string,List<object>>();

        public SimplePathSelector(params Type[] orderOfPathProviders)
        {
            OrderOfPathProviders = orderOfPathProviders;
        }

        public void AddPathFor(string id, object pathProvider)
        {
            if (Paths.ContainsKey(id) == false)
                Paths.Add(id, new List<object>());

            Paths[id].Add(pathProvider);
        }
        public IEnumerable<string> PathsFor(string id)
        {
            if (Paths.ContainsKey(id) == false)
                return new List<string>();

            var paths = Paths[id]
                .Distinct(new ClassEqualityComparer())
                .ToList();

            var selectedPaths = new List<string>();
            foreach (var entry in OrderOfPathProviders)
            {
                var result = paths.FirstOrDefault(x => x.GetType() == entry);
                if (result != null) 
                    selectedPaths.Add(result.ToString());
            }

            return selectedPaths;
        }
        public string SelectPathFor(string id)
        {
            if (PathsFor(id).FirstOrDefault() == null)
                throw new SimplePathSelectorExceptions.NoPathsForThisEntry();

            return PathsFor(id).First();
        }
    }
}
