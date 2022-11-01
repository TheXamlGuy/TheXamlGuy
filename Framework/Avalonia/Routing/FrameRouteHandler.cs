using Avalonia.Controls.Primitives;
using FluentAvalonia.UI.Controls;
using FluentAvalonia.UI.Navigation;

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
