using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using TheXamlGuy.Framework.Core;
using TheXamlGuy.UI.WPF;

namespace TheXamlGuy.Framework.WPF
{
    public class TemplateSelector : DataTemplateSelector, ITemplateSelector
    {
        private readonly Dictionary<object, DataTemplate> dataTracking = new();

        private readonly ITemplateFactory templateFactory;
        private readonly IEventAggregator eventAggregator;

        public TemplateSelector(ITemplateFactory templateFactory, 
            IEventAggregator eventAggregator)
        {
            this.templateFactory = templateFactory;
            this.eventAggregator = eventAggregator;
        }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            if (item is not null)
            {
                if (dataTracking.TryGetValue(item, out DataTemplate? cachedDataTemplate))
                {
                    return cachedDataTemplate;
                }

                if (templateFactory.Create(item) is FrameworkElement template)
                {
                    if (TemplateGenerator.CreateDataTemplate(() => template) is DataTemplate dataTemplate)
                    {
                        template.DataContext = item;

                        dataTemplate.Seal();
                        if (template is ICachable cachable)
                        {
                            dataTracking[item!] = dataTemplate;
                        }

                        eventAggregator.Publish(Navigated.Create((dynamic?)template, (dynamic?)item));
                        return dataTemplate;
                    }
                }
            }

            return base.SelectTemplate(item, container);
        }
    }
}