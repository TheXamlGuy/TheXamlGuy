using System.Windows;

namespace TheXamlGuy.Framework.WPF;

public class Interaction
{
    public static readonly DependencyProperty XamlEventAggregatorProperty =
        DependencyProperty.RegisterAttached("XamlEventAggregator",
            typeof(XamlEventAggregator), typeof(Interaction));

    public static readonly DependencyProperty InteractiveFrameProperty =
        DependencyProperty.RegisterAttached("TouchFrame",
            typeof(InteractiveFrame), typeof(Interaction));

    public static XamlEventAggregator GetXamlEventAggregator(DependencyObject dependencyObject)
    {
        return (XamlEventAggregator)dependencyObject.GetValue(XamlEventAggregatorProperty);
    }

    public static XamlEventAggregator GetInteractiveFrame(DependencyObject dependencyObject)
    {
        return (XamlEventAggregator)dependencyObject.GetValue(InteractiveFrameProperty);
    }

    public static void SetXamlEventAggregator(DependencyObject dependencyObject, XamlEventAggregator value)
    {
        dependencyObject.SetValue(XamlEventAggregatorProperty, value);
    }

    public static void SetInteractiveFrame(DependencyObject dependencyObject, InteractiveFrame value)
    {
        dependencyObject.SetValue(InteractiveFrameProperty, value);
    }
}
