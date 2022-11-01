using Avalonia.Controls.Primitives;
using FluentAvalonia.UI.Navigation;
using TheXamlGuy.UI.Avalonia.Controls;

namespace TheXamlGuy.Framework.Avalonia;

public class FrameRouteHandler : RouteHandler<Frame>
{
    public override void Handle(Route<Frame> request)
    {
        if (request.Template is Type type)
        {
            void HandleNavigated(object sender, NavigationEventArgs args)
            {
                request.Target.Navigated -= HandleNavigated;
                if (request.Target.Content is TemplatedControl control)
                {
                    control.DataContext = request.Data;
                }
            }

            request.Target.Navigated += HandleNavigated;
            request.Target.Navigate(type);
        }
    }
}
