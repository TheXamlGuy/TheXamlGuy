using System.Collections.Generic;

namespace TheXamlGuy.Framework.WPF
{
    public class RouteDescriptorCollection : List<IRouteDescriptor>, IRouteDescriptorCollection
    {
        public RouteDescriptorCollection(IEnumerable<IRouteDescriptor> collection) : base(collection)
        {

        }
    }
}