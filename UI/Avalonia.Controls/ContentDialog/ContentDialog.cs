using Avalonia.Styling;

namespace TheXamlGuy.UI.Avalonia.Controls;

public class ContentDialog : FluentAvalonia.UI.Controls.ContentDialog, IStyleable
{
    Type IStyleable.StyleKey => typeof(FluentAvalonia.UI.Controls.ContentDialog);
}