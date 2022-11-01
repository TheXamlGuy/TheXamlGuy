using System;

namespace TheXamlGuy.Framework.Core
{
    public record WritableJsonConfigurationDescriptor(Type ConfigurationType, string Key) : IWritableJsonConfigurationDescriptor;
}