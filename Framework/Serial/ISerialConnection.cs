namespace TheXamlGuy.Framework.Serial;

public interface ISerialConnection
{
    bool IsOpen { get; }

    void Close();

    bool Open();
}