namespace TheXamlGuy.Media.Capture;

internal interface IRemoteMediaFrameReaderFactory
{
    Func<IRemoteMediaFrameReader> Factory { get; }
}