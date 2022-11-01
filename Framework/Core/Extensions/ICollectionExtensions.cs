using System.Collections.Generic;

namespace TheXamlGuy.Framework.Core
{
    public static class ICollectionExtensions
    {
        public static void AddRange<T>(this ICollection<T> target, IEnumerable<T> source)
        {
            foreach (T item in source)
            {
                target.Add(item);
            }
        }
    }
}
