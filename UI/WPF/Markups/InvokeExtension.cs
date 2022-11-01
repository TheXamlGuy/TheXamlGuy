using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;

namespace TheXamlGuy.UI.WPF;

public partial class InvokeExtension : TriggerExtension
{
    private static readonly DependencyProperty ParameterProperty =
        DependencyProperty.RegisterAttached("Parameter",
            typeof(object), typeof(InvokeExtension));

    private static readonly DependencyProperty TargetProperty =
       DependencyProperty.RegisterAttached("Target",
           typeof(object), typeof(InvokeExtension));

    private readonly Dictionary<Type, Delegate?> dummyHandlers = new();
    private readonly string name;
    private readonly List<object> parameters = new();
    private PropertyChangedRevoker? dataContextPropertyChangedRevoker;

    public InvokeExtension(string name)
    {
        this.name = name;
    }

    public InvokeExtension(string name,
        object args1)
    {
        this.name = name;

        parameters.Add(args1 is MarkupExtension ? args1 : args1.ToBinding());
    }

    public InvokeExtension(string name,
        object args1,
        object args2)
    {
        this.name = name;

        parameters.Add(args1 is MarkupExtension ? args1 : args1.ToBinding());
        parameters.Add(args2 is MarkupExtension ? args2 : args2.ToBinding());
    }

    public InvokeExtension(string name,
        object args1,
        object args2,
        object args3)
    {
        this.name = name;

        parameters.Add(args1 is MarkupExtension ? args1 : args1.ToBinding());
        parameters.Add(args2 is MarkupExtension ? args2 : args2.ToBinding());
        parameters.Add(args3 is MarkupExtension ? args3 : args3.ToBinding());
    }

    public InvokeExtension(string name,
        object args1,
        object args2,
        object args3,
        object args4)
    {
        this.name = name;

        parameters.Add(args1 is MarkupExtension ? args1 : args1.ToBinding());
        parameters.Add(args2 is MarkupExtension ? args2 : args2.ToBinding());
        parameters.Add(args3 is MarkupExtension ? args3 : args3.ToBinding());
        parameters.Add(args4 is MarkupExtension ? args4 : args4.ToBinding());
    }

    public InvokeExtension(string name,
        object args1,
        object args2,
        object args3,
        object args4,
        object args5)
    {
        this.name = name;

        parameters.Add(args1 is MarkupExtension ? args1 : args1.ToBinding());
        parameters.Add(args2 is MarkupExtension ? args2 : args2.ToBinding());
        parameters.Add(args3 is MarkupExtension ? args3 : args3.ToBinding());
        parameters.Add(args4 is MarkupExtension ? args4 : args4.ToBinding());
        parameters.Add(args5 is MarkupExtension ? args5 : args5.ToBinding());
    }

    public InvokeExtension(string name,
        object args1,
        object args2,
        object args3,
        object args4,
        object args5,
        object args6)
    {
        this.name = name;

        parameters.Add(args1 is MarkupExtension ? args1 : args1.ToBinding());
        parameters.Add(args2 is MarkupExtension ? args2 : args2.ToBinding());
        parameters.Add(args3 is MarkupExtension ? args3 : args3.ToBinding());
        parameters.Add(args4 is MarkupExtension ? args4 : args4.ToBinding());
        parameters.Add(args5 is MarkupExtension ? args5 : args5.ToBinding());
        parameters.Add(args6 is MarkupExtension ? args6 : args6.ToBinding());
    }

    public InvokeExtension(string name,
        object args1,
        object args2,
        object args3,
        object args4,
        object args5,
        object args6,
        object args7)
    {
        this.name = name;

        parameters.Add(args1 is MarkupExtension ? args1 : args1.ToBinding());
        parameters.Add(args2 is MarkupExtension ? args2 : args2.ToBinding());
        parameters.Add(args3 is MarkupExtension ? args3 : args3.ToBinding());
        parameters.Add(args4 is MarkupExtension ? args4 : args4.ToBinding());
        parameters.Add(args5 is MarkupExtension ? args5 : args5.ToBinding());
        parameters.Add(args6 is MarkupExtension ? args6 : args6.ToBinding());
        parameters.Add(args7 is MarkupExtension ? args7 : args7.ToBinding());
    }

    public InvokeExtension(string name,
        object args1,
        object args2,
        object args3,
        object args4,
        object args5,
        object args6,
        object args7,
        object args8)
    {
        this.name = name;

        parameters.Add(args1 is MarkupExtension ? args1 : args1.ToBinding());
        parameters.Add(args2 is MarkupExtension ? args2 : args2.ToBinding());
        parameters.Add(args3 is MarkupExtension ? args3 : args3.ToBinding());
        parameters.Add(args4 is MarkupExtension ? args4 : args4.ToBinding());
        parameters.Add(args5 is MarkupExtension ? args5 : args5.ToBinding());
        parameters.Add(args6 is MarkupExtension ? args6 : args6.ToBinding());
        parameters.Add(args7 is MarkupExtension ? args7 : args7.ToBinding());
        parameters.Add(args8 is MarkupExtension ? args8 : args8.ToBinding());
    }

    public InvokeExtension(string name,
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
        this.name = name;

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

    public InvokeExtension(string name,
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
        this.name = name;

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

    public InvokeExtension(string name,
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
        this.name = name;

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

    public InvokeExtension(string name,
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
        this.name = name;

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

    public InvokeExtension(string name,
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
        this.name = name;

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

    public InvokeExtension(string name,
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
        this.name = name;

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

    public InvokeExtension(string name,
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
        this.name = name;

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

    public Binding? BindingTarget { get; set; }

    protected override void OnInvoked(object sender, EventArgs args)
    {
        if (sender is DependencyObject dependencyObject)
        {
            if (!TryGetDataContext(dependencyObject, out object dataContext))
            {
                dataContextPropertyChangedRevoker = new PropertyChangedRevoker(dependencyObject, FrameworkElement.DataContextProperty, OnDataContextPropertyChangedChanged);
                void OnDataContextPropertyChangedChanged(object sender, DependencyPropertyChangedEventArgs _)
                {
                    if (TryGetDataContext(dependencyObject, out dataContext))
                    {
                        dataContextPropertyChangedRevoker?.Dispose();
                        CreaterHandler(dependencyObject, args, dataContext);
                    }
                }
            }
            else
            {
                CreaterHandler(dependencyObject, args, dataContext);
            }
        }
    }

    private bool TryGetInvoke(DependencyObject sender, object dataContext, [AllowNull]out MethodInfo? methodInfo)
    {
        methodInfo = null;
        if (dataContext.GetType().GetMethod(name, BindingFlags.Public | BindingFlags.Instance) is MethodInfo dataContextMethodInfo)
        {
            methodInfo = dataContextMethodInfo;
            return true;
        }

        if (sender.GetType().GetMethod(name, BindingFlags.Public | BindingFlags.Instance) is MethodInfo senderMethodInfo)
        {
            methodInfo = senderMethodInfo;
            return true;
        }

        return false;
    }

    private void CreaterHandler(DependencyObject sender, EventArgs args, object dataContext)
    {
        if (TryGetInvoke(sender, dataContext, out MethodInfo? methodInfo))
        {
            ParameterInfo[] parameterInfo = methodInfo!.GetParameters();

            List<object> parameters = new();
            foreach (object? parameter in this.parameters)
            {
                switch (parameter)
                {
                    case IParameter keyedParameter:
                        BindingOperations.SetBinding(sender, ParameterProperty, parameter.ToBinding());
                        parameters.Add(new KeyValuePair<string, object>(keyedParameter.Key, (dynamic)sender.GetValue(ParameterProperty)));
                        break;
                    case IEventParameter eventParameter:
                        parameters.AddRange(eventParameter.GetParameters(args));
                        break;
                    default:
                        BindingOperations.SetBinding(sender, ParameterProperty, parameter.ToBinding());
                        parameters.Add((dynamic)sender.GetValue(ParameterProperty));
                        break;
                }
            }

            if (methodInfo is { })
            {
                methodInfo.Invoke(dataContext, parameters.Any() ? parameters.ToArray() : parameterInfo.Length > 0 ? new object?[] { null } : Array.Empty<object>());
            }
        }
    }

    private bool TryGetDataContext(DependencyObject sender, out object dataContext)
    {
        if (BindingTarget is not null)
        {
            BindingOperations.SetBinding(sender, TargetProperty, BindingTarget);
            dataContext = sender.GetValue(TargetProperty);
        }
        else
        {
            dataContext = sender.GetValue(FrameworkElement.DataContextProperty) ?? sender.GetValue(FrameworkContentElement.DataContextProperty);
        }

        return dataContext is not null;
    }
}