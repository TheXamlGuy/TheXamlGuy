using Avalonia.Styling;

namespace TheXamlGuy.UI.Avalonia.Controls;

public class NavigationViewItem : FluentAvalonia.UI.Controls.NavigationViewItem, IStyleable
{
    Type IStyleable.StyleKey => typeof(FluentAvalonia.UI.Controls.NavigationViewItem);
}
