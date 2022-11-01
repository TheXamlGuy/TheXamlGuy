using Microsoft.Extensions.DependencyInjection;

namespace TheXamlGuy.Framework.Core;

public interface ITemplateBuilder
{
    IReadOnlyCollection<ITemplateDescriptor> Descriptors { get; }

    ITemplateBuilder Add<TViewModel, TView>(string name, ServiceLifetime lifetime = ServiceLifetime.Transient);

    ITemplateBuilder Add<TViewModel, TView>(ServiceLifetime lifetime = ServiceLifetime.Transient);
}