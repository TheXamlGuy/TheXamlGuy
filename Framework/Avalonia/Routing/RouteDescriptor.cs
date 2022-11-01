namespace TheXamlGuy.Framework.Avalonia;

public record RouteDescriptor : IRouteDescriptor
{
    public RouteDescriptor(string name, object route)
    {
        Name = name;
        Route = route;
    }

    public string Name { get; }

    public object Route { get; }
}