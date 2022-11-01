namespace TheXamlGuy.Framework.Core;

public interface IEventBuilder
{
    IReadOnlyCollection<IEventBuilderConfiguration> Configurations { get; }

    IEventBuilderConfiguration<TEvent> Add<TEvent>() where TEvent : class;
}