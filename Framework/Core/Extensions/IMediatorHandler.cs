namespace TheXamlGuy.Framework.Core
{
    public interface IMediatorHandler<TRequest>
    {
        void Handle(TRequest request);
    }

    public interface IMediatorHandler<TResponse, TRequest>
    {
        TResponse Handle(TRequest request);
    }
}
