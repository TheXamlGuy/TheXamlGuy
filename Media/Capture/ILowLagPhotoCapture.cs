namespace TheXamlGuy.Media.Capture
{
    public interface ILowLagPhotoCapture
    {
        Task<CapturedPhoto?> CaptureAsync();
    }
}