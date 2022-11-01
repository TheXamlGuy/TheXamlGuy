namespace TheXamlGuy.Framework.Core
{
    public record ConfigurationChanged<TConfiguration>(TConfiguration Configuration) where TConfiguration : class;
}