using TheXamlGuy.Framework.Core;

namespace KingPing;

public class DigitalOutputCollectionViewModel : ObservableViewModel
{
    public DigitalOutputCollectionViewModel(IPropertyBuilder propertyBuilder,
        IEventAggregator eventAggregator,
        IServiceFactory serviceFactory,
        IDisposer disposer) : base(propertyBuilder, eventAggregator, serviceFactory, disposer)
    {

    }
}
