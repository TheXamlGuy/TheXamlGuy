using TheXamlGuy.Framework.Core;

namespace WeddingBooth.Views;

public class TableViewModel : ObservableViewModelCollection<GuestViewModel>
{
    public TableViewModel(IPropertyBuilder propertyBuilder,
        IEventAggregator eventAggregator,
        IServiceFactory serviceFactory,
        IDisposer disposer,
        string name) : base(propertyBuilder, eventAggregator, serviceFactory, disposer)
    {
        Name = name;
    }

    public string Name { get; }
}
