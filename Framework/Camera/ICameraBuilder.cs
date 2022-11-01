using Microsoft.Extensions.Configuration;

namespace TheXamlGuy.Framework.Camera;

public interface ICameraBuilder
{
    IReadOnlyCollection<ICameraBuilderConfiguration> Configurations { get; }

    ICameraBuilderConfiguration<TConfiguration> Add<TConfiguration>(IConfiguration configuration) where TConfiguration : IRemoteCameraConfiguration, new();
}
