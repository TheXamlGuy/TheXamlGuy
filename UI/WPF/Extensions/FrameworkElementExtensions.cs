using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace TheXamlGuy.UI.WPF;

public static class FrameworkElementExtensions
{

    public static Rect? BoundsRelativeTo(this FrameworkElement source, Visual parent)
    {
        try
        {
            GeneralTransform generalTransform = source.TransformToAncestor(parent);
            return generalTransform.TransformBounds(new Rect(0, 0, source.ActualWidth, source.ActualHeight));
        }
        catch
        {

        }

        return Rect.Empty;
    }

    public static bool IsTemplateParent(this FrameworkElement source, FrameworkElement target)
    {
        if (source is null)
        {
            return false;
        }

        if (ReferenceEquals(source, target))
        {
            return true;
        }

        if (ReferenceEquals(source?.Parent, target))
        {
            return true;
        }

        FrameworkElement? parent = source?.Parent as FrameworkElement;
        while (true)
        {
            if (parent is null)
            {
                break;
            }

            if (ReferenceEquals(parent, target))
            {
                return true;
            }

            parent = parent.Parent as FrameworkElement;
        }

        FrameworkElement? templateParent = source?.TemplatedParent as FrameworkElement;
        while (true)
        {
            if (templateParent is null)
            {
                break;
            }

            if (ReferenceEquals(templateParent, target))
            {
                return true;
            }

            templateParent = templateParent.TemplatedParent as FrameworkElement;
        }

        return false;
    }

    public static bool IsPointerWithin(this FrameworkElement frameworkElement)
    {
        Point position = Mouse.PrimaryDevice.GetPosition(frameworkElement);
        return position.X >= 0 && position.X <= frameworkElement.ActualWidth && position.Y >= 0 &&
               position.Y <= frameworkElement.ActualHeight;
    }

    public static IEnumerable<VisualStateGroup> FindVisualStateGroups(this FrameworkElement frameworkElement)
    {
        IEnumerable<VisualStateGroup> visualStateGroups = (IEnumerable<VisualStateGroup>)VisualStateManager.GetVisualStateGroups(frameworkElement);
        return visualStateGroups ?? Enumerable.Empty<VisualStateGroup>();
    }

    public static VisualStateGroup? FindVisualGroup(this FrameworkElement frameworkElement, string name)
    {
        IEnumerable<VisualStateGroup> visualStateGroups = (IEnumerable<VisualStateGroup>)VisualStateManager.GetVisualStateGroups(frameworkElement);
        return visualStateGroups?.FirstOrDefault(x => x.Name == name);
    }

    public static VisualState? FindVisualState(this FrameworkElement frameworkElement, string name)
    {
        Collection<VisualStateGroup> visualStateGroups = (Collection<VisualStateGroup>)VisualStateManager.GetVisualStateGroups(frameworkElement);
        return visualStateGroups
            ?.SelectMany(visualStateGroup => visualStateGroup.States.Cast<VisualState>())
            .FirstOrDefault(visualState => visualState.Name == name);
    }

    public static VisualTransition? FindVisualTransition(this FrameworkElement frameworkElement, string name)
    {
        Collection<VisualStateGroup> visualStateGroups = (Collection<VisualStateGroup>)VisualStateManager.GetVisualStateGroups(frameworkElement);
        return visualStateGroups
            ?.SelectMany(visualStateGroup => visualStateGroup.Transitions.Cast<VisualTransition>())
            .FirstOrDefault(visualTransition => visualTransition.To == name);
    }

    public static bool IsElementFullyVisibleInContainer(this FrameworkElement frameworkElement, UIElement element)
    {
        Rect panelRect = element.TransformToAncestor(frameworkElement)
            .TransformBounds(new Rect(0.0, 0.0, element.DesiredSize.Width, element.DesiredSize.Height));

        double roundedActualHeight = Math.Round(frameworkElement.ActualHeight, 2);
        double roundedActualWidth = Math.Round(frameworkElement.ActualWidth, 2);

        Rect containerRect = new(0.0, 0.0, roundedActualWidth, roundedActualHeight);

        Point topLeftPointRounded = new(Math.Round(panelRect.TopLeft.X, 2), Math.Round(panelRect.TopLeft.Y, 2));
        Point topRightPointRounded = new(Math.Round(panelRect.TopRight.X, 2), Math.Round(panelRect.TopRight.Y, 2));
        Point bottomLeftPointRounded = new(Math.Round(panelRect.BottomLeft.X, 2), Math.Round(panelRect.BottomLeft.Y, 2));
        Point bottomRightPointRounded = new(Math.Round(panelRect.BottomRight.X, 2), Math.Round(panelRect.BottomRight.Y, 2));

        return containerRect.Contains(topLeftPointRounded) && containerRect.Contains(topRightPointRounded) &&
               containerRect.Contains(bottomLeftPointRounded) && containerRect.Contains(bottomRightPointRounded);
    }
}