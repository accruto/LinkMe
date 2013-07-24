using System.Text;
using LinkMe.Apps.Agents.Applications;
using LinkMe.Apps.Agents.Applications.Queries;
using LinkMe.Apps.Agents.Communications.Campaigns.Emails;
using LinkMe.Apps.Agents.Communications.Emails;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Communications.Campaigns;
using LinkMe.Domain.Roles.Test.Communications.Mocks;
using LinkMe.Framework.Utility.Unity;

namespace LinkMe.Apps.Agents.Test.Communications.Campaigns.Members
{
    internal static class CampaignsTestHelper
    {
        private static string _rootPath;
        private static IWebSiteQuery _webSiteQuery;

        private const string ReturnEmailAddress = "do_not_reply@test.linkme.net.au";
        private const string ReturnDisplayName = "LinkMe";

        public static void TestInitialize()
        {
            _webSiteQuery = Container.Current.Resolve<IWebSiteQuery>();
            Container.Current.Resolve<ITinyUrlQuery>();

            _rootPath = _webSiteQuery.GetUrl((int)WebSite.LinkMe, null, false, "~/").AbsoluteUri.ToLower();
            MockEmailTestExtensions.RootPath = _rootPath;
        }

        public static CampaignEmail CreateEmail(Campaign campaign, ICommunicationUser to)
        {
            return new MemberCampaignEmail(campaign, null, null, to, new EmailUser(ReturnEmailAddress, ReturnDisplayName, null));
        }

        public static string GetBody(CampaignEmail email, string content)
        {
            var sb = new StringBuilder();
            GetBodyStart(sb);
            sb.Append(content);
            GetBodyEnd(email, sb);
            return sb.ToString();
        }

        private static void GetBodyStart(StringBuilder sb)
        {
            sb.AppendLine();
            sb.AppendLine("<html>");
            sb.AppendLine("  <head>");
            sb.AppendLine("    <link href=\"" + _rootPath + "email/emails.css\" rel=\"stylesheet\" type=\"text/css\" media=\"screen\" />");
            sb.AppendLine("    <link href=\"" + _rootPath + "email/print-emails.css\" rel=\"stylesheet\" type=\"text/css\" media=\"print\" />");
            sb.AppendLine("  </head>");
            sb.AppendLine("  <body style=\"margin: 0px; padding: 0px;\">");
            sb.AppendLine("    <div id=\"letter-layout\" style=\"font-family: Arial, Helvetica, sans-serif; color: #474747;\">");
            sb.Append("      <div class=\"body\" style=\"padding-top: 20px; padding-left: 10px; font-family: Arial, Helvetica, sans-serif; font-size: 10pt;\">");
        }

        private static void GetBodyEnd(TemplateEmail email, StringBuilder sb)
        {
            sb.AppendLine("      </div>");
/*
            sb.AppendLine("      <div class=\"signature\">");
            GetSignature(sb);
            sb.AppendLine("      </div>");
            sb.AppendLine();
            sb.AppendLine("      <div class=\"unsubscribe\">");
            sb.AppendLine("        <p>");
            sb.AppendLine("          Receiving too many emails?");
            sb.AppendLine("          <br />");
            sb.AppendLine("          <a href=\"" + GetTinyUrl(email, true, "~/members/settings") + "\">");
            sb.AppendLine("            Click here");
            sb.AppendLine("          </a>");
            sb.AppendLine("          to log in to manage the amount of emails you receive from us.");
            sb.AppendLine("        </p>");
            sb.AppendLine("        <p>");
            sb.AppendLine("          Alternatively, if you're sure you wish to unsubscribe from this type of");
            sb.AppendLine("          email completely,");
            sb.AppendLine("          <a href=\"" + GetTinyUrl(email, false, "~/accounts/settings/unsubscribe", "userId", email.To.Id.ToString("n"), "category", "Campaign") + "\">");
            sb.AppendLine("            click here");
            sb.AppendLine("          </a>.");
            sb.AppendLine("        </p>");
            sb.AppendLine("      </div>");
            sb.AppendLine();
*/
            sb.AppendLine("    </div>");
            sb.AppendLine("    <img src=\"" + GetTrackingPixelUrl(email) + "\" width=\"1\" height=\"1\" />");
            sb.AppendLine("  </body>");
            sb.AppendLine("</html>");
        }

        private static string GetTrackingPixelUrl(TemplateEmail email)
        {
            var applicationPath = "~/url/" + email.Id.ToString("n") + ".aspx";
            return _webSiteQuery.GetUrl((int)WebSite.LinkMe, email.AffiliateId, false, applicationPath).AbsoluteUri;
        }

        public static CampaignCategory GetCampaignCategory()
        {
            return CampaignCategory.Member;
        }
    }
}