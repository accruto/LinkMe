using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LinkMe.Framework.Utility;
using LinkMe.Framework.Utility.Urls;

namespace LinkMe.Apps.Asp.Mvc.Widgets
{
    public class TooltipIcon
    {
        public string Text { get; set; }

        public override String ToString()
        {
            // MF (2010-10-06): Supports only one image type (the [?]).
            // Derived from the custom control, "TooltipIcon".
            //
            // All pages are set to import TooltipBehaviour.js since it's in very common use.
            //
            Text = Text ?? "";

            var guid = Guid.NewGuid().ToString();
            var imageId = "imgTooltipIcon" + guid;
            var infoDivId = "divTooltip" + guid;

            var sanitisedText = TextUtil.StripExtraWhiteSpace(Text ?? "")
                .Replace("\r", "")
                .Replace("\n", "")
                .Replace("\'", "\\\'");

            return String.Format(
                "<img id=\"{0}\" class=\"helpicon\" src=\"{1}\" onmouseover=\"{2}\" onmouseout=\"{3}\" />",
                new[]
                {
                    imageId,
                    new ReadOnlyApplicationUrl("~/ui/img/help.gif").ToString(),
                    string.Format(
                        "javascript:LinkMeUI.TooltipBehaviour.mouseOver(event, '{0}', '{1}', '{2}');",
                        imageId, infoDivId, sanitisedText),
                    string.Format(
                        "javascript:LinkMeUI.TooltipBehaviour.mouseOut(event, '{0}', '{1}');",
                        imageId, infoDivId)
                }
            );
        }
    }
}
