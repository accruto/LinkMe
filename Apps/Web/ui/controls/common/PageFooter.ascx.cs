using System;
using System.Diagnostics;
using System.Web.UI;
using System.Web.UI.WebControls;
using LinkMe.Framework.Utility.Urls;

namespace LinkMe.Web.UI.Controls.Common
{
	/// <summary>
	/// // JMOR 11/05/06: people r welcomed to add more than hyperlinks (image links, static labels ...)
	/// </summary>
	public partial class PageFooter : UserControl
	{
		private const string whiteSpace = "&nbsp;&nbsp;";

        public void AddLinks(string text, ReadOnlyUrl url, string id)
		{
            HyperLink hl = new HyperLink();
			Debug.Assert((id != null && id.Length > 0), "Invalid link ID!");
			
			hl.ID = id;
			hl.Text = text;
			hl.NavigateUrl = url.ToString();
			phItems.Controls.Add(hl);
			phItems.Controls.Add(GetWhiteSpaceLabel());	// now add space for future item
		}

/*        public void AddLinks(string text, Url url, string id)
        {
            AddLinks(text, url.ToString(), id);
        }
*/
        private static Label GetWhiteSpaceLabel()
		{
			Label lbl = new Label();
			lbl.Text = whiteSpace;
			return lbl;
		}
	}
}