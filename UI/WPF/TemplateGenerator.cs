using System;
using System.Windows;

namespace TheXamlGuy.UI.WPF;

public static class TemplateGenerator
{
    public static DataTemplate CreateDataTemplate(Func<object> factory)
    {
        FrameworkElementFactory frameworkElementFactory = new(typeof(TemplateGeneratorControl));
        frameworkElementFactory.SetValue(TemplateGeneratorControl.FactoryProperty, factory);

        DataTemplate dataTemplate = new(typeof(DependencyObject))
        {
            VisualTree = frameworkElementFactory
        };

        return dataTemplate;
    }
}