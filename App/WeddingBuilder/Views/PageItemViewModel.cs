using TheXamlGuy.Framework.Avalonia;
using TheXamlGuy.Framework.Core;

namespace Builder;

public class PageItemViewModel : ObservableViewModel
{
    public PageItemViewModel(IPropertyBuilder propertyBuilder,
        IEventAggregator eventAggregator,
        IServiceFactory serviceFactory,
        IDisposer disposer,
        IRouter route,
        string name) : base(propertyBuilder, eventAggregator, serviceFactory, disposer)
    {
        Route = route;
        Name = name;
    }

    public IRouter Route { get; }

    public string Name { get; }
}
