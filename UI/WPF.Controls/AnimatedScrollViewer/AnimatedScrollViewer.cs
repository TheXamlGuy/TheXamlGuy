using System.Windows.Controls;
using System.Windows.Media.Animation;
using System.Windows.Media;
using System.Windows;

namespace TheXamlGuy.UI.WPF.Controls;

public class AnimatedScrollViewer : ScrollViewer
{
    public static readonly DependencyProperty CanMouseWheelProperty =
        DependencyProperty.Register("CanMouseWheel",
            typeof(bool), typeof(ScrollViewer), new PropertyMetadata(ValueBoxes.TrueBox));

    public static readonly DependencyProperty IsInertiaEnabledProperty =
        DependencyProperty.RegisterAttached("IsInertiaEnabled",
            typeof(bool), typeof(ScrollViewer), new PropertyMetadata(ValueBoxes.FalseBox));


    public static readonly DependencyProperty IsPenetratingProperty =
        DependencyProperty.RegisterAttached("IsPenetrating",
            typeof(bool), typeof(ScrollViewer), new PropertyMetadata(ValueBoxes.FalseBox));


    internal static readonly DependencyProperty CurrentHorizontalOffsetProperty =
        DependencyProperty.Register("CurrentHorizontalOffset",
            typeof(double), typeof(ScrollViewer), new PropertyMetadata(ValueBoxes.Double0Box, OnCurrentHorizontalOffsetChanged));


    internal static readonly DependencyProperty CurrentVerticalOffsetProperty =
        DependencyProperty.Register("CurrentVerticalOffset",
            typeof(double), typeof(ScrollViewer), new PropertyMetadata(ValueBoxes.Double0Box, OnCurrentVerticalOffsetChanged));

    public bool CanMouseWheel
    {
        get => (bool)GetValue(CanMouseWheelProperty);
        set => SetValue(CanMouseWheelProperty, ValueBoxes.BooleanBox(value));
    }

    public bool IsInertiaEnabled
    {
        get => (bool)GetValue(IsInertiaEnabledProperty);
        set => SetValue(IsInertiaEnabledProperty, ValueBoxes.BooleanBox(value));
    }

    public bool IsPenetrating
    {
        get => (bool)GetValue(IsPenetratingProperty);
        set => SetValue(IsPenetratingProperty, ValueBoxes.BooleanBox(value));
    }


    internal double CurrentHorizontalOffset
    {
        get => (double)GetValue(CurrentHorizontalOffsetProperty);
        set => SetValue(CurrentHorizontalOffsetProperty, value);
    }

    internal double CurrentVerticalOffset
    {
        get => (double)GetValue(CurrentVerticalOffsetProperty);
        set => SetValue(CurrentVerticalOffsetProperty, value);
    }

    public static bool GetIsInertiaEnabled(DependencyObject element) => (bool)element.GetValue(IsInertiaEnabledProperty);

    public static bool GetIsPenetrating(DependencyObject element) => (bool)element.GetValue(IsPenetratingProperty);

    public static void SetIsInertiaEnabled(DependencyObject element, bool value) => element.SetValue(IsInertiaEnabledProperty, ValueBoxes.BooleanBox(value));

    public static void SetIsPenetrating(DependencyObject element, bool value) => element.SetValue(IsPenetratingProperty, ValueBoxes.BooleanBox(value));

    public void ScrollToHorizontalOffsetWithAnimation(double offset, double milliseconds = 500)
    {
        var animation = AnimationHelper.CreateAnimation(offset, milliseconds);
        animation.EasingFunction = new CubicEase
        {
            EasingMode = EasingMode.EaseOut
        };

        animation.FillBehavior = FillBehavior.Stop;
        animation.Completed += (s, e1) =>
        {
            CurrentHorizontalOffset = offset;
        };

        BeginAnimation(CurrentHorizontalOffsetProperty, animation, HandoffBehavior.Compose);
    }

    public void ScrollToVerticalOffsetWithAnimation(double offset, double milliseconds = 500)
    {
        DoubleAnimation animation = AnimationHelper.CreateAnimation(offset, milliseconds);
        animation.EasingFunction = new CubicEase
        {
            EasingMode = EasingMode.EaseOut
        };
        animation.FillBehavior = FillBehavior.Stop;
        animation.Completed += (s, e1) =>
        {
            CurrentVerticalOffset = offset;
        };

        BeginAnimation(CurrentVerticalOffsetProperty, animation, HandoffBehavior.Compose);
    }

    protected override HitTestResult? HitTestCore(PointHitTestParameters hitTestParameters) => IsPenetrating ? null : base.HitTestCore(hitTestParameters);

    private static void OnCurrentHorizontalOffsetChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs args)
    {
        (dependencyObject as AnimatedScrollViewer)?.ScrollToHorizontalOffset((double)args.NewValue);
    }

    private static void OnCurrentVerticalOffsetChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs args)
    {
        (dependencyObject as AnimatedScrollViewer)?.ScrollToVerticalOffset((double)args.NewValue);
    }
}