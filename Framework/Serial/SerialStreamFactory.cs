using System.IO.Ports;

namespace TheXamlGuy.Framework.Serial;

public class SerialStreamer : ISerialStreamer
{
    private readonly SerialPort serialPort;

    public SerialStreamer(SerialPort serialPort)
    {
        this.serialPort = serialPort;
    }

    public Stream Create()
    {
        return serialPort.BaseStream;
    }
}
