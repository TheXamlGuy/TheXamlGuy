using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

namespace TheXamlGuy.Framework.Serial;

public static class IHostBuilderExtensions
{
    public static IHostBuilder ConfigureSerial<TConfiguration, TSerialReader, TContent>(this IHostBuilder hostBuilder, Action<HostBuilderContext> configureDelegate) where TSerialReader : SerialReader<TContent> where TConfiguration : ISerialConfiguration, new()
    {
        hostBuilder.ConfigureServices((hostBuilderContext, serviceCollection) =>
        {
            configureDelegate.Invoke(hostBuilderContext);

            serviceCollection.TryAddSingleton<ISerialFactory, SerialFactory>();
            serviceCollection.AddSingleton(provider => provider.GetService<ISerialFactory>()!.Create<TSerialReader, TContent>(provider.GetService<IOptionsMonitor<TConfiguration>>()!.CurrentValue));
        });

        return hostBuilder;
    }
}