namespace TheXamlGuy.Framework.Core
{
    public interface IWritableConfigurationProvider
    {
        void Write<TValue>(string section, TValue value) where TValue : class, new();
    }
}