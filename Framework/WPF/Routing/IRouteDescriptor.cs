using System.Collections.Generic;

namespace TheXamlGuy.Framework.WPF
{
    public interface IRouteDescriptor
    {
        object Route { get; }

        string? Name { get; }
    }
}