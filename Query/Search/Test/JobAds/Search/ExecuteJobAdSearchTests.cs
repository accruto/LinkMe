using System;
using LinkMe.Domain.Roles.JobAds;
using LinkMe.Domain.Roles.JobAds.Commands;
using LinkMe.Framework.Host;
using LinkMe.Framework.Utility.Wcf;
using LinkMe.Query.JobAds;
using LinkMe.Query.Search.Engine.JobAds.Search;
using LinkMe.Query.Search.JobAds.Commands;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Query.Search.Test.JobAds.Search
{
    [TestClass]
    public abstract class ExecuteJobAdSearchTests
        : TestClass
    {
        protected readonly IJobAdSearchService _jobAdSearchService;
        protected readonly IExecuteJobAdSearchCommand _executeJobAdSearchCommand;
        private readonly IJobAdsCommand _jobAdsCommand = Resolve<IJobAdsCommand>();
        private readonly IJobPostersCommand _jobPostersCommand = Resolve<IJobPostersCommand>();

        private const string TitleFormat = "The {0} job title.";
        private const string ContentFormat = "The {0} job content.";

        protected ExecuteJobAdSearchTests()
        {
            _jobAdSearchService = Resolve<JobAdSearchService>();
            ((IChannelAware)_jobAdSearchService).OnOpen();
            _executeJobAdSearchCommand = new ExecuteJobAdSearchCommand(new LocalChannelManager<IJobAdSearchService>(_jobAdSearchService));
        }

        [TestInitialize]
        public void ExecuteJobAdSearchTestsInitialize()
        {
            _jobAdSearchService.Clear();
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
            if (initialiseJobAd != null)
                initialiseJobAd(jobAd);
            _jobAdsCommand.CreateJobAd(jobAd);
            _jobAdsCommand.OpenJobAd(jobAd);
            _jobAdSearchService.UpdateJobAd(jobAd.Id);
            return jobAd;
        }
    }
}
