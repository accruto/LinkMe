using System.Drawing;

namespace LinkMe.Domain.Contacts
{
    public class Constants
    {
        private static readonly Size _photoMaxSize = new Size(100, 125);
        private static readonly Size _thumbnailMaxSize = new Size(53, 66);

        public static Size PhotoMaxSize
        {
            get { return new Size(_photoMaxSize.Width, _photoMaxSize.Height); }
        }

        public static Size ThumbnailMaxSize
        {
            get { return new Size(_thumbnailMaxSize.Width, _thumbnailMaxSize.Height); }
        }

        public const int MaxJobTitleLength = 100;
    }
}
