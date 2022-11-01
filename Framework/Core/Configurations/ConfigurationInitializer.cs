namespace TheXamlGuy.Framework.Core
{
    public class ConfigurationInitializer<TConfiguration> : IInitializer where TConfiguration : class, new()
    {
        private readonly TConfiguration configuration;
        private readonly IEventAggregator eventAggregator;

        public ConfigurationInitializer(TConfiguration configuration, IEventAggregator eventAggregator)
        {
            this.configuration = configuration;
            this.eventAggregator = eventAggregator;
        }

        public async Task InitializeAsync()
        {
            eventAggregator.Publish(configuration);
            await Task.CompletedTask;
        }
    }
}