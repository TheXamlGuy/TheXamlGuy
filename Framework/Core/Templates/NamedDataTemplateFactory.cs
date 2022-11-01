namespace TheXamlGuy.Framework.Core
{
    public class NamedDataTemplateFactory : INamedDataTemplateFactory
    {
        private readonly Dictionary<string, object> dataTracking = new();

        private readonly IReadOnlyCollection<ITemplateDescriptor> descriptors;
        private readonly IServiceFactory serviceFactory;

        public NamedDataTemplateFactory(IReadOnlyCollection<ITemplateDescriptor> descriptors,
            IServiceFactory serviceFactory)
        {
            this.descriptors = descriptors;
            this.serviceFactory = serviceFactory;
        }

        public virtual object? Create(string name, params object[] parameters)
        {
            if (dataTracking.TryGetValue(name, out object? data))
            {
                return data;
            }

            if (descriptors.FirstOrDefault(x => x.Name == name) is ITemplateDescriptor descriptor)
            {
                data = parameters is { Length: > 0 } ? serviceFactory.Create<object>(descriptor.DataType, parameters) : serviceFactory.Get<object>(descriptor.DataType);
                if (data is ICachable cachable)
                {
                    dataTracking[name] = cachable;
                }
            }

            return data;
        }
    }
}