namespace TheXamlGuy.Media.Capture;

public class RemoteMediaCapture : IRemoteMediaCapture
{
    private readonly Dictionary<IRemoteMediaFrameSource, IRemoteMediaFrameReader> frameReaderCache = new();

    public IRemoteMediaFrameSource? FrameSource { get; private set; }

    public async Task<IRemoteMediaFrameReader?> CreateFrameReaderAsync(IRemoteMediaFrameSource frameSource)
    {
        if (frameSource is not null)
        {
            if (frameReaderCache.TryGetValue(frameSource, out IRemoteMediaFrameReader? frameReader))
            {
                return frameReader;
            }

            if (frameSource is IRemoteMediaFrameReaderFactory frameReaderFactory)
            {
                frameReader = frameReaderFactory.Factory();
                frameReaderCache.Add(frameSource, frameReader);
            }

            return await Task.FromResult(frameReader);
        }

        return default;
    }

    public void Initialize(IRemoteMediaCaptureInitializationSettings initializationSettings)
    {
        FrameSource = initializationSettings.Source;
    }

    public async Task<LowLagPhotoCapture?> PrepareLowLagPhotoCaptureAsync()
    {
        IRemoteMediaFrameSource? frameSource = FrameSource;
        if (FrameSource is null)
        {
            if (await RemoteMediaFrameSource.FindAllAsync() is IReadOnlyList<IRemoteMediaFrameSource> { Count: > 0 } sourceGroups)
            {
                frameSource = sourceGroups[0];
            }
        }

        if (frameSource is not null)
        {
            IMediaFrameReader? frameReader = await CreateFrameReaderAsync(frameSource);
            if (frameReader is not null)
            {
                LowLagPhotoCapture photoCapture = new(frameReader);
                await frameReader.StartAsync();

                return photoCapture;
            }
        }

        return default;
    }
}
