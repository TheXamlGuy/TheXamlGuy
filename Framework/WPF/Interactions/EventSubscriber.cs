using System;
using System.Reflection;
using System.Windows;
using TheXamlGuy.Framework.Core;
using Windows.Foundation;

namespace TheXamlGuy.Framework.WPF;

public class EventSubscriber : DependencyObject, IEventSubscriber
{
    public static DependencyProperty TypeProperty =
        DependencyProperty.Register(nameof(Type),
            typeof(Type), typeof(EventSubscriber));

    public event TypedEventHandler<EventSubscriber, EventInvokedArgs>? Invoked;

    public Type Type
    {
        get => (Type)GetValue(TypeProperty);
        set => SetValue(TypeProperty, value);
    }

    public void Subscribe(IEventAggregator eventAggregator)
    {
        if (eventAggregator is not null)
        {
            MethodInfo? methodInfo = typeof(EventSubscriber).GetMethod(nameof(EventSubscriber.SubscribeWithType), BindingFlags.NonPublic | BindingFlags.Instance)?.MakeGenericMethod(Type);
            methodInfo?.Invoke(this, new object[] { eventAggregator });
        }
    }

    private void SubscribeWithType<TEvent>(IEventAggregator eventAggregator)
    {
        if (eventAggregator is not null)
        {
            eventAggregator.SubscribeUI<TEvent>(args =>
            {
                if (CanInvoke(args))
                {
                    Invoked?.Invoke(this, new EventInvokedArgs(args!));
                }
            });
        }
    }

    protected virtual bool CanInvoke(object? args)
    {
        return true;
    }
}