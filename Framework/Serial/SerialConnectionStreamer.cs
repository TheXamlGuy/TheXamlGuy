using System.IO.Ports;

namespace TheXamlGuy.Framework.Serial;

public class SerialConnectionStreamer : ISerialConnectionStreamer
{
    public SerialConnectionStreamer(SerialPort serial)
    {
        Stream = serial.BaseStream;
    }

    public Stream Stream { get; }
}
