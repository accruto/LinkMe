using System;
using LinkMe.Domain.Industries.Queries;
using LinkMe.Domain.Location.Queries;
using LinkMe.Domain.Roles.JobAds;
using LinkMe.Domain.Roles.JobAds.Commands;
using LinkMe.Domain.Roles.JobAds.Queries;
using LinkMe.Domain.Test.Data;
using LinkMe.Framework.Utility.Sql;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Domain.Users.Test.Employers.JobAds.Create
{
    [TestClass]
    public abstract class CreateJobAdTests
        : TestClass
    {
        protected readonly ILocationQuery _locationQuery = Resolve<ILocationQuery>();
        protected readonly IIndustriesQuery _industriesQuery = Resolve<IIndustriesQuery>();
        protected readonly IJobAdsCommand _jobAdsCommand = Resolve<IJobAdsCommand>();
        protected readonly IJobAdsQuery _jobAdsQuery = Resolve<IJobAdsQuery>();
        protected readonly IJobPostersCommand _jobPostersCommand = Resolve<IJobPostersCommand>();

        private const string TitleFormat = "This is the title {0}";
        private const string ContentFormat = "This is the content {0}";

        [TestInitialize]
        public void CreateJobAdTestsInitialize()
        {
            Resolve<IDbConnectionFactory>().DeleteAllTestData();
        }

        protected JobPoster CreateJobPoster()
        {
            var poster = new JobPoster { Id = Guid.NewGuid() };
            _jobPostersCommand.CreateJobPoster(poster);
            return poster;
        }

        protected JobAd CreateJobAd(Guid posterId, int index)
        {
            return new JobAd
            {
                PosterId = posterId,
                Title = string.Format(TitleFormat, index),
                Description =
                {
                    Content = string.Format(ContentFormat, index),
                },
            };
        }
    }
}