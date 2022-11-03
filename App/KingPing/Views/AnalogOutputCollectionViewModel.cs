using System;
using TheXamlGuy.Framework.Core;

namespace KingPing;

public class AnalogOutputCollectionViewModel : ObservableViewModelCollection<AnalogOutputViewModel>
{
    public AnalogOutputCollectionViewModel(IPropertyBuilder propertyBuilder,
        IEventAggregator eventAggregator,
        IServiceFactory serviceFactory,
        IDisposer disposer,
        ITemplateSelector templateSelector) : base(propertyBuilder, eventAggregator, serviceFactory, disposer)
    {
       for(int i = 0; i < 1000; i++)
        {
            Add<AnalogOutputViewModel>(Guid.NewGuid().ToString());
        }
        TemplateSelector = templateSelector;
    }

    public ITemplateSelector TemplateSelector { get; }
}
