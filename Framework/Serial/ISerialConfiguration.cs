namespace TheXamlGuy.Framework.Serial;

public interface ISerialConfiguration
{
    public string PortName { get; set; }

    public int BaudRate { get; set; }
}