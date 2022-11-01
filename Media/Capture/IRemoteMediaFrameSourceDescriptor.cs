namespace TheXamlGuy.Media.Capture;

public interface IRemoteMediaFrameSourceDescriptor
{
    string DisplayName { get; }

    Func<IRemoteMediaFrameReader> FrameReaderFactory { get; }

    string Id { get; }
}
