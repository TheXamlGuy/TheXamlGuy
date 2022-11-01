using TheXamlGuy.Framework.Core;
using TheXamlGuy.Media.Capture;

namespace TheXamlGuy.Framework.Camera;

public class CameraContext : ICameraContext
{
    private readonly IEventAggregator eventAggregator;
    private ILowLagPhotoCapture? lowLagPhotoCapture;
    private IMediaCapture? mediaCapture;

    public CameraContext(IEventAggregator eventAggregator)
    {
        this.eventAggregator = eventAggregator;
    }

    public async Task InitializeAsync()
    {
        IReadOnlyList<IMediaFrameSource> sourceGroups = await MediaFrameSource.FindAllAsync();
        if (sourceGroups.FirstOrDefault(x => x.DisplayName.Contains("USB", StringComparison.InvariantCultureIgnoreCase)) is IMediaFrameSource source)
        {
            if (source.SupportedFormats.OrderByDescending(x => x.Size.Width & x.Size.Height).FirstOrDefault() is MediaFrameFormat bestSupportedFormat)
            {
                source.SetFormat(bestSupportedFormat);
            }

            MediaCaptureInitializationSettings settings = new()
            {
                Source = source
            };

            mediaCapture = new MediaCapture();
            mediaCapture.Initialize(settings);

            lowLagPhotoCapture = await mediaCapture.PrepareLowLagPhotoCaptureAsync();
            eventAggregator.SubscribeUI<Capture>(OnCapture);
        }
    }


    private async void OnCapture(Capture args)
    {
        if (lowLagPhotoCapture is not null)
        {
            await lowLagPhotoCapture.CaptureAsync();
        }
    }
}
