using Microcontroller;
using TheXamlGuy.Framework.Core;

namespace TheXamlGuy.Framework.Microcontroller;

public record CapactiveSensor : IMicrocontrollerModule, ITwoStateSensor, IHasSensorPlacement
{
    public SensorState State { get; init; }

    public SensorPlacement Placement { get; init; }
}