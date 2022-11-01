using System.Collections;
using System.Collections.Specialized;
using System.Windows;
using System.Windows.Markup;
using TheXamlGuy.Framework.Core;
using TheXamlGuy.Framework.WPF.Interactions;

namespace TheXamlGuy.Framework.WPF;

[ContentProperty("Buttons")]
public class InteractiveFrame : DependencyObject
{
    public static DependencyProperty ButtonsProperty =
        DependencyProperty.Register(nameof(Buttons),
            typeof(InteractiveFrameButtonCollection), typeof(InteractiveFrame));

    public static DependencyProperty EventAggregatorProperty =
        DependencyProperty.Register(nameof(EventAggregator),
            typeof(IEventAggregator), typeof(InteractiveFrame), new PropertyMetadata(null, OnEventAggregatorPropertyChanged));

    public InteractiveFrame()
    {
        InteractiveFrameButtonCollection? collection = new();
        collection.CollectionChanged += OnCollectionChanged;

        SetValue(ButtonsProperty, collection);
    }

    public InteractiveFrameButtonCollection Buttons
    {
        get => (InteractiveFrameButtonCollection)GetValue(ButtonsProperty);
        set => SetValue(ButtonsProperty, value);
    }

    public IEventAggregator EventAggregator
    {
        get => (IEventAggregator)GetValue(EventAggregatorProperty);
        set => SetValue(EventAggregatorProperty, value);
    }

    private static void OnEventAggregatorPropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs args)
    {
        (sender as InteractiveFrame)?.OnEventAggregatorPropertyChanged();
    }

    private void OnCollectionChanged(object? sender, NotifyCollectionChangedEventArgs args)
    {
        if (EventAggregator is not null)
        {
            if (args.NewItems is IList newItems)
            {
                foreach (object? item in newItems)
                {
                    if (item is InteractiveFrameButton button)
                    {
                        button.Register(EventAggregator);
                    }
                }
            }
        }
    }
    private void OnEventAggregatorPropertyChanged()
    {
        if (EventAggregator is not null)
        {
            foreach (InteractiveFrameButton button in Buttons)
            {
                button.Register(EventAggregator);
            }
        }
    }
}