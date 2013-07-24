using System;
using System.Web;
using LinkMe.Framework.Utility.Urls;

namespace LinkMe.Web.UI.Controls.Common.FileManager
{
    public sealed class RootFolder
    {
        private string _path;
        private string _text;
        private int _expandDepth = -1;

        public string Path
        {
            get { return _path ?? ""; }
            set { _path = value; }
        }

        public int ExpandDepth
        {
            get { return _expandDepth; }
            set { _expandDepth = value; }
        }

        public Url SmallImageUrl { get; set; }
        public Url LargeImageUrl { get; set; }
        public bool ShowRootIndex { get; set; }

        public string Text
        {
            get { return _text ?? String.Empty; }
            set { _text = value; }
        }

        internal string TextInternal
        {
            get
            {
                if (string.IsNullOrEmpty(Text))
                {
                    if (string.IsNullOrEmpty(Path))
                        return string.Empty;
                    return VirtualPathUtility.GetFileName(Path);
                }
                return Text;
            }
        }

        public override string ToString()
        {
            return Path;
        }
    }
}
