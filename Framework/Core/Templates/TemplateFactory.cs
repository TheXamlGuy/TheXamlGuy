using System.Diagnostics.CodeAnalysis;

namespace TheXamlGuy.Framework.Core;

public class TemplateFactory : ITemplateFactory
{
    private readonly Dictionary<object, object> dataTracking = new();

    private readonly ITemplateDescriptorProvider provider;
    private readonly IServiceFactory serviceFactory;

    public TemplateFactory(ITemplateDescriptorProvider provider,
        IServiceFactory serviceFactory)
    {
        this.provider = provider;
        this.serviceFactory = serviceFactory;
    }

    public virtual object? Create([MaybeNull] object? data)
    {
        if (data is null)
        {
            return null;
        }

        if (dataTracking.TryGetValue(data, out object? template))
        {
            return template;
        }

        if (provider.Get(data.GetType()) is ITemplateDescriptor descriptor)
        {
            template = serviceFactory.Get<object>(descriptor.TemplateType);
            if (template is ICachable cachable)
            {
                dataTracking[data] = cachable;
            }
        }

        return template;
    }
}