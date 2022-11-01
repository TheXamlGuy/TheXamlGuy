using System.Reactive.Concurrency;
using System.Reactive.Subjects;

namespace TheXamlGuy.Framework.Core
{
    public interface IEventAggregator
    {
        IEventAggregator Current { get; }

        IScheduler Dispatcher { get; }

        IEventAggregatorInvoker Invoker { get; }

        IObservable<TEvent> GetEvent<TEvent>();

        ISubject<TEvent> GetSubject<TEvent>();

        void Publish<TEvent>(TEvent args);
    }
}
