using System;

namespace TheXamlGuy.Framework.Core
{
    public interface IServiceCreator<I>
    {
        object Create(Func<Type, object[], object> creator, params object[] parameters);
    }
}
