using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using TheXamlGuy.Framework.Core;
using TheXamlGuy.Framework.Serial;

namespace TheXamlGuy.Framework.Microcontroller;

public static class IHostBuilderExtensions
{
    public static IHostBuilder ConfigureMicrocontrollers(this IHostBuilder hostBuilder, Action<HostBuilderContext, IMicrocontrollerBuilder> builderDelegate)
    {
        hostBuilder.ConfigureServices((hostBuilderContext, serviceCollection) =>
        {
            MicrocontrollerBuilder? builder = new();

            builderDelegate.Invoke(hostBuilderContext, builder);
            serviceCollection.TryAddSingleton<ISerialFactory, SerialFactory>();
            serviceCollection.TryAddSingleton<IMicrocontrollerFactory, MicrocontrollerFactory>();

            foreach (IMicrocontrollerBuilderConfiguration configuration in builder.Configurations)
            {
                serviceCollection.AddSingleton(provider => configuration.Factory.Invoke(provider));
            }

            serviceCollection.RegisterHandlers(typeof(IMicrocontrollerContext).Assembly, typeof(ISerialContext).Assembly);
        });

        return hostBuilder;
    }
}