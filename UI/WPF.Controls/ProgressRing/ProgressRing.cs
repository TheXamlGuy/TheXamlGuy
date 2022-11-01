using System.Windows;
using System.Windows.Controls.Primitives;

namespace TheXamlGuy.UI.WPF.Controls;

public class ProgressRing : RangeBase
{
    public static readonly DependencyProperty IsActiveProperty =
        DependencyProperty.Register(nameof(IsActive),
            typeof(bool), typeof(ProgressRing), new PropertyMetadata(true, OnIsActivePropertyChanged));

    public static readonly DependencyProperty IsIndeterminateProperty =
        DependencyProperty.Register(nameof(IsIndeterminate),
            typeof(bool), typeof(ProgressRing), new PropertyMetadata(false, OnIsIndeterminatePropertyChanged));

    public static readonly DependencyProperty ThicknessProperty =
        DependencyProperty.Register(nameof(Thickness),
            typeof(double), typeof(ProgressRing));

    public static DependencyProperty TemplateSettingsProperty =
        DependencyProperty.Register(nameof(TemplateSettings),
            typeof(ProgressRingTemplateSettings), typeof(ProgressRing));

    public ProgressRing()
    {
        DefaultStyleKey = typeof(ProgressRing);
        SetValue(TemplateSettingsProperty, new ProgressRingTemplateSettings());
    }

    public bool IsActive
    {
        get => (bool)GetValue(IsActiveProperty);
        set => SetValue(IsActiveProperty, value);
    }

    public bool IsIndeterminate
    {
        get => (bool)GetValue(IsIndeterminateProperty);
        set => SetValue(IsIndeterminateProperty, value);
    }

    public double Thickness
    {
        get => (double)GetValue(ThicknessProperty);
        set => SetValue(ThicknessProperty, value);
    }

    public ProgressRingTemplateSettings TemplateSettings
    {
        get => (ProgressRingTemplateSettings)GetValue(TemplateSettingsProperty);
        set => SetValue(TemplateSettingsProperty, value);
    }

    public override void OnApplyTemplate()
    {
        UpdateValue();
        UpdateVisualState();
    }

    protected override void OnValueChanged(double oldValue, double newValue)
    {
        UpdateValue();
        base.OnValueChanged(oldValue, newValue);
    }

    private void UpdateValue()
    {
        TemplateSettings.EndAngle = Value == Maximum ? 359.999 : 359.999 * (Value / (Maximum - Minimum));
    }

    private static void OnIsActivePropertyChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs args)
    {
        (dependencyObject as ProgressRing)?.OnIsActivePropertyChanged();
    }

    private static void OnIsIndeterminatePropertyChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs args)
    {
        (dependencyObject as ProgressRing)?.OnIsIndeterminatePropertyChanged();
    }

    private void OnIsActivePropertyChanged()
    {
        UpdateVisualState();
    }

    private void OnIsIndeterminatePropertyChanged()
    {
        UpdateVisualState();
    }

    private void UpdateVisualState()
    {
        VisualStateManager.GoToState(this, IsActive ? IsIndeterminate ? "IndeterminateActive" : "Active" : "Inactive", true);
    }
}