using Avalonia.Styling;
using FluentAvalonia.UI.Controls;
using PropertyChanged;
using System;

namespace Builder;

[DoNotNotify]
public partial class ProjectConfigurationView : ContentDialog, IStyleable
{
    public ProjectConfigurationView()
    {
        InitializeComponent();
    }

    Type IStyleable.StyleKey => typeof(ContentDialog);
}
