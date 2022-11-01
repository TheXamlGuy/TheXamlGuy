namespace TheXamlGuy.Media.Capture;

public class LowLagPhotoCapture : ILowLagPhotoCapture
{
    private readonly IMediaFrameReader frameReader;

    internal LowLagPhotoCapture(IMediaFrameReader frameReader)
    {
        this.frameReader = frameReader;
    }

    public async Task<CapturedPhoto?> CaptureAsync()
    {
        if (await frameReader.TryAcquireLatestFrameAsync() is MediaFrame frame)
        {
            return new CapturedPhoto(frame.Bitmap, frame.Width, frame.Height);
        }

        return await Task.FromResult<CapturedPhoto?>(default);
    }
}
