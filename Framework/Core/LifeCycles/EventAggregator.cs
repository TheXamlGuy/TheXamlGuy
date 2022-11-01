using System.Collections.Concurrent;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Reactive.Subjects;

namespace TheXamlGuy.Framework.Core
{
    public class EventAggregator : IEventAggregator
    {
        private readonly ConcurrentDictionary<Type, object> subjects;

        public EventAggregator(IEventAggregatorInvoker invoker)
        {
            subjects = new ConcurrentDictionary<Type, object>();
            Dispatcher = new SynchronizationContextScheduler(SynchronizationContext.Current!);
            Invoker = invoker;

            Current = new EventAggregatorCurrent(this);
        }

        protected EventAggregator(EventAggregator eventAggregator)
        {
            subjects = eventAggregator.subjects;
            Dispatcher = eventAggregator.Dispatcher;
            Invoker = eventAggregator.Invoker;
            Current = eventAggregator.Current;
        }

        public IEventAggregator Current { get; }

        public IScheduler Dispatcher { get; }

        public IEventAggregatorInvoker Invoker { get; }

        public virtual IObservable<TEvent> GetEvent<TEvent>()
        {
            return GetObservable<TEvent>().Skip(1);
        }

        public virtual ISubject<TEvent> GetSubject<TEvent>()
        {
            return (ISubject<TEvent>)subjects.GetOrAdd(typeof(TEvent), x => new BehaviorSubject<TEvent>(default!));
        }

        public virtual void Publish<TEvent>(TEvent domainEvent)
        {
            GetSubject<TEvent>().OnNext(domainEvent);
        }
        protected virtual IObservable<TEvent> GetObservable<TEvent>()
        {
            return GetSubject<TEvent>().AsObservable();
        }
    }
}
