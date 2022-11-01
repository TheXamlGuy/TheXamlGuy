using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace TheXamlGuy.Framework.Core
{
    public static class IHostBuilderExtensions
    {
        public static IHostBuilder UseContentRoot(this IHostBuilder hostBuilder, string contentRoot, bool createDirectory)
        {
            if (!Directory.Exists(contentRoot) && createDirectory)
            {
                Directory.CreateDirectory(contentRoot);
            }

            return hostBuilder.UseContentRoot(contentRoot);
        }

        public static IHostBuilder ConfigureEvents(this IHostBuilder hostBuilder, Action<IEventBuilder> builderDelegate)
        {
            hostBuilder.ConfigureServices((hostBuilderContext, serviceCollection) =>
            {
                EventBuilder? builder = new();
                builderDelegate?.Invoke(builder);

                foreach (IEventBuilderConfiguration? configuration in builder.Configurations)
                {
                    Type? type = configuration.GetType().GetGenericArguments()[0];

                    serviceCollection.AddSingleton(typeof(IEventBuilderConfiguration<>).MakeGenericType(type), configuration);
                    serviceCollection.AddSingleton(typeof(EventHandler<>).MakeGenericType(type));
                }
            });

            return hostBuilder;
        }
    }
}