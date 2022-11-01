using System.Collections.Generic;
using System.Reflection;

namespace TheXamlGuy.Framework.WPF
{
    public static class ObjectExtensions
    {
        public static T ToObject<T>(this IDictionary<string, object> source) where T : class, new()
        {
            T? target = new();
            if (target.GetType() is { } targetType)
            {
                foreach (KeyValuePair<string, object> item in source)
                {
                    if (targetType.GetProperty(item.Key) is PropertyInfo propertyInfo)
                    {
                        propertyInfo.SetValue(target, item.Value, null);
                    }
                }
            }
            return target;
        }
    }
}