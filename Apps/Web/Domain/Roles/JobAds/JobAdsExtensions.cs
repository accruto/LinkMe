using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using HtmlAgilityPack;
using LinkMe.Apps.Presentation.Domain;
using LinkMe.Apps.Presentation.Domain.Roles.JobAds;
using LinkMe.Domain;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Industries;
using LinkMe.Domain.Roles.JobAds;
using LinkMe.Domain.Users.Anonymous;
using LinkMe.Domain.Users.Members.JobAds;
using LinkMe.Framework.Utility;
using LinkMe.Framework.Utility.Urls;

namespace LinkMe.Web.Domain.Roles.JobAds
{
    public static class JobAdsExtensions
    {
        private static readonly ReadOnlyUrl BaseBrowseJobsUrl = new ReadOnlyApplicationUrl("~/jobs/");
        private const string JobsUrlSuffix = "-jobs";
        private const string OrganisationJobAdCssPath = "~/Content/Organisations/Css/JobAds/";
        private static Dictionary<Guid, string> _organisationJobAdCssFiles;

        public static ReadOnlyUrl GetBrowseJobsUrl(this Industry industry)
        {
            return new ReadOnlyApplicationUrl(BaseBrowseJobsUrl, "-/" + industry.UrlName + JobsUrlSuffix);
        }

        public static IList<JobTypes> GetOrderedJobTypes(this JobTypes jobTypes)
        {
            return jobTypes.GetFlagged(JobTypes.FullTime, JobTypes.PartTime, JobTypes.Contract, JobTypes.Temp, JobTypes.JobShare);
        }

        public static IList<JobTypes> GetOrderedJobTypes(this JobTypes jobTypes, JobTypes otherJobTypes)
        {
            return jobTypes.GetOrderedJobTypes().Intersect(otherJobTypes.GetOrderedJobTypes()).ToList();
        }

        public static string GetPageTitle(this JobAdView jobAd)
        {
            if (jobAd == null)
                return string.Empty;

            var sb = new StringBuilder();
            if (!string.IsNullOrEmpty(jobAd.Description.PositionTitle))
                sb.AppendFormat("{0} Job | ", jobAd.Description.PositionTitle);
            else
                Append(sb, jobAd.Title);

            if (!string.IsNullOrEmpty(jobAd.GetLocationDisplayText()))
                sb.AppendFormat("{0} | ", jobAd.GetLocationDisplayText());

            if (jobAd.Description.Industries != null && jobAd.Description.Industries.Count > 0)
            {
                foreach (var industry in jobAd.Description.Industries)
                    sb.AppendFormat("{0} | ", industry.Name);
            }

            if (jobAd.Description.Salary != null)
                Append(sb, jobAd.Description.Salary.GetDisplayText());

            sb.Replace('\r', ' ').Replace('\n', ' '); // Remove linebreaks which show up as "<br />"
            sb.Append("Jobs | Career | Resumes");
            return sb.ToString();
        }

        public static string GetOrganisationJobAdCssFile(this HttpContextBase context, Guid organisationId)
        {
            if (_organisationJobAdCssFiles == null)
                _organisationJobAdCssFiles = GetOrganisationJobAdCssFiles(context);

            string cssFile;
            if (_organisationJobAdCssFiles.TryGetValue(organisationId, out cssFile))
                return OrganisationJobAdCssPath + cssFile;

            return null;
        }

        public static string GetDefaultCoverLetter(this JobAdEntry jobAd, IMember member)
        {
            var phoneNumber = member.GetBestPhoneNumber();
            return GetDefaultCoverLetter(jobAd.Title, jobAd.Integration.ExternalReferenceId, member.FullName, member.GetBestEmailAddress().Address, phoneNumber == null ? null : phoneNumber.Number);
        }

        public static string GetDefaultCoverLetter(this JobAdView jobAd, IMember member)
        {
            var phoneNumber = member.GetBestPhoneNumber();
            return GetDefaultCoverLetter(jobAd.Title, jobAd.Integration.ExternalReferenceId, member.FullName, member.GetBestEmailAddress().Address, phoneNumber == null ? null : phoneNumber.Number);
        }

        public static string GetDefaultCoverLetter(this JobAdEntry jobAd, AnonymousContact contact)
        {
            return GetDefaultCoverLetter(jobAd.Title, jobAd.Integration.ExternalReferenceId, contact.FullName, contact.EmailAddress, null);
        }

        public static string GetDefaultCoverLetter(this JobAdView jobAd, AnonymousContact contact)
        {
            return GetDefaultCoverLetter(jobAd.Title, jobAd.Integration.ExternalReferenceId, contact.FullName, contact.EmailAddress, null);
        }

        public static bool IsNew(this JobAdEntry jobAd)
        {
            return jobAd.CreatedTime > DateTime.Now.AddDays(-2);
        }

        public static bool IsNew(this JobAdView jobAd)
        {
            return jobAd.CreatedTime > DateTime.Now.AddDays(-2);
        }

        public static string GetContentDisplayText(this string content)
        {
            return HtmlUtil.StripHtmlTags(HtmlUtil.HtmlLineBreakToText(HtmlUtil.StripHtmlComments(content)));
        }

        public static string GetContentDisplayHtml(this string content, bool isSecureConnection)
        {
            content = HtmlUtil.AposToHtml(HtmlUtil.LineBreaksToHtml(HtmlUtil.StripHtmlComments(content)));
            content = isSecureConnection
                ? ReplaceScheme(content, "http:", "https:")
                : ReplaceScheme(content, "https:", "http:");
            return content;
        }

        public static int GetDaysLeft(this DateTime expiryTime)
        {
            return (expiryTime - DateTime.Now.Date.AddDays(1).AddSeconds(-1)).Days;
        }

        public static string GetDaysLeftDisplayText(this int days)
        {
            switch (days)
            {
                case 0:
                    return "today";

                case 1:
                    return "in " + days + " day";

                default:
                    return "in " + days + " days";
            }
        }

        public static string GetDaysLeftDisplayText(this DateTime expiryTime)
        {
            return GetDaysLeft(expiryTime).GetDaysLeftDisplayText();
        }

        private static string ReplaceScheme(string content, string currentScheme, string newScheme)
        {
            // Do a string search first just to make sure there is something to replace.

            if (!content.Contains(currentScheme))
                return content;

            // Need to replace.

            var doc = new HtmlDocument();
            doc.LoadHtml(content);

            // Update images.

            var nodes = doc.DocumentNode.SelectNodes("//img");
            if (nodes != null)
            {
                foreach (var node in nodes)
                {
                    var src = node.Attributes["src"];
                    if (src != null && src.Value.StartsWith(currentScheme))
                        src.Value = newScheme + src.Value.Substring(currentScheme.Length);
                }
            }

            return doc.DocumentNode.OuterHtml;
        }

        private static string GetDefaultCoverLetter(string title, string externalReferenceId, string fullName, string emailAddress, string phoneNumber)
        {
            var sb = new StringBuilder();

            sb.Append("Dear Sir or Madam,");
            sb.AppendFormat("\n\nI would like to apply for the position you advertised on LinkMe for \"{0}\"", title);

            if (!string.IsNullOrEmpty(externalReferenceId))
                sb.AppendFormat(" (ref# {0})", externalReferenceId);
            sb.Append(".\n\n");

            if (!string.IsNullOrEmpty(phoneNumber))
                sb.AppendFormat("I can be contacted at {0} at your convenience.\n\n", emailAddress);
            else
                sb.AppendFormat("I can be contacted on {0} or {1} at your convenience.\n\n", phoneNumber, emailAddress);

            sb.Append("Thanks,\n\n");
            sb.AppendFormat("{0}.", fullName);

            return sb.ToString();
        }

        private static Dictionary<Guid, string> GetOrganisationJobAdCssFiles(HttpContextBase context)
        {
            var directory = context.Server.MapPath(OrganisationJobAdCssPath);
            if (!Directory.Exists(directory))
                throw new DirectoryNotFoundException("The company CSS directory, '" + directory + "', does not exist.");

            var dictionary = new Dictionary<Guid, string>();
            foreach (var cssFile in Directory.GetFiles(directory, "*.css", SearchOption.TopDirectoryOnly))
            {
                try
                {
                    var fileName = Path.GetFileName(cssFile);
                    var organisationIds = GetOrganisationIds(fileName);
                    foreach (var organisationId in organisationIds)
                        dictionary.Add(organisationId, fileName);
                }
                catch (Exception ex)
                {
                    throw new ApplicationException("Failed to process CSS file '" + cssFile + "'.", ex);
                }
            }

            return dictionary;
        }

        private static IList<Guid> GetOrganisationIds(string fileName)
        {
            fileName = Path.GetFileNameWithoutExtension(fileName);
            var parts = fileName.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            var ids = new List<Guid>();

            foreach (var part in parts)
            {
                var toParse = part;
                var lastSpace = toParse.LastIndexOf(' ');
                if (lastSpace != -1)
                    toParse = toParse.Substring(lastSpace + 1);
                try
                {
                    ids.Add(new Guid(toParse.Trim()));
                }
                catch (FormatException)
                {
                    // Not a GUID - fine.
                }
            }

            return ids;
        }

        private static void Append(StringBuilder sb, string text)
        {
            if (!string.IsNullOrEmpty(text))
                sb.AppendFormat("{0} | ", text);
        }
    }
}
