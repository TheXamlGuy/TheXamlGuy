using AForge.Video.DirectShow;

namespace TheXamlGuy.Media.Capture;

public class MediaFrameSource : IMediaFrameSource
{
    public List<MediaFrameFormat> supportedFormats = new();
    private MediaFrameFormat? currentFormat = null;

    internal MediaFrameSource(string id, string displayName)
    {
        Info = new MediaFrameSourceInfo(id);
        DisplayName = displayName;

        InitializeSupportedFormats(id);
    }

    public MediaFrameFormat CurrentFormat => currentFormat ?? supportedFormats.FirstOrDefault()!;

    public string DisplayName { get; }

    public MediaFrameSourceInfo Info { get; }

    public IReadOnlyList<MediaFrameFormat> SupportedFormats => supportedFormats;

    public static async Task<IReadOnlyList<IMediaFrameSource>> FindAllAsync()
    {
        List<IMediaFrameSource> result = new();
        foreach (FilterInfo videoInputDevice in new FilterInfoCollection(FilterCategory.VideoInputDevice))
        {
            result.Add(new MediaFrameSource(videoInputDevice.MonikerString, videoInputDevice.Name));
        }

        return await Task.FromResult(result);
    }

    public void SetFormat(MediaFrameFormat format)
    {
        currentFormat = format;
    }

    private void InitializeSupportedFormats(string id)
    {
        VideoCaptureDevice videoCaptureDevice = new(id);
        foreach (VideoCapabilities videoCapabilities in videoCaptureDevice.VideoCapabilities)
        {
            supportedFormats.Add(new MediaFrameFormat 
            { 
                Size = videoCapabilities.FrameSize, 
                FrameRate = videoCapabilities.AverageFrameRate 
            });
        }
    }
}