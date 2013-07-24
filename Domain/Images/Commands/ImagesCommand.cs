using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;

namespace LinkMe.Domain.Images.Commands
{
    public class ImagesCommand
        : IImagesCommand
    {
        private static readonly string[] ValidExtensions = new[] { "jpg", "jpeg", "gif", "png", "bmp" };

        bool IImagesCommand.IsValidFileExtension(string extension)
        {
            extension = extension.TrimStart('.');
            return ValidExtensions.Contains(extension.ToLower());
        }

        ImageFormat IImagesCommand.GetFormat(string extension)
        {
            extension = extension.TrimStart('.');
            switch (extension)
            {
                case "gif":
                    return ImageFormat.Gif;

                case "png":
                    return ImageFormat.Png;

                case "bmp":
                    return ImageFormat.Bmp;

                default:
                    return ImageFormat.Jpeg;
            }
        }

        Stream IImagesCommand.Resize(Stream stream, Size maximumSize, ImageFormat format)
        {
            using (var image = Image.FromStream(stream))
            {
                using (var resized = Resize(image, maximumSize))
                {
                    using (var cropped = Crop(resized, maximumSize))
                    {
                        var outputStream = new MemoryStream();
                        cropped.Save(outputStream, format);
                        return outputStream;
                    }
                }
            }
        }

        Stream IImagesCommand.Crop(Stream stream, Size maximumSize, ImageFormat format)
        {
            using (var image = Image.FromStream(stream))
            {
                using (var cropped = Crop(image, maximumSize))
                {
                    var outputStream = new MemoryStream();
                    cropped.Save(outputStream, format);
                    return outputStream;
                }
            }
        }

        /// <summary>
        /// Resizes the supplied image, preserving the width/height ratio, so that it fits within the specified size.
        /// </summary>
        private static Image Resize(Image source, Size maximumSize)
        {
            var currentSize = new Size(source.Width, source.Height);
            var newSize = GetSize(currentSize, maximumSize);
            if (currentSize == newSize)
                return source;

            var bitmap = new Bitmap(newSize.Width, newSize.Height, PixelFormat.Format64bppPArgb);
            try
            {
                bitmap.SetResolution(source.HorizontalResolution, source.VerticalResolution);
                using (var graphics = Graphics.FromImage(bitmap))
                {
                    graphics.CompositingQuality = CompositingQuality.HighQuality;
                    graphics.SmoothingMode = SmoothingMode.HighQuality;
                    graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;

                    // Offset bitmap to avoid grey lines.

                    graphics.DrawImage(source, -1, -1, newSize.Width + 1, newSize.Height + 1);
                }
            }
            catch
            {
                bitmap.Dispose();
                throw;
            }

            return bitmap;
        }

        /// <summary>
        /// Calculates the new size of an image to be downsized. If the current size fits within the
        /// specified maximum height and width returns it, otherwise returns an appropriately scaled
        /// image (preserving ratios) that is no larger than the specified maximum size.
        /// </summary>
        private static Size GetSize(Size currentSize, Size maximumSize)
        {
            var size = new Size();

            if (currentSize.Width < currentSize.Height)
            {
                size.Width = maximumSize.Width;

                if (currentSize.Width > size.Width)
                {
                    var ratio = currentSize.Width / (float)size.Width;
                    size.Height = (int)(currentSize.Height / ratio);
                }
                else
                {
                    size.Width = currentSize.Width;
                    size.Height = currentSize.Height;
                }
            }
            else
            {
                size.Height = maximumSize.Height;

                if (currentSize.Height > size.Height)
                {
                    var ratio = currentSize.Height / (float)size.Height;
                    size.Width = (int)(currentSize.Width / ratio);
                }
                else
                {
                    size.Width = currentSize.Width;
                    size.Height = currentSize.Height;
                }
            }

            return size;
        }

        /// <summary>
        /// Crop the supplied image around the edges (equally on each side), so that it's no larger than
        /// the specified maximum size.
        /// </summary>
        private static Image Crop(Image source, Size maximumSize)
        {
            if (source.Width <= maximumSize.Width && source.Height <= maximumSize.Height)
                return source;

            var newSize = new Size(Math.Min(source.Width, maximumSize.Width), Math.Min(source.Height, maximumSize.Height));
            var sourceX = (source.Width - newSize.Width) / 2;
            var sourceY = (source.Height - newSize.Height) / 2;

            var bitmap = new Bitmap(newSize.Width, newSize.Height, PixelFormat.Format64bppPArgb);

            try
            {
                bitmap.SetResolution(source.HorizontalResolution, source.VerticalResolution);
                using (var graphics = Graphics.FromImage(bitmap))
                {
                    graphics.Clear(Color.FromArgb(200, 200, 200));
                    graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    graphics.DrawImage(source, new Rectangle(0, 0, newSize.Width, newSize.Height), new Rectangle(sourceX, sourceY, newSize.Width, newSize.Height), GraphicsUnit.Pixel);
                }
            }
            catch
            {
                bitmap.Dispose();
                throw;
            }

            return bitmap;
        }
    }
}
