using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using TheXamlGuy.Framework.Core;

namespace TheXamlGuy.Framework.WPF
{
    public class Router : IRouterContext
    {
        private readonly IRouteDescriptorCollection descriptors;
        private readonly IDisposer disposer;
        private readonly IEventAggregator eventAggregator;
        private readonly INamedDataTemplateFactory namedDataTemplateFactory;
        private readonly INamedTemplateFactory namedTemplateFactory;
        private readonly ITemplateFactory templateFactory;
        private readonly ITypedDataTemplateFactory typedDataTemplateFactory;

        public Router(ITemplateFactory templateFactory,
            INamedTemplateFactory namedTemplateFactory,
            INamedDataTemplateFactory namedDataTemplateFactory,
            ITypedDataTemplateFactory typedDataTemplateFactory,
            IEventAggregator eventAggregator,
            IDisposer disposer,
            IRouteDescriptorCollection descriptors)
        {
            this.templateFactory = templateFactory;
            this.namedTemplateFactory = namedTemplateFactory;
            this.namedDataTemplateFactory = namedDataTemplateFactory;
            this.typedDataTemplateFactory = typedDataTemplateFactory;
            this.eventAggregator = eventAggregator;
            this.disposer = disposer;
            this.descriptors = descriptors;
        }

        public async Task InitializeAsync()
        {
            disposer.Add(this, eventAggregator.Current.SubscribeUI<Navigate>(OnNavigate));
            disposer.Add(this, eventAggregator.Current.SubscribeUI<NavigateBack>(OnNavigateBack));
            
            await Task.CompletedTask;
        }
 
        private void OnNavigate(Navigate args)
        {
            object? dataTemplate = null;
            object? template = null;

            Dictionary<string, object> keyedParameters = new();
            List<object> parameters = new();

            foreach (object? parameter in args.Parameters)
            {
                if (parameter is KeyValuePair<string, object> keyed)
                {
                    keyedParameters.Add(keyed.Key, keyed.Value);
                }
                else
                {
                    parameters.Add(parameter);
                }
            }

            if (args.Name is { Length: > 0 } name)
            {
                dataTemplate = namedDataTemplateFactory.Create(name, parameters.ToArray());
                template = namedTemplateFactory.Create(name);
            }

            if (args.Type is Type type)
            {
                dataTemplate = typedDataTemplateFactory.Create(type, parameters.ToArray());
                template = templateFactory.Create(dataTemplate);
            }

            if (template is FrameworkElement content)
            {
                content.DataContext = dataTemplate;
                if (descriptors.FirstOrDefault(x => args.Route is string { } name && name == x.Name) is RouteDescriptor descriptor)
                {
                    if (descriptor.Route is Frame frame)
                    {
                        frame.Navigate(content);
                    }

                    if (descriptor.Route is ContentControl contentControl)
                    {
                        contentControl.Content = content;
                    }
                }

                eventAggregator.Publish(Navigated.Create((dynamic?)content, (dynamic?)dataTemplate, keyedParameters));
            }
            else
            {
                if (descriptors.FirstOrDefault(x => args.Route is string { } name && name == x.Name) is { Route: ContentControl contentControl })
                {
                    contentControl.Content = null;
                }
            }
        }

        private void OnNavigateBack(NavigateBack args)
        {
            if (descriptors.FirstOrDefault(x => args.Route is string { } name && name == x.Name) is RouteDescriptor descriptor)
            {
                if (descriptor.Route is ContentControl { Content: FrameworkElement content })
                {
                    if (content.DataContext is IDisposable disposable)
                    {
                        disposable.Dispose();
                    }
                }

                if (descriptor.Route is Frame frame)
                {
                    frame.GoBack();
                }
            }
        }
    }
}