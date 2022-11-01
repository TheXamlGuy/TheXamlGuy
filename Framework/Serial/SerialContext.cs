using TheXamlGuy.Framework.Core;

namespace TheXamlGuy.Framework.Serial;

public class SerialContext<TSerialReader, TContent> : ISerialContext<TSerialReader, TContent> where TSerialReader : SerialReader<TContent>
{
    private readonly ISerialConnection connection;
    private readonly IEventAggregator eventAggregator;
    private readonly ISerialStreamer serialStreamer;

    public SerialContext(IEventAggregator eventAggregator,
        ISerialConnection connection,
        ISerialStreamer serialStreamer)
    {
        this.eventAggregator = eventAggregator;
        this.connection = connection;
        this.serialStreamer = serialStreamer;
    }

    public async void Open()
    {
        if (connection.Open())
        {
            Stream stream = serialStreamer.Create();

            if ((TSerialReader?)Activator.CreateInstance(typeof(TSerialReader), new object[] { stream }) is TSerialReader reader)
            {
                await foreach (TContent content in reader.ReadAsync())
                {
                    eventAggregator.Publish(SerialResponse.Create(this, content));
                }
            }
        }
    }
}
