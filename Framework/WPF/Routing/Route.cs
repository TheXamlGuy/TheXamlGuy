using System.Linq;
using System.Windows;

namespace TheXamlGuy.Framework.WPF
{
    public class Route : IRoute
    {
        private readonly IRouteDescriptorCollection routes;

        public Route(IRouteDescriptorCollection routes)
        {
            this.routes = routes;
        }

        public void AddRoute(string name, object route)
        {
            if (route is FrameworkElement frameworkElement)
            {
                void HandleUnloaded(object sender, RoutedEventArgs args)
                {
                    if (routes.FirstOrDefault(x => x.Route == sender) is IRouteDescriptor descriptor)
                    {
                        routes.Remove(descriptor);
                    }
                }

                frameworkElement.Unloaded += HandleUnloaded;
            }

            if (routes.FirstOrDefault(x => x.Name == name) is IRouteDescriptor descriptor)
            {
                routes.Remove(descriptor);
            }

            routes.Add(new RouteDescriptor(name, route));
        }
    }
}