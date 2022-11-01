using System.Drawing;

namespace TheXamlGuy.Framework.Camera;

public record Captured
{
    public Bitmap? Photo { get; init; }

    public int Width  { get; init; }

    public int Height { get; init; }
}
