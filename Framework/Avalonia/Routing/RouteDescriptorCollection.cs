namespace TheXamlGuy.Framework.Avalonia;

public class RouteDescriptorCollection : List<IRouteDescriptor>, IRouteDescriptorCollection
{
    public RouteDescriptorCollection(IEnumerable<IRouteDescriptor> collection) : base(collection)
    {

    }
}