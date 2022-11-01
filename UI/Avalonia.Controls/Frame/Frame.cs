using Avalonia.Styling;

namespace TheXamlGuy.UI.Avalonia.Controls;

public class Frame : FluentAvalonia.UI.Controls.Frame, IStyleable
{
    Type IStyleable.StyleKey => typeof(FluentAvalonia.UI.Controls.Frame);
}