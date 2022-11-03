using Avalonia;
using Avalonia.Controls.Primitives;

namespace TheXamlGuy.UI.Avalonia.Controls
{
    public class ProgressRingTemplateSettings : TemplatedControl
    {
        public static readonly StyledProperty<double> SweepAngleProperty =
            AvaloniaProperty.Register<ProgressRingTemplateSettings, double>(nameof(SweepAngle), 0);

        public double SweepAngle
        {
            get => GetValue(SweepAngleProperty);
            set => SetValue(SweepAngleProperty, value);
        }
    }
}
