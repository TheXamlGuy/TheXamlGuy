using Avalonia.Styling;

namespace TheXamlGuy.UI.Avalonia.Controls;

public class PathIconEx : FluentAvalonia.UI.Controls.FAPathIcon, IStyleable
{
    Type IStyleable.StyleKey => typeof(FluentAvalonia.UI.Controls.FAPathIcon);
}