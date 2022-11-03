using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;

namespace TheXamlGuy.UI.Avalonia.Controls
{
    public class ProgressRing : RangeBase
    {
        public static readonly StyledProperty<bool> IsIndeterminateProperty =
            AvaloniaProperty.Register<ProgressRing, bool>(nameof(IsIndeterminate), false);

        public static readonly StyledProperty<ProgressRingTemplateSettings> TemplateSettingsProperty =
            AvaloniaProperty.Register<ProgressRing, ProgressRingTemplateSettings>(nameof(TemplateSettings));

        public ProgressRing()
        {
            SetValue(TemplateSettingsProperty, new ProgressRingTemplateSettings());
        }

        public bool IsIndeterminate
        {
            get => GetValue(IsIndeterminateProperty);
            set => SetValue(IsIndeterminateProperty, value);
        }

        public ProgressRingTemplateSettings TemplateSettings
        {
            get => GetValue(TemplateSettingsProperty);
            set => SetValue(TemplateSettingsProperty, value);
        }

        protected override void OnApplyTemplate(TemplateAppliedEventArgs args)
        {
            UpdateIsIndeterminate();
            UpdateValue();
        }

        protected override void OnPropertyChanged(AvaloniaPropertyChangedEventArgs args)
        {
            base.OnPropertyChanged(args);
            if (args.Property == IsIndeterminateProperty)
            {
                UpdateIsIndeterminate();
            }
            
            if (args.Property == ValueProperty)
            {
                UpdateValue();
            }
        }

        private void UpdateIsIndeterminate()
        {
            PseudoClasses.Set(":indeterminate", IsIndeterminate);
        }

        private void UpdateValue()
        {
            TemplateSettings.SetValue(ProgressRingTemplateSettings.SweepAngleProperty, Value == Maximum ? 360 : 360 * (Value / (Maximum - Minimum)));
        }
    }
}
