using TheXamlGuy.Framework.Core;

namespace TheXamlGuy.Framework.Camera;

public class CameraFactory : ICameraFactory
{
    private readonly Dictionary<INamedCameraConfiguration, ICameraContext> cache = new();
    private readonly IServiceFactory factory;

    public CameraFactory(IServiceFactory factory)
    {
        this.factory = factory;
    }

    public ICameraContext Create(INamedCameraConfiguration configuration)
    {
        if (cache.TryGetValue(configuration, out ICameraContext? context))
        {
            return context;
        }

        if (configuration is IRemoteCameraConfiguration)
        {
            context = factory.Create<RemoteCameraContext>();
        }
        else
        {
            context = factory.Create<CameraContext>();
        }

        cache.Add(configuration, context);
        return context;
    }
}
