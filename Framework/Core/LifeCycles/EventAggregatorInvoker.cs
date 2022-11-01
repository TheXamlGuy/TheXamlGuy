using System.Reflection;

namespace TheXamlGuy.Framework.Core
{
    public class EventAggregatorInvoker : IEventAggregatorInvoker
    {
        private readonly IScope scope;

        public EventAggregatorInvoker(IScope scope)
        {
            this.scope = scope;
        }

        public void Invoke<TEvent>(object target, TEvent item, MethodInfo methodInfo)
        {
            using (scope.Enter<TEvent>(target))
            {
                methodInfo.Invoke(target, new object[] { item! });
            }
        }

        public bool IsInvoking<TEvent>(object target)
        {
            return scope.IsActive<TEvent>(target);
        }
    }
}
