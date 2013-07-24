using System;
using System.Collections.Generic;
using System.Linq;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.JobAds;
using LinkMe.Domain.Roles.JobAds.Commands;
using LinkMe.Domain.Roles.Recruiters.Commands;
using LinkMe.Domain.Roles.Test.Recruiters;
using LinkMe.Domain.Test.Data;
using LinkMe.Domain.Users.Employers.Candidates;
using LinkMe.Domain.Users.Employers.Commands;
using LinkMe.Domain.Users.Members.Commands;
using LinkMe.Domain.Users.Members.JobAds;
using LinkMe.Domain.Users.Members.JobAds.Commands;
using LinkMe.Domain.Users.Members.JobAds.Queries;
using LinkMe.Domain.Users.Test.Employers;
using LinkMe.Framework.Utility;
using LinkMe.Framework.Utility.Sql;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Domain.Users.Test.Members.JobAds.FlagLists
{
    [TestClass]
    public abstract class JobAdFlagListsTests
        : TestClass
    {
        protected readonly IMemberJobAdListsCommand _memberJobAdListsCommand = Resolve<IMemberJobAdListsCommand>();
        protected readonly IJobAdFlagListsQuery _jobAdFlagListsQuery = Resolve<IJobAdFlagListsQuery>();
        protected readonly IJobAdsCommand _jobAdsCommand = Resolve<IJobAdsCommand>();
        private readonly IEmployersCommand _employersCommand = Resolve<IEmployersCommand>();
        private readonly IJobPostersCommand _jobPostersCommand = Resolve<IJobPostersCommand>();
        private readonly IMembersCommand _membersCommand = Resolve<IMembersCommand>();
        private readonly IOrganisationsCommand _organisationsCommand = Resolve<IOrganisationsCommand>();

        [TestInitialize]
        public void JobAdFlagListsTestsInitialize()
        {
            Resolve<IDbConnectionFactory>().DeleteAllTestData();
        }

        protected Employer CreateEmployer(int index)
        {
            var employer = _employersCommand.CreateTestEmployer(index, _organisationsCommand.CreateTestVerifiedOrganisation(index));
            _jobPostersCommand.CreateJobPoster(new JobPoster { Id = employer.Id });
            return employer;
        }

        protected Member CreateMember(int index)
        {
            return _membersCommand.CreateTestMember(index);
        }

        protected JobAdFlagList GetFlaggedFlagList(IMember member)
        {
            return _jobAdFlagListsQuery.GetFlagList(member);
        }

        protected JobAdFlagList GetFlaggedFlagList(IMember member, int index)
        {
            return GetFlaggedFlagList(member);
        }

        protected void AssertFlagListEntries(IMember member, ICollection<IJobAd> isFlaggedJobAds, ICollection<IJobAd> isNotFlaggedJobAds)
        {
            // IsFlagged.

            foreach (var isFlaggedJobAd in isFlaggedJobAds)
                Assert.IsTrue(_jobAdFlagListsQuery.IsFlagged(member, isFlaggedJobAd.Id));
            foreach (var isNotFlaggedMember in isNotFlaggedJobAds)
                Assert.IsFalse(_jobAdFlagListsQuery.IsFlagged(member, isNotFlaggedMember.Id));

            // GetFlaggedCount.

            Assert.AreEqual(isFlaggedJobAds.Count, _jobAdFlagListsQuery.GetFlaggedCount(member));

            // GetFlaggedCandidateIds.

            Assert.IsTrue((from j in isFlaggedJobAds select j.Id).CollectionEqual(_jobAdFlagListsQuery.GetFlaggedJobAdIds(member)));
            Assert.IsTrue((from j in isFlaggedJobAds select j.Id).CollectionEqual(_jobAdFlagListsQuery.GetFlaggedJobAdIds(member, from j in isFlaggedJobAds.Concat(isNotFlaggedJobAds) select j.Id)));
        }

        protected static void AssertFlagList(IEmployer employer, CandidateFlagList expectedFlagList, IList<CandidateFlagList> flagLists)
        {
            Assert.AreEqual(1, flagLists.Count);
            AssertFlagList(employer, expectedFlagList, flagLists[0]);
        }

        protected static void AssertFlagList(IEmployer employer, CandidateFlagList expectedFlagList, CandidateFlagList flagList)
        {
            Assert.AreNotEqual(Guid.Empty, flagList.Id);
            Assert.AreNotEqual(DateTime.MinValue, flagList.CreatedTime);

            if (expectedFlagList.Id != Guid.Empty)
                Assert.AreEqual(expectedFlagList.Id, flagList.Id);
            Assert.AreEqual(expectedFlagList.Name, flagList.Name);

            Assert.IsNull(flagList.Name);
            Assert.AreEqual(employer.Id, flagList.RecruiterId);
            Assert.AreEqual(expectedFlagList.RecruiterId, flagList.RecruiterId);
        }
    }
}