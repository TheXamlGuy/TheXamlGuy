using Avalonia.Markup.Xaml;
using Avalonia.Styling;
using FluentAvalonia.Styling;
using FluentAvalonia.UI.Controls.Primitives;

namespace TheXamlGuy.UI.Avalonia;

public class XamlControlsResources : FluentAvaloniaTheme
{
    public XamlControlsResources(IServiceProvider serviceProvider) : base(serviceProvider)
    {
    }

    //private void XamlControlsResources_OwnerChanged(object? sender, EventArgs e)
    //{
    //    Styles styles = (Styles)AvaloniaXamlLoader.Load(new Uri($"avares://TheXamlGuy.UI.Avalonia/Themes/ControlResources.axaml"));
        
    //    foreach (var style in styles)
    //    {
    //        style.add
    //    }
    //}
}
