namespace TheXamlGuy.Framework.Core;

public interface ITypedDataTemplateFactory
{
    object? Create(Type type, params object[] parameters);
}