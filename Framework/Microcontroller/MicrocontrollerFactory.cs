using TheXamlGuy.Framework.Core;
using TheXamlGuy.Framework.Serial;

namespace TheXamlGuy.Framework.Microcontroller;

public class MicrocontrollerFactory : IMicrocontrollerFactory
{
    private readonly IServiceFactory factory;
    private readonly ISerialFactory serialFactory;
    private readonly Dictionary<ISerialConfiguration, IMicrocontrollerContext> cache = new();

    public MicrocontrollerFactory(IServiceFactory factory, 
        ISerialFactory serialFactory)
    {
        this.factory = factory;
        this.serialFactory = serialFactory;
    }

    public IMicrocontrollerContext<TRead, TReadDeserializer> Create<TSerialReader, TRead, TReadDeserializer>(IMicrocontrollerConfiguration configuration, IReadOnlyCollection<IMicrocontrollerModuleDescriptor> modules) where TSerialReader : SerialReader<TRead> where TReadDeserializer : IMicrocontrollerModuleDeserializer<TRead>, new()
    {
        if (cache.TryGetValue(configuration, out IMicrocontrollerContext? context))
        {
            return (IMicrocontrollerContext<TRead, TReadDeserializer>)context;
        }

        ISerialContext<TSerialReader, TRead> serialContext = serialFactory.Create<TSerialReader, TRead>(configuration);

        context = factory.Create<MicrocontrollerContext<TRead, TReadDeserializer>>(modules, serialContext);
        cache.Add(configuration, context);

        return (IMicrocontrollerContext<TRead, TReadDeserializer>)context;
    }
}
