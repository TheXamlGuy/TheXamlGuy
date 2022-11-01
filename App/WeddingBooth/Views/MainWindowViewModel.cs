using TheXamlGuy.Framework.Core;
using TheXamlGuy.Framework.WPF;

namespace WeddingBooth.Views;

public class MainWindowViewModel : ObservableViewModel
{
    public MainWindowViewModel(IPropertyBuilder propertyBuilder,
        IEventAggregator eventAggregator,
        IServiceFactory serviceFactory,
        IDisposer disposer,
        IRoute route) : base(propertyBuilder, eventAggregator, serviceFactory, disposer)
    {
        Route = route;
    }

    public IRoute Route { get; }
}
