using TheXamlGuy.Framework.Avalonia;
using TheXamlGuy.Framework.Core;

namespace Builder;

public class PageCollectionViewModel : ObservableViewModelCollection<PageItemViewModel>
{
    public PageCollectionViewModel(IPropertyBuilder propertyBuilder,
        IEventAggregator eventAggregator,
        IServiceFactory serviceFactory,
        IDisposer disposer,
        ITemplateSelector templateSelector,
        IRouter route) : base(propertyBuilder, eventAggregator, serviceFactory, disposer)
    {
        TemplateSelector = templateSelector;
        Route = route;
    }

    public ITemplateSelector TemplateSelector { get; }

    public IRouter Route { get; }
}