namespace TheXamlGuy.Framework.Core
{
    public interface IEventHandler<TTEvent> : IInitializer, IDisposable where TTEvent : class
    {

    }
}
