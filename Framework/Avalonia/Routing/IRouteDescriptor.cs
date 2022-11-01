namespace TheXamlGuy.Framework.Avalonia;

public interface IRouteDescriptor
{
    object Route { get; }

    string? Name { get; }
}