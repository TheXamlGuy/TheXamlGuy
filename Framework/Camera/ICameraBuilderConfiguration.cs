namespace TheXamlGuy.Framework.Camera;

public interface ICameraBuilderConfiguration
{
    Func<IServiceProvider, ICameraContext> Factory { get; }
}

public interface ICameraBuilderConfiguration<TConfiguration> : ICameraBuilderConfiguration where TConfiguration : INamedCameraConfiguration, new()
{

}
