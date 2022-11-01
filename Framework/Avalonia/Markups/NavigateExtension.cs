using Avalonia.Controls;
using Avalonia;
using Avalonia.Data;
using Avalonia.Markup.Xaml;
using TheXamlGuy.Framework.Core;
using TheXamlGuy.UI.Avalonia;

namespace TheXamlGuy.Framework.Avalonia;

public class NavigateExtension : TriggerExtension
{
    private static readonly AttachedProperty<IEventAggregator> EventAggregatorProperty =
        AvaloniaProperty.RegisterAttached<NavigateExtension, Control, IEventAggregator>("EventAggregator");

    private static readonly AttachedProperty<object> ParameterProperty =
        AvaloniaProperty.RegisterAttached<NavigateExtension, Control, object>("Parameter");

    private static readonly AvaloniaProperty RouteProperty =
        AvaloniaProperty.RegisterAttached<NavigateExtension, Control, object>("Route");

    private static readonly AvaloniaProperty ToProperty =
        AvaloniaProperty.RegisterAttached<NavigateExtension, Control, object>("To");

    private readonly Binding eventAggregatorBinding;
    private readonly List<object> parameters = new();
    private readonly Binding toBinding;
    private object? route;
    private Binding? routeBinding;

    public NavigateExtension(object eventAggregator,
        object to)
    {
        eventAggregatorBinding = eventAggregator.ToBinding();
        this.toBinding = to is Binding toBinding ? toBinding : to.ToBinding();
    }

    public NavigateExtension(object eventAggregator,
        object to,
        object args1)
    {
        eventAggregatorBinding = eventAggregator.ToBinding();
        this.toBinding = to is Binding toBinding ? toBinding : to.ToBinding();

        parameters.Add(args1 is MarkupExtension ? args1 : args1.ToBinding());
    }

    public NavigateExtension(object eventAggregator,
        object to,
        object args1,
        object args2)
    {
        eventAggregatorBinding = eventAggregator.ToBinding();
        this.toBinding = to is Binding toBinding ? toBinding : to.ToBinding();

        parameters.Add(args1 is MarkupExtension ? args1 : args1.ToBinding());
        parameters.Add(args2 is MarkupExtension ? args2 : args2.ToBinding());
    }

    public NavigateExtension(object eventAggregator,
        object to,
        object args1,
        object args2,
        object args3)
    {
        eventAggregatorBinding = eventAggregator.ToBinding();
        this.toBinding = to is Binding toBinding ? toBinding : to.ToBinding();

        parameters.Add(args1 is MarkupExtension ? args1 : args1.ToBinding());
        parameters.Add(args2 is MarkupExtension ? args2 : args2.ToBinding());
        parameters.Add(args3 is MarkupExtension ? args3 : args3.ToBinding());
    }

    public NavigateExtension(object eventAggregator,
        object to,
        object args1,
        object args2,
        object args3,
        object args4)
    {
        eventAggregatorBinding = eventAggregator.ToBinding();
        this.toBinding = to is Binding toBinding ? toBinding : to.ToBinding();

        parameters.Add(args1 is MarkupExtension ? args1 : args1.ToBinding());
        parameters.Add(args2 is MarkupExtension ? args2 : args2.ToBinding());
        parameters.Add(args3 is MarkupExtension ? args3 : args3.ToBinding());
        parameters.Add(args4 is MarkupExtension ? args4 : args4.ToBinding());
    }

    public NavigateExtension(object eventAggregator,
        object to,
        object args1,
        object args2,
        object args3,
        object args4,
        object args5)
    {
        eventAggregatorBinding = eventAggregator.ToBinding();
        this.toBinding = to is Binding toBinding ? toBinding : to.ToBinding();

        parameters.Add(args1 is MarkupExtension ? args1 : args1.ToBinding());
        parameters.Add(args2 is MarkupExtension ? args2 : args2.ToBinding());
        parameters.Add(args3 is MarkupExtension ? args3 : args3.ToBinding());
        parameters.Add(args4 is MarkupExtension ? args4 : args4.ToBinding());
        parameters.Add(args5 is MarkupExtension ? args5 : args5.ToBinding());
    }

    public NavigateExtension(object eventAggregator,
        object to,
        object args1,
        object args2,
        object args3,
        object args4,
        object args5,
        object args6)
    {
        eventAggregatorBinding = eventAggregator.ToBinding();
        this.toBinding = to is Binding toBinding ? toBinding : to.ToBinding();

        parameters.Add(args1 is MarkupExtension ? args1 : args1.ToBinding());
        parameters.Add(args2 is MarkupExtension ? args2 : args2.ToBinding());
        parameters.Add(args3 is MarkupExtension ? args3 : args3.ToBinding());
        parameters.Add(args4 is MarkupExtension ? args4 : args4.ToBinding());
        parameters.Add(args5 is MarkupExtension ? args5 : args5.ToBinding());
        parameters.Add(args6 is MarkupExtension ? args6 : args6.ToBinding());
    }

    public NavigateExtension(object eventAggregator,
        object to,
        object args1,
        object args2,
        object args3,
        object args4,
        object args5,
        object args6,
        object args7)
    {
        eventAggregatorBinding = eventAggregator.ToBinding();
        this.toBinding = to is Binding toBinding ? toBinding : to.ToBinding();

        parameters.Add(args1 is MarkupExtension ? args1 : args1.ToBinding());
        parameters.Add(args2 is MarkupExtension ? args2 : args2.ToBinding());
        parameters.Add(args3 is MarkupExtension ? args3 : args3.ToBinding());
        parameters.Add(args4 is MarkupExtension ? args4 : args4.ToBinding());
        parameters.Add(args5 is MarkupExtension ? args5 : args5.ToBinding());
        parameters.Add(args6 is MarkupExtension ? args6 : args6.ToBinding());
        parameters.Add(args7 is MarkupExtension ? args7 : args7.ToBinding());
    }

    public NavigateExtension(object eventAggregator,
        object to,
        object args1,
        object args2,
        object args3,
        object args4,
        object args5,
        object args6,
        object args7,
        object args8)
    {
        eventAggregatorBinding = eventAggregator.ToBinding();
        this.toBinding = to is Binding toBinding ? toBinding : to.ToBinding();

        parameters.Add(args1 is MarkupExtension ? args1 : args1.ToBinding());
        parameters.Add(args2 is MarkupExtension ? args2 : args2.ToBinding());
        parameters.Add(args3 is MarkupExtension ? args3 : args3.ToBinding());
        parameters.Add(args4 is MarkupExtension ? args4 : args4.ToBinding());
        parameters.Add(args5 is MarkupExtension ? args5 : args5.ToBinding());
        parameters.Add(args6 is MarkupExtension ? args6 : args6.ToBinding());
        parameters.Add(args7 is MarkupExtension ? args7 : args7.ToBinding());
        parameters.Add(args8 is MarkupExtension ? args8 : args8.ToBinding());
    }

    public NavigateExtension(object eventAggregator,
        object to,
        object args1,
        object args2,
        object args3,
        object args4,
        object args5,
        object args6,
        object args7,
        object args8,
        object args9)
    {
        eventAggregatorBinding = eventAggregator.ToBinding();
        this.toBinding = to is Binding toBinding ? toBinding : to.ToBinding();

        parameters.Add(args1 is MarkupExtension ? args1 : args1.ToBinding());
        parameters.Add(args2 is MarkupExtension ? args2 : args2.ToBinding());
        parameters.Add(args3 is MarkupExtension ? args3 : args3.ToBinding());
        parameters.Add(args4 is MarkupExtension ? args4 : args4.ToBinding());
        parameters.Add(args5 is MarkupExtension ? args5 : args5.ToBinding());
        parameters.Add(args6 is MarkupExtension ? args6 : args6.ToBinding());
        parameters.Add(args7 is MarkupExtension ? args7 : args7.ToBinding());
        parameters.Add(args8 is MarkupExtension ? args8 : args8.ToBinding());
        parameters.Add(args9 is MarkupExtension ? args9 : args9.ToBinding());
    }

    public NavigateExtension(object eventAggregator,
        object to,
        object args1,
        object args2,
        object args3,
        object args4,
        object args5,
        object args6,
        object args7,
        object args8,
        object args9,
        object args10)
    {
        eventAggregatorBinding = eventAggregator.ToBinding();
        this.toBinding = to is Binding toBinding ? toBinding : to.ToBinding();

        parameters.Add(args1 is MarkupExtension ? args1 : args1.ToBinding());
        parameters.Add(args2 is MarkupExtension ? args2 : args2.ToBinding());
        parameters.Add(args3 is MarkupExtension ? args3 : args3.ToBinding());
        parameters.Add(args4 is MarkupExtension ? args4 : args4.ToBinding());
        parameters.Add(args5 is MarkupExtension ? args5 : args5.ToBinding());
        parameters.Add(args6 is MarkupExtension ? args6 : args6.ToBinding());
        parameters.Add(args7 is MarkupExtension ? args7 : args7.ToBinding());
        parameters.Add(args8 is MarkupExtension ? args8 : args8.ToBinding());
        parameters.Add(args9 is MarkupExtension ? args9 : args9.ToBinding());
        parameters.Add(args10 is MarkupExtension ? args10 : args10.ToBinding());
    }

    public NavigateExtension(object eventAggregator,
        object to,
        object args1,
        object args2,
        object args3,
        object args4,
        object args5,
        object args6,
        object args7,
        object args8,
        object args9,
        object args10,
        object args11)
    {
        eventAggregatorBinding = eventAggregator.ToBinding();
        this.toBinding = to is Binding toBinding ? toBinding : to.ToBinding();

        parameters.Add(args1 is MarkupExtension ? args1 : args1.ToBinding());
        parameters.Add(args2 is MarkupExtension ? args2 : args2.ToBinding());
        parameters.Add(args3 is MarkupExtension ? args3 : args3.ToBinding());
        parameters.Add(args4 is MarkupExtension ? args4 : args4.ToBinding());
        parameters.Add(args5 is MarkupExtension ? args5 : args5.ToBinding());
        parameters.Add(args6 is MarkupExtension ? args6 : args6.ToBinding());
        parameters.Add(args7 is MarkupExtension ? args7 : args7.ToBinding());
        parameters.Add(args8 is MarkupExtension ? args8 : args8.ToBinding());
        parameters.Add(args9 is MarkupExtension ? args9 : args9.ToBinding());
        parameters.Add(args10 is MarkupExtension ? args10 : args10.ToBinding());
        parameters.Add(args11 is MarkupExtension ? args11 : args11.ToBinding());
    }

    public NavigateExtension(object eventAggregator,
        object to,
        object args1,
        object args2,
        object args3,
        object args4,
        object args5,
        object args6,
        object args7,
        object args8,
        object args9,
        object args10,
        object args11,
        object args12)
    {
        eventAggregatorBinding = eventAggregator.ToBinding();
        this.toBinding = to is Binding toBinding ? toBinding : to.ToBinding();

        parameters.Add(args1 is MarkupExtension ? args1 : args1.ToBinding());
        parameters.Add(args2 is MarkupExtension ? args2 : args2.ToBinding());
        parameters.Add(args3 is MarkupExtension ? args3 : args3.ToBinding());
        parameters.Add(args4 is MarkupExtension ? args4 : args4.ToBinding());
        parameters.Add(args5 is MarkupExtension ? args5 : args5.ToBinding());
        parameters.Add(args6 is MarkupExtension ? args6 : args6.ToBinding());
        parameters.Add(args7 is MarkupExtension ? args7 : args7.ToBinding());
        parameters.Add(args8 is MarkupExtension ? args8 : args8.ToBinding());
        parameters.Add(args9 is MarkupExtension ? args9 : args9.ToBinding());
        parameters.Add(args10 is MarkupExtension ? args10 : args10.ToBinding());
        parameters.Add(args11 is MarkupExtension ? args11 : args11.ToBinding());
        parameters.Add(args12 is MarkupExtension ? args12 : args12.ToBinding());
    }

    public NavigateExtension(object eventAggregator,
        object to,
        object args1,
        object args2,
        object args3,
        object args4,
        object args5,
        object args6,
        object args7,
        object args8,
        object args9,
        object args10,
        object args11,
        object args12,
        object args13)
    {
        eventAggregatorBinding = eventAggregator.ToBinding();
        this.toBinding = to is Binding toBinding ? toBinding : to.ToBinding();

        parameters.Add(args1 is MarkupExtension ? args1 : args1.ToBinding());
        parameters.Add(args2 is MarkupExtension ? args2 : args2.ToBinding());
        parameters.Add(args3 is MarkupExtension ? args3 : args3.ToBinding());
        parameters.Add(args4 is MarkupExtension ? args4 : args4.ToBinding());
        parameters.Add(args5 is MarkupExtension ? args5 : args5.ToBinding());
        parameters.Add(args6 is MarkupExtension ? args6 : args6.ToBinding());
        parameters.Add(args7 is MarkupExtension ? args7 : args7.ToBinding());
        parameters.Add(args8 is MarkupExtension ? args8 : args8.ToBinding());
        parameters.Add(args9 is MarkupExtension ? args9 : args9.ToBinding());
        parameters.Add(args10 is MarkupExtension ? args10 : args10.ToBinding());
        parameters.Add(args11 is MarkupExtension ? args11 : args11.ToBinding());
        parameters.Add(args12 is MarkupExtension ? args12 : args12.ToBinding());
        parameters.Add(args13 is MarkupExtension ? args13 : args13.ToBinding());
    }

    public NavigateExtension(object eventAggregator,
        object to,
        object args1,
        object args2,
        object args3,
        object args4,
        object args5,
        object args6,
        object args7,
        object args8,
        object args9,
        object args10,
        object args11,
        object args12,
        object args13,
        object args14)
    {
        eventAggregatorBinding = eventAggregator.ToBinding();
        this.toBinding = to is Binding toBinding ? toBinding : to.ToBinding();

        parameters.Add(args1 is MarkupExtension ? args1 : args1.ToBinding());
        parameters.Add(args2 is MarkupExtension ? args2 : args2.ToBinding());
        parameters.Add(args3 is MarkupExtension ? args3 : args3.ToBinding());
        parameters.Add(args4 is MarkupExtension ? args4 : args4.ToBinding());
        parameters.Add(args5 is MarkupExtension ? args5 : args5.ToBinding());
        parameters.Add(args6 is MarkupExtension ? args6 : args6.ToBinding());
        parameters.Add(args7 is MarkupExtension ? args7 : args7.ToBinding());
        parameters.Add(args8 is MarkupExtension ? args8 : args8.ToBinding());
        parameters.Add(args9 is MarkupExtension ? args9 : args9.ToBinding());
        parameters.Add(args10 is MarkupExtension ? args10 : args10.ToBinding());
        parameters.Add(args11 is MarkupExtension ? args11 : args11.ToBinding());
        parameters.Add(args12 is MarkupExtension ? args12 : args12.ToBinding());
        parameters.Add(args13 is MarkupExtension ? args13 : args13.ToBinding());
        parameters.Add(args14 is MarkupExtension ? args14 : args14.ToBinding());
    }

    public NavigateExtension(object eventAggregator,
        object to,
        object args1,
        object args2,
        object args3,
        object args4,
        object args5,
        object args6,
        object args7,
        object args8,
        object args9,
        object args10,
        object args11,
        object args12,
        object args13,
        object args14,
        object args15)
    {
        eventAggregatorBinding = eventAggregator.ToBinding();
        this.toBinding = to is Binding toBinding ? toBinding : to.ToBinding();

        parameters.Add(args1 is MarkupExtension ? args1 : args1.ToBinding());
        parameters.Add(args2 is MarkupExtension ? args2 : args2.ToBinding());
        parameters.Add(args3 is MarkupExtension ? args3 : args3.ToBinding());
        parameters.Add(args4 is MarkupExtension ? args4 : args4.ToBinding());
        parameters.Add(args5 is MarkupExtension ? args5 : args5.ToBinding());
        parameters.Add(args6 is MarkupExtension ? args6 : args6.ToBinding());
        parameters.Add(args7 is MarkupExtension ? args7 : args7.ToBinding());
        parameters.Add(args8 is MarkupExtension ? args8 : args8.ToBinding());
        parameters.Add(args9 is MarkupExtension ? args9 : args9.ToBinding());
        parameters.Add(args10 is MarkupExtension ? args10 : args10.ToBinding());
        parameters.Add(args11 is MarkupExtension ? args11 : args11.ToBinding());
        parameters.Add(args12 is MarkupExtension ? args12 : args12.ToBinding());
        parameters.Add(args13 is MarkupExtension ? args13 : args13.ToBinding());
        parameters.Add(args14 is MarkupExtension ? args14 : args14.ToBinding());
        parameters.Add(args15 is MarkupExtension ? args15 : args15.ToBinding());
    }

    public object? Route
    {
        get
        {
            return route;
        }
        set
        {
            route = value;
            if (route is not null)
            {
                routeBinding = route.ToBinding();
            }
        }

    }

    protected override void OnInvoked(object sender, EventArgs args)
    {
        if (TargetObject is not null)
        {
            TargetObject.Bind(EventAggregatorProperty, eventAggregatorBinding);
            if (TargetObject.GetValue(EventAggregatorProperty) is IEventAggregator eventAggregator)
            {
               TargetObject.Bind(ToProperty, toBinding);
                if (TargetObject.GetValue(ToProperty) is { } to)
                {
                    object? route = null;
                    if (routeBinding is not null)
                    {
                        TargetObject.Bind(RouteProperty, routeBinding);
                        route = TargetObject.GetValue(RouteProperty);
                    }

                    if (to is string name)
                    {
                        if (toBinding?.StringFormat is string format)
                        {
                            name = string.Format(format, name);
                        }

                        eventAggregator.Publish(new Navigate(name, parameters.ToArray()) { Route = route });
                    }

                    if (to is Type type)
                    {
                        eventAggregator.Publish(new Navigate(type, parameters.ToArray()) { Route = route });
                    }
                }

            }

            base.OnInvoked(sender, args);
        }

    }
}
