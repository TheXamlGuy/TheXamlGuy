using System.Diagnostics.CodeAnalysis;

namespace TheXamlGuy.Framework.Camera;

public class RemoteCameraConfiguration : IRemoteCameraConfiguration
{
    [NotNull]
    public string? Name { get; set; }
}
