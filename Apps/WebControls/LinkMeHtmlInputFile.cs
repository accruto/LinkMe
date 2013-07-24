using System.Web.UI.HtmlControls;
using LinkMe.Utility.Utilities;

namespace LinkMe.WebControls
{
	public class LinkMeHtmlInputFile : HtmlInputFile
	{
		public LinkMeHtmlInputFile()
		{
		}

        public string CssClass
        {
            get { return Attributes["class"]; }
            set { Attributes["class"] = value; }
        }

        public bool HaveFile
        {
            get
            {
                return (PostedFile != null && !string.IsNullOrEmpty(PostedFile.FileName)
                    && PostedFile.ContentLength > 0);
            }
        }

        public byte[] ReadFile()
        {
            if (!HaveFile)
                return null;

            return StreamHelper.ReadFully(PostedFile.InputStream);
        }
	}
}