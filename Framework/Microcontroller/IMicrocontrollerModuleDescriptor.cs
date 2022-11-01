using Microcontroller;

namespace TheXamlGuy.Framework.Microcontroller;

public interface IMicrocontrollerModuleDescriptor<TModule> : IMicrocontrollerModuleDescriptor where TModule : IMicrocontrollerModule
{
    Func<TModule>? Factory { get; }
}

public interface IMicrocontrollerModuleDescriptor
{
    Type Type { get; }
}

