using System.IO.Ports;

namespace TheXamlGuy.Framework.Serial;

public class SerialConnection : ISerialConnection
{
    private readonly SerialPort serialPort;

    public SerialConnection(SerialPort serialPort)
    {
        this.serialPort = serialPort;
    }

    public bool IsOpen { get; private set; }

    public void Close()
    {
        if (IsOpen)
        {
            try
            {
                serialPort.Close();
            }
            catch
            {

            }

            IsOpen = serialPort.IsOpen;
        }
    }

    public bool Open()
    {
        if (!IsOpen)
        {
            try
            {
                serialPort.Open();

                serialPort.DiscardInBuffer();
                serialPort.DiscardOutBuffer();
            }
            catch
            {

            }

            IsOpen = serialPort.IsOpen;
        }

        return IsOpen;
    }
}

