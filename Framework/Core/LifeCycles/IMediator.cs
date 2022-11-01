namespace TheXamlGuy.Framework.Core
{
    public interface IMediator
    {
        void Handle(object request, params object[] parameters);

        TResponse? Handle<TResponse>(object request, params object[] parameters);

        Task HandleAsync(object request, params object[] parameters);

        Task HandleAsync(object request, CancellationToken cancellationToken, params object[] parameters);

        Task<TResponse?> HandleAsync<TResponse>(object request, params object[] parameters);

        Task<TResponse?> HandleAsync<TResponse>(object request, CancellationToken cancellationToken, params object[] parameters);
    }
}
