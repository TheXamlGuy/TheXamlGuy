using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using TheXamlGuy.Media.Capture;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;
using System.Windows.Interop;
using System.Runtime.InteropServices;

namespace TheXamlGuy.UI.WPF.Controls;

public class CameraPreview : Control
{
    private Image? image;

    public CameraPreview()
    {
        DefaultStyleKey = typeof(CameraPreview);
    }

    public override void OnApplyTemplate()
    {
        image = GetTemplateChild("Image") as Image;
    }

    public async Task StartAsync(IMediaFrameSource source)
    {
        MediaCaptureInitializationSettings settings = new()
        {
            Source = source
        };

        MediaCapture mediaCapture = new();
        mediaCapture.Initialize(settings);

        if (await mediaCapture.CreateFrameReaderAsync() is IMediaFrameReader frameReader)
        {
            frameReader.FrameArrived += OnFrameArrived;
            await frameReader.StartAsync();
        }
    }

    public async Task StartAsync()
    {
        if (image is null)
        {
            return;
        }

        IReadOnlyList<IMediaFrameSource> sourceGroups = await MediaFrameSource.FindAllAsync();
        if (sourceGroups.FirstOrDefault(x => x.DisplayName.Contains("USB", System.StringComparison.InvariantCultureIgnoreCase)) is IMediaFrameSource source)
        {
            if (source.SupportedFormats.OrderByDescending(x => x.Size.Width & x.Size.Height).FirstOrDefault() is MediaFrameFormat bestSupportedFormat)
            {
                source.SetFormat(bestSupportedFormat);
            }

            await StartAsync(source);
        }
    }

    [DllImport("gdi32.dll")]
    private static extern bool DeleteObject(IntPtr hObject);

    private async void OnFrameArrived(IMediaFrameReader sender, MediaFrameArrivedEventArgs args)
    {
        if (image is null)
        {
            return;
        }

        if (await sender.TryAcquireLatestFrameAsync() is MediaFrame frame)
        {
            Dispatcher.Invoke(() =>
            {
                IntPtr handle = frame.Bitmap.GetHbitmap();
                image.Source = Imaging.CreateBitmapSourceFromHBitmap(handle, IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromWidthAndHeight(frame.Width, frame.Height));
                DeleteObject(handle);
            });
        }
    }
}
