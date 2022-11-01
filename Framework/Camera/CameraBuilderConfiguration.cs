using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace TheXamlGuy.Framework.Camera;

public class CameraBuilderConfiguration<TConfiguration> : ICameraBuilderConfiguration<TConfiguration> where TConfiguration : INamedCameraConfiguration, new()
{
    private readonly IConfiguration configuration;

    public CameraBuilderConfiguration(IConfiguration configuration)
    {
        this.configuration = configuration;
    }

    public Func<IServiceProvider, ICameraContext> Factory => (IServiceProvider provider) => provider.GetService<ICameraFactory>()!.Create(configuration.Get<TConfiguration>());
}
