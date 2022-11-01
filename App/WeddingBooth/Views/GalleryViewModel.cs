using TheXamlGuy.Framework.Core;

namespace WeddingBooth.Views;

public class GalleryViewModel : ObservableViewModel
{
    public GalleryViewModel(IPropertyBuilder propertyBuilder,
        IEventAggregator eventAggregator,
        IServiceFactory serviceFactory,
        IDisposer disposer) : base(propertyBuilder, eventAggregator, serviceFactory, disposer)
    {

    }
}
