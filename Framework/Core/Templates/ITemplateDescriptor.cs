using Microsoft.Extensions.DependencyInjection;

namespace TheXamlGuy.Framework.Core;

public interface ITemplateDescriptor
{
    Type DataType { get; }

    ServiceLifetime Lifetime { get; }

    string? Name { get; }

    Type TemplateType { get; }
}