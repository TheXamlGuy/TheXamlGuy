using System;

namespace TheXamlGuy.Framework.Core
{
    public interface IWritableJsonConfigurationDescriptor
    {
        Type ConfigurationType { get; }

        string Key { get; }
    }
}