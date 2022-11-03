using Avalonia.Styling;
using PropertyChanged;
using System;
using TheXamlGuy.UI.Avalonia.Controls;

namespace KingPing;

[DoNotNotify]
public partial class AnalogOutputView : SettingsExpander, IStyleable
{
    public AnalogOutputView()
    {
        InitializeComponent();
    }

    Type IStyleable.StyleKey => typeof(FluentAvalonia.UI.Controls.SettingsExpander);
}
