using System;
using System.Windows;
using System.Windows.Media.Animation;

namespace TheXamlGuy.UI.WPF;

public class AnimationHelper
{
    public static ThicknessAnimation CreateAnimation(Thickness thickness = default, double milliseconds = 200)
    {
        return new(thickness, new Duration(TimeSpan.FromMilliseconds(milliseconds)))
        {
            EasingFunction = new PowerEase { EasingMode = EasingMode.EaseInOut }
        };
    }
    public static DoubleAnimation CreateAnimation(double toValue, double milliseconds = 200)
    {
        return new(toValue, new Duration(TimeSpan.FromMilliseconds(milliseconds)))
        {
            EasingFunction = new PowerEase { EasingMode = EasingMode.EaseInOut }
        };
    }

}
