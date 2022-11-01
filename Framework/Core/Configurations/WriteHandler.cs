namespace TheXamlGuy.Framework.Core
{
    public class WriteHandler<TConfiguration> : IMediatorHandler<Write<TConfiguration>> where TConfiguration : class
    {
        private readonly IEventAggregator eventAggregator;
        private readonly TConfiguration configuration;
        private readonly IConfigurationWriter<TConfiguration> writer;

        public WriteHandler(TConfiguration configuration,
            IConfigurationWriter<TConfiguration> writer,
            IEventAggregator eventAggregator)
        {
            this.eventAggregator = eventAggregator;
            this.configuration = configuration;
            this.writer = writer;
        }

        public void Handle(Write<TConfiguration> request)
        {
            request.UpdateDelegate.Invoke(configuration);
            writer.Write(request.Section, configuration);

            eventAggregator.Publish(new ConfigurationChanged<TConfiguration>(configuration));
        }
    }
}