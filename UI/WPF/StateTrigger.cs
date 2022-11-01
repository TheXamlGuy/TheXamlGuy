using System.Windows;

namespace TheXamlGuy.UI.WPF;

public class StateTrigger : StateTriggerBase
{
    public static readonly DependencyProperty IsActiveProperty =
        DependencyProperty.Register(nameof(IsActive),
            typeof(bool), typeof(StateTrigger),
            new PropertyMetadata(false, OnIsActivePropertyChanged));

    public bool IsActive
    {
        get => (bool)GetValue(IsActiveProperty);
        set => SetValue(IsActiveProperty, value);
    }

    private static void OnIsActivePropertyChanged(DependencyObject dependencyObject,  DependencyPropertyChangedEventArgs args)
    {
        (dependencyObject as StateTrigger)?.SetActive((bool)args.NewValue);
    }
}