using System;
using System.Collections.Generic;
using LinkMe.Apps.Mocks.Hosts;
using LinkMe.Domain;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Industries;
using LinkMe.Domain.Industries.Queries;
using LinkMe.Domain.Roles.JobAds;
using LinkMe.Domain.Roles.JobAds.Commands;
using LinkMe.Domain.Roles.Recruiters.Commands;
using LinkMe.Domain.Roles.Test.Recruiters;
using LinkMe.Domain.Test.Data;
using LinkMe.Domain.Users.Employers.Commands;
using LinkMe.Domain.Users.Members.JobAds.Commands;
using LinkMe.Domain.Users.Members.JobAds.Queries;
using LinkMe.Domain.Users.Test.Employers;
using LinkMe.Framework.Utility.Results;
using LinkMe.Framework.Utility.Sql;
using LinkMe.Query.JobAds;
using LinkMe.Query.Search.JobAds;
using LinkMe.Query.Search.JobAds.Commands;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Apps.Agents.Test.Query.Search.JobAdSort
{
    [TestClass]
    public class ContentTests
        : TestClass
    {
        private readonly IEmployersCommand _employersCommand = Resolve<IEmployersCommand>();
        private readonly IOrganisationsCommand _organisationsCommand = Resolve<IOrganisationsCommand>();
        private readonly IJobPostersCommand _jobPostersCommand = Resolve<IJobPostersCommand>();
        private readonly IJobAdsCommand _jobAdsCommand = Resolve<IJobAdsCommand>();
        private readonly IExecuteJobAdSortCommand _executeJobAdSortCommand = Resolve<IExecuteJobAdSortCommand>();
        private readonly IIndustriesQuery _industriesQuery = Resolve<IndustriesQuery>();
        private static IJobAdSortService _sortService;
        private readonly IMemberJobAdListsCommand _memberJobAdListsCommand = Resolve<IMemberJobAdListsCommand>();
        private readonly IJobAdFlagListsQuery _jobAdFlagListsQuery = Resolve<IJobAdFlagListsQuery>();

        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            Resolve<IDbConnectionFactory>().DeleteAllTestData();
            JobAdSortHost.ClearIndex();

            _sortService = Resolve<IJobAdSortService>();
        }

        [TestMethod]
        public void TestClosedAds()
        {
            var employer = CreateEmployer(0);
            var member = new Member { Id = Guid.NewGuid() };

            var jobAd = CreateJobAd(member, employer.Id, JobAdStatus.Closed);
            IndexJobAd(jobAd);

            // Search without filter.

            var jobQuery = new JobAdSearchSortCriteria();
            var results = Sort(member, jobQuery);
            Assert.AreEqual(1, results.Count);

            //Now add an open one

            jobAd = CreateJobAd(member, employer.Id, JobAdStatus.Open);
            IndexJobAd(jobAd);

            // Search without filter.

            results = Sort(member, jobQuery);
            Assert.AreEqual(2, results.Count);

            //Now add another closed one

            jobAd = CreateJobAd(member, employer.Id, JobAdStatus.Closed);
            IndexJobAd(jobAd);

            // Search without filter.

            results = Sort(member, jobQuery);
            Assert.AreEqual(3, results.Count);

        }

        private JobAd CreateJobAd(IMember member, Guid employerId, JobAdStatus status)
        {
            var jobAd = new JobAd
            {
                Id = Guid.NewGuid(),
                Status = status,
                Title = "Best Job in the World",
                CreatedTime = DateTime.Now,
                PosterId = employerId,
                Description =
                {
                    BulletPoints = new[] { "good verbal communication", "self management and independency", "bullet point 3" },
                    Content = "Mutley, you snickering, floppy eared hound. When courage is needed, you're never around.",
                    JobTypes = JobTypes.FullTime,
                    Industries = new List<Industry> { _industriesQuery.GetIndustry("Engineering") },
                },
            };

            _jobAdsCommand.CreateJobAd(jobAd);
            _jobAdsCommand.OpenJobAd(jobAd);
            if (status == JobAdStatus.Closed)
                _jobAdsCommand.CloseJobAd(jobAd);

            _memberJobAdListsCommand.AddJobAdToFlagList(member, _jobAdFlagListsQuery.GetFlagList(member), jobAd.Id);
            return jobAd;
        }

        protected void IndexJobAd(JobAd jobAd)
        {
            _sortService.UpdateJobAd(jobAd.Id);
        }


        protected IEmployer CreateEmployer(int index)
        {
            var employer = _employersCommand.CreateTestEmployer(index, _organisationsCommand.CreateTestOrganisation(1));
            _jobPostersCommand.CreateJobPoster(new JobPoster { Id = employer.Id });
            return employer;
        }

        private ICollection<Guid> Sort(IMember member, JobAdSearchSortCriteria criteria)
        {
            return _executeJobAdSortCommand.SortFlagged(member, criteria, new Range(0, 10)).Results.JobAdIds;
        }
    }
}