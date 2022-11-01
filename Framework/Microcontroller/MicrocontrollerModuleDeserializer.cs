namespace TheXamlGuy.Framework.Microcontroller;


public abstract record MicrocontrollerModuleDeserializer<TRead> : IMicrocontrollerModuleDeserializer<TRead>
{
    public TRead? Read { get; set; }
}
