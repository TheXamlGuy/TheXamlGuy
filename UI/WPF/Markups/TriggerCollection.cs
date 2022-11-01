using System.Collections.ObjectModel;
using TheXamlGuy.UI.WPF;

namespace TheXamlGuy.UI.WPF;

public class TriggerCollection : Collection<TriggerExtension>
{
    public void Add(object item)
    {
        if (item is TriggerExtension trigger)
        {
            base.Add(trigger);
        }
    }
}