using Microcontroller;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace TheXamlGuy.Framework.Microcontroller;

public class MicrocontrollerModuleJsonDeserializerHandler : MicrocontrollerModuleDeserializerHandler<MicrocontrollerModuleJsonDeserializer, string>
{
    public MicrocontrollerModuleJsonDeserializerHandler(IReadOnlyCollection<IMicrocontrollerModuleDescriptor> modules) : base(modules)
    {

    }

    public override async Task<IMicrocontrollerModule?> Handle(MicrocontrollerModuleJsonDeserializer request, CancellationToken cancellationToken = default)
    {
        if (JsonNode.Parse(request.Read!) is JsonNode body)
        {
            if (body["module"] is JsonNode triggerNode)
            {
                if (triggerNode["type"] is JsonNode typeNode)
                {
                    if (Modules.FirstOrDefault(x => x.Type.Name.Equals(typeNode.GetValue<string>(), StringComparison.InvariantCultureIgnoreCase)) is IMicrocontrollerModuleDescriptor descriptor)
                    {
                        if (triggerNode["parameters"] is JsonNode parametersNode)
                        {
                            return (IMicrocontrollerModule?)parametersNode.Deserialize(descriptor.Type, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                        }
                        else
                        {
                            return (IMicrocontrollerModule?)Activator.CreateInstance(descriptor.Type);
                        }
                    }        
                }
            }
        }

        return await Task.FromResult<IMicrocontrollerModule?>(default);
    }
}
