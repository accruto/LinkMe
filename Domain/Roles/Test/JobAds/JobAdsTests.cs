using System;
using LinkMe.Domain.Roles.JobAds;
using LinkMe.Domain.Roles.JobAds.Commands;
using LinkMe.Domain.Roles.JobAds.Queries;
using LinkMe.Domain.Test.Data;
using LinkMe.Framework.Utility.Sql;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Domain.Roles.Test.JobAds
{
    [TestClass]
    public abstract class JobAdsTests
        : TestClass
    {
        protected readonly IJobAdsCommand _jobAdsCommand = Resolve<IJobAdsCommand>();
        protected readonly IJobAdsQuery _jobAdsQuery = Resolve<IJobAdsQuery>();
        private readonly IJobPostersCommand _jobPostersCommand = Resolve<IJobPostersCommand>();

        private const string TitleFormat = "This is the {0} title.";
        private const string ContentFormat = "This is the {0} content.";

        [TestInitialize]
        public void JobAdsTestInitialize()
        {
            Resolve<IDbConnectionFactory>().DeleteAllTestData();
        }

        protected JobPoster CreateJobPoster()
        {
            var jobPoster = new JobPoster { Id = Guid.NewGuid() };
            _jobPostersCommand.CreateJobPoster(jobPoster);
            return jobPoster;
        }

        protected JobAd CreateJobAd(int index, JobPoster jobPoster, Action<JobAd> prepare)
        {
            var jobAd = new JobAd
            {
                Id = Guid.NewGuid(),
                PosterId = jobPoster.Id,
                Title = string.Format(TitleFormat, index),
                Description =
                {
                    Content = string.Format(ContentFormat, index),
                },
            };

            if (prepare != null)
                prepare(jobAd);

            _jobAdsCommand.CreateJobAd(jobAd);
            return jobAd;
        }
    }
}
