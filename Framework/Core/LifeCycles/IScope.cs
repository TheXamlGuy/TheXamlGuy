using System;

namespace TheXamlGuy.Framework.Core
{
    public interface IScope
    {
        IDisposable Enter<T>(object target);

        bool IsActive<T>(object target);
    }
}
