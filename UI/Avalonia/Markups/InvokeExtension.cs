using Avalonia;
using Avalonia.Controls;
using Avalonia.Data;
using Avalonia.Markup.Xaml;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace TheXamlGuy.UI.Avalonia;

[ConstructorArgument(nameof(name))]
public class InvokeExtension : TriggerExtension
{
    private static readonly AvaloniaProperty TargetProperty =
        AvaloniaProperty.RegisterAttached<InvokeExtension, Control, object>("Target");

    private readonly object name;
    private readonly List<object> parameters = new();

    public InvokeExtension(object name)
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

    // KEEP THIS
    //protected override void OnAttached(IServiceProvider serviceProvider)
    //{
    //    if (name.StartsWith("$"))
    //    {
    //        if (serviceProvider is ITypeDescriptorContext typeDescriptorContext)
    //        {
    //            targetBinding = new(name) { DefaultAnchor = new WeakReference(TargetObject) };
    //            if (serviceProvider.GetService(typeof(INameScope)) is INameScope nameScope)
    //            {
    //                targetBinding.NameScope = new WeakReference<INameScope>(nameScope);
    //            }

    //            if (typeDescriptorContext.GetService(typeof(IXamlTypeResolver)) is IXamlTypeResolver xamlTypeResolver)
    //            {
    //                targetBinding.TypeResolver = (prefix, type) =>
    //                {
    //                    string name = string.IsNullOrEmpty(prefix) ? type : $"{prefix}:{type}";
    //                    return xamlTypeResolver.Resolve(name);
    //                };
    //            }
    //        }
    //    }

    //    base.OnAttached(serviceProvider);
    //}

    protected override void OnInvoked(object sender, EventArgs args)
    {
        if (sender is AvaloniaObject avaloniaObject)
        {
            CreaterHandler(avaloniaObject, args);
        }
    }

    private void CreaterHandler(AvaloniaObject sender, EventArgs args)
    {
        if (TryGetInvoke(sender, out (object? Target, MethodInfo? MethodInfo) invoker))
        {
            if (invoker.Target is object target)
            {
                if (invoker.MethodInfo is MethodInfo methodInfo)
                {
                    ParameterInfo[] parameterInfo = methodInfo.GetParameters();
                    List<object> parameters = new();

                    //foreach (object? parameter in this.parameters)
                    //{
                    //    switch (parameter)
                    //    {
                    //        case IParameter keyedParameter:
                    //            BindingOperations.SetBinding(sender, ParameterProperty, parameter.ToBinding());
                    //            parameters.Add(new KeyValuePair<string, object>(keyedParameter.Key, (dynamic)sender.GetValue(ParameterProperty)));
                    //            break;
                    //        case IEventParameter eventParameter:
                    //            parameters.AddRange(eventParameter.GetParameters(args));
                    //            break;
                    //        default:
                    //            BindingOperations.SetBinding(sender, ParameterProperty, parameter.ToBinding());
                    //            parameters.Add((dynamic)sender.GetValue(ParameterProperty));
                    //            break;
                    //    }
                    //}

                    methodInfo.Invoke(target is Action action ? action.Target : target, parameters.Any() ? parameters.ToArray() : parameterInfo.Length > 0 ? new object?[] { null } : Array.Empty<object>());
                }
            }
        }
    }

    private bool TryGetInvoke(AvaloniaObject sender, [AllowNull] out (object?, MethodInfo?) invoker)
    {
        if (name is Binding binding)
        {
            sender.Bind(TargetProperty, binding);
            if (sender.GetValue(TargetProperty) is Action action)
            {
                invoker = new(action.Target, action.Method);
                return true;
            }
        }

        if (name is string)
        {
            if (sender.GetValue(StyledElement.DataContextProperty) is object dataContext)
            {
                if (dataContext.GetType().GetMethod((string)name, BindingFlags.Public | BindingFlags.Instance) is MethodInfo methodInfo)
                {
                    invoker = new(dataContext, methodInfo);
                    return true;
                }
            }
        }

        invoker = default;
        return false;
    }
}
