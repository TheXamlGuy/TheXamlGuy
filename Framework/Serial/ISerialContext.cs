namespace TheXamlGuy.Framework.Serial;

public interface ISerialContext
{
    void Open();
}

public interface ISerialContext<TSerialReader, TContent> : ISerialContext where TSerialReader : SerialReader<TContent>
{

}