using System.Collections.ObjectModel;

namespace TheXamlGuy.Framework.Core
{
    public class EventBuilder : IEventBuilder
    {
        private readonly List<IEventBuilderConfiguration> configurations = new();

        public IReadOnlyCollection<IEventBuilderConfiguration> Configurations => new ReadOnlyCollection<IEventBuilderConfiguration>(configurations);

        public IEventBuilderConfiguration<TEvent> Add<TEvent>() where TEvent : class
        {
            EventConfiguration<TEvent>? configuration = new();
            configurations.Add(configuration);

            return configuration;
        }
    }
}