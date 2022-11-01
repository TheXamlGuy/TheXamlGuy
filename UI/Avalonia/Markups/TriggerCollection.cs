using System.Collections.ObjectModel;

namespace TheXamlGuy.UI.Avalonia;

public class TriggerCollection : Collection<Delegate>
{
    public void Add(object item)
    {
        if (item is Delegate trigger)
        {
            base.Add(trigger);
        }
    }
}
