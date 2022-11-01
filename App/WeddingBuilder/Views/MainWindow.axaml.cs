using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;
using Avalonia.Media.Immutable;
using FluentAvalonia.Styling;
using FluentAvalonia.UI.Controls;
using FluentAvalonia.UI.Media;
using PropertyChanged;
using System;
using System.Runtime.InteropServices;

namespace Builder;

[DoNotNotify]
public partial class MainWindow : CoreWindow
{
    public MainWindow()
    {
        InitializeComponent();
    }

    protected override void OnOpened(EventArgs args)
    {
        if (AvaloniaLocator.Current.GetService<FluentAvaloniaTheme>() is FluentAvaloniaTheme theme)
        {
            theme.RequestedThemeChanged += OnRequestedThemeChanged;

            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                if (IsWindows11 && theme.RequestedTheme != FluentAvaloniaTheme.HighContrastModeString)
                {
                    TransparencyBackgroundFallback = Brushes.Transparent;
                    TransparencyLevelHint = WindowTransparencyLevel.Mica;

                    TryEnableMicaEffect(theme);
                }
            }
        }

        base.OnOpened(args);
    }

    private void OnRequestedThemeChanged(FluentAvaloniaTheme sender, RequestedThemeChangedEventArgs args)
    {
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            if (IsWindows11 && args.NewTheme != FluentAvaloniaTheme.HighContrastModeString)
            {
                TryEnableMicaEffect(sender);
            }
            else if (args.NewTheme == FluentAvaloniaTheme.HighContrastModeString)
            {
                SetValue(BackgroundProperty, AvaloniaProperty.UnsetValue);
            }
        }
    }

    private void TryEnableMicaEffect(FluentAvaloniaTheme theme)
    {
        if (theme.RequestedTheme == FluentAvaloniaTheme.DarkModeString)
        {
            Color2 color = this.TryFindResource("SolidBackgroundFillColorBase", out object? value) ? (Color2)(Color)value! : new Color2(32, 32, 32);
            color = color.LightenPercent(-0.5f);

            Background = new ImmutableSolidColorBrush(color, 0.78);
        }
        else if (theme.RequestedTheme == FluentAvaloniaTheme.LightModeString)
        {
            Color2 color = this.TryFindResource("SolidBackgroundFillColorBase", out object? value) ? (Color2)(Color)value! : new Color2(243, 243, 243);
            color = color.LightenPercent(0.5f);

            Background = new ImmutableSolidColorBrush(color, 0.9);
        }
    }
}
