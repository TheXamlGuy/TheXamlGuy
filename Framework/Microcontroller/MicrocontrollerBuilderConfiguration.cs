using Microcontroller;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.ObjectModel;
using TheXamlGuy.Framework.Serial;

namespace TheXamlGuy.Framework.Microcontroller;

public class MicrocontrollerBuilderConfiguration<TConfiguration, TSerialReader, TRead, TReadDeserializer> : IMicrocontrollerBuilderConfiguration<TConfiguration, TSerialReader, TRead, TReadDeserializer> where TConfiguration : IMicrocontrollerConfiguration, new()
        where TSerialReader : SerialReader<TRead> where TReadDeserializer : IMicrocontrollerModuleDeserializer<TRead>, new()
{
    public MicrocontrollerBuilderConfiguration(IConfiguration configuration)
    {
        this.configuration = configuration;
    }

    private readonly List<IMicrocontrollerModuleDescriptor> modules = new();
    private readonly IConfiguration configuration;

    public Func<IServiceProvider, IMicrocontrollerContext> Factory => (IServiceProvider provider) => provider.GetService<IMicrocontrollerFactory>()!.Create<TSerialReader, TRead, TReadDeserializer>(configuration.Get<TConfiguration>(), Modules);
    
    public IReadOnlyCollection<IMicrocontrollerModuleDescriptor> Modules => new ReadOnlyCollection<IMicrocontrollerModuleDescriptor>(modules);

    public IMicrocontrollerBuilderConfiguration<TConfiguration, TSerialReader, TRead, TReadDeserializer> AddModule<TModule>() where TModule : IMicrocontrollerModule, new()
    {
        modules.Add(new MicrocontrollerModuleDescriptor<TModule>());
        return this;
    }
}
