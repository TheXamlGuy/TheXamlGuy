using TheXamlGuy.UI.Avalonia.Controls;

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
