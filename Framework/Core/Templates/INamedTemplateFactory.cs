namespace TheXamlGuy.Framework.Core;

public interface INamedTemplateFactory
{
    object? Create(string name);
}