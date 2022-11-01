using System;
using System.Windows;
using System.Windows.Controls;

namespace TheXamlGuy.UI.WPF.Controls;

public class FlipView : ListBox
{
    public static readonly DependencyProperty ItemContainerTemplateSelectorProperty =
        DependencyProperty.Register(nameof(ItemContainerTemplateSelector),
            typeof(DataTemplateSelector), typeof(FlipView), new PropertyMetadata(new DefaultItemContainerTemplateSelector()));

    public static readonly DependencyProperty UsesItemContainerTemplateSelectorProperty =
        DependencyProperty.Register(nameof(UsesItemContainerTemplateSelector),
            typeof(bool), typeof(FlipView));

    private object? current;

    private AnimatedScrollViewer? scrollViewer;

    public FlipView()
    {
        DefaultStyleKey = typeof(FlipView);
    }

    public DataTemplateSelector ItemContainerTemplateSelector
    {
        get => (DataTemplateSelector)GetValue(ItemContainerTemplateSelectorProperty);
        set => SetValue(ItemContainerTemplateSelectorProperty, value);
    }

    public bool UsesItemContainerTemplateSelector
    {
        get => (bool)GetValue(UsesItemContainerTemplateSelectorProperty);
        set => SetValue(UsesItemContainerTemplateSelectorProperty, value);
    }

    public void Next()
    {
        if (SelectedIndex < Items.Count - 1)
        {
            SelectedIndex++;
        }
        else
        {
            SelectedIndex = 0;
        }

        if (scrollViewer is not null)
        {
            double scrollOffset = Math.Min(scrollViewer.CurrentHorizontalOffset + GetDesiredItemWidth(), scrollViewer.ScrollableWidth);
            scrollViewer.ScrollToHorizontalOffsetWithAnimation(scrollOffset);
        }
    }

    public override void OnApplyTemplate()
    {
        scrollViewer = GetTemplateChild("ScrollingHost") as AnimatedScrollViewer;
        if (scrollViewer is not null)
        {
            scrollViewer.SizeChanged += OnScrollViewerSizeChanged;
        }

        base.OnApplyTemplate();
    }

    public void Previous()
    {
        if (SelectedIndex > 0)
        {
            SelectedIndex--;
        }
        else
        {
            SelectedIndex = Items.Count - 1;
        }

        if (scrollViewer is not null)
        {
            double scrollOffset = Math.Min(scrollViewer.CurrentHorizontalOffset - GetDesiredItemWidth(), scrollViewer.ScrollableWidth);
            scrollViewer.ScrollToHorizontalOffsetWithAnimation(scrollOffset);
        }
    }

    protected override DependencyObject GetContainerForItemOverride()
    {
        object? current = this.current;
        this.current = null;

        if (current is not null)
        {
            FlipViewItem? item = null;

            if (UsesItemContainerTemplateSelector)
            {
                if (ItemContainerTemplateSelector.SelectTemplate(current, this) is DataTemplate dataTemplate)
                {
                    DependencyObject container = dataTemplate.LoadContent();

                    switch (container)
                    {
                        case FlipViewItem:
                            item = container as FlipViewItem;
                            break;
                        case TemplateGeneratorControl template:
                            item = template.Content is FlipViewItem ? template.Content as FlipViewItem : new FlipViewItem { Content = template.Content };
                            break;
                    }
                }

                item ??= new FlipViewItem();
                return item;
            }
        }

        return new FlipViewItem();
    }

    protected override bool IsItemItsOwnContainerOverride(object item)
    {
        if (item is not FlipViewItem)
        {
            current = item;
            return false;
        }

        return true;
    }

    protected override void PrepareContainerForItemOverride(DependencyObject element, object item)
    {
        base.PrepareContainerForItemOverride(element, item);

        if (element is FlipViewItem flipViewItem)
        {
            Thickness flipViewItemMargin = flipViewItem.Margin;

            double value = GetDesiredItemWidth();

            value -= (flipViewItemMargin.Left + flipViewItemMargin.Right);
            flipViewItem.Width = value;

            value = GetDesiredItemHeight();

            value -= (flipViewItemMargin.Top + flipViewItemMargin.Bottom);
            flipViewItem.Height = value;
        }
    }

    private double GetDesiredItemHeight()
    {
        double height = scrollViewer is not null ? scrollViewer.ActualHeight : ActualHeight;

        if (height <= 0)
        {
            height = Height;
        }

        return height;
    }

    private double GetDesiredItemWidth()
    {
        double width = scrollViewer is not null ? scrollViewer.ActualWidth : ActualWidth;

        if (width <= 0)
        {
            width = Width;
        }

        return width;
    }

    private void OnScrollViewerSizeChanged(object sender, SizeChangedEventArgs args)
    {
        double width = GetDesiredItemWidth();
        double height = GetDesiredItemHeight();
        for (int i = 0; i < Items.Count; i++)
        {
            if (ItemContainerGenerator.ContainerFromIndex(i) is FlipViewItem container)
            {
                Thickness margin  = container.Margin;

                double value = width - (margin.Left + margin.Right);
                container.Width = value;

                value = height - (margin.Top + margin.Bottom);
                container.Height = value;

                ScrollIntoView(SelectedItem);
            }
        }
    }
}
