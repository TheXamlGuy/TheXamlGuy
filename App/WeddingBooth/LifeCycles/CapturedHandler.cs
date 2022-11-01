using Microsoft.Extensions.Hosting;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using TheXamlGuy.Framework.Camera;
using TheXamlGuy.Framework.Core;

namespace WeddingBooth.LifeCycles
{
    public class CapturedHandler : IMediatorHandler<Captured>
    {
        private readonly IHostEnvironment hostEnvironment;

        public CapturedHandler(IHostEnvironment hostEnvironment)
        {
            this.hostEnvironment = hostEnvironment;
        }

        public void Handle(Captured request)
        {
            if (request.Photo is Bitmap bitmap)
            {
                using Bitmap writableBitmap = new(bitmap);
                string directory = Path.Combine(hostEnvironment.ContentRootPath, "Photos");
                Directory.CreateDirectory(directory);

                ImageCodecInfo encoder = ImageCodecInfo.GetImageEncoders().First(x => x.FormatID == ImageFormat.Jpeg.Guid);
                EncoderParameters encoderParameters = new() { Param = new[] { new EncoderParameter(Encoder.Quality, 100L) } };
                writableBitmap.Save($"{directory}\\{DateTime.Now:MMddyyyy-HHmmss}.jpg", encoder, encoderParameters);
            }
        }
    }
}
