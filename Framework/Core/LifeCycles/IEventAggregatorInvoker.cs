using System.Reflection;

namespace TheXamlGuy.Framework.Core
{
    public interface IEventAggregatorInvoker
    {
        void Invoke<TEvent>(object target, TEvent item, MethodInfo methodInfo);

        bool IsInvoking<TEvent>(object target);
    }
}