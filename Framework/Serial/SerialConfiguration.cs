﻿using System.Diagnostics.CodeAnalysis;

namespace TheXamlGuy.Framework.Serial;

public class SerialConfiguration : ISerialConfiguration
{
    [NotNull]
    public string? PortName { get; set; }

    public int BaudRate { get; set; }
}
