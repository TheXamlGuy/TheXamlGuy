using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System.Reflection;

namespace TheXamlGuy.Framework.Core
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddConfiguration<TConfiguration>(this IServiceCollection serviceCollection, IConfiguration configuration) where TConfiguration : class, new()
        {
            serviceCollection.Configure<TConfiguration>(configuration);
            serviceCollection.AddTransient(provider => provider.GetService<IOptionsMonitor<TConfiguration>>()!.CurrentValue);
            serviceCollection.AddTransient<ConfigurationInitializer<TConfiguration>>();

            return serviceCollection;
        }

        public static IServiceCollection AddCreatable<TService, TImplementation>(this IServiceCollection services)
        {
            return services.AddSingleton(typeof(IServiceCreator<>).MakeGenericType(typeof(TService)), typeof(ServiceCreator<,>).MakeGenericType(typeof(TService), typeof(TImplementation)));
        }

        public static IServiceCollection AddCreatable(this IServiceCollection services, Type serviceType, Type implementationType)
        {
            return services.AddSingleton(typeof(IServiceCreator<>).MakeGenericType(serviceType), typeof(ServiceCreator<,>).MakeGenericType(serviceType, implementationType));
        }

        public static IServiceCollection AddCreatableSingleton(this IServiceCollection services, Type serviceType, Type implementationType)
        {
            services.AddSingleton(serviceType, implementationType);
            services.AddCreatable(serviceType, implementationType);

            return services;
        }

        public static IServiceCollection AddCreatableTransient(this IServiceCollection services, Type serviceType, Type implementationType)
        {
            services.AddTransient(serviceType, implementationType);
            services.AddCreatable(serviceType, implementationType);

            return services;
        }

        public static IServiceCollection AddReqiredCore(this IServiceCollection services)
        {
            return services
                .AddSingleton<IServiceFactory>(provider => new ServiceFactory(provider.GetService, (type, parameters) => ActivatorUtilities.CreateInstance(provider, type, parameters!)))
                .AddSingleton<IDisposer, Disposer>()
                .AddSingleton<IEventAggregator, EventAggregator>()
                .AddSingleton<IEventAggregatorInvoker, EventAggregatorInvoker>()
                .AddSingleton<IMediator, Mediator>()
                .AddSingleton<IScope, Scope>()
                .AddTransient<IPropertyBinderCollection, PropertyBinderCollection>()
                .AddTransient<IPropertyBuilder, PropertyBuilder>()
                .AddSingleton<IInitialization, Initialization>(provider => new Initialization(() =>
                {
                    return services.Where(x => x.ServiceType.GetInterfaces()
                        .Contains(typeof(IInitializer)) || x.ServiceType == typeof(IInitializer))
                        .GroupBy(x => x.ServiceType)
                        .Select(x => x.First())
                        .SelectMany(x => provider.GetServices(x.ServiceType)
                        .Select(x => (IInitializer?)x)).ToList();
                }))
                .RegisterHandlers();
        }

        public static IServiceCollection AddWritableConfiguration<TConfiguration>(this IServiceCollection serviceCollection, IConfiguration configuration) where TConfiguration : class, new()
        {
            serviceCollection.Configure<TConfiguration>(configuration);
            serviceCollection.AddTransient<IConfigurationWriter<TConfiguration>, ConfigurationWriter<TConfiguration>>();
            serviceCollection.AddTransient(provider => provider.GetService<IOptionsMonitor<TConfiguration>>()!.CurrentValue);
            serviceCollection.AddTransient<IMediatorHandler<Write<TConfiguration>>, WriteHandler<TConfiguration>>();
            serviceCollection.AddTransient<ConfigurationInitializer<TConfiguration>>();

            return serviceCollection;
        }

        public static IServiceCollection RegisterHandlers(this IServiceCollection services, params Assembly[] assemblies)
        {
            foreach (Tuple<Type, Type> item in GetImplementations(assemblies.Append(Assembly.GetCallingAssembly()), "Handler", typeof(IMediatorHandler<>), typeof(IMediatorHandler<,>), typeof(IMediatorAsyncHandler<>), typeof(IMediatorAsyncHandler<,>)))
            {
                if (!item.Item2.GetConstructors().Any(x => x.IsPublic))
                {
                    continue;
                }

                services.AddCreatableTransient(item.Item1, item.Item2);
            }

            return services;
        }

        private static IEnumerable<Tuple<Type, Type>> GetImplementations(IEnumerable<Assembly> assemblies, string endsWith, params Type[] interfaces)
        {
            return assemblies.Distinct().SelectMany(a => a.GetTypes())
                .Where(impl => impl.IsClass && impl.IsPublic && !impl.IsAbstract && impl.Name.EndsWith(endsWith))
                .SelectMany(impl => impl.GetInterfaces(), (impl, iface) => new { impl, iface }) 
                .Where(i => i.iface.IsGenericType && interfaces.Contains(i.iface.GetGenericTypeDefinition())) 
                .Select(x => Tuple.Create(x.iface, x.impl));
        }
    }
}