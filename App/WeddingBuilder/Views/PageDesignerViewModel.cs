using TheXamlGuy.Framework.Avalonia;
using TheXamlGuy.Framework.Core;

namespace Builder;

public class PageDesignerViewModel : ObservableViewModel
{
    public PageDesignerViewModel(IPropertyBuilder propertyBuilder,
        IEventAggregator eventAggregator,
        IServiceFactory serviceFactory,
        IDisposer disposer,
        IRouter route) : base(propertyBuilder, eventAggregator, serviceFactory, disposer)
    {
        Route = route;
    }

    public IRouter Route { get; }
}
