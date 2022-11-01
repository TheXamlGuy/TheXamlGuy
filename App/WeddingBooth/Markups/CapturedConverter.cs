using System;
using System.Drawing;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media.Imaging;
using TheXamlGuy.Framework.Camera;
using TheXamlGuy.UI.WPF;

namespace WeddingBooth.Markups
{
    public class CapturedConverter : ValueConverter<Captured, BitmapSource>
    {
        [DllImport("gdi32.dll")]
        private static extern bool DeleteObject(IntPtr hObject);

        protected override BitmapSource? ConvertTo(Captured value, Type? targetType, object? parameter, CultureInfo? culture)
        {
            if (value.Photo is Bitmap bitmap)
            {
                IntPtr handle = bitmap.GetHbitmap();
                BitmapSource image = Imaging.CreateBitmapSourceFromHBitmap(handle, IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromWidthAndHeight(value.Width, value.Height));

                DeleteObject(handle);
                return image;
            }

            return default;
        }
    }
}
