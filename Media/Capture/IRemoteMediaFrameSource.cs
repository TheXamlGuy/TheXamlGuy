namespace TheXamlGuy.Media.Capture;

public interface IRemoteMediaFrameSource
{
    string DisplayName { get; }

    MediaFrameSourceInfo Info { get; }
}
