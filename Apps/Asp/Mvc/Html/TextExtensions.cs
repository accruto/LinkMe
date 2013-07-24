using System.Web.Mvc;
using LinkMe.Framework.Utility;

namespace LinkMe.Apps.Asp.Mvc.Html
{
    public static class TextExtensions
    {
        #region ASP.NET MVC

        public static string TruncateForDisplay(this HtmlHelper helper, string text, int maxLength)
        {
            return TextUtil.TruncateForDisplay(helper.Encode(text), maxLength);
        }

        #endregion

        #region ASP.NET Web Forms

        public static string TruncateForDisplay(this HtmlUtilsHelper helper, string text, int maxLength)
        {
            return TextUtil.TruncateForDisplay(HtmlUtil.TextToHtml(text), maxLength);
        }

        #endregion
    }
}
