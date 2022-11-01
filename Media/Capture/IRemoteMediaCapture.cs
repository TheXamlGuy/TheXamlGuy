namespace TheXamlGuy.Media.Capture
{
    public interface IRemoteMediaCapture
    {
        IRemoteMediaFrameSource? FrameSource { get; }

        Task<IRemoteMediaFrameReader?> CreateFrameReaderAsync(IRemoteMediaFrameSource frameSource);

        void Initialize(IRemoteMediaCaptureInitializationSettings initializationSettings);

        Task<LowLagPhotoCapture?> PrepareLowLagPhotoCaptureAsync();
    }
}