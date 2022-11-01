namespace TheXamlGuy.Framework.Core;

public interface INamedDataTemplateFactory
{
    object? Create(string name, params object[] parameters);
}