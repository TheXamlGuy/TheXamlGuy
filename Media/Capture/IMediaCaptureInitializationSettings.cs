namespace TheXamlGuy.Media.Capture;

public interface IMediaCaptureInitializationSettings
{
    IMediaFrameSource? Source { get; set; }
}