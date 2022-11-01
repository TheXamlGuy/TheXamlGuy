using Avalonia.Styling;

namespace TheXamlGuy.UI.Avalonia.Controls;

public class SettingsExpander : FluentAvalonia.UI.Controls.SettingsExpander, IStyleable
{
    Type IStyleable.StyleKey => typeof(FluentAvalonia.UI.Controls.SettingsExpander);
}