using System;
using System.Web.UI;
using LinkMe.Framework.Utility;

namespace LinkMe.WebControls
{

    public class TooltipIcon : Control
    {
        public enum IconStyleEnum
        {
            Question,
            Information
        }

        private readonly string uniqueTooltipId = Guid.NewGuid().ToString("n");

        public TooltipIcon()
        {
            Style = IconStyleEnum.Question;
            HtmlFormat = false;
        }

        public string HeadingText { get; set; }
        public string Text { get; set; }
        public bool HtmlFormat { get; set; }
        public IconStyleEnum Style { get; set; }

        private string ApplicationPath
        {
            get
            {
                if (Page == null)
                    return "";

                string appPath = Page.Request.ApplicationPath;
                return (appPath == "/" ? "" : appPath);
            }
        }

        private string InfoDivId
        {
            get { return ClientID + "_tip_" + uniqueTooltipId; }
        }

        private string ImageId
        {
            get { return ClientID + "_image_" + uniqueTooltipId; }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            // Register general scripts for all instance of this control on the page.

            RegisterScripts();
        }

        private void RegisterScripts()
        {
            // MF (2010-10-06): TooltipBehaviour is so common it's now included for all pages
            //Page.ClientScript.RegisterClientScriptInclude("TooltipBehaviour.js", ApplicationPath + "/js/LinkMeUI/behaviours/TooltipBehaviour.js");

            // MF (2008-09-17): moved popupInfo CSS class into universal-layout.css as .tooltip
            //
            //Page.ClientScript.RegisterClientScriptBlock(typeof(TooltipIcon), "ToggleTooltipStyle",
            //    TOOLTIP_STYLE, false); // Not actually a script, but should work anyway.
        }

        private string removeLineBreaks(string str)
        {
            return str.Replace("\r", "").Replace("\n", "");
        }

        protected override void Render(HtmlTextWriter writer)
        {
            if (string.IsNullOrEmpty(Text))
                return;

            var sanitisedText = HtmlFormat ?
                removeLineBreaks(TextUtil.StripExtraWhiteSpace(Text)) :
                HtmlUtil.TextToHtml(Text);
            sanitisedText = sanitisedText.Replace("\'", "\\\'");

            if (String.IsNullOrEmpty(HeadingText))
            {
                writer.WriteBeginTag("img");
                writer.WriteAttribute("id", ImageId);
                writer.WriteAttribute("class", "helpicon");
                writer.WriteAttribute("src", ApplicationPath + GetIconImagePath());
                writer.WriteAttribute("onmouseover", string.Format(
                    "javascript:LinkMeUI.TooltipBehaviour.mouseOver(event, '{0}', '{1}', '{2}');",
                    ImageId, InfoDivId, sanitisedText));
                writer.WriteAttribute("onmouseout", string.Format(
                    "javascript:LinkMeUI.TooltipBehaviour.mouseOut(event, '{0}', '{1}');",
                    ImageId, InfoDivId));
                writer.WriteLine(HtmlTextWriter.SelfClosingTagEnd);
            } 
            else
            {
                writer.WriteBeginTag("a");
                writer.WriteAttribute("href", "javascript:void(0);");
                writer.WriteAttribute("id", ImageId);
                writer.WriteAttribute("class", "helptitle");
                //writer.WriteAttribute("class", "helpicon");
                writer.WriteAttribute("onclick", string.Format(
                    "javascript:LinkMeUI.TooltipBehaviour.toggle(event, '{0}', '{1}', '{2}');",
                    ImageId, InfoDivId, sanitisedText));
                writer.Write(HtmlTextWriter.TagRightChar);
                writer.WriteEncodedText(HeadingText);
                writer.WriteEndTag("a");

            }
        }

        private string GetIconImagePath()
        {
            switch (Style)
            {
                case IconStyleEnum.Question:
                    return "/ui/img/help.gif";

                case IconStyleEnum.Information:
                    return "/ui/img/info.gif";

                default:
                    throw new ApplicationException("Unexpected value of Style: " + Style);
            }
        }

    }
}
