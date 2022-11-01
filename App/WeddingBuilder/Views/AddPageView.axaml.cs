using Avalonia.Styling;
using PropertyChanged;
using System;
using TheXamlGuy.UI.Avalonia.Controls;

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
