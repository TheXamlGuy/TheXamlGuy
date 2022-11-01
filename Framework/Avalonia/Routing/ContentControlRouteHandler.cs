using Avalonia.Controls;
using Avalonia.Controls.Primitives;

namespace TheXamlGuy.Framework.Avalonia;

public class ContentControlRouteHandler : RouteHandler<ContentControl>
{
    public override void Handle(Route<ContentControl> request)
    {
        if (request.Template is TemplatedControl control)
        {
            control.DataContext = request.Data;
            request.Target.Content = control;
        }
    }
}
