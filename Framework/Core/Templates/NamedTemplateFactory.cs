namespace TheXamlGuy.Framework.Core;

public class NamedTemplateFactory : INamedTemplateFactory
{
    private readonly Dictionary<string, object> dataTracking = new();

    private readonly ITemplateDescriptorProvider provider;
    private readonly IServiceFactory serviceFactory;

    public NamedTemplateFactory(ITemplateDescriptorProvider provider,
        IServiceFactory serviceFactory)
    {
        this.provider = provider;
        this.serviceFactory = serviceFactory;
    }

    public virtual object? Create(string name)
    {
        if (dataTracking.TryGetValue(name, out object? view))
        {
            return view;
        }

        if (provider.Get(name) is ITemplateDescriptor descriptor)
        {
            view = serviceFactory.Get<object>(descriptor.TemplateType);
            if (view is ICachable cachable)
            {
                dataTracking[name] = cachable;
            }

            if (descriptor.GetType().GenericTypeArguments is { Length: 2 })
            {
                (descriptor as dynamic).ViewInvoker?.Invoke(view);
            }
        }

        return view;
    }
}