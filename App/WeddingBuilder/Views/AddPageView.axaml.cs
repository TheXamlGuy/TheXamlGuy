using Avalonia.Styling;
using FluentAvalonia.UI.Controls;
using PropertyChanged;
using System;

namespace Builder;

[DoNotNotify]
public partial class AddPageView : ContentDialog, IStyleable
{
    public AddPageView()
    {
        InitializeComponent();
    }

    Type IStyleable.StyleKey => typeof(ContentDialog);
}
