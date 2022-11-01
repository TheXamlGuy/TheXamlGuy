namespace TheXamlGuy.Media.Capture;

public interface IMediaFrameSource
{
    MediaFrameFormat CurrentFormat { get; }

    string DisplayName { get; }

    MediaFrameSourceInfo Info { get; }

    IReadOnlyList<MediaFrameFormat> SupportedFormats { get; }

    void SetFormat(MediaFrameFormat format);
}
