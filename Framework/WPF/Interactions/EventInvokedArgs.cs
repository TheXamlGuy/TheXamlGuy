using System;

namespace TheXamlGuy.Framework.WPF;

public class EventInvokedArgs : EventArgs
{
    public EventInvokedArgs(object args)
    {
        Invoked = args;
    }

    public object Invoked { get; }
}
