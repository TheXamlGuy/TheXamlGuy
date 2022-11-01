using Microsoft.Extensions.DependencyInjection;
using TheXamlGuy.Framework.Core;

namespace TheXamlGuy.Framework.WPF
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddRequiredWpf(this IServiceCollection serviceCollection)
        {
            return serviceCollection
                .AddSingleton<IRoute, Route>()
                .AddSingleton<IRouteDescriptorCollection, RouteDescriptorCollection>()
                .AddSingleton<IRouterContext, Router>()
                .AddTransient<IMediatorHandler<Navigate>, NavigateHandler>()
                .RegisterHandlers();
        }
    }
}