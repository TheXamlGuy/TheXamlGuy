using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Data;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using TheXamlGuy.UI.Avalonia;

namespace TheXamlGuy.Framework.Avalonia;

public class RouteExtension : MarkupExtension
{
    private static readonly AttachedProperty<object> RouteProperty =
        AvaloniaProperty.RegisterAttached<RouteExtension, Control, object>("Route");

    private readonly string name;
    private readonly Binding routeBinding;

    public RouteExtension(object route, string name)
    {
        routeBinding = route.ToBinding();
        this.name = name;
    }

    private bool TryGetBinding(AvaloniaObject sender, out object? binding)
    {
        binding = sender.GetValue(StyledElement.DataContextProperty);
        return binding is not null;
    }

    public override object? ProvideValue(IServiceProvider serviceProvider)
    {
        if (serviceProvider.GetService(typeof(IProvideValueTarget)) is IProvideValueTarget target)
        {
            if (target.TargetObject is TemplatedControl control)
            {
                if (!TryGetBinding(control, out object? binding))
                {
                    void HandleDataContextChanged(object? sender, EventArgs args)
                    {
                        if (TryGetBinding(control, out binding))
                        {
                            control.Loaded -= HandleLoaded;
                            control.Bind(RouteProperty, routeBinding);

                            if (control?.GetValue(RouteProperty) is IRouter route)
                            {
                                route.Add(name, control);
                                control.ClearValue(RouteProperty);
                            }
                        }
                    }

                    control.DataContextChanged += HandleDataContextChanged;

                    void HandleLoaded(object? sender, RoutedEventArgs args)
                    {
                        control.Loaded -= HandleLoaded;
                        if (TryGetBinding(control, out binding))
                        {
                            control.Bind(RouteProperty, routeBinding);
                            if (control?.GetValue(RouteProperty) is IRouter route)
                            {
                                route.Add(name, control);
                                control.ClearValue(RouteProperty);
                            }
                        }
                    }

                    control.Loaded += HandleLoaded;
                }
                else
                {
                    control.Bind(RouteProperty, routeBinding);
                    if (control?.GetValue(RouteProperty) is IRouter route)
                    {
                        route.Add(name, control);
                        control.ClearValue(RouteProperty);
                    }
                }
            }
        }

        return null;
    }
}