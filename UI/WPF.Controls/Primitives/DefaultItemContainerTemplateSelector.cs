using System;
using System.Windows;
using System.Windows.Controls;

namespace TheXamlGuy.UI.WPF.Controls;

public class DefaultItemContainerTemplateSelector : DataTemplateSelector
{
    public override DataTemplate SelectTemplate(object item, DependencyObject container)
    {
        if (container is ItemsControl itemsControl)
        {
            Type itemType = item.GetType();
            DataTemplateKey key = new(itemType);

            if (itemsControl.TryFindResource(key) is DataTemplate template)
            {
                return template;
            }
        }

        return base.SelectTemplate(item, container);
    }
}
