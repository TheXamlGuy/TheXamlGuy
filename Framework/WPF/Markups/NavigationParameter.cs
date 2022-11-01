using System.Windows;

namespace TheXamlGuy.Framework.WPF
{
    public class NavigationParameter : DependencyObject
    {
        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register(nameof(Value),
                typeof(object), typeof(NavigationParameter));

        public object Value
        {
            get => GetValue(ValueProperty);
            set => SetValue(ValueProperty, value);
        }
    }
}