using TheXamlGuy.Framework.Core;

namespace TheXamlGuy.Framework.WPF;

public interface IEventSubscriber
{
    void Subscribe(IEventAggregator eventAggregator);
}