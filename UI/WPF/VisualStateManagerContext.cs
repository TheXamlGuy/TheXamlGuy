using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Windows;

namespace TheXamlGuy.UI.WPF;

internal class VisualStateManagerContext : IDisposable
{
    private static readonly DependencyProperty VisualStateManagerContextProperty =
        DependencyProperty.RegisterAttached("VisualStateManagerContext",
            typeof(VisualStateManagerContext), typeof(VisualStateManagerContext));

    private VisualStateManagerContext(FrameworkElement element)
    {
        Element = element;

        VisualStateGroups = (ObservableCollection<VisualStateGroup>) element.GetValue(VisualStateManager.VisualStateGroupsProperty);

        VisualStateGroups.CollectionChanged += CollectionChanged;
        ApplyElement(VisualStateGroups);
    }

    private FrameworkElement? Element { get; set; }

    private ObservableCollection<VisualStateGroup>? VisualStateGroups { get; set; }

    public void Dispose()
    {
        RemoveElement(VisualStateGroups);
        if (VisualStateGroups is not null)
        {
            VisualStateGroups.CollectionChanged -= CollectionChanged;
        }

        Element = null;
        VisualStateGroups = null;
    }

    internal static void Set(FrameworkElement element)
    {
        VisualStateManagerContext hook = new(element);
        element.SetValue(VisualStateManagerContextProperty, hook);
    }

    internal static void UnSet(FrameworkElement element)
    {
        if (element.GetValue(VisualStateManagerContextProperty) is VisualStateManagerContext hook)
        {
            element.SetValue(VisualStateManagerContextProperty, null);
            hook.Dispose();
        }
    }

    private void CollectionChanged(object? sender, NotifyCollectionChangedEventArgs args)
    {
        if (args.NewItems != null)
        {
            ApplyElement(args.NewItems.OfType<VisualStateGroup>());
        }

        if (args.OldItems != null)
        {
            RemoveElement(args.OldItems.OfType<VisualStateGroup>());
        }
    }

    private void ApplyElement(IEnumerable<VisualStateGroup> visualStateGroups)
    {
        foreach (VisualStateExtension? state in visualStateGroups.SelectMany(group => group.States.OfType<VisualStateExtension>()))
        {
            state.Element = Element;
        }
    }

    private void RemoveElement(IEnumerable<VisualStateGroup>? visualStateGroups)
    {
        if (visualStateGroups is not null)
        {
            foreach (VisualStateExtension? state in visualStateGroups.SelectMany(group => group.States.OfType<VisualStateExtension>()))
            {
                state.Element = null;
            }
        }
    }
}