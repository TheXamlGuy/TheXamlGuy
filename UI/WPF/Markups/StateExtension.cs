using System.Windows;
using System.Windows.Data;

namespace TheXamlGuy.UI.WPF;

public class StateExtension : TriggerExtension
{
    private static readonly DependencyProperty PropertyTargetProperty =
        DependencyProperty.RegisterAttached("PropertyTarget",
            typeof(object), typeof(StateExtension));

    private static readonly DependencyProperty StateProperty =
        DependencyProperty.RegisterAttached("State",
            typeof(object), typeof(StateExtension));

    private readonly BindingBase targetBinding;

    private readonly BindingBase stateBinding;

    public StateExtension(object target, object state)
    {
        this.targetBinding = target is BindingBase targetBinding ? targetBinding : target.ToBinding();
        this.stateBinding = state is BindingBase stateBinding ? stateBinding : state.ToBinding();
    }

    protected override void OnInvoked(object sender, EventArgs args)
    {
        BindingOperations.SetBinding(TargetObject, PropertyTargetProperty, targetBinding);
        if (TargetObject?.GetValue(PropertyTargetProperty) is FrameworkElement target)
        {
            BindingOperations.SetBinding(target, StateProperty, stateBinding);
            object? state = target.GetValue(StateProperty);
            target.ApplyTemplate();
            
            VisualStateManager.GoToElementState(target, (string)state, true);
        }
    }
}
