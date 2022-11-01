using System;
using System.Windows;
using TheXamlGuy.UI;
using TheXamlGuy.Framework.Core;
using TheXamlGuy.Framework.Microcontroller;

namespace TheXamlGuy.Framework.WPF;

public class InteractiveFrameButton : DependencyObject
{
    public static DependencyProperty PlacementProperty =
        DependencyProperty.Register(nameof(Type),
            typeof(InteractiveFrameButtonPlacement), typeof(InteractiveFrameButton));

    public event TypedEventHandler<InteractiveFrameButton, InteractiveFrameButtonInvokedArgs>? Invoked;

    public InteractiveFrameButtonPlacement Placement
    {
        get => (InteractiveFrameButtonPlacement)GetValue(PlacementProperty);
        set => SetValue(PlacementProperty, value);
    }

    public void Register(IEventAggregator eventAggregator)
    {
        eventAggregator.SubscribeUI<CapactiveSensor>(args =>
        {
            if ((int)args.Placement == (int)Placement)
            {
                if (args.State == SensorState.On)
                {
                    Invoked?.Invoke(this, new InteractiveFrameButtonInvokedArgs());
                }
            }
        });
    }
}
