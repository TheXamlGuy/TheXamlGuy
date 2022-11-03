using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Controls.Templates;
using Avalonia.Markup.Xaml.Templates;
using TheXamlGuy.Framework.Core;
using TheXamlGuy.UI.Avalonia.Controls;

namespace TheXamlGuy.Framework.Avalonia;

public class TemplateSelector : IDataTemplate, ITemplateSelector
{
    private readonly Dictionary<object, IControl> dataTracking = new();

    private readonly IEventAggregator eventAggregator;
    private readonly ITemplateFactory templateFactory;

    public TemplateSelector(ITemplateFactory templateFactory,
        IEventAggregator eventAggregator)
    {
        this.templateFactory = templateFactory;
        this.eventAggregator = eventAggregator;
    }

    public IControl? Build(object? item)
    {
        if (item is not null)
        {
            if (dataTracking.TryGetValue(item, out IControl? control))
            {
                return control;
            }

            return (IControl?)templateFactory.Create(item);
        }

        return null;
    }

    public bool Match(object? data)
    {
        return true;
    }
}