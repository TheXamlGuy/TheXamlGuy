namespace TheXamlGuy.Framework.Serial;

public interface ISerialFactory
{
    ISerialContext<TSerialReader, TContent> Create<TSerialReader, TContent>(ISerialConfiguration configuration) where TSerialReader : SerialReader<TContent>;
}