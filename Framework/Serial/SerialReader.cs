namespace TheXamlGuy.Framework.Serial;

public abstract class SerialReader<TContent>
{
    public SerialReader(Stream stream)
    {
        Stream = stream;
    }

    public Stream Stream { get; }

    public abstract IAsyncEnumerable<TContent> ReadAsync();
}