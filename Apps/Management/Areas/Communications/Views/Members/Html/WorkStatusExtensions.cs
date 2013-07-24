using System.Text;
using System.Web.Mvc;
using LinkMe.Framework.Utility;
using LinkMe.Utility.Utilities;

namespace LinkMe.Apps.Management.Areas.Communications.Views.Members.Html
{
    public static class WorkStatusExtensions
    {
        public static string ActivelyLookingDescription(this HtmlHelper htmlHelper, string desiredJobTitle, string url)
        {
            var sb = new StringBuilder();
            sb.Append("You are looking for work");
            if (string.IsNullOrEmpty(desiredJobTitle))
                sb.Append(". ").AppendDreamJob(url);
            else
                sb.AppendDesiredJobTitle(desiredJobTitle).Append(".");
            return sb.ToString();
        }

        public static string NotLookingDescription(this HtmlHelper htmlHelper)
        {
            return "You are not currently looking for work.";
        }

        public static string UnspecifiedDescription(this HtmlHelper htmlHelper, string url)
        {
            var sb = new StringBuilder();
            sb.Append("You have not specified a work status. ").AppendUpdateStatus(url);
            return sb.ToString();
        }

        public static string OpenToOffersDescription(this HtmlHelper htmlHelper, string desiredJobTitle, string url)
        {
            var sb = new StringBuilder();
            sb.Append("You are not looking but are happy to discuss new opportunities");
            if (string.IsNullOrEmpty(desiredJobTitle))
                sb.Append(". ").AppendDreamJob(url);
            else
                sb.AppendDesiredJobTitle(desiredJobTitle);
            return sb.ToString();
        }

        private static void AppendUpdateStatus(this StringBuilder sb, string url)
        {
            sb.Append("<a href=\"")
                .Append(url)
                .Append("\">Let employers know</a> if you're looking for work or just happy to talk.");
            return;
        }

        private static void AppendDreamJob(this StringBuilder sb, string url)
        {
            sb.Append("Let employers know about <a href=\"")
                .Append(url)
                .Append("\">your dream job</a>.");
            return;
        }

        private static StringBuilder AppendDesiredJobTitle(this StringBuilder sb, string desiredJobTitle)
        {
            return sb.Append(" as ")
                .Append(StringUtils.StartsWithAVowel(desiredJobTitle) ? "an" : "a")
                .Append(" ")
                .Append(HtmlUtil.TextToHtml(desiredJobTitle))
                .Append(".");
        }
    }
}