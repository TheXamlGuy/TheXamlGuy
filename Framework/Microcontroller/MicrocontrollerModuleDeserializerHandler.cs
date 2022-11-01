using Microcontroller;
using TheXamlGuy.Framework.Core;

namespace TheXamlGuy.Framework.Microcontroller;

public abstract class MicrocontrollerModuleDeserializerHandler<TDeserializer, TRead> : IMediatorAsyncHandler<IMicrocontrollerModule?, TDeserializer> where TDeserializer : MicrocontrollerModuleDeserializer<TRead>
{
    public MicrocontrollerModuleDeserializerHandler(IReadOnlyCollection<IMicrocontrollerModuleDescriptor> modules)
    {
        Modules = modules;
    }

    public IReadOnlyCollection<IMicrocontrollerModuleDescriptor> Modules { get; }

    public abstract Task<IMicrocontrollerModule?> Handle(TDeserializer request, CancellationToken cancellationToken = default);
}
