using System;

namespace TheXamlGuy.Framework.Core
{
    public record Show(Type ViewModelType, params object[] Parameters);
}