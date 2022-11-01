using Builder.LifeCycles;
using TheXamlGuy.Framework.Core;

namespace Builder;

public class AddPageViewModel : ObservableViewModel
{
    public AddPageViewModel(IPropertyBuilder propertyBuilder,
        IEventAggregator eventAggregator,
        IServiceFactory serviceFactory,
        IDisposer disposer) : base(propertyBuilder, eventAggregator, serviceFactory, disposer)
    {

    }

    public string? Name { get; set; }

    public void Add()
    {
        EventAggregator.Publish(new AddPage(Name));
    }
}