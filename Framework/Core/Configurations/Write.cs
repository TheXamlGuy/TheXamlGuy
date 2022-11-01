using System;

namespace TheXamlGuy.Framework.Core
{
    public record Write<TConfiguration>(string Section, Action<TConfiguration> UpdateDelegate) where TConfiguration : class;
}