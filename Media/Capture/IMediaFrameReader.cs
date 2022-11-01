using TheXamlGuy.UI;

namespace TheXamlGuy.Media.Capture;

public interface IMediaFrameReader : IDisposable
{
    event TypedEventHandler<IMediaFrameReader, MediaFrameArrivedEventArgs>? FrameArrived;

    Task StartAsync();

    Task StopAsync();

    Task<MediaFrame?> TryAcquireLatestFrameAsync();
}
