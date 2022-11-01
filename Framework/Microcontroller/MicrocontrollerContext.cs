using Microcontroller;
using TheXamlGuy.Framework.Core;
using TheXamlGuy.Framework.Serial;

namespace TheXamlGuy.Framework.Microcontroller;

public class MicrocontrollerContext<TRead, TReadDeserializer> : IMicrocontrollerContext<TRead, TReadDeserializer> where TReadDeserializer : IMicrocontrollerModuleDeserializer<TRead>, new()
{
    private readonly IReadOnlyCollection<IMicrocontrollerModuleDescriptor> modules;
    private readonly IEventAggregator eventAggregator;
    private readonly IMediator mediator;
    private readonly ISerialContext serialContext;

    public MicrocontrollerContext(IReadOnlyCollection<IMicrocontrollerModuleDescriptor> modules,
        IEventAggregator eventAggregator,
        IMediator mediator,
        ISerialContext serialContext)
    {
        this.modules = modules;
        this.eventAggregator = eventAggregator;
        this.mediator = mediator;
        this.serialContext = serialContext;
    }

    public async Task InitializeAsync()
    {
        eventAggregator.Subscribe<SerialResponse<TRead>>(OnEvent, null, args => args.Context.Equals(serialContext));
        serialContext.Open();

        await Task.CompletedTask;
    }

    private async void OnEvent(SerialResponse<TRead> args)
    {
        IMicrocontrollerModule? module = await mediator.HandleAsync<IMicrocontrollerModule>(new TReadDeserializer { Read = args.Content }, modules);
        eventAggregator.Publish((dynamic?)module);
    }
}