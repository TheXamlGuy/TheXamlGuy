using System;
using System.Collections.Concurrent;
using System.Reactive.Disposables;

namespace TheXamlGuy.Framework.Core
{
    public class Scope : IScope
    {
        private readonly ConcurrentDictionary<object, bool> scopes = new ConcurrentDictionary<object, bool>();

        public IDisposable Enter<T>(object target)
        {
            scopes.TryAdd(Tuple.Create(target, typeof(T)), true);
            return Disposable.Create(() => scopes.TryRemove(Tuple.Create(target, typeof(T)), out bool value));
        }

        public bool IsActive<T>(object target)
        {
            return scopes.ContainsKey(Tuple.Create(target, typeof(T)));
        }
    }
}
