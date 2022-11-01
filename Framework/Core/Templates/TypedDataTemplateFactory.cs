namespace TheXamlGuy.Framework.Core;

 public class TypedDataTemplateFactory : ITypedDataTemplateFactory
{
    private readonly Dictionary<Type, object> dataTracking = new();

    private readonly IReadOnlyCollection<ITemplateDescriptor> descriptors;
    private readonly IServiceFactory serviceFactory;

    public TypedDataTemplateFactory(IReadOnlyCollection<ITemplateDescriptor> descriptors,
        IServiceFactory serviceFactory)
    {
        this.descriptors = descriptors;
        this.serviceFactory = serviceFactory;
    }

    public virtual object? Create(Type type, params object[] parameters)
    {
        if (dataTracking.TryGetValue(type, out object? data))
        {
            return data;
        }

        if (descriptors.FirstOrDefault(x => x.DataType == type) is ITemplateDescriptor descriptor)
        {
            data = parameters is { Length: > 0 } ? serviceFactory.Create<object>(descriptor.DataType, parameters) : serviceFactory.Get<object>(descriptor.DataType);
            if (data is ICachable cachable)
            {
                dataTracking[type] = cachable;
            }
        }

        return data;
    }
}