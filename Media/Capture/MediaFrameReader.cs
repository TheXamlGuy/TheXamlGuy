using TheXamlGuy.UI;
using AForge.Video.DirectShow;
using System.Drawing;
using AForge.Video;
using System.Drawing.Imaging;

namespace TheXamlGuy.Media.Capture;

public class MediaFrameReader : IMediaFrameReader
{
    private readonly VideoCaptureDevice captureDevice;
    private bool isAquiringFrames;

    internal MediaFrameReader(IMediaFrameSource frameSource)
    {
        captureDevice = new(frameSource.Info.Id);

        captureDevice.VideoResolution = captureDevice
            .VideoCapabilities
            .FirstOrDefault(x => frameSource.CurrentFormat.FrameRate == x.AverageFrameRate && x.FrameSize.Equals(frameSource.CurrentFormat.Size));

        captureDevice.NewFrame += OnNewFrame;
    }

    public event TypedEventHandler<IMediaFrameReader, MediaFrameArrivedEventArgs>? FrameArrived;

    public void Dispose()
    {
        captureDevice.Stop();
    }

    public async Task StartAsync()
    {
        if (!captureDevice.IsRunning)
        {
            isAquiringFrames = true;
            captureDevice.Start();
        }

        await Task.CompletedTask;
    }

    public async Task StopAsync()
    {
        if (captureDevice.IsRunning)
        {
            isAquiringFrames = false;
            captureDevice.Stop();
        }

        await Task.CompletedTask;
    }

    public async Task<MediaFrame?> TryAcquireLatestFrameAsync()
    {
        TaskCompletionSource<Bitmap> completionSource = new();

        void HandleNewFrame(object sender, NewFrameEventArgs args)
        {
            if (args.Frame.PixelFormat is not PixelFormat.DontCare)
            {
                captureDevice.NewFrame -= HandleNewFrame;
                completionSource.SetResult(new Bitmap(args.Frame));
            }
        }

        captureDevice.NewFrame += HandleNewFrame;
        if (!isAquiringFrames)
        {
            captureDevice.Start();
        }

        Bitmap frame = await completionSource.Task;

        if (!isAquiringFrames)
        {
            captureDevice.SignalToStop();
            captureDevice.WaitForStop();
        }

        return new MediaFrame(frame, frame.Width, frame.Height);
    }

    private void OnNewFrame(object sender, NewFrameEventArgs args)
    {
        FrameArrived?.Invoke(this, new MediaFrameArrivedEventArgs());
    }
}
