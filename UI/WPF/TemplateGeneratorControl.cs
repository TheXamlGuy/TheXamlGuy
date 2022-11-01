using System;
using System.Windows;
using System.Windows.Controls;

namespace TheXamlGuy.UI.WPF;

public class TemplateGeneratorControl : ContentControl
{
    internal static readonly DependencyProperty FactoryProperty =
        DependencyProperty.Register("Factory", typeof(Func<object>),
            typeof(TemplateGeneratorControl), new PropertyMetadata(null, OnFactoryPropertyChanged));

    private static void OnFactoryPropertyChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs args)
    {
        if (dependencyObject is TemplateGeneratorControl sender && args.NewValue is not null)
        {
            Func<object> factory = (Func<object>)args.NewValue;
            sender.Content = factory();
        }
    }
}