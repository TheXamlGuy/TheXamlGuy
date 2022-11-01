using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using TheXamlGuy.Framework.Core;
using TheXamlGuy.UI.Avalonia.Controls;

namespace TheXamlGuy.Framework.Avalonia;

public class RouterContext : IRouterContext
{
    private readonly IRouteDescriptorCollection descriptors;
    private readonly IDisposer disposer;
    private readonly IEventAggregator eventAggregator;
    private readonly IMediator mediator;
    private readonly INamedDataTemplateFactory namedDataTemplateFactory;
    private readonly INamedTemplateFactory namedTemplateFactory;
    private readonly ITemplateDescriptorProvider templateDescriptorProvider;
    private readonly ITemplateFactory templateFactory;
    private readonly ITypedDataTemplateFactory typedDataTemplateFactory;

    public RouterContext(ITemplateDescriptorProvider templateDescriptorProvider,
        ITemplateFactory templateFactory,
        INamedTemplateFactory namedTemplateFactory,
        INamedDataTemplateFactory namedDataTemplateFactory,
        ITypedDataTemplateFactory typedDataTemplateFactory,
        IEventAggregator eventAggregator,
        IMediator mediator,
        IDisposer disposer,
        IRouteDescriptorCollection descriptors)
    {
        this.templateDescriptorProvider = templateDescriptorProvider;
        this.templateFactory = templateFactory;
        this.namedTemplateFactory = namedTemplateFactory;
        this.namedDataTemplateFactory = namedDataTemplateFactory;
        this.typedDataTemplateFactory = typedDataTemplateFactory;
        this.eventAggregator = eventAggregator;
        this.mediator = mediator;
        this.disposer = disposer;
        this.descriptors = descriptors;
    }

    public async Task InitializeAsync()
    {
        disposer.Add(this, eventAggregator.Current.SubscribeUI<Navigate>(OnNavigate));
        disposer.Add(this, eventAggregator.Current.SubscribeUI<NavigateBack>(OnNavigateBack));
        
        await Task.CompletedTask;
    }

    private void OnNavigate(Navigate args)
    {
        object? data = null;
        object? template = null;

        Dictionary<string, object> keyedParameters = new();
        List<object> parameters = new();

        foreach (object? parameter in args.Parameters)
        {
            if (parameter is KeyValuePair<string, object> keyed)
            {
                keyedParameters.Add(keyed.Key, keyed.Value);
            }
            else
            {
                parameters.Add(parameter);
            }
        }

        if (args.Name is { Length: > 0 } name)
        {
            data = namedDataTemplateFactory.Create(name, parameters.ToArray());
            template = descriptors.FirstOrDefault(x => args.Route is string { } name && name == x.Name) is { Route: Frame } ? templateDescriptorProvider.Get(name)?.TemplateType : namedTemplateFactory.Create(name);
        }

        if (args.Type is Type type)
        {
            data = typedDataTemplateFactory.Create(type, parameters.ToArray());
            template = descriptors.FirstOrDefault(x => args.Route is string { } name && name == x.Name) is { Route: Frame } ? templateDescriptorProvider.Get(type)?.TemplateType : templateFactory.Create(data);
        }

        if (template is not null)
        {
            if (template is ContentDialog contentDialog)
            {
                mediator.Handle(new Route<ContentDialog>(contentDialog, data, template));
            }
            else
            {
                if (descriptors.FirstOrDefault(x => args.Route is string { } name && name == x.Name) is RouteDescriptor descriptor)
                {
                    switch (descriptor.Route)
                    {
                        case Frame frame:
                            mediator.Handle(new Route<Frame>(frame, data, template));
                            break;
                        case ContentControl contentControl:
                            mediator.Handle(new Route<ContentControl>(contentControl, data, template));
                            break;
                    }
                }
            }
        }
        else
        {
            if (descriptors.FirstOrDefault(x => args.Route is string { } name && name == x.Name) is RouteDescriptor descriptor)
            {
                if (descriptor.Route is ContentControl contentControl)
                {
                    contentControl.Content = null;
                }
            }
        }
    }

    private void OnNavigateBack(NavigateBack args)
    {
        if (descriptors.FirstOrDefault(x => args.Route is string { } name && name == x.Name) is RouteDescriptor descriptor)
        {
            if (descriptor.Route is ContentControl { Content: TemplatedControl content })
            {
                if (content.DataContext is IDisposable disposable)
                {
                    disposable.Dispose();
                }
            }

            if (descriptor.Route is Frame frame)
            {
                frame.GoBack();
            }
        }
    }
}