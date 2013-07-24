using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Xml;
using System.Xml.Serialization;
using LinkMe.Apps.Agents.Security.Queries;
using LinkMe.Apps.Presentation.Errors;
using LinkMe.Apps.Services.JobAds;
using LinkMe.Apps.Services.JobAds.Commands;
using LinkMe.Apps.Services.JobAds.Queries;
using LinkMe.Apps.Services.Security;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Industries.Queries;
using LinkMe.Domain.Location.Queries;
using LinkMe.Domain.Roles.Integration;
using LinkMe.Domain.Roles.JobAds;
using LinkMe.Domain.Roles.JobAds.Commands;
using LinkMe.Domain.Roles.JobAds.Queries;
using LinkMe.Domain.Users.Employers.Credits.Queries;
using LinkMe.Domain.Users.Employers.JobAds.Commands;
using LinkMe.Domain.Users.Employers.Queries;
using LinkMe.Domain.Users.Members.JobAds.Queries;
using LinkMe.Framework.Instrumentation;
using LinkMe.Framework.Utility;
using LinkMe.Framework.Utility.Exceptions;
using LinkMe.Query.Reports.Roles.Integration;
using LinkMe.Query.Reports.Roles.Integration.Commands;
using LinkMe.Query.Search.JobAds.Commands;
using LinkMe.Query.Search.JobAds.Queries;
using LinkMe.Utility;
using LinkMe.Web.Areas.Integration.Models.JobAds;

namespace LinkMe.Web.Areas.Integration.Controllers
{
    public class JobAdsController
        : IntegrationController
    {
        private static readonly EventSource EventSource = new EventSource<JobAdsController>();
        private readonly IJobAdFeedsManager _jobAdFeedsManager;
        private readonly IJobAdPostsManager _jobAdPostsManager;
        private readonly IJobAdIntegrationReportsCommand _jobAdIntegrationReportsCommand;
        private readonly IIndustriesQuery _industriesQuery;
        private readonly IServiceAuthenticationManager _serviceAuthenticationManager;
        private readonly IEmployersQuery _employersQuery;
        private readonly IExternalJobAdsQuery _externalJobAdsQuery;
        private readonly ILoginCredentialsQuery _loginCredentialsQuery;
        private readonly ILocationQuery _locationQuery;

        private readonly IJobAdSearchesQuery _jobAdSearchesQuery;

        protected const string XmlNamespacePrefix = "lm";
        protected const string XmlNamespace = "http://www.linkme.com.au/";
        private static readonly XmlSerializer Serializer = new XmlSerializer(typeof(JobAdElement), null, new Type[] { }, null, Apps.Services.Constants.XmlSerializationNamespace);

        private class XmlFragmentWriter
            : XmlTextWriter
        {
            public XmlFragmentWriter(TextWriter writer)
                : base(writer)
            {
            }

            public override void WriteStartDocument()
            {
            }
        }

        public JobAdsController(IEmployerJobAdsCommand employerJobAdsCommand, IJobAdsCommand jobAdsCommand, IJobAdsQuery jobAdsQuery, IMemberJobAdViewsQuery memberJobAdViewsQuery, IJobAdIntegrationQuery jobAdIntegrationQuery, IExternalJobAdsCommand externalJobAdsCommand, IExternalJobAdsQuery externalJobAdsQuery, IEmployersQuery employersQuery, IEmployerCreditsQuery employerCreditsQuery, ILoginCredentialsQuery loginCredentialsQuery, IIndustriesQuery industriesQuery, ILocationQuery locationQuery, IServiceAuthenticationManager serviceAuthenticationManager, IJobAdSearchesQuery jobAdSearchesQuery, IExecuteJobAdSearchCommand executeJobAdSearchCommand, IJobAdIntegrationReportsCommand jobAdIntegrationReportsCommand)
        {
            _jobAdFeedsManager = new JobAdFeedsManager(memberJobAdViewsQuery, jobAdIntegrationQuery, employersQuery, employerCreditsQuery, executeJobAdSearchCommand);
            _jobAdPostsManager = new JobAdPostsManager(employerJobAdsCommand, jobAdsCommand, jobAdsQuery, jobAdIntegrationQuery, externalJobAdsCommand);
            _employersQuery = employersQuery;
            _externalJobAdsQuery = externalJobAdsQuery;
            _loginCredentialsQuery = loginCredentialsQuery;
            _jobAdIntegrationReportsCommand = jobAdIntegrationReportsCommand;
            _industriesQuery = industriesQuery;
            _serviceAuthenticationManager = serviceAuthenticationManager;
            _jobAdSearchesQuery = jobAdSearchesQuery;
            _locationQuery = locationQuery;
        }

        public ActionResult JobAds(string industries, string modifiedSince, string modifiedInLastDays)
        {
            const string method = "JobAds";

            IntegratorUser user = null;

            try
            {
                // Authenticate.

                user = _serviceAuthenticationManager.AuthenticateRequest(HttpContext, IntegratorPermissions.GetJobAds);

                // Attempt to get saved searches for this integrator user. In this context saved searches act as the basis for the feed

                var searches = _jobAdSearchesQuery.GetJobAdSearches(user.Id);

                // Process.

                var industryIds = GetIndustryIds(industries);
                var modifiedDate = GetModifiedSince(modifiedSince, modifiedInLastDays);

                var jobAds = (searches != null && searches.Count > 0)
                    ? _jobAdFeedsManager.GetJobAdFeed(from s in searches select s.Criteria, industryIds, modifiedDate)
                    : _jobAdFeedsManager.GetJobAdFeed(industryIds, modifiedDate);

                // Record it.

                _jobAdIntegrationReportsCommand.CreateJobAdIntegrationEvent(new JobAdExportFeedEvent { IntegratorUserId = user.Id, Success = true, JobAds = jobAds.Count });

                return Xml(new JobAdsResponse(GetResponseXml(jobAds)));
            }
            catch (Exception ex)
            {
                EventSource.Raise(Event.Error, method, "Cannot process the request.", ex, new StandardErrorHandler());

                if (user != null)
                    _jobAdIntegrationReportsCommand.CreateJobAdIntegrationEvent(new JobAdExportFeedEvent { IntegratorUserId = user.Id, Success = false, JobAds = 0 });

                return Xml(new JobAdsResponse(ex));
            }
        }

        public ActionResult JobAdIds()
        {
            const string method = "JobAdIds";

            IntegratorUser user = null;

            try
            {
                // Authenticate.

                user = _serviceAuthenticationManager.AuthenticateRequest(HttpContext, IntegratorPermissions.GetJobAds);

                // Process.

                var jobAdIds = _jobAdFeedsManager.GetJobAdIdFeed();

                // Record it.

                _jobAdIntegrationReportsCommand.CreateJobAdIntegrationEvent(new JobAdExportFeedIdEvent { IntegratorUserId = user.Id, JobAds = jobAdIds.Count });

                return Xml(new JobAdIdsResponse(GetResponseXml(jobAdIds)));
            }
            catch (Exception ex)
            {
                EventSource.Raise(Event.Error, method, "Cannot process the request.", ex, new StandardErrorHandler());

                if (user != null)
                    _jobAdIntegrationReportsCommand.CreateJobAdIntegrationEvent(new JobAdExportFeedIdEvent { IntegratorUserId = user.Id, Success = false, JobAds = 0 });
               
                return Xml(new JobAdIdsResponse(ex));
            }
        }

        [HttpPost]
        public ActionResult JobAds()
        {
            const string method = "JobAds";

            IntegratorUser user = null;

            try
            {
                // Authenticate.

                user = _serviceAuthenticationManager.AuthenticateRequest(HttpContext, IntegratorPermissions.PostJobAds);

                // Load the request.

                var xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(GetRequestXml());
                var xmlNsMgr = new XmlNamespaceManager(xmlDoc.NameTable);
                xmlNsMgr.AddNamespace(XmlNamespacePrefix, XmlNamespace);
                var xmlNode = xmlDoc.SelectSingleNode("/" + XmlNamespacePrefix + ":PostJobAdsRequest", xmlNsMgr);

                // Extract values.

                var jobPoster = GetJobPoster(xmlNode, xmlNsMgr);
                var integratorJobPoster = jobPoster == null
                    ? _externalJobAdsQuery.GetJobPoster(user)
                    : null;

                var closeAllOtherJobAds = jobPoster != null && GetCloseAllOtherAds(xmlNode, xmlNsMgr);

                var jobAds = GetJobAds(xmlNode, xmlNsMgr, jobPoster == null ? JobAdStatus.Draft : (JobAdStatus?) null);

                // Process.

                var report = _jobAdPostsManager.PostJobAds(user, jobPoster, integratorJobPoster, jobAds.Where(j => j != null), closeAllOtherJobAds);

                // Record it, including read failures.

                report.Failed += jobAds.Count(j => j == null);
                _jobAdIntegrationReportsCommand.CreateJobAdIntegrationEvent(new JobAdImportPostEvent { Success = true, IntegratorUserId = user.Id, PosterId = (jobPoster ?? integratorJobPoster).Id, JobAds = jobAds.Count(j => j != null), Closed = report.Closed, Failed = report.Failed, Posted = report.Posted, Updated = report.Updated });

                return Xml(new PostJobAdsResponse(GetResponseXml(report), report.Errors.ToArray()));
            }
            catch (Exception ex)
            {
                EventSource.Raise(ex.Message.EndsWith("disabled.") ? (Event)Event.NonCriticalError : Event.Error, method, "Cannot process the request.", ex, new StandardErrorHandler());

                if (user != null)
                    _jobAdIntegrationReportsCommand.CreateJobAdIntegrationEvent(new JobAdImportPostEvent { Success = false, IntegratorUserId = user.Id });

                return Xml(new PostJobAdsResponse(ex));
            }
        }

        [HttpPost]
        public ActionResult CloseJobAds()
        {
            const string method = "CloseJobAds";

            IntegratorUser user = null;

            try
            {
                // Authenticate.

                user = _serviceAuthenticationManager.AuthenticateRequest(HttpContext, IntegratorPermissions.PostJobAds);

                // Load the request.

                var xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(GetRequestXml());
                var xmlNsMgr = new XmlNamespaceManager(xmlDoc.NameTable);
                xmlNsMgr.AddNamespace(XmlNamespacePrefix, XmlNamespace);
                var xmlNode = xmlDoc.SelectSingleNode("/" + XmlNamespacePrefix + ":CloseJobAdsRequest", xmlNsMgr);

                // Extract values.

                var jobPoster = GetJobPoster(xmlNode, xmlNsMgr);
                var externalReferenceIds = GetExternalReferenceIds(xmlNode, xmlNsMgr);

                // Close.

                var report = _jobAdPostsManager.CloseJobAds(user.Id, jobPoster, externalReferenceIds);

                // Record it, including read failures.

                _jobAdIntegrationReportsCommand.CreateJobAdIntegrationEvent(new JobAdImportCloseEvent { IntegratorUserId = user.Id, Success = true, JobAds = externalReferenceIds.Count, Closed = report.Closed, Failed = report.Failed });

                return Xml(new CloseJobAdsResponse(GetResponseXml(report), report.Errors.ToArray()));
            }
            catch (Exception ex)
            {
                EventSource.Raise(ex.Message.EndsWith("disabled.") ? (Event)Event.NonCriticalError : Event.Error, method, "Cannot process the request.", ex, new StandardErrorHandler());

                if (user != null)
                    _jobAdIntegrationReportsCommand.CreateJobAdIntegrationEvent(new JobAdImportCloseEvent { IntegratorUserId = user.Id, Success = false });
               
                return Xml(new CloseJobAdsResponse(ex));
            }
        }

        private IEnumerable<Guid> GetIndustryIds(string industries)
        {
            if (string.IsNullOrEmpty(industries))
                return null;

            var ids = new List<Guid>();
            foreach (var name in industries.Split('|'))
            {
                if (string.IsNullOrEmpty(name))
                    continue;

                var industry = _industriesQuery.GetIndustryByAnyName(name);
                if (industry == null)
                {
                    var sb = new StringBuilder();
                    sb.AppendFormat("The '{0}' industry is unknown.", name);
                    throw new UserException(sb.ToString());
                }

                ids.Add(industry.Id);
            }

            return ids;
        }

        private static DateTime? GetModifiedSince(string modifiedSince, string modifiedInLastDays)
        {
            // use modifiedSince in preference to modifiedInLastDays

            if (!string.IsNullOrEmpty(modifiedSince))
            {
                modifiedSince = Convert24ModifiedSince(modifiedSince);

                DateTime parsedDate;

                if (DateTime.TryParse(modifiedSince, out parsedDate))
                    return parsedDate;
             
                throw new UserException("The 'modifiedSince' parameter is incorrect. Please use the following format for this field: [-]CCYY-MM-DDThh:mm:ssZ");
            }

            if (!string.IsNullOrEmpty(modifiedInLastDays))
            {
                int parsedDays;

                if (int.TryParse(modifiedInLastDays, out parsedDays))
                    return DateTime.Now.AddDays(Math.Abs(parsedDays)*-1);

                throw new UserException("The 'modifiedInLastDays' parameter is incorrect. Please use specify a number of days in the past");
            }

            return null;
        }

        private static string Convert24ModifiedSince(string modifiedSince)
        {
            // Some send a 24 hr time, just convert to 0.

            if (modifiedSince.EndsWith("T24:00:00Z"))
                modifiedSince = modifiedSince.Replace("T24:00:00Z", "T00:00:00Z");

            return modifiedSince;
        }

        private static string GetResponseXml(IEnumerable<JobAdFeedElement> jobAdFeeds)
        {
            var sb = new StringBuilder();
            using (var writer = new StringWriter(sb))
            {
                using (var fragmentWriter = new XmlFragmentWriter(writer))
                {
                    var serializer = new XmlSerializer(typeof(JobAdFeedElement), null, new Type[0], null, Apps.Services.Constants.XmlSerializationNamespace);
                    foreach (var jobAdFeed in jobAdFeeds)
                        serializer.Serialize(fragmentWriter, jobAdFeed);
                }
            }

            return sb.ToString();
        }

        private static string GetResponseXml(IEnumerable<Guid> jobAdIds)
        {
            // Serialize them.

            var sb = new StringBuilder();
            using (var writer = new StringWriter(sb))
            {
                using (var fragmentWriter = new XmlFragmentWriter(writer))
                {
                    foreach (var jobAdId in jobAdIds)
                    {
                        fragmentWriter.WriteStartElement("JobAd");
                        fragmentWriter.WriteAttributeString("id", jobAdId.ToString());
                        fragmentWriter.WriteEndElement();
                    }
                }
            }

            return sb.ToString();
        }

        private Employer GetJobPoster(XmlNode xmlDoc, XmlNamespaceManager xmlNsMgr)
        {
            var node = xmlDoc.SelectSingleNode(XmlNamespacePrefix + ":JobAds/@jobPosterUserId", xmlNsMgr);
            var loginId = node != null ? node.Value : null;
            if (string.IsNullOrEmpty(loginId))
                return null;

            // If it is supplied then validate it.

            var jobPosterId = _loginCredentialsQuery.GetUserId(loginId);
            if (jobPosterId == null)
                throw new ServiceEndUserException("There is no job poster with user ID '" + loginId + "'.");

            var jobPoster = _employersQuery.GetEmployer(jobPosterId.Value);
            if (jobPoster == null)
                throw new ServiceEndUserException("There is no job poster with user ID '" + loginId + "'.");
            if (!jobPoster.IsEnabled)
                throw new ServiceEndUserException("Job poster user '" + loginId + "' is disabled.");

            return jobPoster;
        }

        private static IList<string> GetExternalReferenceIds(XmlNode xmlNode, XmlNamespaceManager xmlNsMgr)
        {
            var externalReferenceIds = new List<string>();
            var nodes = xmlNode.SelectNodes(XmlNamespacePrefix + ":JobAds/" + XmlNamespacePrefix + ":JobAd/@externalReferenceId", xmlNsMgr);
            if (nodes != null)
                externalReferenceIds.AddRange(from XmlNode node in nodes select node.Value);
            return externalReferenceIds;
        }

        private static bool GetCloseAllOtherAds(XmlNode xmlNode, XmlNamespaceManager xmlNsMgr)
        {
            var node = xmlNode.SelectSingleNode(XmlNamespacePrefix + ":JobAds/@closeAllOtherAds", xmlNsMgr);
            if (node == null)
                return false;

            bool closeAllOtherJobAds;
            return bool.TryParse(node.Value, out closeAllOtherJobAds) && closeAllOtherJobAds;
        }

        private IList<JobAd> GetJobAds(XmlNode node, XmlNamespaceManager xmlNsMgr, JobAdStatus? status)
        {
            var jobAds = new List<JobAd>();
            var jobAdByExternalId = new Dictionary<string, int>(StringComparer.InvariantCultureIgnoreCase);

            var jobAdNodes = node.SelectNodes(XmlNamespacePrefix + ":JobAds/" + XmlNamespacePrefix + ":JobAd", xmlNsMgr);
            if (jobAdNodes != null)
            {
                foreach (XmlNode jobAdNode in jobAdNodes)
                {
                    JobAd jobAd;
                    try
                    {
                        jobAd = GetJobAd(jobAdNode, status);
                    }
                    catch (XmlException ex)
                    {
                        // Treat any XML errors as user errors. Integrators routinely send us invalid XML.
                        throw new ServiceEndUserException("Failed to parse request XML.", ex);
                    }

                    if (jobAd == null)
                    {
                        jobAds.Add(null);
                        continue;
                    }

                    // Bug 8624 - integrators actually manage to send us multiple instance of the same
                    // external reference ID in the one request! Detect that here and only post the first
                    // of these job ads.

                    var isDuplicate = false;
                    if (!string.IsNullOrEmpty(jobAd.Integration.ExternalReferenceId))
                    {
                        int countById;
                        isDuplicate = jobAdByExternalId.TryGetValue(jobAd.Integration.ExternalReferenceId, out countById);
                        jobAdByExternalId[jobAd.Integration.ExternalReferenceId] = countById + 1;
                    }

                    if (!isDuplicate)
                        jobAds.Add(jobAd);
                }
            }

            return jobAds;
        }

        private JobAd GetJobAd(XmlNode jobAdNode, JobAdStatus? status)
        {
            JobAdElement jobAd;

            try
            {
                using (var reader = new StringReader(jobAdNode.OuterXml))
                {
                    jobAd = (JobAdElement)Serializer.Deserialize(reader);
                }
            }
            catch (Exception)
            {
                return null;
            }

            if (jobAd != null)
            {
                // Strip out script tags, and remove onX="Y" attributes from content.

                jobAd.Content = HtmlUtil.CleanScriptAndEventTags(jobAd.Content);
                if (jobAd.Content != null)
                    jobAd.Content = jobAd.Content.Replace("\n", "\r\n").Replace("\r\r\n", "\r\n");

                // PositionTitle is now optional. No point in storing the same text twice.

                if (jobAd.PositionTitle == jobAd.Title)
                    jobAd.PositionTitle = null;

                // Override the status if needed.

                if (status != null)
                    jobAd.Status = status;
            }

            return jobAd.Map(_industriesQuery, _locationQuery);
        }

        private static string GetResponseXml(CloseReport report)
        {
            var sb = new StringBuilder();
            using (var writer = new StringWriter(sb))
            {
                using (var xmlWriter = new XmlTextWriter(writer))
                {
                    xmlWriter.WriteStartElement("JobAds");
                    xmlWriter.WriteAttributeString("closed", report.Closed.ToString(CultureInfo.InvariantCulture));
                    xmlWriter.WriteAttributeString("failed", report.Failed.ToString(CultureInfo.InvariantCulture));
                    xmlWriter.WriteEndElement();
                }
            }

            return sb.ToString();
        }

        private static string GetResponseXml(PostReport report)
        {
            var sb = new StringBuilder();
            using (var writer = new StringWriter(sb))
            {
                using (var xmlWriter = new XmlTextWriter(writer))
                {
                    xmlWriter.WriteStartElement("JobAds");
                    xmlWriter.WriteAttributeString("added", report.Posted.ToString(CultureInfo.InvariantCulture));
                    xmlWriter.WriteAttributeString("updated", report.Updated.ToString(CultureInfo.InvariantCulture));
                    xmlWriter.WriteAttributeString("closed", report.Closed.ToString(CultureInfo.InvariantCulture));
                    xmlWriter.WriteAttributeString("failed", report.Failed.ToString(CultureInfo.InvariantCulture));
                    xmlWriter.WriteEndElement();
                }
            }

            return sb.ToString();
        }
    }
}
