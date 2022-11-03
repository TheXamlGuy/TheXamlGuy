using TheXamlGuy.Framework.Core;

namespace KingPing;

public class AnalogOutputViewModel : ObservableViewModel
{
    public AnalogOutputViewModel(IPropertyBuilder propertyBuilder,
        IEventAggregator eventAggregator,
        IServiceFactory serviceFactory,
        IDisposer disposer,
        string name) : base(propertyBuilder, eventAggregator, serviceFactory, disposer)
    {
        Name = name;
    }

    public string Name { get; }
}
