using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Controls.Templates;
using Avalonia.Markup.Xaml.Templates;
using FluentAvalonia.UI.Controls;
using TheXamlGuy.Framework.Core;

namespace TheXamlGuy.Framework.Avalonia;

public class TemplateSelector : DataTemplateSelector, ITemplateSelector
{
    private readonly Dictionary<object, DataTemplate> dataTracking = new();

    private readonly ITemplateFactory templateFactory;
    private readonly IEventAggregator eventAggregator;

    public TemplateSelector(ITemplateFactory templateFactory, 
        IEventAggregator eventAggregator)
    {
        this.templateFactory = templateFactory;
        this.eventAggregator = eventAggregator;
    }

    protected override IDataTemplate SelectTemplateCore(object item, IControl container)
    {
        if (item is not null)
        {
            if (dataTracking.TryGetValue(item, out DataTemplate? cachedDataTemplate))
            {
                return cachedDataTemplate;
            }

            return new FuncDataTemplate(item.GetType(), (value, namescope) =>
            {
                if (templateFactory.Create(item) is TemplatedControl template)
                {
                    return template;
                }

                return null;
            });
                        
        }

        return base.SelectTemplateCore(item, container);
    }
}