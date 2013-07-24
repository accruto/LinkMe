using System;
using System.Collections.Generic;
using System.Linq;
using LinkMe.Apps.Presentation.Errors;
using LinkMe.Apps.Services.JobAds;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Credits;
using LinkMe.Domain.Roles.JobAds.Queries;
using LinkMe.Domain.Users.Employers.Credits.Queries;
using LinkMe.Domain.Users.Employers.Queries;
using LinkMe.Domain.Users.Members.JobAds;
using LinkMe.Domain.Users.Members.JobAds.Queries;
using LinkMe.Framework.Instrumentation;
using LinkMe.Query.Search.JobAds;
using LinkMe.Query.Search.JobAds.Commands;
using LinkMe.Web.Areas.Members.Routes;

namespace LinkMe.Web.Areas.Integration.Controllers
{
    public class JobAdFeedsManager
        : IJobAdFeedsManager
    {
        private static readonly EventSource EventSource = new EventSource<JobAdFeedsManager>();
        private readonly IMemberJobAdViewsQuery _memberJobAdViewsQuery;
        private readonly IJobAdIntegrationQuery _jobAdIntegrationQuery;
        private readonly IEmployersQuery _employersQuery;
        private readonly IEmployerCreditsQuery _employerCreditsQuery;
        private readonly IExecuteJobAdSearchCommand _executeJobAdSearchCommand;

        private const int BatchSize = 10;

        public JobAdFeedsManager(IMemberJobAdViewsQuery memberJobAdViewsQuery, IJobAdIntegrationQuery jobAdIntegrationQuery, IEmployersQuery employersQuery, IEmployerCreditsQuery employerCreditsQuery, IExecuteJobAdSearchCommand executeJobAdSearchCommand)
        {
            _memberJobAdViewsQuery = memberJobAdViewsQuery;
            _jobAdIntegrationQuery = jobAdIntegrationQuery;
            _employersQuery = employersQuery;
            _employerCreditsQuery = employerCreditsQuery;
            _executeJobAdSearchCommand = executeJobAdSearchCommand;
        }

        IList<JobAdFeedElement> IJobAdFeedsManager.GetJobAdFeed(IEnumerable<JobAdSearchCriteria> criterias, IEnumerable<Guid> industryIds, DateTime? modifiedSince)
        {
            IList<Guid> jobAdIds = null;

            foreach (var criteria in criterias)
            {
                if (modifiedSince != null)
                    criteria.Recency = DateTime.Now - modifiedSince;

                if (industryIds != null)
                {
                    var industryIdList = industryIds.ToList();
                    if (industryIdList.Count > 0)
                    {
                        criteria.IndustryIds = criteria.IndustryIds == null
                            ? industryIdList
                            : criteria.IndustryIds.Concat(industryIdList).ToList();
                    }
                }

                var execution = _executeJobAdSearchCommand.Search(null, criteria, null);

                jobAdIds = jobAdIds == null
                    ? execution.Results.JobAdIds
                    : jobAdIds.Concat(execution.Results.JobAdIds).ToList();
            }

            return GetJobAdFeeds(jobAdIds).ToList();
        }

        IList<JobAdFeedElement> IJobAdFeedsManager.GetJobAdFeed(IEnumerable<Guid> industryIds, DateTime? modifiedSince)
        {
            return GetJobAdFeeds(GetOpenJobAdIds(industryIds, modifiedSince)).ToList();
        }

        IList<Guid> IJobAdFeedsManager.GetJobAdIdFeed()
        {
            return (from j in GetJobAdFeeds(GetOpenJobAdIds(null, null))
                    select j.Id).ToList();
        }

        private IEnumerable<JobAdFeedElement> GetJobAdFeeds(IList<Guid> jobAdIds)
        {
            // Get the job ads in batches.

            var jobAdFeeds = new List<JobAdFeedElement>();
            var credits = new Dictionary<Guid, int>();
            var affiliateIds = new Dictionary<Guid, Guid?>();

            while (jobAdIds.Count != 0)
            {
                var batchJobAdIds = jobAdIds.Take(BatchSize);
                jobAdIds = jobAdIds.Skip(BatchSize).ToList();

                var jobAds = _memberJobAdViewsQuery.GetMemberJobAdViews(null, batchJobAdIds);
                foreach (var jobAd in jobAds)
                {
                    var jobAdFeed = GetJobAdFeed(jobAd, credits, affiliateIds);
                    if (jobAdFeed != null)
                        jobAdFeeds.Add(jobAdFeed);
                }
            }

            return jobAdFeeds;
        }

        private JobAdFeedElement GetJobAdFeed(MemberJobAdView jobAd, IDictionary<Guid, int> credits, IDictionary<Guid, Guid?> affiliateIds)
        {
            const string method = "GetJobAdFeed";

            try
            {
                var viewJobAdUrl = jobAd.GenerateJobAdUrl();
                var applyJobAdUrl = jobAd.GenerateJobAdUrl();

                Employer employer = null;

                int credit;
                if (!credits.TryGetValue(jobAd.PosterId, out credit))
                {
                    employer = _employersQuery.GetEmployer(jobAd.PosterId);
                    credit = GetEmployerCredit(employer);
                    credits[jobAd.PosterId] = credit;
                }

                Guid? affiliateId;
                if (!affiliateIds.TryGetValue(jobAd.PosterId, out affiliateId))
                {
                    if (employer == null)
                        employer = _employersQuery.GetEmployer(jobAd.PosterId);
                    affiliateId = employer.Organisation.AffiliateId;
                    affiliateIds[jobAd.PosterId] = affiliateId;
                }

                // Only add the job ad if it is not associated with an affiliate.

                if (affiliateId != null)
                    return null;

                return jobAd.Map(viewJobAdUrl.AbsoluteUri, applyJobAdUrl.AbsoluteUri, credit > 0);
            }
            catch (Exception ex)
            {
                EventSource.Raise(Event.Error, method, "Cannot get the job ad feed for job ad '" + jobAd.Id + "'.", ex, new StandardErrorHandler(), Event.Arg("jobAdId", jobAd.Id));
                return null;
            }
        }

        private IList<Guid> GetOpenJobAdIds(IEnumerable<Guid> industryIds, DateTime? modifiedSince)
        {
            return _jobAdIntegrationQuery.GetOpenJobAdIds(industryIds, modifiedSince);
        }

        private int GetEmployerCredit(IEmployer employer)
        {
            var credits = _employerCreditsQuery.GetEffectiveActiveAllocation<ApplicantCredit>(employer);
            return credits.RemainingQuantity ?? 0;
        }
    }
}
