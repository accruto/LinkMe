using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace LinkMe.Domain.Images.Commands
{
    public interface IImagesCommand
    {
        bool IsValidFileExtension(string extension);
        ImageFormat GetFormat(string extension);
        Stream Resize(Stream stream, Size maximumSize, ImageFormat format);
        Stream Crop(Stream stream, Size maximumSize, ImageFormat format);
    }
}