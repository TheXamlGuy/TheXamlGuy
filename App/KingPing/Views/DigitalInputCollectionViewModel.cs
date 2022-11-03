using TheXamlGuy.Framework.Core;

namespace KingPing;

public class DigitalInputCollectionViewModel : ObservableViewModel
{
    public DigitalInputCollectionViewModel(IPropertyBuilder propertyBuilder,
        IEventAggregator eventAggregator,
        IServiceFactory serviceFactory,
        IDisposer disposer) : base(propertyBuilder, eventAggregator, serviceFactory, disposer)
    {

    }
}
