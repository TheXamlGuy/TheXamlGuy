using System;
using System.Windows;
using System.Windows.Data;
using TheXamlGuy.Framework.Core;
using TheXamlGuy.UI.WPF;

namespace TheXamlGuy.Framework.WPF
{
    public class NavigateBackExtension : TriggerExtension
    {
        private static readonly DependencyProperty EventAggregatorProperty =
            DependencyProperty.RegisterAttached("EventAggregator",
                typeof(IEventAggregator), typeof(NavigateBackExtension));

        private static readonly DependencyProperty RouteProperty =
            DependencyProperty.RegisterAttached("Route",
                typeof(object), typeof(NavigateBackExtension));

        private readonly Binding eventAggregatorBinding;
        private readonly Binding routeBinding;

        public NavigateBackExtension(object eventAggregator, 
            object? route)
        {
            eventAggregatorBinding = eventAggregator.ToBinding();
            routeBinding = route.ToBinding();
        }

        protected override void OnInvoked(object sender, EventArgs args)
        {
            BindingOperations.SetBinding(TargetObject, EventAggregatorProperty, eventAggregatorBinding);
            if (TargetObject?.GetValue(EventAggregatorProperty) is IEventAggregator eventAggregator)
            {
                BindingOperations.SetBinding(TargetObject, RouteProperty, routeBinding);
                if (TargetObject.GetValue(RouteProperty) is { } toTarget)
                {
                    eventAggregator.Publish(new NavigateBack(toTarget));
                }

                return;
            }

            base.OnInvoked(sender, args);
        }
    }
}