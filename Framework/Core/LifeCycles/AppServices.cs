using Microsoft.Extensions.Hosting;

namespace TheXamlGuy.Framework.Core
{
    public class AppServices : IHostedService
    {
        private readonly IMediator mediator;
        private readonly IDisposer disposer;
        private readonly IInitialization initialization;

        public AppServices(IMediator mediator,
            IDisposer disposer,
            IInitialization initialization)
        {
            this.mediator = mediator;
            this.disposer = disposer;
            this.initialization = initialization;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            mediator.Handle<Started>();
            await initialization.InitializeAsync();
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            disposer.Dispose(this);
            return Task.CompletedTask;
        }
    }
}
