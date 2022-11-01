using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace TheXamlGuy.Framework.Core
{
    public interface IPropertyBinderCollection : IReadOnlyCollection<PropertyBinder>
    {
        void Add(string key, PropertyBinder binder);

        bool TryGet(string key, [MaybeNull] out PropertyBinder? value);
    }
}