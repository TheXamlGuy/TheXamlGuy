using Avalonia.Controls.Primitives;
using Avalonia.Interactivity;

namespace TheXamlGuy.Framework.Avalonia;

public class Router : IRouter
{
    private readonly IRouteDescriptorCollection routes;

    public Router(IRouteDescriptorCollection routes)
    {
        this.routes = routes;
    }

    public void Add(string name, object route)
    {
        if (route is TemplatedControl control)
        {
            void HandleUnloaded(object? sender, RoutedEventArgs args)
            {
                if (routes.FirstOrDefault(x => x.Route == sender) is IRouteDescriptor descriptor)
                {
                    routes.Remove(descriptor);
                }
            }

            control.Unloaded += HandleUnloaded;
        }

        if (routes.FirstOrDefault(x => x.Name == name) is IRouteDescriptor descriptor)
        {
            routes.Remove(descriptor);
        }

        routes.Add(new RouteDescriptor(name, route));
    }
}