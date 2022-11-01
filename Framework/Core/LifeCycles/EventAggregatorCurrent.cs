using System.Reactive.Linq;

namespace TheXamlGuy.Framework.Core
{
    public class EventAggregatorCurrent : EventAggregator
    {
        public EventAggregatorCurrent(EventAggregator eventAggregator) : base(eventAggregator)
        {

        }

        public override IObservable<TEvent> GetEvent<TEvent>()
        {
            return GetObservable<TEvent>().Skip(0).Where(x => x != null);
        }
    }
}
