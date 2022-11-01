using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media.Animation;

namespace TheXamlGuy.UI.WPF;

public class VisualStateExtension : VisualState, ISupportInitialize
{
    public static readonly DependencyProperty IsStateTriggersAttachedProperty =
        DependencyProperty.RegisterAttached("IsStateTriggersAttached",
            typeof(bool), typeof(VisualStateExtension),
            new PropertyMetadata(false, EnableStateTriggersChanged));

    private readonly ObservableCollection<StateTriggerBase> stateTriggers = new();

    private Action? afterInit;
    private FrameworkElement? element;

    private bool isInitializing;
    private bool lastState;
    private bool storyboardStateInvalidated;

    public VisualStateExtension()
    {
        Name = Guid.NewGuid().ToString();

        Setters = new SetterBaseCollection();
        Setters.CollectionChanged += OnSettersCollectionChanged;

        stateTriggers.CollectionChanged += OnTriggersCollectionChanged;
    }

    public SetterBaseCollection Setters { get; }
    public IList StateTriggers => stateTriggers;

    internal FrameworkElement? Element
    {
        get => element;
        set
        {
            element = value;

            if (element is not null)
            {
                if (element.IsLoaded)
                {
                    UpdateActiveState();
                }
                else
                {
                    element.Loaded += OnElementLoaded;
                }
            }
              
        }
    }

    public static bool GetIsStateTriggersAttached(FrameworkElement element)
    {
        return (bool)element.GetValue(IsStateTriggersAttachedProperty);
    }

    public static void SetIsStateTriggersAttached(FrameworkElement element, bool value)
    {
        element.SetValue(IsStateTriggersAttachedProperty, value);
    }

    void ISupportInitialize.BeginInit()
    {
        isInitializing = true;
    }

    void ISupportInitialize.EndInit()
    {
        isInitializing = false;
        afterInit?.Invoke();
    }
    internal void SetActive(bool active)
    {
        if (Element is null)
        {
            return;
        }

        if (isInitializing)
        {
            afterInit = () => SetActive(active);
            return;
        }

        if (Storyboard is not null && lastState != active)
        {
            if (active)
            {
                ClockState state = storyboardStateInvalidated ? Storyboard.GetCurrentState(Element) : ClockState.Stopped;

                if (state == ClockState.Stopped)
                {
                    Storyboard.Begin(Element, true);
                }
            }
            else
            {
                if (storyboardStateInvalidated)
                {
                    Storyboard.Stop(Element);
                }
            }
        }

        if (active)
        {
            foreach (Setter setter in Setters.OfType<Setter>())
            {
                DependencyProperty property = setter.Property;
                object value = setter.Value;
                string targetName = setter.TargetName;
                DependencyObject? target = Element.FindName(targetName) as DependencyObject;

                if (setter.Value is BindingBase binding)
                {
                    BindingOperations.SetBinding(target, property, value as BindingBase);
                }
                else
                {
                    target?.SetValue(property, value);
                }
            }
        }

        lastState = active;
    }

    protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs args)
    {
        base.OnPropertyChanged(args);
        if (args.Property.Name == nameof(Storyboard))
        {
            if (args.OldValue is Storyboard oldStoryBoard)
            {
                oldStoryBoard.CurrentStateInvalidated -= OnStoryboardCurrentStateInvalidated;
            }

            if (args.NewValue is Storyboard newStoryBoard)
            {
                newStoryBoard.CurrentStateInvalidated += OnStoryboardCurrentStateInvalidated;
            }

            storyboardStateInvalidated = false;
        }
    }

    private static void EnableStateTriggersChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs args)
    {
        if (dependencyObject is FrameworkElement element)
        {
            if ((bool)args.NewValue)
            {
                VisualStateManagerContext.Set(element);
            }
            else
            {
                VisualStateManagerContext.UnSet(element);
            }
        }
    }

    private void OnElementLoaded(object sender, RoutedEventArgs args)
    {
        ((FrameworkElement)sender).Loaded -= OnElementLoaded;
        UpdateActiveState();
    }

    private void OnSettersCollectionChanged(object? sender, EventArgs args)
    {
        UpdateActiveState();
    }
    private void OnStoryboardCurrentStateInvalidated(object? sender, EventArgs args)
    {
        storyboardStateInvalidated = true;
    }

    private void OnTriggersCollectionChanged(object? sender, NotifyCollectionChangedEventArgs args)
    {
        if (args.NewItems is not null)
        {
            foreach (StateTriggerBase item in args.NewItems.OfType<StateTriggerBase>())
            {
                item.Owner = this;
            }
        }

        if (args.OldItems is not null)
        {
            foreach (StateTriggerBase? item in args.OldItems.OfType<StateTriggerBase>().Where(item => item.Owner == this))
            {
                item.Owner = null;
            }
        }

        UpdateActiveState();
    }

    private void UpdateActiveState()
    {
        SetActive(stateTriggers.Any(t => t.IsTriggerActive));
    }
}