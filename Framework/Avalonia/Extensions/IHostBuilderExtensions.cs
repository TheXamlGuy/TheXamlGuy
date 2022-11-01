using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TheXamlGuy.Framework.Core;

namespace TheXamlGuy.Framework.Avalonia;

public static class IHostBuilderExtensions
{
    public static IHostBuilder ConfigureTemplates(this IHostBuilder hostBuilder, Action<ITemplateBuilder> builderDelegate)
    {
        hostBuilder.ConfigureServices((hostBuilderContext, serviceCollection) =>
        {
            TemplateBuilder? builder = new();
            builderDelegate?.Invoke(builder);

            serviceCollection
                .AddSingleton(builder.Descriptors)
                .AddSingleton<ITemplateDescriptorProvider, TemplateDescriptorProvider>()
                .AddSingleton<ITemplateFactory, TemplateFactory>()
                .AddSingleton<INamedTemplateFactory, NamedTemplateFactory>()
                .AddSingleton<ITypedDataTemplateFactory, TypedDataTemplateFactory>()
                .AddSingleton<INamedDataTemplateFactory, NamedDataTemplateFactory>()
                .AddSingleton<ITemplateSelector, TemplateSelector>();

            foreach (ITemplateDescriptor? descriptor in builder.Descriptors)
            {
                serviceCollection.Add(new ServiceDescriptor(descriptor.TemplateType, descriptor.TemplateType, descriptor.Lifetime));
                serviceCollection.Add(new ServiceDescriptor(descriptor.DataType, descriptor.DataType, descriptor.Lifetime));
            }
        });

        return hostBuilder;
    }
}