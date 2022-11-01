using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace TheXamlGuy.Framework.Core
{
    public class EventConfiguration<TEvent> : IEventBuilderConfiguration<TEvent> where TEvent : class
    {
        private readonly List<IEventDescriptor> descriptors = new();

        public EventConfiguration()
        {

        }

        public IReadOnlyCollection<IEventDescriptor> Descriptors => new ReadOnlyCollection<IEventDescriptor>(descriptors);

        public Action<IServiceProvider, TEvent>? Factory { get; }

        public Action<TEvent>? Next { get; }

        public IEventBuilderConfiguration<TEvent> WithHandler<THandlerEvent>() where THandlerEvent : class
        {
            descriptors.Add(new EventDescriptor<TEvent, THandlerEvent>());
            return this;
        }

        public IEventBuilderConfiguration<TEvent> WithHandler<THandlerEvent>(Func<TEvent, THandlerEvent> factoryDelegate) where THandlerEvent : class
        {
            descriptors.Add(new EventDescriptor<TEvent, THandlerEvent>(factoryDelegate));
            return this;
        }

        public IEventBuilderConfiguration<TEvent> WithHandler<THandlerEvent>(Func<IServiceProvider, TEvent, THandlerEvent> factoryDelegate) where THandlerEvent : class
        {
            descriptors.Add(new EventDescriptor<TEvent, THandlerEvent>(factoryDelegate));
            return this;
        }
    }
}