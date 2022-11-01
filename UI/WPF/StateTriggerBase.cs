using System.Windows;

namespace TheXamlGuy.UI.WPF;

public abstract class StateTriggerBase : DependencyObject
{
    internal bool IsTriggerActive { get; private set; }

    internal VisualStateExtension? Owner { get; set; }

    protected void SetActive(bool isActive)
    {
        IsTriggerActive = isActive;
        Owner?.SetActive(IsTriggerActive);
    }
}