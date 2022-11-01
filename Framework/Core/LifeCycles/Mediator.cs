namespace TheXamlGuy.Framework.Core;

public class Mediator : IMediator
{
    private readonly IServiceFactory serviceFactory;

    public Mediator(IServiceFactory serviceFactory)
    {
        this.serviceFactory = serviceFactory;
    }

    public void Handle(object request, params object[] parameters)
    {
        if (GetHandler(typeof(IMediatorHandler<>).MakeGenericType(request.GetType()), parameters) is { } handler)
        {
            handler.Handle((dynamic)request);
        }
    }

    public TResponse? Handle<TResponse>(object request, params object[] parameters)
    {
        if (GetHandler(typeof(IMediatorHandler<,>).MakeGenericType(typeof(TResponse), request.GetType()), parameters) is { } handler)
        {
            return handler.Handle((dynamic)request);
        }

        return default;
    }

    public Task HandleAsync(object request, CancellationToken cancellationToken, params object[] parameters)
    {
        if (GetHandler(typeof(IMediatorAsyncHandler<>).MakeGenericType(request.GetType()), parameters) is { } handler)
        {
            return handler.Handle((dynamic)request, cancellationToken);
        }

        return Task.CompletedTask;
    }

    public Task HandleAsync(object request, params object[] parameters)
    {
        return HandleAsync(request, CancellationToken.None, parameters);
    }

    public Task<TResponse?> HandleAsync<TResponse>(object request, CancellationToken cancellationToken, params object[] parameters)
    {
        if (GetHandler(typeof(IMediatorAsyncHandler<,>).MakeGenericType(typeof(TResponse), request.GetType()), parameters) is { } handler)
        {
            return handler.Handle((dynamic)request, cancellationToken);
        }

        return Task.FromResult<TResponse?>(default);
    }

    public Task<TResponse?> HandleAsync<TResponse>(object request, params object[] parameters)
    {
        return HandleAsync<TResponse?>(request, CancellationToken.None, parameters);
    }

    private dynamic? GetHandler(Type type, params object[] parameters)
    {
        return parameters.Length == 0 ? serviceFactory.Get<object>(type) : serviceFactory.Create<object>(type, parameters);
    }
}
