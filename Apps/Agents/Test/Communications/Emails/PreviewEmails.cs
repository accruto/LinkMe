using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Xml;
using LinkMe.Apps.Agents.Communications.Emails;
using LinkMe.Apps.Agents.Communications.Emails.Commands;
using LinkMe.Apps.Agents.Communications.Emails.CustodianEmails;
using LinkMe.Apps.Agents.Communications.Emails.EmployerEmails;
using LinkMe.Apps.Agents.Communications.Emails.EmployerToMemberNotifications;
using LinkMe.Apps.Agents.Communications.Emails.InternalEmails;
using LinkMe.Apps.Agents.Communications.Emails.MemberAlerts;
using LinkMe.Apps.Agents.Communications.Emails.MemberEmails;
using LinkMe.Apps.Agents.Communications.Emails.MemberToMemberNotifications;
using LinkMe.Apps.Agents.Communications.Emails.UserEmails;
using LinkMe.Apps.Mocks.Hosts;
using LinkMe.Domain.Roles.Affiliations.Communities;
using LinkMe.Domain.Roles.Affiliations.Communities.Commands;
using LinkMe.Domain.Roles.Affiliations.Verticals.Commands;
using LinkMe.Domain.Roles.Test.Communications.Mocks;
using LinkMe.Domain.Test.Data;
using LinkMe.Environment;
using LinkMe.Framework.Content;
using LinkMe.Framework.Utility;
using LinkMe.Framework.Utility.Sql;
using LinkMe.Framework.Utility.Urls;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Apps.Agents.Test.Communications.Emails
{
    [TestClass]
    public class PreviewEmails
        : TestClass
    {
        protected readonly ICommunitiesCommand _communitiesCommand = Resolve<ICommunitiesCommand>();
        protected readonly IVerticalsCommand _verticalsCommand = Resolve<IVerticalsCommand>();
        protected readonly IContentEngine _contentEngine = Resolve<IContentEngine>();

        private class Comparer
            : IComparer<TemplateEmail>
        {
            int IComparer<TemplateEmail>.Compare(TemplateEmail e1, TemplateEmail e2)
            {
                return StringComparer.InvariantCultureIgnoreCase.Compare(e1.Definition, e2.Definition);
            }
        }

        private class CommunityEmails
        {
            public readonly SortedList<TemplateEmail, MockEmail> CustodianEmails = new SortedList<TemplateEmail, MockEmail>(new Comparer());
            public readonly SortedList<TemplateEmail, MockEmail> EmployerEmails = new SortedList<TemplateEmail, MockEmail>(new Comparer());
            public readonly SortedList<TemplateEmail, MockEmail> EmployerToMemberNotifications = new SortedList<TemplateEmail, MockEmail>(new Comparer());
            public readonly SortedList<TemplateEmail, MockEmail> InternalEmails = new SortedList<TemplateEmail, MockEmail>(new Comparer());
            public readonly SortedList<TemplateEmail, MockEmail> MemberAlerts = new SortedList<TemplateEmail, MockEmail>(new Comparer());
            public readonly SortedList<TemplateEmail, MockEmail> MemberEmails = new SortedList<TemplateEmail, MockEmail>(new Comparer());
            public readonly SortedList<TemplateEmail, MockEmail> MemberToMemberNotifications = new SortedList<TemplateEmail, MockEmail>(new Comparer());
            public readonly SortedList<TemplateEmail, MockEmail> RegisteredUserEmails = new SortedList<TemplateEmail, MockEmail>(new Comparer());
            public readonly IList<string> References = new List<string>();
        }

        private readonly SortedList<string, CommunityEmails> _communityEmails = new SortedList<string, CommunityEmails>();
        private IMockEmailServer _emailServer;
        private IEmailsCommand _emailsCommand;

        [TestInitialize]
        public void TestInitialize()
        {
            Resolve<IDbConnectionFactory>().DeleteAllTestData();

            _emailServer = EmailHost.Start();
            _emailServer.ClearEmails();

            _emailsCommand = Resolve<IEmailsCommand>();
        }

        [TestMethod, Ignore]
        public void GeneratePreviews()
        {
            GenerateEmails();
            SaveEmails();
            SaveReferences();
        }

        [TestMethod, Ignore]
        public void CheckPreviews()
        {
            // Find emails that don't have previews.

            var emails = GetEmails();
            var previews = GetPreviews();

            var sb = new StringBuilder();
            var missingEmails = emails.Except(previews).ToList();
            for (var index = 0; index < missingEmails.Count; ++index)
                sb.AppendLine(index + ": " + missingEmails[index]);

            using (var writer = new StreamWriter(Path.Combine(GetEmailsFolder(null), "MissingPreviews.txt")))
            {
                writer.Write(sb.ToString());
                writer.Flush();
                writer.Close();
            }
        }

        private static IEnumerable<string> GetPreviews()
        {
            var folder = FileSystem.GetAbsolutePath(@"Test\Emails", RuntimeEnvironment.GetSourceFolder());
            return Directory.GetFiles(folder, "*.html").Select(Path.GetFileNameWithoutExtension).ToList();
        }

        private static IEnumerable<string> GetEmails()
        {
            var emails = new List<string>();

            // Search through all template files.

            var folder = FileSystem.GetAbsolutePath(@"Apps\Config", RuntimeEnvironment.GetSourceFolder());
            foreach (var file in Directory.GetFiles(folder, "email-template*"))
            {
                var document = new XmlDocument();
                document.Load(file);

                foreach (XmlNode xmlNode in document.SelectNodes("/ContentItems/TemplateContentItems/TemplateContentItem/@Name"))
                {
                    if (!emails.Contains(xmlNode.Value))
                        emails.Add(xmlNode.Value);
                }
            }

            return emails;
        }

        private void SaveReferences()
        {
            foreach (var community in _communityEmails)
                SaveReferences(community.Key, community.Value);
        }

        private static void SaveReferences(string community, CommunityEmails emails)
        {
            foreach (var url in emails.References)
            {
                var request = WebRequest.Create(url);

                using (var response = request.GetResponse())
                {
                    var reader = new BinaryReader(response.GetResponseStream());
                    Save(community, Path.GetFileName(url), reader);
                }
            }
        }

        private void GenerateEmails()
        {
            // Look for all types derived from EmailTest.

            var assembly = Assembly.GetExecutingAssembly();
            foreach (var type in assembly.GetTypes())
            {
                if (type.IsSubclassOf(typeof(EmailTests)) && !type.IsAbstract)
                {
                    var emailTest = Activator.CreateInstance(type) as EmailTests;
                    if (emailTest != null)
                    {
                        emailTest.EmailTestsInitialize();
                        var templateEmail = emailTest.GeneratePreview(null);
                        if (templateEmail != null)
                        {
                            try
                            {
                                _emailsCommand.TrySend(templateEmail);
                                var mockEmail = _emailServer.AssertEmailSent();
                                Add(null, templateEmail, mockEmail);
                            }
                            catch (Exception ex)
                            {
                                throw new ApplicationException("Cannot send a '" + type.FullName + "' templateEmail.", ex);
                            }
                        }
                    }
                }
            }
        }

        private void SaveEmails()
        {
            foreach (var community in _communityEmails)
                SaveEmails(community.Key, community.Value);
        }

        private static void SaveEmails(string community, CommunityEmails emails)
        {
            var sb = new StringBuilder();
            sb.AppendLine("<html>");
            sb.AppendLine("  <body>");
            sb.AppendLine("    <div>");
            sb.AppendLine("      <style type='text/css'>");
            sb.AppendLine("           html, body { font-family:tahoma, sans-serif; }	");
            sb.AppendLine("           .alignRight { text-align:right; }");
            sb.AppendLine("           .alignLeft  { text-align:left; }");
            sb.AppendLine("           .bgGray { background-color: #E5E5E5; }");
            sb.AppendLine("      </style>");

            SaveEmails(sb, "Member emails", community, emails.MemberEmails, emails.References);
            SaveEmails(sb, "Member to member notifications", community, emails.MemberToMemberNotifications, emails.References);
            SaveEmails(sb, "Employer to member notifications", community, emails.EmployerToMemberNotifications, emails.References);
            SaveEmails(sb, "Member alerts", community, emails.MemberAlerts, emails.References);
            SaveEmails(sb, "Registered user emails", community, emails.RegisteredUserEmails, emails.References);
            SaveEmails(sb, "Employer emails", community, emails.EmployerEmails, emails.References);
            SaveEmails(sb, "Custodian emails", community, emails.CustodianEmails, emails.References);
            SaveEmails(sb, "Internal emails", community, emails.InternalEmails, emails.References);

            sb.AppendLine("    </div>");
            sb.AppendLine("  </body>");
            sb.AppendLine("</html>");

            Save(community, "Emails.html", sb.ToString(), emails.References);
        }

        private static void Save(string community, string fileName, string contents, ICollection<string> references)
        {
            var emailsFolder = GetEmailsFolder(community);
            contents = UpdateLinks(contents, references);

            using (var writer = new StreamWriter(Path.Combine(emailsFolder, fileName)))
            {
                writer.Write(contents);
                writer.Flush();
                writer.Close();
            }
        }

        private static void Save(string community, string fileName, BinaryReader reader)
        {
            var emailsFolder = GetEmailsFolder(community);

            using (var file = new FileStream(Path.Combine(emailsFolder, fileName), FileMode.Create))
            {
                using (var binaryWriter = new BinaryWriter(file))
                {
                    var buffer = new byte[1024];
                    var read = reader.Read(buffer, 0, 1024);
                    while (read != 0)
                    {
                        binaryWriter.Write(buffer, 0, read);
                        read = reader.Read(buffer, 0, 1024);
                    }
                }
            }
        }

        private static string GetEmailsFolder(string community)
        {
            var emailsFolder = FileSystem.GetAbsolutePath(@"Test\Emails", RuntimeEnvironment.GetSourceFolder());
            if (!string.IsNullOrEmpty(community))
                emailsFolder = Path.Combine(emailsFolder, community);
            if (!Directory.Exists(emailsFolder))
                Directory.CreateDirectory(emailsFolder);
            return emailsFolder;
        }

        private static void SaveEmails(StringBuilder sb, string title, string community, SortedList<TemplateEmail, MockEmail> emails, ICollection<string> references)
        {
            if (emails.Count > 0)
            {
                sb.AppendLine("      <h2>" + title + "</h2>");
                sb.AppendLine("        <table cellspacing=\"0\" cellpadding=\"6\">");

                for (var index = 0; index < emails.Count; ++index)
                {
                    var templateEmail = emails.Keys[index];
                    var mockEmail = emails.Values[index];

                    if (index%2 == 0)
                        sb.AppendLine("          <tr class=\"bgGray\">");
                    else
                        sb.AppendLine("          <tr>");
                    sb.AppendLine("            <td class=\"alignLeft\">" + templateEmail.Definition + "</td>");

                    if (!string.IsNullOrEmpty(mockEmail.GetHtmlView().Body))
                    {
                        string fileName = templateEmail.Definition + ".html";

                        sb.Append("            <td class=\"alignLeft\">");
                        sb.Append("<a href=\"" + fileName + "\">HTML version</a>");
                        sb.AppendLine("</td>");
                        sb.AppendLine("            <td></td>");

                        Save(community, fileName, mockEmail.GetHtmlView().Body, references);
                    }
                    else
                    {
                        string fileName = templateEmail.Definition + ".html";
                        sb.Append("            <td class=\"alignLeft\">");
                        sb.Append("<a href=\"" + fileName + "\">HTML version</a>");
                        sb.AppendLine("</td>");
                        Save(community, fileName, mockEmail.AlternateViews[0].Body, references);

                        fileName = templateEmail.Definition + ".txt";
                        sb.Append("            <td class=\"alignLeft\">");
                        sb.Append("<a href=\"" + fileName + "\">Text version</a>");
                        sb.AppendLine("</td>");
                        Save(community, fileName, mockEmail.AlternateViews[1].Body, references);
                    }

                    sb.AppendLine("          </tr>");
                }

                sb.AppendLine("        </table>");
            }
        }

        private static string UpdateLinks(string contents, ICollection<string> references)
        {
            string url = new ApplicationUrl("~/").AbsoluteUri + "/Email/";

            var sb = new StringBuilder();
            int pos = contents.IndexOf(url);
            while (pos != -1)
            {
                sb.Append(contents.Substring(0, pos));
                contents = contents.Substring(pos + url.Length);

                // Look for the file name.

                pos = contents.IndexOf("\"");
                string file = contents.Substring(0, pos);

                if (!references.Contains(url + file))
                    references.Add(url + file);

                sb.Append(file);
                contents = contents.Substring(pos);

                pos = contents.IndexOf(url);
            }

            sb.Append(contents);
            return sb.ToString();
        }

        private void Add(Community community, TemplateEmail templateEmail, MockEmail mockEmail)
        {
            CommunityEmails emails;
            _communityEmails.TryGetValue(community == null ? string.Empty : community.Name, out emails);
            if (emails == null)
            {
                emails = new CommunityEmails();
                _communityEmails[community == null ? string.Empty : community.Name] = emails;
            }

            if (templateEmail.GetType().IsSubclassOf(typeof(EmployerToMemberNotification)))
                emails.EmployerToMemberNotifications[templateEmail] = mockEmail;
            else if (templateEmail.GetType().IsSubclassOf(typeof(MemberToMemberNotification)))
                emails.MemberToMemberNotifications[templateEmail] = mockEmail;
            else if (templateEmail.GetType().IsSubclassOf(typeof(MemberAlertEmail)))
                emails.MemberAlerts[templateEmail] = mockEmail;
            else if (templateEmail.GetType().IsSubclassOf(typeof(MemberEmail)))
                emails.MemberEmails[templateEmail] = mockEmail;
            else if (templateEmail.GetType().IsSubclassOf(typeof(EmployerEmail)))
                emails.EmployerEmails[templateEmail] = mockEmail;
            else if (templateEmail.GetType().IsSubclassOf(typeof(CustodianEmail)))
                emails.CustodianEmails[templateEmail] = mockEmail;
            else if (templateEmail.GetType().IsSubclassOf(typeof(UserEmail)))
                emails.RegisteredUserEmails[templateEmail] = mockEmail;
            else if (templateEmail.GetType().IsSubclassOf(typeof(InternalEmail)))
                emails.InternalEmails[templateEmail] = mockEmail;
            else 
                throw new ApplicationException("Non-recognised email type '" + templateEmail.GetType() + "'.");
        }
    }
}