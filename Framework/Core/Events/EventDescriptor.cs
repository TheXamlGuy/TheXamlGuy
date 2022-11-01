namespace TheXamlGuy.Framework.Core
{
    public record EventDescriptor<TEvent, THandlerEvent> : IEventDescriptor
    {
        public EventDescriptor()
        {

        }

        public EventDescriptor(Func<IServiceProvider, TEvent, THandlerEvent>? factoryDelegate)
        {
            FactoryDelegate = factoryDelegate;
        }

        public EventDescriptor(Func<TEvent, THandlerEvent>? nextDelegate)
        {
            NextDelegate = nextDelegate;
        }

        public Func<IServiceProvider, TEvent, THandlerEvent>? FactoryDelegate { get; }

        public Func<TEvent, THandlerEvent>? NextDelegate { get; }
    }
}