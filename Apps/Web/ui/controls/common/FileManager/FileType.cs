using LinkMe.Framework.Utility.Urls;

namespace LinkMe.Web.UI.Controls.Common.FileManager
{
    public sealed class FileType
    {
        private static readonly FileType _styleSheet = new FileType("Cascading Style Sheet", "css", new ApplicationUrl("~/ui/images/controls/FileManager/FileTypes/32x32/stylesheet.gif"), new ApplicationUrl("~/ui/images/controls/FileManager/FileTypes/16x16/stylesheet.gif"));
        private static readonly FileType _javaScript = new FileType("Java Script", "js", new ApplicationUrl("~/ui/images/controls/FileManager/FileTypes/32x32/stylesheet.gif"), new ApplicationUrl("~/ui/images/controls/FileManager/FileTypes/16x16/stylesheet.gif"));
        private static readonly FileType _image = new FileType("Image", "gif, jpg, png", new ApplicationUrl("~/ui/images/controls/FileManager/FileTypes/32x32/image.gif"), new ApplicationUrl("~/ui/images/controls/FileManager/FileTypes/16x16/image.gif"));

        private string _name;

        private FileType(string name, string extensions, Url largeImageUrl, Url smallImageUrl)
        {
            _name = name;
            Extensions = extensions;
            LargeImageUrl = largeImageUrl;
            SmallImageUrl = smallImageUrl;
        }

        public static FileType StyleSheet
        {
            get { return _styleSheet; }
        }

        public static FileType Image
        {
            get { return _image; }
        }

        public static FileType JavaScript
        {
            get { return _image; }
        }

        public string Name
        {
            get { return _name ?? string.Empty; }
            set { _name = value; }
        }

        public string Extensions { get; set; }
        public Url SmallImageUrl { get; set; }
        public Url LargeImageUrl { get; set; }
    }
}
