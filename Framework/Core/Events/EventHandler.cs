namespace TheXamlGuy.Framework.Core
{
    public class EventHandler<TTEvent> : IEventHandler<TTEvent> where TTEvent : class
    {
        private readonly IEventBuilderConfiguration<TTEvent> configuration;
        private readonly IDisposer disposer;
        private readonly IEventAggregator eventAggregator;
        private readonly IMediator mediator;
        private readonly IServiceFactory serviceFactory;
        private readonly IServiceProvider serviceProvider;

        public EventHandler(IEventBuilderConfiguration<TTEvent> configuration,
            IServiceProvider serviceProvider,
            IServiceFactory serviceFactory,
            IEventAggregator eventAggregator,
            IMediator mediator,
            IDisposer disposer)
        {
            this.configuration = configuration;
            this.serviceProvider = serviceProvider;
            this.serviceFactory = serviceFactory;
            this.eventAggregator = eventAggregator;
            this.mediator = mediator;
            this.disposer = disposer;
        }

        public void Dispose()
        {
            disposer.Dispose(this);
            GC.SuppressFinalize(this);
        }

        public async Task InitializeAsync()
        {
            disposer.Add(this, eventAggregator.SubscribeUI<TTEvent>(OnEvent));
            await Task.CompletedTask;
        }

        private void OnEvent(TTEvent args)
        {
            foreach (IEventDescriptor? descriptor in configuration.Descriptors)
            {
                mediator.Handle((descriptor as dynamic).NextDelegate?.Invoke(args) ?? (descriptor as dynamic).FactoryDelegate?.Invoke(serviceProvider, args));
            }
        }
    }
}
