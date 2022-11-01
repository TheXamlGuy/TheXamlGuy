using TheXamlGuy.Framework.Core;

namespace Builder;

public class ExistingProjectConfigurationViewModel : ObservableViewModel
{
    public ExistingProjectConfigurationViewModel(IPropertyBuilder propertyBuilder,
        IEventAggregator eventAggregator,
        IServiceFactory serviceFactory,
        IDisposer disposer) : base(propertyBuilder, eventAggregator, serviceFactory, disposer)
    {

    }
}
