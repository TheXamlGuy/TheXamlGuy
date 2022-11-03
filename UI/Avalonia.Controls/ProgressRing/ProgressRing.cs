using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;

namespace TheXamlGuy.UI.Avalonia.Controls
{
    public class ProgressRing : RangeBase
    {
        public static readonly StyledProperty<bool> IsIndeterminateProperty =
            AvaloniaProperty.Register<ProgressRing, bool>(nameof(IsIndeterminate), true);

        public bool IsIndeterminate
        {
            get => GetValue(IsIndeterminateProperty);
            set => SetValue(IsIndeterminateProperty, value);
        }

        protected override void OnApplyTemplate(TemplateAppliedEventArgs args)
        {
            UpdateIsIndeterminate();
        }

        private void UpdateIsIndeterminate()
        {
            PseudoClasses.Set(":indeterminate", IsIndeterminate);

        }
        protected override void OnPropertyChanged(AvaloniaPropertyChangedEventArgs change)
        {
            base.OnPropertyChanged(change);
            if (change.Property == IsIndeterminateProperty)
            {
                UpdateIsIndeterminate();
            }
        }
    }
}
