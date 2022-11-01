using System.Diagnostics.CodeAnalysis;

namespace TheXamlGuy.Framework.Microcontroller;

public class MicrocontrollerConfiguration : IMicrocontrollerConfiguration
{
    [NotNull]
    public string? PortName { get; set; }

    public int BaudRate { get; set; }
}

