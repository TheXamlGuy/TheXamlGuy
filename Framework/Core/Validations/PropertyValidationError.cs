using System.Diagnostics.CodeAnalysis;

namespace TheXamlGuy.Framework.Core
{
    public class PropertyValidationError<TKey, TValue> where TKey : class where TValue : class
    {
        private readonly Dictionary<TKey, TValue?> dictionary = new();

        public int Count => dictionary.Count;

        public TValue? this[[MaybeNull]TKey key]
        {
            get => dictionary.ContainsKey(key) ? dictionary[key] : null;
            set
            {
                if (Contains(key))
                {
                    dictionary.Remove(key);
                }

                dictionary.Add(key, value);
            }
        }

        public bool Contains(TKey key)
        {
            return dictionary.ContainsKey(key);
        }

        public void Remove(TKey key)
        {
            if (Contains(key))
            {
                dictionary.Remove(key);
            }
        }
    }
}