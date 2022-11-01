using System;
using System.Reflection;

namespace TheXamlGuy.Framework.Core
{
    public static class IObservableExtensions
    {
        public static IDisposable WeakSubscribe<TEvent>(this IObservable<TEvent> observable, IEventAggregatorInvoker invoker, Action<TEvent> onNext)
        {
            MethodInfo methodInfo = onNext.Method;
            WeakReference weakReference = new(onNext.Target);

            IDisposable? subscription = null;
            subscription = observable.Subscribe(item =>
            {
                if (weakReference.Target is object target)
                {
                    invoker.Invoke(target, item, methodInfo);
                }
                else
                {
                    subscription?.Dispose();
                }
            });

            return subscription;
        }
    }
}
