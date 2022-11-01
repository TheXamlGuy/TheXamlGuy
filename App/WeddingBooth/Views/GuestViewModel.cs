using TheXamlGuy.Framework.Core;

namespace WeddingBooth.Views;

public class GuestViewModel : ObservableViewModel
{
    public GuestViewModel(IPropertyBuilder propertyBuilder, 
        IEventAggregator eventAggregator, 
        IServiceFactory serviceFactory, 
        IDisposer disposer,
        string name) : base(propertyBuilder, eventAggregator, serviceFactory, disposer)
    {
        Name = name;
    }

    public string Name { get; }
}
