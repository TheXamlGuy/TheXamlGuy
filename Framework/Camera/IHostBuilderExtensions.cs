using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;

namespace TheXamlGuy.Framework.Camera;

public static class IHostBuilderExtensions
{
    public static IHostBuilder ConfigureCamera(this IHostBuilder hostBuilder, Action<HostBuilderContext, ICameraBuilder> builderDelegate)
    {
        hostBuilder.ConfigureServices((hostBuilderContext, serviceCollection) =>
        {
            CameraBuilder? builder = new();

            builderDelegate.Invoke(hostBuilderContext, builder);
            serviceCollection.TryAddSingleton<ICameraFactory, CameraFactory>();

            foreach (ICameraBuilderConfiguration configuration in builder.Configurations)
            {
                serviceCollection.AddSingleton(provider => configuration.Factory.Invoke(provider));
            }
        });

        return hostBuilder;
    }
}