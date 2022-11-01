using System.Threading.Tasks;

namespace TheXamlGuy.Framework.Core
{
    public static class IMediatorExtensions
    {
        public static void Handle<TEvent>(this IMediator mediator) where TEvent : new()
        {
            mediator.Handle(new TEvent());
        }

        public static TResponse? Handle<TResponse, TRequest>(this IMediator mediator, params object[] parameters) where TRequest : new()
        {
            return mediator.Handle<TResponse?>(new TRequest(), parameters);
        }

        public static Task HandleAsync<TEvent>(this IMediator mediator) where TEvent : new()
        {
            return mediator.HandleAsync(new TEvent());
        }

        public static Task<TResponse?> HandleAsync<TResponse, TRequest>(this IMediator mediator, params object[] parameters) where TRequest : new()
        {
            return mediator.HandleAsync<TResponse?>(new TRequest(), parameters);
        }
    }
}
