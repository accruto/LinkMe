using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Script.Serialization;
using LinkMe.Apps.Agents.Test.Users.Employers;
using LinkMe.Apps.Agents.Test.Users.Members;
using LinkMe.Apps.Agents.Users.Employers.Commands;
using LinkMe.Apps.Agents.Users.Members.Commands;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Contenders;
using LinkMe.Domain.Roles.JobAds;
using LinkMe.Domain.Roles.JobAds.Commands;
using LinkMe.Domain.Roles.Recruiters.Commands;
using LinkMe.Domain.Roles.Test.Recruiters;
using LinkMe.Domain.Users.Employers.Applicants;
using LinkMe.Domain.Users.Employers.Applicants.Commands;
using LinkMe.Domain.Users.Employers.Applicants.Queries;
using LinkMe.Domain.Users.Employers.JobAds.Commands;
using LinkMe.Framework.Utility;
using LinkMe.Framework.Utility.Urls;
using LinkMe.Web.Areas.Employers.Models.Candidates;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Employers.Candidates.JobAds
{
    [TestClass]
    public abstract class ApiJobAdsTests
        : ApiTests
    {
        private readonly IMemberAccountsCommand _memberAccountsCommand = Resolve<IMemberAccountsCommand>();
        private readonly IEmployerAccountsCommand _employerAccountsCommand = Resolve<IEmployerAccountsCommand>();
        private readonly IOrganisationsCommand _organisationsCommand = Resolve<IOrganisationsCommand>();
        protected readonly IJobAdsCommand _jobAdsCommand = Resolve<IJobAdsCommand>();
        protected readonly IJobAdApplicationSubmissionsCommand _jobAdApplicationSubmissionsCommand = Resolve<IJobAdApplicationSubmissionsCommand>();
        protected readonly IJobAdApplicantsCommand _jobAdApplicationsCommand = Resolve<IJobAdApplicantsCommand>();
        protected readonly IJobAdApplicantsQuery _jobAdApplicantsQuery = Resolve<IJobAdApplicantsQuery>();

        protected ReadOnlyUrl _baseJobAdsUrl;
        protected ReadOnlyUrl _jobAdsUrl;

        [TestInitialize]
        public void TestInitialize()
        {
            _baseJobAdsUrl = new ReadOnlyApplicationUrl(true, "~/employers/candidates/jobads/api/");
            _jobAdsUrl = new ReadOnlyApplicationUrl(true, "~/employers/candidates/jobads/api");
        }

        protected Employer CreateEmployer()
        {
            return _employerAccountsCommand.CreateTestEmployer(0, _organisationsCommand.CreateTestOrganisation(0));
        }

        protected Member CreateMember(int index)
        {
            return _memberAccountsCommand.CreateTestMember(index);
        }

        protected JsonJobAdsModel JobAds()
        {
            var response = Post(_jobAdsUrl);
            return new JavaScriptSerializer().Deserialize<JsonJobAdsModel>(response);
        }

        protected void AssertModel(int expectedNewCount, int expectedShortlistedCount, int expectedRejectedCount, JsonJobAdsModel model)
        {
            AssertJsonSuccess(model);
            Assert.AreEqual(1, model.JobAds.Count);
            Assert.AreEqual(expectedNewCount, model.JobAds[0].ApplicantCounts.New);
            Assert.AreEqual(expectedShortlistedCount, model.JobAds[0].ApplicantCounts.ShortListed);
            Assert.AreEqual(expectedRejectedCount, model.JobAds[0].ApplicantCounts.Rejected);
        }

        protected void AssertCandidates(JobAd jobAd, Member[] expectedMembers, ApplicantStatus expectedStatus)
        {
            AssertCandidates(expectedMembers, _jobAdApplicantsQuery.GetApplicantIds(jobAd.Id, expectedStatus));
        }

        protected void AssertCandidates(JobAd jobAd, Member[] expectedNewMembers, Member[] expectedShortlistedMembers, Member[] expectedRejectedMembers, Member[] expectedRemovedMembers)
        {
            AssertCandidates(expectedNewMembers, _jobAdApplicantsQuery.GetApplicantIds(jobAd.Id, ApplicantStatus.New));
            AssertCandidates(expectedShortlistedMembers, _jobAdApplicantsQuery.GetApplicantIds(jobAd.Id, ApplicantStatus.Shortlisted));
            AssertCandidates(expectedRejectedMembers, _jobAdApplicantsQuery.GetApplicantIds(jobAd.Id, ApplicantStatus.Rejected));
            AssertCandidates(expectedRemovedMembers, _jobAdApplicantsQuery.GetApplicantIds(jobAd.Id, ApplicantStatus.Removed));
        }

        private static void AssertCandidates(IEnumerable<Member> expectedMembers, IEnumerable<Guid> applicantIds)
        {
            Assert.IsTrue((from m in expectedMembers select m.Id).CollectionEqual(applicantIds));
        }
    }
}
