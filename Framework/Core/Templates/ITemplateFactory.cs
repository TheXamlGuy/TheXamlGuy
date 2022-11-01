using System.Diagnostics.CodeAnalysis;

namespace TheXamlGuy.Framework.Core;

public interface ITemplateFactory
{
    object? Create([MaybeNull] object? data);
}