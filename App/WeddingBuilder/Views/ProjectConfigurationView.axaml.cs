using Avalonia.Styling;
using PropertyChanged;
using System;
using TheXamlGuy.UI.Avalonia.Controls;

namespace Builder;

[DoNotNotify]
public partial class ProjectConfigurationView : ContentDialog, IStyleable
{
    public ProjectConfigurationView()
    {
        InitializeComponent();
    }

    Type IStyleable.StyleKey => typeof(FluentAvalonia.UI.Controls.ContentDialog);
}
