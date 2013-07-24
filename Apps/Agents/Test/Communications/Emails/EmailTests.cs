using System;
using System.Text;
using LinkMe.Apps.Agents.Applications;
using LinkMe.Apps.Agents.Applications.Queries;
using LinkMe.Apps.Agents.Communications.Emails;
using LinkMe.Apps.Agents.Communications.Emails.Commands;
using LinkMe.Apps.Agents.Communications.Emails.Queries;
using LinkMe.Apps.Agents.Test.Users.Administrators;
using LinkMe.Apps.Agents.Test.Users.Employers;
using LinkMe.Apps.Agents.Test.Users.Members;
using LinkMe.Apps.Agents.Users.Administrators.Commands;
using LinkMe.Apps.Agents.Users.Employers.Commands;
using LinkMe.Apps.Agents.Users.Members.Commands;
using LinkMe.Apps.Mocks.Hosts;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Location;
using LinkMe.Domain.Location.Queries;
using LinkMe.Domain.Roles.Affiliations.Communities;
using LinkMe.Domain.Roles.Affiliations.Communities.Commands;
using LinkMe.Domain.Roles.Affiliations.Verticals.Commands;
using LinkMe.Domain.Roles.Communications.Settings.Commands;
using LinkMe.Domain.Roles.Communications.Settings.Queries;
using LinkMe.Domain.Roles.Recruiters.Commands;
using LinkMe.Domain.Roles.Test.Communications.Mocks;
using LinkMe.Domain.Roles.Test.Recruiters;
using LinkMe.Domain.Test.Data;
using LinkMe.Domain.Users.Anonymous;
using LinkMe.Domain.Users.Anonymous.Commands;
using LinkMe.Domain.Users.Members.Affiliations.Commands;
using LinkMe.Framework.Communications;
using LinkMe.Framework.Content;
using LinkMe.Framework.Utility.Sql;
using LinkMe.Framework.Utility.Urls;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Apps.Agents.Test.Communications.Emails
{
    [TestClass]
    public abstract class EmailTests
        : TestClass
    {
        private const string ReturnEmailAddress = "do_not_reply@test.linkme.net.au";
        private const string ReturnDisplayName = "LinkMe";
        private const string MemberServicesInboxEmailAddress = "msinbox@test.linkme.net.au";
        private const string MemberServicesInboxDisplayName = "LinkMe";
        private const string ClientServicesInboxEmailAddress = "csinbox@test.linkme.net.au";
        private const string ClientServicesInboxDisplayName = "LinkMe";
        private const string SystemEmailAddress = "system@test.linkme.net.au";
        private const string SystemDisplayName = "LinkMe";
        private const string AllStaffEmailAddress = "allstaff@test.linkme.net.au";
        private const string AllStaffDisplayName = "LinkMe Staff";
        private const string RedStarResumeEmailAddress = "redstarresume@test.linkme.net.au";
        private const string RedStarResumeDisplayName = "RedStar Resume";

        protected readonly static EmailRecipient Return = new EmailRecipient(ReturnEmailAddress, ReturnDisplayName);
        protected readonly static EmailRecipient MemberServicesInbox = new EmailRecipient(MemberServicesInboxEmailAddress, MemberServicesInboxDisplayName);
        protected readonly static EmailRecipient ClientServicesInbox = new EmailRecipient(ClientServicesInboxEmailAddress, ClientServicesInboxDisplayName);
        protected readonly static EmailRecipient System = new EmailRecipient(SystemEmailAddress, SystemDisplayName);
        protected readonly static EmailRecipient AllStaff = new EmailRecipient(AllStaffEmailAddress, AllStaffDisplayName);
        protected readonly static EmailRecipient RedStarResume = new EmailRecipient(RedStarResumeEmailAddress, RedStarResumeDisplayName);

        protected readonly ILocationQuery _locationQuery = Resolve<ILocationQuery>();
        protected readonly ICommunitiesCommand _communitiesCommand = Resolve<ICommunitiesCommand>();
        protected readonly IMemberAffiliationsCommand _memberAffiliationsCommand = Resolve<IMemberAffiliationsCommand>();
        protected readonly IVerticalsCommand _verticalsCommand = Resolve<IVerticalsCommand>();
        protected readonly IContentEngine _contentEngine = Resolve<IContentEngine>();

        protected IMockEmailServer _emailServer;
        protected IEmailsCommand _emailsCommand = Resolve<IEmailsCommand>();
        protected ISettingsQuery _settingsQuery = Resolve<ISettingsQuery>();
        protected ISettingsCommand _settingsCommand = Resolve<ISettingsCommand>();
        protected ITinyUrlQuery _tinyUrlQuery = Resolve<ITinyUrlQuery>();
        protected IWebSiteQuery _webSiteQuery = Resolve<IWebSiteQuery>();
        protected IAffiliateEmailsQuery _affiliateEmailsQuery = Resolve<IAffiliateEmailsQuery>();
        protected readonly IMemberAccountsCommand _memberAccountsCommand = Resolve<IMemberAccountsCommand>();
        private readonly IAnonymousUsersCommand _anonymousUsersCommand = Resolve<IAnonymousUsersCommand>();
        private readonly IEmployerAccountsCommand _employerAccountsCommand = Resolve<IEmployerAccountsCommand>();
        private readonly IOrganisationsCommand _organisationsCommand = Resolve<IOrganisationsCommand>();
        private readonly IAdministratorAccountsCommand _administratorAccountsCommand = Resolve<IAdministratorAccountsCommand>();

        protected Country _australia;
        protected Country _notAustralia;

        private const string EmailAddress = "barney@test.linkme.net.au";
        private const string FirstName = "Barney";
        private const string LastName = "Gumble";
        private const string OtherEmailAddress = "homer@test.linkme.net.au";
        private const string OtherFirstName = "Homer";
        private const string OtherLastName = "Simpson";
        private const string EmployerLoginId = "mburns";
        private const string EmployerEmailAddress = "monty@test.linkme.net.au";
        private const string EmployerFirstName = "Monty";
        private const string EmployerLastName = "Burns";
        private const string OrganisationName = "Acme";
        private const string AdministratorLoginId = "admin";
        private const string AdministratorFirstName = "Waylon";
        private const string AdministratorLastName = "Smithers";

        [TestInitialize]
        public void EmailTestsInitialize()
        {
            Resolve<IDbConnectionFactory>().DeleteAllTestData();
            _emailServer = EmailHost.Start();
            _emailServer.ClearEmails();

            InsecureRootPath = _webSiteQuery.GetUrl(WebSite.LinkMe, null, false, "~/").AbsoluteUri.ToLower();
            SecureRootPath = _webSiteQuery.GetUrl(WebSite.LinkMe, null, true, "~/").AbsoluteUri.ToLower();

            MockEmailTestExtensions.RootPath = InsecureRootPath;

            _australia = _locationQuery.GetCountry("Australia");
            _notAustralia = _locationQuery.GetCountry("New Zealand");
        }

        public abstract TemplateEmail GeneratePreview(Community community);

        protected static string InsecureRootPath { get; private set; }

        protected static string SecureRootPath { get; private set; }

        protected AnonymousContact CreateAnonymousContact()
        {
            var contactDetails = new ContactDetails
            {
                EmailAddress = EmailAddress,
                FirstName = FirstName,
                LastName = LastName,
            };
            return _anonymousUsersCommand.CreateContact(new AnonymousUser(), contactDetails);
        }

        protected Member CreateMember()
        {
            return _memberAccountsCommand.CreateTestMember(EmailAddress, FirstName, LastName);
        }

        protected Member CreateMember(int index)
        {
            return _memberAccountsCommand.CreateTestMember(index + EmailAddress, FirstName, LastName);
        }

        protected Member CreateMember(int index, DateTime createTime)
        {
            return _memberAccountsCommand.CreateTestMember(index + EmailAddress, FirstName, LastName, createTime);
        }

        protected Member CreateMember(Community community)
        {
            return _memberAccountsCommand.CreateTestMember(EmailAddress, FirstName, LastName, community != null ? community.Id : (Guid?)null);
        }

        protected Member CreateOtherMember()
        {
            return _memberAccountsCommand.CreateTestMember(OtherEmailAddress, OtherFirstName, OtherLastName);
        }

        protected Employer CreateEmployer()
        {
            return _employerAccountsCommand.CreateTestEmployer(EmployerLoginId, EmployerFirstName, EmployerLastName, EmployerEmailAddress, _organisationsCommand.CreateTestVerifiedOrganisation(OrganisationName));
        }

        protected Employer CreateEmployer(int index)
        {
            return _employerAccountsCommand.CreateTestEmployer(EmployerLoginId + index, EmployerFirstName, EmployerLastName, EmployerEmailAddress, _organisationsCommand.CreateTestOrganisation(OrganisationName));
        }

        protected Administrator CreateAdministrator()
        {
            return _administratorAccountsCommand.CreateTestAdministrator(AdministratorLoginId, AdministratorFirstName, AdministratorLastName);
        }

        protected static void AssertCompatibleAddresses(MockEmail email)
        {
            // Should not be being sent to the same address.

            if (email.To.Contains(email.From))
                Assert.Fail("Email being sent to the same address it is from.");

            // Based upon who the message is from check who it is going to.

            if (email.From.Equals(Return))
            {
                // Should be an external address.

                if (email.To.Contains(Return)
                    || email.To.Contains(System))
                {
                    Assert.Fail("Email being sent from do_not_reply@linkme.com.au is being sent to an internal address.");
                }
            }
            else if (email.From.Equals(System) || email.From.Equals(AllStaff))
            {
                // No email should be coming from these addresses.

                Assert.Fail("Email is being sent from in incorrect address");
            }
        }

        protected string GetBody(TemplateEmail email, ICommunicationUser to, string content)
        {
            var sb = new StringBuilder();
            GetBodyStart(sb);
            sb.Append(content);
            GetBodyEnd(email, to, sb);
            return sb.ToString();
        }

        protected string GetBody(TemplateEmail email, string content)
        {
            return GetBody(email, null, content);
        }

        protected static void GetBodyStart(StringBuilder sb)
        {
            sb.AppendLine();
            sb.AppendLine("<html>");
            sb.AppendLine("  <head>");
            sb.AppendLine("    <link href=\"" + InsecureRootPath + "email/emails.css\" rel=\"stylesheet\" type=\"text/css\" media=\"screen\" />");
            sb.AppendLine("    <link href=\"" + InsecureRootPath + "email/print-emails.css\" rel=\"stylesheet\" type=\"text/css\" media=\"print\" />");
            sb.AppendLine("  </head>");
            sb.AppendLine("  <body style=\"margin: 0px; padding: 0px;\">");
            sb.AppendLine("    <div id=\"letter-layout\" style=\"font-family: Arial, Helvetica, sans-serif; color: #474747;\">");
            sb.AppendLine("      <div class=\"body\" style=\"padding-top: 20px; padding-left: 10px; font-family: Arial, Helvetica, sans-serif; font-size: 10pt;\">");
        }

        protected void GetBodyEnd(TemplateEmail email, ICommunicationUser to, StringBuilder sb)
        {
            sb.AppendLine("      </div>");

            if (to != null)
            {
                if (to is Member)
                {
                    sb.AppendLine("      <div class=\"signature\" style=\"padding-left: 9px; font-family: Arial, Helvetica, sans-serif; color: gray; font-size: 9pt; padding-top:1em;\">");
                    GetMemberSignature(email, sb);
                    sb.AppendLine("      </div>");
                    sb.AppendLine();
                }
                else if (to is IEmployer)
                {
                    sb.AppendLine("      <div class=\"signature\" style=\"padding-left: 9px; font-family: Arial, Helvetica, sans-serif; color: gray; font-size: 9pt; padding-top:1em;\">");
                    GetClientSignature(email, sb);
                    sb.AppendLine("      </div>");
                }
                else if (to is Custodian)
                {
                    sb.AppendLine("      <div class=\"signature\" style=\"padding-left: 9px; font-family: Arial, Helvetica, sans-serif; color: gray; font-size: 9pt; padding-top:1em;\">");
                    GetCommunitySignature(sb);
                    sb.AppendLine("      </div>");
                }
            }

            string category = null;
            var definition = _settingsQuery.GetDefinition(email.Definition);
            if (definition != null)
                category = _settingsQuery.GetCategory(definition.CategoryId).Name;

            if (category != null && to is Member && to.FirstName != null)
            {
                sb.AppendLine("      <div class=\"unsubscribe\" style=\"padding-left: 9px; font-family: Arial, Helvetica, sans-serif; color: gray; font-size: 8pt; padding-top:1em; margin-left: 20px;\">");
                sb.AppendLine("        <p>");
                sb.AppendLine("          Receiving too many emails?");
                sb.AppendLine("          <br />");
                sb.AppendLine("          <a href=\"" + GetTinyUrl(email, true, "~/members/settings") + "\">");
                sb.AppendLine("            Log in");
                sb.AppendLine("          </a>");
                sb.AppendLine("          to choose the emails you receive from us.");
                sb.AppendLine("        </p>");
                sb.AppendLine("        <p>");
                sb.AppendLine("          Alternatively, you can");
                sb.AppendLine("          <a href=\"" + GetTinyUrl(email, false, "~/accounts/settings/unsubscribe", "userId", to.Id.ToString("n"), "category", category) + "\">");
                sb.AppendLine("            unsubscribe");
                sb.AppendLine("          </a>");
                sb.AppendLine("          from this type of email entirely.");
                sb.AppendLine("        </p>");
                sb.AppendLine("      </div>");
                sb.AppendLine();
            }

            sb.AppendLine("    </div>");
            sb.AppendLine("    <img src=\"" + GetTrackingPixelUrl(email) + "\" width=\"1\" height=\"1\" />");
            sb.AppendLine("  </body>");
            sb.AppendLine("</html>");
        }

        protected void GetBodyEnd(TemplateEmail email, StringBuilder sb)
        {
            sb.AppendLine("      </div>");
            sb.AppendLine("    </div>");
            sb.AppendLine("    <img src=\"" + GetTrackingPixelUrl(email) + "\" width=\"1\" height=\"1\" />");
            sb.AppendLine("  </body>");
            sb.AppendLine("</html>");
        }

        protected virtual void GetMemberSignature(TemplateEmail email, StringBuilder sb)
        {
            sb.AppendLine("        <br />Thanks,");
            sb.AppendLine("        <br />Your LinkMe team");
            sb.AppendLine("        <br />");
            sb.AppendLine("        <br />For support please");
            sb.AppendLine("        <a href=\"" + GetTinyUrl(email, false, "~/contactus") + "\">contact us</a>.");
            sb.AppendLine("        <p><img src=\"" + InsecureRootPath + "email/logo-for-emails.gif\" alt=\"\" /></p>");
        }

        protected virtual void GetClientSignature(TemplateEmail email, StringBuilder sb)
        {
            sb.AppendLine("        <br />Thanks,");
            sb.AppendLine("        <br />Your LinkMe team");
            sb.AppendLine("        <br />");
            sb.AppendLine("        <br />For support please");
            sb.AppendLine("        <a href=\"" + GetTinyUrl(email, false, "~/contactus") + "\">contact us</a>.");
            sb.AppendLine("        <p><img src=\"" + InsecureRootPath + "email/logo-for-emails.gif\" alt=\"\" /></p>");
        }

        protected virtual void GetCommunitySignature(StringBuilder sb)
        {
            sb.AppendLine("        <br />Thanks,");
            sb.AppendLine("        <br />Your LinkMe team");
            sb.AppendLine("        <br />");
            sb.AppendLine("        <p><img src=\"" + InsecureRootPath + "email/logo-for-emails.gif\" alt=\"\" /></p>");
        }

        protected string GetPlainBody(TemplateEmail email, string content)
        {
            return GetPlainBody(email, true, content);
        }

        protected string GetPlainBody(TemplateEmail email, bool includeSignature, string content)
        {
            var sb = new StringBuilder();
            GetPlainBodyStart(sb);
            sb.Append(content);
            GetPlainBodyEnd(email, includeSignature, sb);
            return sb.ToString();
        }

        protected static void GetPlainBodyStart(StringBuilder sb)
        {
            sb.AppendLine();
        }

        protected void GetPlainBodyEnd(TemplateEmail email, bool includeSignature, StringBuilder sb)
        {
            if (includeSignature)
                GetPlainMemberSignature(email, sb);
        }

        protected virtual void GetPlainMemberSignature(TemplateEmail email, StringBuilder sb)
        {
            sb.AppendLine("Thanks,");
            sb.AppendLine("Your LinkMe team");
            sb.AppendLine();
            sb.AppendLine("For support please contact us here");
            sb.AppendLine(GetTinyUrl(email, "text/plain", false, "~/contactus"));
        }

        protected static string GetTrackingPixelUrl(TemplateEmail email)
        {
            var applicationPath = "~/url/" + email.Id.ToString("n") + ".aspx";
            // return _webSiteQuery.GetUrl(WebSite.LinkMe, email.Vertical, false, applicationPath).AbsoluteUri;
            return new ApplicationUrl(applicationPath).AbsoluteUri;
        }

        protected string GetTinyUrl(TemplateEmail email, string mimeType, int instance, bool secure, string applicationPath, QueryString queryString)
        {
            var url = new ApplicationUrl(applicationPath, queryString);
            var affiliateId = _affiliateEmailsQuery.GetAffiliateId(email);
            var mappings = _tinyUrlQuery.GetMappings(WebSite.LinkMe, affiliateId, secure, url.IsAbsolute ? url.AbsoluteUri : "~" + url.AppRelativePathAndQuery, email.Id, mimeType, instance);
            return mappings.Count == 0 ? null : _webSiteQuery.GetUrl(WebSite.LinkMe, affiliateId, false, "~/url/" + mappings[0].TinyId.ToString("n")).AbsoluteUri;
        }

        protected string GetTinyUrl(TemplateEmail email, bool secure, string applicationPath, QueryString queryString)
        {
            return GetTinyUrl(email, "text/html", 0, secure, applicationPath, queryString);
        }
        
        protected string GetTinyUrl(TemplateEmail email, int instance, bool secure, string applicationPath, params string[] queryString)
        {
            return GetTinyUrl(email, "text/html", instance, secure, applicationPath, new QueryString(queryString));
        }

        protected string GetTinyUrl(TemplateEmail email, bool secure, string applicationPath, params string[] queryString)
        {
            return GetTinyUrl(email, "text/html", 0, secure, applicationPath, new QueryString(queryString));
        }

        protected string GetTinyUrl(TemplateEmail email, string mimeType, bool secure, string applicationPath, params string[] queryString)
        {
            return GetTinyUrl(email, mimeType, 0, secure, applicationPath, new QueryString(queryString));
        }
    }
}