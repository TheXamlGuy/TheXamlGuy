using TheXamlGuy.Framework.Core;

namespace WeddingBooth.Views;

public class SeatingChartViewModel : ObservableViewModelCollection<TableViewModel>
{
    public SeatingChartViewModel(IPropertyBuilder propertyBuilder,
        IEventAggregator eventAggregator,
        IServiceFactory serviceFactory,
        IDisposer disposer,
        ITemplateSelector templateSelector) : base(propertyBuilder, eventAggregator, serviceFactory, disposer)
    {
        TemplateSelector = templateSelector;
    }

    public ITemplateSelector TemplateSelector { get; }
}
