using Avalonia.Styling;

namespace TheXamlGuy.UI.Avalonia.Controls;

public class NavigationView : FluentAvalonia.UI.Controls.NavigationView, IStyleable
{
    Type IStyleable.StyleKey => typeof(FluentAvalonia.UI.Controls.NavigationView);
}
