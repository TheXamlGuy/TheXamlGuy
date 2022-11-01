namespace TheXamlGuy.Framework.Camera;

public interface ICameraFactory
{
    ICameraContext Create(INamedCameraConfiguration configuration);
}