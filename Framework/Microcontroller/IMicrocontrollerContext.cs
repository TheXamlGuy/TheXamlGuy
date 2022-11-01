using TheXamlGuy.Framework.Core;

namespace TheXamlGuy.Framework.Microcontroller;

public interface IMicrocontrollerContext : IInitializer
{

}

public interface IMicrocontrollerContext<TRead, TModuleDeserializer> : IMicrocontrollerContext where TModuleDeserializer : IMicrocontrollerModuleDeserializer<TRead>, new()
{

}