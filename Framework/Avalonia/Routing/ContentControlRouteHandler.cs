using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using FluentAvalonia.UI.Controls;

namespace TheXamlGuy.Framework.Avalonia;

public class ContentDialogRouteHandler : RouteHandler<ContentDialog>
{
    public override async void Handle(Route<ContentDialog> request)
    {
        if (request.Template is ContentDialog contentDialog)
        {
            contentDialog.DataContext = request.Data;
            await contentDialog.ShowAsync();
        }
    }
}


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
