using LinkMe.Framework.Utility.Urls;

namespace LinkMe.Web.UI.Controls.Common.FileManager
{
    public sealed class SpecialFolder
    {
        private string path;
        private Url smallImageUrl;
        private Url largeImageUrl;
        private string text;

        public string Path
        {
            get { return path ?? string.Empty; }
            set { path = value; }
        }

        public Url SmallImageUrl
        {
            get { return smallImageUrl; }
            set { smallImageUrl = value; }
        }

        public Url LargeImageUrl
        {
            get { return largeImageUrl; }
            set { largeImageUrl = value; }
        }

        public string Text
        {
            get { return text ?? string.Empty; }
            set { text = value; }
        }

        public override string ToString()
        {
            return Path;
        }
    }
}
