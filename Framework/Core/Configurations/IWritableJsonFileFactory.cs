using System.IO;

namespace TheXamlGuy.Framework.Core
{
    public interface IWritableJsonConfigurationBuilder
    {
        Stream? DefaultFileStream { get; }

        IWritableJsonConfigurationBuilder AddDefaultConfiguration<TConfiguration>(string Key) where TConfiguration : class;

        IWritableJsonConfigurationBuilder AddDefaultFileStream(Stream stream);

        void Build(string path);
    }
}