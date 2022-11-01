namespace TheXamlGuy.Media.Capture;

public class MediaCapture : IMediaCapture
{
    private readonly Dictionary<IMediaFrameSource, IMediaFrameReader> frameReaderCache = new();

    public IMediaFrameSource? FrameSource { get; private set; }

    public async Task<IMediaFrameReader?> CreateFrameReaderAsync()
    {
        IMediaFrameSource? frameSource = FrameSource;
        if (FrameSource is null)
        {
            if (await MediaFrameSource.FindAllAsync() is IReadOnlyList<IMediaFrameSource> { Count: > 0 } sourceGroups)
            {
                frameSource = sourceGroups[0];
            }
        }

        if (frameSource is not null)
        {
            if (frameReaderCache.TryGetValue(frameSource, out IMediaFrameReader? frameReader))
            {
                return frameReader;
            }

            frameReader = new MediaFrameReader(frameSource);
            frameReaderCache.Add(frameSource, frameReader);

            return frameReader;
        }

        return default;
    }

    public async Task<IMediaFrameReader?> CreateFrameReaderAsync(IMediaFrameSource frameSource)
    {
        if (frameSource is not null)
        {
            if (frameReaderCache.TryGetValue(frameSource, out IMediaFrameReader? frameReader))
            {
                return frameReader;
            }

            frameReader = new MediaFrameReader(frameSource);
            frameReaderCache.Add(frameSource, frameReader);

            return await Task.FromResult(frameReader);
        }

        return default;
    }

    public void Initialize(IMediaCaptureInitializationSettings initializationSettings)
    {
        FrameSource = initializationSettings.Source;
    }

    public async Task<LowLagPhotoCapture?> PrepareLowLagPhotoCaptureAsync()
    {
        IMediaFrameSource? frameSource = FrameSource;
        if (FrameSource is null)
        {
            if (await MediaFrameSource.FindAllAsync() is IReadOnlyList<IMediaFrameSource> { Count: > 0 } sourceGroups)
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
                return photoCapture;
            }
        }

        return default;
    }
}