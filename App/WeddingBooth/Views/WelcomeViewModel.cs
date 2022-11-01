using TheXamlGuy.Framework.Core;

namespace WeddingBooth.Views;

public class WelcomeViewModel : ObservableViewModel
{
    public WelcomeViewModel(IPropertyBuilder propertyBuilder,
        IEventAggregator eventAggregator,
        IServiceFactory serviceFactory,
        IDisposer disposer) : base(propertyBuilder, eventAggregator, serviceFactory, disposer)
    {

    }

}
