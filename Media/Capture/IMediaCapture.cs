namespace TheXamlGuy.Media.Capture
{
    public interface IMediaCapture
    {
        IMediaFrameSource? FrameSource { get; }

        Task<IMediaFrameReader?> CreateFrameReaderAsync();

        Task<IMediaFrameReader?> CreateFrameReaderAsync(IMediaFrameSource frameSource);

        void Initialize(IMediaCaptureInitializationSettings initializationSettings);

        Task<LowLagPhotoCapture?> PrepareLowLagPhotoCaptureAsync();
    }
}