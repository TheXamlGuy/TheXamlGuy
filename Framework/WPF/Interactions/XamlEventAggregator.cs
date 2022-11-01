using System.Collections;
using System.Collections.Specialized;
using System.Windows;
using System.Windows.Markup;
using TheXamlGuy.Framework.Core;

namespace TheXamlGuy.Framework.WPF;

[ContentProperty("Subscribers")]
public class XamlEventAggregator : DependencyObject
{
    public static DependencyProperty EventAggregatorProperty =
        DependencyProperty.Register(nameof(EventAggregator),
            typeof(IEventAggregator), typeof(XamlEventAggregator), new PropertyMetadata(null, OnEventAggregatorPropertyChanged));

    public static DependencyProperty SubscribersProperty =
        DependencyProperty.Register(nameof(Subscribers),
            typeof(EventSubscriberCollection), typeof(XamlEventAggregator));

    public XamlEventAggregator()
    {
        EventSubscriberCollection? collection = new();
        collection.CollectionChanged += OnCollectionChanged;

        SetValue(SubscribersProperty, collection);
    }

    public IEventAggregator EventAggregator
    {
        get => (IEventAggregator)GetValue(EventAggregatorProperty);
        set => SetValue(EventAggregatorProperty, value);
    }

    public EventSubscriberCollection Subscribers
    {
        get => (EventSubscriberCollection)GetValue(SubscribersProperty);
        set => SetValue(SubscribersProperty, value);
    }

    private static void OnEventAggregatorPropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs args)
    {
        (sender as XamlEventAggregator)?.OnEventAggregatorPropertyChanged();
    }

    private void OnCollectionChanged(object? sender, NotifyCollectionChangedEventArgs args)
    {
        if (EventAggregator is not null)
        {
            if (args.NewItems is IList newItems)
            {
                foreach (object? item in newItems)
                {
                    if (item is IEventSubscriber subscriber)
                    {
                        subscriber.Subscribe(EventAggregator);
                    }
                }
            }
        }
    }

    private void OnEventAggregatorPropertyChanged()
    {
        if (EventAggregator is not null)
        {
            foreach (IEventSubscriber subscriber in Subscribers)
            {
                subscriber.Subscribe(EventAggregator);
            }
        }
    }
}