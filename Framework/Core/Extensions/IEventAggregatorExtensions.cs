using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Reflection;

namespace TheXamlGuy.Framework.Core;

public static class IEventAggregatorExtensions
{
    public static IDisposable Subscribe<TEvent>(this IEventAggregator eventAggregator, Action<TEvent> onNext, IScheduler? scheduler = null, Func<TEvent, bool>? where = null)
    {
        scheduler ??= Scheduler.Default;
        where ??= x => true;

        return eventAggregator.GetEvent<TEvent>().Where(where).ObserveOn(scheduler).WeakSubscribe(eventAggregator.Invoker, onNext);
    }

    public static IDisposable SubscribeUI<TEvent>(this IEventAggregator eventAggregator, Action<TEvent> onNext, Func<TEvent, bool>? where = null)
    {
        return eventAggregator.Subscribe(onNext, eventAggregator.Dispatcher, where);
    }

    public static void Publish<TEvent>(this IEventAggregator eventAggregator) where TEvent : new()
    {
        eventAggregator.Publish(new TEvent());
    }
}
