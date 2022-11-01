namespace TheXamlGuy.Framework.Core.Extensions
{
    public static class IDisposerExtensions
    {
        public static void Add(this IDisposer disposer, object subject, IEnumerable<IDisposable> disposers)
        {
            disposer.Add(subject, disposers.ToArray());
        }
    }
}
