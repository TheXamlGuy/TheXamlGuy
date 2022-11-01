using TheXamlGuy.Framework.Core;

namespace WeddingBooth.Views;

public class NavigationViewModel : ObservableViewModelCollection<IObservableViewModel>
{
    public NavigationViewModel(IPropertyBuilder propertyBuilder,
        IEventAggregator eventAggregator, 
        IServiceFactory serviceFactory,
        IDisposer disposer,
        ITemplateSelector templateSelector) : base(propertyBuilder, eventAggregator, serviceFactory, disposer)
    {
        TemplateSelector = templateSelector;
    }

    public ITemplateSelector TemplateSelector { get; }
}
