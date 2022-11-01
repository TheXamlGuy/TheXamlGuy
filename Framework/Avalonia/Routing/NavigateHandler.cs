using TheXamlGuy.Framework.Core;

namespace TheXamlGuy.Framework.Avalonia
{
    public class NavigateHandler : IMediatorHandler<Navigate>
    {
        private readonly IEventAggregator eventAggregator;

        public NavigateHandler(IEventAggregator eventAggregator)
        {
            this.eventAggregator = eventAggregator;
        }

        public void Handle(Navigate request)
        {
            eventAggregator.Publish(request);
        }
    }
}