using TheXamlGuy.Framework.Core;
using TheXamlGuy.Media.Capture;

namespace TheXamlGuy.Framework.Camera;

public class RemoteCameraContext : ICameraContext
{
    private readonly IEventAggregator eventAggregator;
    private ILowLagPhotoCapture? lowLagPhotoCapture;
    private IRemoteMediaCapture? mediaCapture;

    public RemoteCameraContext(IEventAggregator eventAggregator)
    {
        this.eventAggregator = eventAggregator;
    }

    public async Task InitializeAsync()
    {
        IReadOnlyList<IRemoteMediaFrameSource> sourceGroups = await RemoteMediaFrameSource.FindAllAsync();
        if (sourceGroups.FirstOrDefault(x => x.DisplayName.Contains("DSC-HX60", StringComparison.InvariantCultureIgnoreCase)) is IRemoteMediaFrameSource source)
        {
            RemoteMediaCaptureInitializationSettings settings = new()
            {
                Source = source
            };

            mediaCapture = new RemoteMediaCapture();
            mediaCapture.Initialize(settings);

            lowLagPhotoCapture = await mediaCapture.PrepareLowLagPhotoCaptureAsync();
            eventAggregator.SubscribeUI<Capture>(OnCapture);
        }
    }


    private async void OnCapture(Capture args)
    {
        if (lowLagPhotoCapture is not null)
        {
            if (await lowLagPhotoCapture.CaptureAsync() is CapturedPhoto capturedPhoto)
            {
                eventAggregator.Publish(new Captured
                {
                    Photo = capturedPhoto.Photo,
                    Width = capturedPhoto.Width,
                    Height = capturedPhoto.Height
                });
            }
        }
    }
}
