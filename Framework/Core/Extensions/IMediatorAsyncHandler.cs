namespace TheXamlGuy.Framework.Core
{
    public interface IMediatorAsyncHandler<TRequest>
    {
        Task Handle(TRequest request, CancellationToken cancellationToken = default);
    }

    public interface IMediatorAsyncHandler<TResponse, TRequest>
    {
        Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken = default);
    }
}
