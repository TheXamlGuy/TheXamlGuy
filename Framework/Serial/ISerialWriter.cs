namespace TheXamlGuy.Framework.Serial;

public interface ISerialWriter
{
    void Write(byte[] buffer, int offset, int count);
}
