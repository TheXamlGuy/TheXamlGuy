using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace TheXamlGuy.UI.WPF.Controls;

public class Countdown : Control
{
    public static readonly DependencyProperty CountdownIdentifierProperty =
        DependencyProperty.Register(nameof(CountdownIdentifier),
            typeof(CountdownIdentifier), typeof(Countdown));

    private Storyboard? completingTransition;
    private VisualStateGroup? countdownGroup;
    private bool isTransitioning;

    public Countdown()
    {
        DefaultStyleKey = typeof(Countdown);
    }

    public event TypedEventHandler<Countdown, CountdownCompletedEventArgs>? Completed;

    public event TypedEventHandler<Countdown, CountdownStartedEventArgs>? Started;

    public CountdownIdentifier CountdownIdentifier
    {
        get => (CountdownIdentifier)GetValue(CountdownIdentifierProperty);
        set => SetValue(CountdownIdentifierProperty, value);
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
            }
        }
    }

    public override void OnApplyTemplate()
    {
        if (GetTemplateChild("Container") is Grid container)
        {
            countdownGroup = ((IEnumerable<VisualStateGroup>)VisualStateManager.GetVisualStateGroups(container))!.FirstOrDefault(x => x.Name == "CountdownStates");
        }
    }

    public void Start()
    {
        if (isTransitioning)
        {
            return;
        }

        Started?.Invoke(this, new CountdownStartedEventArgs());

        isTransitioning = true;
        CompletingTransition = GetTransitionStoryboardByName($"{CountdownIdentifier}");
        VisualStateManager.GoToState(this, $"{CountdownIdentifier}", true);
    }

    private Storyboard GetTransitionStoryboardByName(string transitionName)
    {
        if (countdownGroup is not null)
        {
            if (((IEnumerable<VisualState>)countdownGroup.States).Where(x => x.Name == transitionName).Select(x => x.Storyboard).FirstOrDefault() is Storyboard transition)
            {
                return transition;
            }
        }

        return new Storyboard();
    }

    private void OnTransitionCompleted(object? sender, EventArgs args)
    {
        CompletingTransition!.Completed -= OnTransitionCompleted;
        VisualStateManager.GoToState(this, "Pending", false);

        isTransitioning = false;

        Completed?.Invoke(this, new CountdownCompletedEventArgs());
    }
}
