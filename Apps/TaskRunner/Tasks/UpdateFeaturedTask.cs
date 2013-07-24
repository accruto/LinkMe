using System;
using System.Linq;
using LinkMe.Apps.Agents.Featured;
using LinkMe.Apps.Agents.Featured.Commands;
using LinkMe.Apps.Agents.Tasks;
using LinkMe.Apps.Presentation.Domain.Roles.JobAds;
using LinkMe.Domain;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.JobAds;
using LinkMe.Domain.Roles.JobAds.Queries;
using LinkMe.Framework.Instrumentation;
using LinkMe.Framework.Utility.Results;
using LinkMe.Framework.Utility.Urls;
using LinkMe.Query.Reports.Accounts.Queries;
using LinkMe.Query.Reports.Roles.JobAds.Queries;
using LinkMe.Query.Reports.Search.Queries;
using LinkMe.Query.Reports.Users.Employers.Queries;

namespace LinkMe.TaskRunner.Tasks
{
    public class UpdateFeaturedTask
        : Task
    {
        private static readonly EventSource EventSource = new EventSource<UpdateFeaturedTask>();
        private readonly IFeaturedCommand _featuredCommand;
        private readonly IJobAdsQuery _jobAdsQuery;
        private readonly IJobAdReportsQuery _jobAdReportsQuery;
        private readonly IAccountReportsQuery _accountReportsQuery;
        private readonly IEmployerMemberAccessReportsQuery _employerMemberAccessReportsQuery;
        private readonly IMemberSearchReportsQuery _memberSearchReportsQuery;

        public UpdateFeaturedTask(IFeaturedCommand featuredCommand, IJobAdsQuery jobAdsQuery, IJobAdReportsQuery jobAdReportsQuery, IAccountReportsQuery accountReportsQuery, IEmployerMemberAccessReportsQuery employerMemberAccessReportsQuery, IMemberSearchReportsQuery memberSearchReportsQuery)
            : base(EventSource)
        {
            _featuredCommand = featuredCommand;
            _jobAdsQuery = jobAdsQuery;
            _jobAdReportsQuery = jobAdReportsQuery;
            _accountReportsQuery = accountReportsQuery;
            _employerMemberAccessReportsQuery = employerMemberAccessReportsQuery;
            _memberSearchReportsQuery = memberSearchReportsQuery;
        }

        public override void ExecuteTask(string[] args)
        {
            const string method = "ExecuteTask";
            EventSource.Raise(Event.Information, method, "Updating featured statistcs.");

            UpdateFeaturedStatistics(args.Length > 0 ? int.Parse(args[0]) : 7);
            UpdateFeaturedJobAds(args.Length > 1 ? int.Parse(args[1]) : 100);

            EventSource.Raise(Event.Information, method, "Updating featured job ads.");
        }

        private void UpdateFeaturedJobAds(int jobAdCount)
        {
            var jobAds = _jobAdsQuery.GetJobAds<JobAd>(_jobAdsQuery.GetRecentOpenJobAdIds(new Range(0, jobAdCount)));

            var featuredJobAds = from j in jobAds
                                 where !string.IsNullOrEmpty(j.Title)
                                 select new FeaturedItem
                                 {
                                     Id = j.Id,
                                     Url = GetJobUrl(j),
                                     Title = GetTitle(j),
                                 };
            _featuredCommand.UpdateFeaturedJobAds(featuredJobAds);
        }

        private static string GetJobUrl(JobAd jobAd)
        {
            var jobsUrl = new ReadOnlyApplicationUrl("~/jobs");
            var jobAdPath = jobAd.GetJobRelativePath();
            var path = jobsUrl.Path.AddUrlSegments(jobAdPath);
            return Application.ApplicationPathStartChar + new ReadOnlyApplicationUrl(path).AppRelativePathAndQuery;
        }

        private static string GetTitle(JobAd jobAd)
        {
            if (jobAd.Description != null && jobAd.Description.Location != null && !jobAd.Description.Location.IsCountry)
                return jobAd.Title + " (" + jobAd.Description.Location + ")";
            return jobAd.Title;
        }

        private void UpdateFeaturedStatistics(int days)
        {
            var today = DateTime.Today;
            var dateRange = new DateRange(today.AddDays(-1 * days), today);

            var statistics = new FeaturedStatistics
            {
                CreatedJobAds = _jobAdReportsQuery.GetCreatedJobAds(dateRange),
                Members = _accountReportsQuery.GetUsers(UserType.Member, DateTime.Now),
                MemberSearches = _memberSearchReportsQuery.GetAllMemberSearches(dateRange),
                MemberAccesses = _employerMemberAccessReportsQuery.GetMemberAccesses(dateRange)
            };

            _featuredCommand.UpdateFeaturedStatistics(statistics);
        }
    }
}
