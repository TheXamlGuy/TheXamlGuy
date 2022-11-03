using TheXamlGuy.Framework.Core;

namespace KingPing;

public class FavouriteCollectionViewModel : ObservableViewModel
{
    public FavouriteCollectionViewModel(IPropertyBuilder propertyBuilder,
        IEventAggregator eventAggregator,
        IServiceFactory serviceFactory,
        IDisposer disposer) : base(propertyBuilder, eventAggregator, serviceFactory, disposer)
    {

    }
}