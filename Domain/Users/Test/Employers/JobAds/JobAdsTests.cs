using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Contenders;
using LinkMe.Domain.Roles.JobAds;
using LinkMe.Domain.Roles.JobAds.Commands;
using LinkMe.Domain.Roles.JobAds.Queries;
using LinkMe.Domain.Roles.Recruiters.Commands;
using LinkMe.Domain.Roles.Test.Recruiters;
using LinkMe.Domain.Test.Data;
using LinkMe.Domain.Users.Employers.Applicants;
using LinkMe.Domain.Users.Employers.Applicants.Commands;
using LinkMe.Domain.Users.Employers.Applicants.Queries;
using LinkMe.Domain.Users.Employers.Candidates.Commands;
using LinkMe.Domain.Users.Employers.Candidates.Queries;
using LinkMe.Domain.Users.Employers.Commands;
using LinkMe.Domain.Users.Employers.JobAds.Commands;
using LinkMe.Domain.Users.Members.Commands;
using LinkMe.Framework.Utility.Sql;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Domain.Users.Test.Employers.JobAds
{
    [TestClass]
    public abstract class JobAdsTests
        : TestClass
    {
        protected readonly IJobAdsCommand _jobAdsCommand = Resolve<IJobAdsCommand>();
        protected readonly IJobAdsQuery _jobAdsQuery = Resolve<IJobAdsQuery>();
        protected readonly IJobPostersCommand _jobPostersCommand = Resolve<IJobPostersCommand>();
        protected readonly IJobAdApplicantsCommand _jobAdApplicantsCommand = Resolve<IJobAdApplicantsCommand>();
        protected readonly IJobAdApplicantsQuery _jobAdApplicantsQuery = Resolve<IJobAdApplicantsQuery>();
        protected readonly IJobAdApplicationSubmissionsCommand _jobAdApplicationSubmissionsCommand = Resolve<IJobAdApplicationSubmissionsCommand>();
        protected readonly IEmployersCommand _employersCommand = Resolve<IEmployersCommand>();
        protected readonly ICandidateFoldersCommand _candidateFoldersCommand = Resolve<ICandidateFoldersCommand>();
        protected readonly ICandidateFoldersQuery _candidateFoldersQuery = Resolve<ICandidateFoldersQuery>();
        protected readonly IOrganisationsCommand _organisationsCommand = Resolve<IOrganisationsCommand>();
        protected readonly IMembersCommand _membersCommand = Resolve<IMembersCommand>();

        [TestInitialize]
        public void JobAdsTestsInitialize()
        {
            Resolve<IDbConnectionFactory>().DeleteAllTestData();
        }

        protected IEmployer CreateEmployer()
        {
            var employer = _employersCommand.CreateTestEmployer(1, _organisationsCommand.CreateTestOrganisation(1));
            _jobPostersCommand.CreateJobPoster(new JobPoster { Id = employer.Id });
            return employer;
        }

        protected void AssertCounts(IEmployer employer, ApplicantList applicantList, int newCount, int shortlistedCount, int rejectedCount)
        {
            Assert.AreEqual(newCount + shortlistedCount + rejectedCount, _jobAdApplicantsQuery.GetApplicantCount(employer, applicantList));

            var counts = _jobAdApplicantsQuery.GetApplicantCounts(employer, applicantList);
            Assert.AreEqual(newCount, counts[ApplicantStatus.New]);
            Assert.AreEqual(shortlistedCount, counts[ApplicantStatus.Shortlisted]);
            Assert.AreEqual(rejectedCount, counts[ApplicantStatus.Rejected]);

            var allCounts = _jobAdApplicantsQuery.GetApplicantCounts(employer, new[] { applicantList });
            Assert.AreEqual(1, allCounts.Count);
            Assert.AreEqual(newCount, allCounts[applicantList.Id][ApplicantStatus.New]);
            Assert.AreEqual(shortlistedCount, allCounts[applicantList.Id][ApplicantStatus.Shortlisted]);
            Assert.AreEqual(rejectedCount, allCounts[applicantList.Id][ApplicantStatus.Rejected]);
        }
    }
}
