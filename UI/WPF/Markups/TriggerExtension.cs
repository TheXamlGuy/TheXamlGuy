using System.Reflection;
using System.Windows.Markup;
using System.Windows;
using System.Xaml;

namespace TheXamlGuy.UI.WPF;

[MarkupExtensionReturnType(typeof(Delegate))]
public class TriggerExtension : MarkupExtension
{
    public DependencyObject? TargetObject { get; protected set; }

    protected object? TargetInvoke { get; private set; }

    public void Invoke(object sender, EventArgs args)
    {
        OnInvoked(sender, args);
    }

    public override object ProvideValue(IServiceProvider serviceProvider)
    {
        if (serviceProvider.GetService(typeof(IProvideValueTarget)) is IProvideValueTarget target)
        {
            if (TargetObject is null)
            {
                if (target.TargetObject is DependencyObject dependencyObject)
                {
                    TargetObject = dependencyObject;
                }
                else if (serviceProvider.GetService(typeof(IRootObjectProvider)) is IRootObjectProvider root)
                {
                    TargetObject = (DependencyObject)root.RootObject;
                }
            }

            TargetInvoke = target.TargetProperty;

            MethodInfo invokeMethod = GetType().GetMethod("Invoke", BindingFlags.Instance | BindingFlags.Public)!;
            if (invokeMethod is null)
            {
                return this;
            }

            switch (TargetInvoke)
            {
                case EventInfo info:
                    return Delegate.CreateDelegate(info.EventHandlerType!, this, invokeMethod);
                case MethodInfo methodInfo:
                    {
                        if (methodInfo.GetParameters() is ParameterInfo[] methodParameters && methodParameters is { Length: 2 })
                        {
                            return Delegate.CreateDelegate(methodParameters[1].ParameterType, this, invokeMethod);
                        }

                        break;
                    }

                default:
                    break;
            }

        }

        return this;
    }

    protected virtual void OnInvoked(object sender, EventArgs args)
    {

    }
}
