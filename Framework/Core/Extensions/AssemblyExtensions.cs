using System.IO;
using System.Reflection;

namespace TheXamlGuy.Framework.Core
{
    public static class AssemblyExtensions
    {
        public static Stream? ExtractResource(this Assembly assembly, string filename)
        {
            string? resourceName = $"{assembly.GetName()?.Name?.Replace("-", "_")}.{filename}";
            return assembly.GetManifestResourceStream(resourceName);
        }
    }
}