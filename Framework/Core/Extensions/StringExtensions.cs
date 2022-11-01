using System;

namespace TheXamlGuy.Framework.Core
{
    public static class StringExtensions
    {
        public static int CountSubstring(this string text, string textToMatch, StringComparison stringComparison)
        {
            int count = 0;
            int minIndex = text.IndexOf(textToMatch, 0, stringComparison);

            while (minIndex != -1)
            {
                minIndex = text.IndexOf(textToMatch, minIndex + textToMatch.Length, stringComparison);
                count++;
            }

            return count;
        }
    }
}