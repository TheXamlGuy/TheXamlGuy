namespace TheXamlGuy.Framework.Microcontroller;

public interface IMicrocontrollerModuleDeserializer<TRead>
{
    public TRead? Read { get; set; }
}
