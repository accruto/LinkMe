using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LinkMe.WebControls
{
    public class ExpandingLabel : WebControl
    {
        private string content = String.Empty;
        private int maxLength = 300;

        protected override void Render(HtmlTextWriter writer)
        {
            base.Render(writer);
            
            if(String.IsNullOrEmpty(Content))
                return;

            if(Content.Length <= MaxLength)
            {
                writer.WriteEncodedText(Content);
                return;
            } else {
                writer.Write(Content.Substring(0, MaxLength));
                string spanId = "span_" + Guid.NewGuid().ToString("n");
                string hrefId = "href_" + Guid.NewGuid().ToString("n");
                writer.Write("<a id=\"" + hrefId + "\"href=\"javascript:void(0);\" onclick=\"document.getElementById('" + spanId + "').style.display=''; document.getElementById('" + hrefId + "').style.display='none';\">...</a>");
                writer.Write("<span id=\"" + spanId + "\" style=\"display:none;\">");
                writer.WriteEncodedText(Content.Substring(MaxLength, Content.Length - MaxLength));
                writer.Write("</span>");
            }
        }

        public string Content
        {
            get { return content; }
            set { content = value; }
        }


        public int MaxLength
        {
            get { return maxLength; }
            set { maxLength = value; }
        }
    }
}
