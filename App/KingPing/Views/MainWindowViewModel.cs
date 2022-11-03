using TheXamlGuy.Framework.Avalonia;
using TheXamlGuy.Framework.Core;

namespace KingPing;

public class MainWindowViewModel : ObservableViewModel
{
    public MainWindowViewModel(IPropertyBuilder propertyBuilder,
        IEventAggregator eventAggregator,
        IServiceFactory serviceFactory,
        IDisposer disposer,
        IRouter route) : base(propertyBuilder, eventAggregator, serviceFactory, disposer)
    {
        Route = route;
    }

    public IRouter Route { get; }
}