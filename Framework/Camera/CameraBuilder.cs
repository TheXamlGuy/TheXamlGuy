using Microsoft.Extensions.Configuration;
using System.Collections.ObjectModel;

namespace TheXamlGuy.Framework.Camera;

public class CameraBuilder : ICameraBuilder
{
    private readonly List<ICameraBuilderConfiguration> configurations = new();

    public IReadOnlyCollection<ICameraBuilderConfiguration> Configurations => new ReadOnlyCollection<ICameraBuilderConfiguration>(configurations);

    public ICameraBuilderConfiguration<TConfiguration> Add<TConfiguration>(IConfiguration configuration) where TConfiguration : IRemoteCameraConfiguration, new()
    {
        CameraBuilderConfiguration<TConfiguration>? builderConfiguration = new(configuration);
        configurations.Add(builderConfiguration);

        return builderConfiguration;
    }
}