using TheXamlGuy.Framework.Core;

namespace Builder;

public class CreateProjectConfigurationViewModel : ObservableViewModel
{
    public CreateProjectConfigurationViewModel(IPropertyBuilder propertyBuilder,
        IEventAggregator eventAggregator,
        IServiceFactory serviceFactory,
        IDisposer disposer) : base(propertyBuilder, eventAggregator, serviceFactory, disposer)
    {

    }
}