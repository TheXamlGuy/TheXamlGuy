using System;

namespace TheXamlGuy.Framework.Core
{
    public interface IConfigurationWriter<TConfiguration> where TConfiguration : class
    {
        void Write(string section, TConfiguration args);
    }
}