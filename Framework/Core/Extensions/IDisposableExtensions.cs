using System;

namespace TheXamlGuy.Framework.Core
{
    public static class IDisposableExtensions
    {
        public static T Dispose<T>(this T target) where T : IDisposable
        {
            target.Dispose();
            return target;
        }
    }
}
