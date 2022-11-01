using System.Windows;

namespace TheXamlGuy.UI.WPF.Controls;

public class ProgressRingTemplateSettings : DependencyObject
{
    public static readonly DependencyProperty EndAngleProperty =
        DependencyProperty.Register(nameof(EndAngle),
            typeof(double), typeof(ProgressRingTemplateSettings));

    public double EndAngle
    {
        get => (double)GetValue(EndAngleProperty);
        set => SetValue(EndAngleProperty, value);
    }
}