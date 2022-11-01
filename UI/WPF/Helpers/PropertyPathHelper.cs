using System.Windows;
using System.Windows.Data;

namespace TheXamlGuy.UI.WPF
{
    public static class PropertyPathHelper
    {
        private static readonly Dummy dummy = new();

        public static object GetValue(object args, string path)
        {
            Binding binding = new(path)
            {
                Mode = BindingMode.OneTime,
                Source = args
            };

            BindingOperations.SetBinding(dummy, Dummy.ValueProperty, binding);
            return dummy.GetValue(Dummy.ValueProperty);
        }

        private class Dummy : DependencyObject
        {
            public static readonly DependencyProperty ValueProperty =
                DependencyProperty.Register("Value", typeof(object), typeof(Dummy), 
                    new UIPropertyMetadata(null));
        }
    }
}