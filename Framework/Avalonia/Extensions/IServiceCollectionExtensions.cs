using Microsoft.Extensions.DependencyInjection;
using TheXamlGuy.Framework.Core;

namespace TheXamlGuy.Framework.Avalonia;

public static class IServiceCollectionExtensions
{
    public static IServiceCollection AddRequiredAvalonia(this IServiceCollection serviceCollection)
    {
        return serviceCollection
            .AddSingleton<IRouter, Router>()
            .AddSingleton<IRouteDescriptorCollection, RouteDescriptorCollection>()
            .AddSingleton<IRouterContext, RouterContext>()
            .AddTransient<IMediatorHandler<Navigate>, NavigateHandler>()
            .RegisterHandlers();
    }
}
