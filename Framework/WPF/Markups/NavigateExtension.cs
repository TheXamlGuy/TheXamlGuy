using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;
using TheXamlGuy.Framework.Core;
using TheXamlGuy.UI;
using TheXamlGuy.UI.WPF;

namespace TheXamlGuy.Framework.WPF;

[ContentProperty("Parameters")]
public class NavigateExtension : TriggerExtension
{
    private static readonly DependencyProperty EventAggregatorProperty =
        DependencyProperty.RegisterAttached("EventAggregator",
            typeof(IEventAggregator), typeof(NavigateExtension));
    
    private static readonly DependencyProperty ParameterProperty =
        DependencyProperty.RegisterAttached("Parameter",
            typeof(object), typeof(NavigateExtension));

    private static readonly DependencyProperty ToProperty =
        DependencyProperty.RegisterAttached("To",
            typeof(object), typeof(NavigateExtension));

    private static readonly DependencyProperty RouteProperty =
        DependencyProperty.RegisterAttached("Route",
            typeof(object), typeof(NavigateExtension));

    private readonly Binding eventAggregatorBinding;
    private readonly Binding? toBinding;
    private readonly Binding? routeBinding;
    private readonly List<object> parameters = new();

    public NavigateExtension(object eventAggregator,
        object route, 
        object to)
    {
        eventAggregatorBinding = eventAggregator.ToBinding();
        routeBinding = route.ToBinding();

        this.toBinding = to is Binding toBinding ? toBinding : to.ToBinding();
    }

    public NavigateExtension(object eventAggregator,
        object route,
        object to,
        object args1)
    {
        eventAggregatorBinding = eventAggregator.ToBinding();
        routeBinding = route.ToBinding();

        this.toBinding = to is Binding toBinding ? toBinding : to.ToBinding();

        parameters.Add(args1 is MarkupExtension ? args1 : args1.ToBinding());
    }

    public NavigateExtension(object eventAggregator,
        object route,
        object to,
        object args1,
        object args2)
    {
        eventAggregatorBinding = eventAggregator.ToBinding();
        routeBinding = route.ToBinding();

        this.toBinding = to is Binding toBinding ? toBinding : to.ToBinding();

        parameters.Add(args1 is MarkupExtension ? args1 : args1.ToBinding());
        parameters.Add(args2 is MarkupExtension ? args2 : args2.ToBinding());
    }

    public NavigateExtension(object eventAggregator,
        object route,
        object to,
        object args1,
        object args2,
        object args3)
    {
        eventAggregatorBinding = eventAggregator.ToBinding();
        routeBinding = route.ToBinding();

        this.toBinding = to is Binding toBinding ? toBinding : to.ToBinding();

        parameters.Add(args1 is MarkupExtension ? args1 : args1.ToBinding());
        parameters.Add(args2 is MarkupExtension ? args2 : args2.ToBinding());
        parameters.Add(args3 is MarkupExtension ? args3 : args3.ToBinding());
    }

    public NavigateExtension(object eventAggregator,
        object route,
        object to,
        object args1,
        object args2,
        object args3,
        object args4)
    {
        eventAggregatorBinding = eventAggregator.ToBinding();
        routeBinding = route.ToBinding();

        this.toBinding = to is Binding toBinding ? toBinding : to.ToBinding();

        parameters.Add(args1 is MarkupExtension ? args1 : args1.ToBinding());
        parameters.Add(args2 is MarkupExtension ? args2 : args2.ToBinding());
        parameters.Add(args3 is MarkupExtension ? args3 : args3.ToBinding());
        parameters.Add(args4 is MarkupExtension ? args4 : args4.ToBinding());
    }

    public NavigateExtension(object eventAggregator,
        object route,
        object to,
        object args1,
        object args2,
        object args3,
        object args4,
        object args5)
    {
        eventAggregatorBinding = eventAggregator.ToBinding();
        routeBinding = route.ToBinding();

        this.toBinding = to is Binding toBinding ? toBinding : to.ToBinding();

        parameters.Add(args1 is MarkupExtension ? args1 : args1.ToBinding());
        parameters.Add(args2 is MarkupExtension ? args2 : args2.ToBinding());
        parameters.Add(args3 is MarkupExtension ? args3 : args3.ToBinding());
        parameters.Add(args4 is MarkupExtension ? args4 : args4.ToBinding());
        parameters.Add(args5 is MarkupExtension ? args5 : args5.ToBinding());
    }

    public NavigateExtension(object eventAggregator,
        object route,
        object to,
        object args1,
        object args2,
        object args3,
        object args4,
        object args5,
        object args6)
    {
        eventAggregatorBinding = eventAggregator.ToBinding();
        routeBinding = route.ToBinding();

        this.toBinding = to is Binding toBinding ? toBinding : to.ToBinding();

        parameters.Add(args1 is MarkupExtension ? args1 : args1.ToBinding());
        parameters.Add(args2 is MarkupExtension ? args2 : args2.ToBinding());
        parameters.Add(args3 is MarkupExtension ? args3 : args3.ToBinding());
        parameters.Add(args4 is MarkupExtension ? args4 : args4.ToBinding());
        parameters.Add(args5 is MarkupExtension ? args5 : args5.ToBinding());
        parameters.Add(args6 is MarkupExtension ? args6 : args6.ToBinding());
    }

    public NavigateExtension(object eventAggregator,
        object route,
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
        routeBinding = route.ToBinding();

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
        object route,
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
        routeBinding = route.ToBinding();

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
        object route,
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
        routeBinding = route.ToBinding();

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
        object route,
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
        routeBinding = route.ToBinding();

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
        object route,
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
        routeBinding = route.ToBinding();

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
        object route,
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
        routeBinding = route.ToBinding();

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
        object route,
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
        routeBinding = route.ToBinding();

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
        object route, 
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
        routeBinding = route.ToBinding();

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
        object route,
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
        routeBinding = route.ToBinding();

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

    protected override void OnInvoked(object sender, EventArgs args)
    {
        BindingOperations.SetBinding(TargetObject, EventAggregatorProperty, eventAggregatorBinding);
        if (TargetObject?.GetValue(EventAggregatorProperty) is IEventAggregator eventAggregator)
        {
            List<object>? parameters = new();

            foreach (object? parameter in this.parameters)
            {
                switch (parameter)
                {
                    case IParameter keyedParameter:
                        BindingOperations.SetBinding(TargetObject, ParameterProperty, parameter.ToBinding());
                        parameters.Add(new KeyValuePair<string, object>(keyedParameter.Key, (dynamic)TargetObject.GetValue(ParameterProperty)));
                        break;
                    case IEventParameter eventParameter:
                        parameters.AddRange(eventParameter.GetParameters(args));
                        break;
                    default:
                        BindingOperations.SetBinding(TargetObject, ParameterProperty, parameter.ToBinding());
                        parameters.Add((dynamic)TargetObject.GetValue(ParameterProperty));
                        break;
                }
            }

            BindingOperations.SetBinding(TargetObject, RouteProperty, routeBinding);
            if (TargetObject.GetValue(RouteProperty) is { } route)
            {
                BindingOperations.SetBinding(TargetObject, ToProperty, toBinding);
                if (TargetObject.GetValue(ToProperty) is string name)
                {
                    if (toBinding?.StringFormat is { } format)
                    {
                        name = string.Format(format, name);
                    }

                    eventAggregator.Publish(new Navigate(name, parameters.ToArray()) { Route = route });
                }

                if (TargetObject.GetValue(ToProperty) is Type type)
                {
                    eventAggregator.Publish(new Navigate(type, parameters.ToArray()) { Route = route });
                }
            }

            return;
        }

        base.OnInvoked(sender, args);
    }
}