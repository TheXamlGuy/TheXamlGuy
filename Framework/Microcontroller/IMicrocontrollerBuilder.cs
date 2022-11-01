using Microsoft.Extensions.Configuration;
using TheXamlGuy.Framework.Serial;

namespace TheXamlGuy.Framework.Microcontroller;

public interface IMicrocontrollerBuilder
{
    IReadOnlyCollection<IMicrocontrollerBuilderConfiguration> Configurations { get; }

    IMicrocontrollerBuilderConfiguration<TConfiguration, TSerialReader, TRead, TReadDeserializer> Add<TConfiguration, TSerialReader, TRead, TReadDeserializer>(IConfiguration configuration)
            where TConfiguration : IMicrocontrollerConfiguration, new()
            where TSerialReader : SerialReader<TRead> where TReadDeserializer : IMicrocontrollerModuleDeserializer<TRead>, new();
}
