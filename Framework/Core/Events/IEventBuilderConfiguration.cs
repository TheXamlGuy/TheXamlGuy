namespace TheXamlGuy.Framework.Core
{
    public interface IEventBuilderConfiguration
    {

    }

    public interface IEventBuilderConfiguration<TEvent> : IEventBuilderConfiguration where TEvent : class
    {
        IReadOnlyCollection<IEventDescriptor> Descriptors { get; }

        Action<IServiceProvider, TEvent>? Factory { get; }

        Action<TEvent>? Next { get; }

        IEventBuilderConfiguration<TEvent> WithHandler<THandlerEvent>() where THandlerEvent : class;

        IEventBuilderConfiguration<TEvent> WithHandler<THandlerEvent>(Func<TEvent, THandlerEvent> factoryDelegate) where THandlerEvent : class;

        IEventBuilderConfiguration<TEvent> WithHandler<THandlerEvent>(Func<IServiceProvider, TEvent, THandlerEvent> factoryDelegate) where THandlerEvent : class;
    }
}