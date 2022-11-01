using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;

namespace TheXamlGuy.UI.WPF.Controls;

public class TransitioningContentControl : ContentControl
{
    public static readonly DependencyProperty TransitionDirectionProperty =
        DependencyProperty.RegisterAttached(nameof(TransitioningContentControlTransitionDirection),
            typeof(TransitioningContentControlTransitionDirection), typeof(TransitioningContentControl), new PropertyMetadata(TransitioningContentControlTransitionDirection.Left));

    public static readonly DependencyProperty TransitionDurationProperty =
        DependencyProperty.RegisterAttached(nameof(Duration),
            typeof(TimeSpan), typeof(TransitioningContentControl), new PropertyMetadata(TimeSpan.FromSeconds(0.3)));

    public static readonly DependencyProperty TransitionProperty =
        DependencyProperty.RegisterAttached(nameof(Transition),
            typeof(TransitioningContentControlTransitionType), typeof(TransitioningContentControl), new PropertyMetadata(TransitioningContentControlTransitionType.Fade));

    private Storyboard? completingTransition;
    private Grid? container;
    private ContentPresenter? currentContentPresentationSite;
    private bool isTransitioning;
    private VisualStateGroup? presentationGroup;
    private Image? previousImageSite;
    private Storyboard? startingTransition;

    public TransitioningContentControl()
    {
        DefaultStyleKey = typeof(TransitioningContentControl);
    }

    public event RoutedEventHandler? TransitionCompleted;

    public event RoutedEventHandler? TransitionStarted;

    public TransitioningContentControlTransitionDirection Direction
    {
        get => (TransitioningContentControlTransitionDirection)GetValue(TransitionDirectionProperty);
        set => SetValue(TransitionDirectionProperty, value);
    }

    public TimeSpan Duration
    {
        get => (TimeSpan)GetValue(TransitionDurationProperty);
        set => SetValue(TransitionDurationProperty, value);
    }

    public TransitioningContentControlTransitionType Transition
    {
        get => (TransitioningContentControlTransitionType)GetValue(TransitionProperty);
        set => SetValue(TransitionProperty, value);
    }

    private Storyboard? CompletingTransition
    {
        get => completingTransition;
        set
        {
            if (completingTransition is not null)
            {
                CompletingTransition!.Completed -= OnTransitionCompleted;
            }

            completingTransition = value;

            if (completingTransition is not null)
            {
                CompletingTransition!.Completed += OnTransitionCompleted;
                SetTransitionDefaultValues();
            }
        }
    }

    private Storyboard? StartingTransition
    {
        get => startingTransition;
        set
        {
            startingTransition = value;
            if (startingTransition is not null)
            {
                SetTransitionDefaultValues();
            }
        }
    }
    public override void OnApplyTemplate()
    {
        container = GetTemplateChild("Container") as Grid;
        if (container is not null)
        {
            presentationGroup = ((IEnumerable<VisualStateGroup>)VisualStateManager.GetVisualStateGroups(container))!.FirstOrDefault(x => x.Name == "PresentationStates");
        }

        currentContentPresentationSite = GetTemplateChild("CurrentContentPresentationSite") as ContentPresenter;
        previousImageSite = GetTemplateChild("PreviousImageSite") as Image;

        if (currentContentPresentationSite is not null)
        {
            currentContentPresentationSite.Content = Content;
        }

        VisualStateManager.GoToState(this, "Normal", false);
    }

    protected override void OnContentChanged(object oldContent, object newContent)
    {
        QueueTransition(newContent);
        base.OnContentChanged(oldContent, newContent);
    }

    private static RenderTargetBitmap GetRenderTargetBitmapFromUiElement(UIElement uiElement)
    {
        if (uiElement.RenderSize.Height == 0 || uiElement.RenderSize.Width == 0)
        {
            return default!;
        }

        DpiScale dpiScale = VisualTreeHelper.GetDpi(uiElement);

        double pixelWidth = Math.Max(1.0, uiElement.RenderSize.Width * dpiScale.DpiScaleX);
        double pixelHeight = Math.Max(1.0, uiElement.RenderSize.Height * dpiScale.DpiScaleY);

        RenderTargetBitmap renderTargetBitmap = new(Convert.ToInt32(pixelWidth), Convert.ToInt32(pixelHeight), dpiScale.PixelsPerInchX, dpiScale.PixelsPerInchY, PixelFormats.Pbgra32);

        renderTargetBitmap.Render(uiElement);
        renderTargetBitmap.Freeze();

        return renderTargetBitmap;
    }

    private void AbortTransition()
    {
        VisualStateManager.GoToState(this, "Normal", false);
        isTransitioning = false;

        if (previousImageSite is not null)
        {
            if (previousImageSite.Source is RenderTargetBitmap renderTargetBitmap)
            {
                renderTargetBitmap.Clear();
            }

            previousImageSite.Source = null;
            previousImageSite.UpdateLayout();
        }
    }

    private Storyboard GetTransitionStoryboardByName(string transitionName)
    {
        if (presentationGroup is not null)
        {
            if (((IEnumerable<VisualState>)presentationGroup.States).Where(x => x.Name == transitionName).Select(x => x.Storyboard).FirstOrDefault() is Storyboard transition)
            {
                return transition;
            }
        }

        return new Storyboard();
    }

    private void OnTransitionCompleted(object? sender, EventArgs args)
    {
        AbortTransition();
        TransitionCompleted?.Invoke(this, new RoutedEventArgs());
    }

    private void QueueTransition(object newContent)
    {
        if (currentContentPresentationSite is null)
        {
            return;
        }

        if (isTransitioning || previousImageSite is null)
        {
            currentContentPresentationSite.Content = newContent;
            return;
        }

        previousImageSite.Source = GetRenderTargetBitmapFromUiElement(currentContentPresentationSite);
        currentContentPresentationSite.Content = newContent;

        string transitionInName = string.Empty;
        int statesRemaining = 0;
        string startingTransitionName = string.Empty;

        if (Transition == TransitioningContentControlTransitionType.Bounce)
        {
            transitionInName = $"{Transition}{Direction}In";
            CompletingTransition = GetTransitionStoryboardByName(transitionInName);

            startingTransitionName = $"{Transition}{Direction}Out";
            StartingTransition = (Storyboard?)GetTransitionStoryboardByName(startingTransitionName);
            statesRemaining = 2;

            StartingTransition!.Completed += NextState;
        }
        else
        {
            if (StartingTransition is not null)
            {
                StartingTransition!.Completed -= NextState;
            }

            StartingTransition = null;
            startingTransitionName = Transition == TransitioningContentControlTransitionType.Fade ? "Fade" : $"{Transition}{Direction}";

            CompletingTransition = GetTransitionStoryboardByName(startingTransitionName);
            statesRemaining = 1;
        }

        isTransitioning = true;
        RaiseTransitionStarted();

        statesRemaining--;
        VisualStateManager.GoToState(this, startingTransitionName, false);

        void NextState(object? sender, EventArgs args)
        {
            StartingTransition!.Completed -= NextState;
            if (statesRemaining == 1)
            {
                statesRemaining--;
                VisualStateManager.GoToState(this, transitionInName, false);
            }
        }
    }

    private void RaiseTransitionStarted()
    {
        TransitionStarted?.Invoke(this, new RoutedEventArgs());
    }

    private void SetTransitionDefaultValues()
    {
        switch (Transition)
        {
            case TransitioningContentControlTransitionType.Fade:
                {
                    if (CompletingTransition is null)
                    {
                        return;
                    }

                    DoubleAnimation completingDoubleAnimation = (DoubleAnimation)CompletingTransition.Children[0];
                    completingDoubleAnimation.Duration = Duration;

                    DoubleAnimation startingDoubleAnimation = (DoubleAnimation)CompletingTransition.Children[1];
                    startingDoubleAnimation.Duration = Duration;

                    break;
                }

            case TransitioningContentControlTransitionType.Slide:
                {
                    if (CompletingTransition is null)
                    {
                        return;
                    }

                    DoubleAnimation startingDoubleAnimation = (DoubleAnimation)CompletingTransition.Children[0];
                    startingDoubleAnimation.Duration = Duration;

                    startingDoubleAnimation.From = Direction switch
                    {
                        TransitioningContentControlTransitionDirection.Down => -ActualHeight,
                        TransitioningContentControlTransitionDirection.Up => ActualHeight,
                        TransitioningContentControlTransitionDirection.Right => -ActualWidth,
                        TransitioningContentControlTransitionDirection.Left => ActualWidth,
                        _ => throw new ArgumentOutOfRangeException(nameof(TransitioningContentControlTransitionDirection))
                    };

                    break;
                }

            case TransitioningContentControlTransitionType.Move:
                {
                    if (CompletingTransition is null)
                    {
                        return;
                    }

                    DoubleAnimation completingDoubleAnimation = (DoubleAnimation)CompletingTransition.Children[0];
                    DoubleAnimation startingDoubleAnimation = (DoubleAnimation)CompletingTransition.Children[1];

                    startingDoubleAnimation.Duration = Duration;
                    completingDoubleAnimation.Duration = Duration;

                    switch (Direction)
                    {
                        case TransitioningContentControlTransitionDirection.Down:
                            startingDoubleAnimation.To = ActualHeight;
                            completingDoubleAnimation.From = -ActualHeight;

                            break;

                        case TransitioningContentControlTransitionDirection.Up:
                            startingDoubleAnimation.To = -ActualHeight;
                            completingDoubleAnimation.From = ActualHeight;

                            break;

                        case TransitioningContentControlTransitionDirection.Right:
                            startingDoubleAnimation.To = ActualWidth;
                            completingDoubleAnimation.From = -ActualWidth;

                            break;

                        case TransitioningContentControlTransitionDirection.Left:
                            startingDoubleAnimation.To = -ActualWidth;
                            completingDoubleAnimation.From = ActualWidth;

                            break;

                        default: throw new ArgumentOutOfRangeException(nameof(TransitioningContentControlTransitionDirection));
                    }

                    break;
                }

            case TransitioningContentControlTransitionType.Bounce:
                {
                    if (CompletingTransition is not null)
                    {
                        DoubleAnimationUsingKeyFrames completingDoubleAnimation = (DoubleAnimationUsingKeyFrames)CompletingTransition.Children[0];
                        completingDoubleAnimation.KeyFrames[1].Value = ActualHeight;
                    }

                    if (StartingTransition is null)
                    {
                        return;
                    }

                    DoubleAnimation startingDoubleAnimation = (DoubleAnimation)StartingTransition.Children[0];

                    startingDoubleAnimation.To = Direction switch
                    {
                        TransitioningContentControlTransitionDirection.Down => ActualHeight,
                        TransitioningContentControlTransitionDirection.Up => -ActualHeight,
                        TransitioningContentControlTransitionDirection.Right => ActualWidth,
                        TransitioningContentControlTransitionDirection.Left => -ActualWidth,
                        _ => throw new ArgumentOutOfRangeException(nameof(TransitioningContentControlTransitionDirection))
                    };

                    break;
                }

            case TransitioningContentControlTransitionType.Drop: break;
            default: throw new ArgumentOutOfRangeException(nameof(TransitioningContentControlTransitionDirection));
        }
    }
}