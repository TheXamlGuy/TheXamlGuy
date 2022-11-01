using TheXamlGuy.Framework.Core;

namespace Builder;

public class StartProjectConfigurationViewModel : ObservableViewModel
{
    public StartProjectConfigurationViewModel(IPropertyBuilder propertyBuilder,
        IEventAggregator eventAggregator,
        IServiceFactory serviceFactory,
        IDisposer disposer) : base(propertyBuilder, eventAggregator, serviceFactory, disposer)
    {

    }
}