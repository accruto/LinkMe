using System;
using LinkMe.Domain.Roles.JobAds;
using LinkMe.Domain.Roles.JobAds.Commands;
using LinkMe.Framework.Host;
using LinkMe.Framework.Utility.Wcf;
using LinkMe.Query.JobAds;
using LinkMe.Query.Search.Engine.JobAds.Sort;
using LinkMe.Query.Search.JobAds.Commands;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Query.Search.Test.JobAds.Sort
{
    [TestClass]
    public abstract class ExecuteJobAdSortTests
        : TestClass
    {
        protected readonly IJobAdSortService _jobAdSortService;
        protected readonly IExecuteJobAdSortCommand _executeJobAdSortCommand;
        private readonly IJobAdsCommand _jobAdsCommand = Resolve<IJobAdsCommand>();
        private readonly IJobPostersCommand _jobPostersCommand = Resolve<IJobPostersCommand>();

        private const string TitleFormat = "The {0} job title.";
        private const string ContentFormat = "The {0} job content.";

        protected ExecuteJobAdSortTests()
        {
            _jobAdSortService = Resolve<JobAdSortService>();
            ((IChannelAware)_jobAdSortService).OnOpen();
            _executeJobAdSortCommand = new ExecuteJobAdSortCommand(new LocalChannelManager<IJobAdSortService>(_jobAdSortService));
        }

        [TestInitialize]
        public void ExecuteJobAdSortTestsInitialize()
        {
            _jobAdSortService.Clear();
        }

        protected JobPoster CreateJobPoster()
        {
            var jobPoster = new JobPoster { Id = Guid.NewGuid() };
            _jobPostersCommand.CreateJobPoster(jobPoster);
            return jobPoster;
        }

        protected JobAd CreateJobAd(JobPoster jobPoster, int index, Action<JobAd> initialiseJobAd)
        {
            var jobAd = new JobAd
            {
                PosterId = jobPoster.Id,
                Title = string.Format(TitleFormat, index),
                Description = new JobAdDescription
                {
                    Content = string.Format(ContentFormat, index),
                },
            };
            initialiseJobAd(jobAd);
            _jobAdsCommand.CreateJobAd(jobAd);
            _jobAdsCommand.OpenJobAd(jobAd);
            _jobAdSortService.UpdateJobAd(jobAd.Id);
            return jobAd;
        }
    }
}