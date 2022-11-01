using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Markup;
using TheXamlGuy.UI.WPF;

namespace TheXamlGuy.Framework.WPF
{
    [MarkupExtensionReturnType(typeof(Control))]
    public class RouteExtension : MarkupExtension
    {
        private static readonly DependencyProperty RouteProperty =
            DependencyProperty.RegisterAttached("Route",
                typeof(IRoute), typeof(RouteExtension));

        private readonly string name;
        private readonly Binding routeBinding;
        private PropertyChangedRevoker? dataContextPropertyChangedRevoker;

        public RouteExtension(object route, string name)
        {
            routeBinding = route.ToBinding();
            this.name = name;
        }

        private bool TryGetDataContext(DependencyObject target, out object dataContext)
        {
            dataContext = target.GetValue(FrameworkElement.DataContextProperty) ?? target.GetValue(FrameworkContentElement.DataContextProperty);
            return dataContext is not null;
        }

        public override object? ProvideValue(IServiceProvider serviceProvider)
        {
            if (dataContextPropertyChangedRevoker is not null)
            {
                dataContextPropertyChangedRevoker.Dispose();
                dataContextPropertyChangedRevoker = null;
            }

            if (serviceProvider.GetService(typeof(IProvideValueTarget)) is IProvideValueTarget target)
            {
                if (target.TargetObject is FrameworkElement frameworkElement)
                {
                    if (!TryGetDataContext(frameworkElement, out object dataContext))
                    {
                        dataContextPropertyChangedRevoker = new PropertyChangedRevoker(frameworkElement, FrameworkElement.DataContextProperty, OnDataContextPropertyChangedChanged);
                        void OnDataContextPropertyChangedChanged(object sender, DependencyPropertyChangedEventArgs _)
                        {
                            if (TryGetDataContext(frameworkElement, out dataContext))
                            {
                                frameworkElement.Loaded -= HandleLoaded;

                                BindingOperations.SetBinding(frameworkElement, RouteProperty, routeBinding);
                                if (frameworkElement?.GetValue(RouteProperty) is IRoute route)
                                {
                                    route.AddRoute(name, frameworkElement);
                                    BindingOperations.ClearBinding(frameworkElement, RouteProperty);
                                }
                            }
                        }

                        void HandleLoaded(object sender, RoutedEventArgs args)
                        {
                            frameworkElement.Loaded -= HandleLoaded;
                            if (TryGetDataContext(frameworkElement, out dataContext))
                            {
                                BindingOperations.SetBinding(frameworkElement, RouteProperty, routeBinding);
                                if (frameworkElement?.GetValue(RouteProperty) is IRoute route)
                                {
                                    route.AddRoute(name, frameworkElement);
                                    BindingOperations.ClearBinding(frameworkElement, RouteProperty);
                                }
                            }
                        }

                        frameworkElement.Loaded += HandleLoaded;
                    }
                    else
                    {
                        BindingOperations.SetBinding(frameworkElement, RouteProperty, routeBinding);
                        if (frameworkElement?.GetValue(RouteProperty) is IRoute route)
                        {
                            route.AddRoute(name, frameworkElement);
                            BindingOperations.ClearBinding(frameworkElement, RouteProperty);
                        }
                    }
                }
            }

            return null;
        }
    }
}