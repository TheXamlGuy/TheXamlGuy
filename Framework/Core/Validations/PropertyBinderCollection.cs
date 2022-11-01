using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace TheXamlGuy.Framework.Core
{
    public class PropertyBinderCollection : IPropertyBinderCollection
    {
        private readonly Dictionary<string, PropertyBinder> binders = new();

        public int Count => binders.Count;

        public void Add(string key, PropertyBinder binder)
        {
            binders.Add(key, binder);
        }

        public IEnumerator<PropertyBinder> GetEnumerator()
        {
            return binders.Select(x => x.Value).GetEnumerator();
        }

        public bool TryGet(string key, [MaybeNull] out PropertyBinder? value)
        {
            return binders.TryGetValue(key, out value);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return binders.Select(x => x.Value).GetEnumerator();
        }
    }
}