using System;
using System.IO;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Files;
using LinkMe.Domain.Files.Commands;
using LinkMe.Domain.Roles.JobAds;
using LinkMe.Domain.Roles.Resumes.Commands;
using LinkMe.Domain.Roles.Test.Candidates.Mocks;
using LinkMe.Domain.Roles.Test.JobAds;
using LinkMe.Domain.Users.Members.JobAds.Commands;
using LinkMe.Domain.Users.Test.Employers.JobAds;
using LinkMe.Query.Search.JobAds;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.TaskRunner.Test.Tasks.EmailJobSearchAlerts
{
    [TestClass]
    public class HasAppliedTests
        : EmailJobSearchAlertsTaskTests
    {
        private readonly IInternalApplicationsCommand _internalApplicationsCommand = Resolve<IInternalApplicationsCommand>();
        private readonly IFilesCommand _filesCommand = Resolve<IFilesCommand>();
        private readonly IParseResumesCommand _parseResumesCommand = Resolve<IParseResumesCommand>();

        [TestMethod]
        public void TestIgnoreHasApplied()
        {
            TestHasApplied(null);
        }

        [TestMethod]
        public void TestHasApplied()
        {
            TestHasApplied(true);
        }

        [TestMethod]
        public void TestHasNotApplied()
        {
            TestHasApplied(false);
        }

        private void TestHasApplied(bool? hasApplied)
        {
            var employer = CreateEmployer();
            var jobAd1 = PostJobAd(employer);
            var jobAd2 = PostJobAd(employer);

            var member = CreateMember();
            _internalApplicationsCommand.SubmitApplication(member, jobAd2, null);

            // Search.

            var criteria = new JobAdSearchCriteria
            {
                AdTitle = BusinessAnalyst,
                HasApplied = hasApplied
            };
            var search = new JobAdSearch { Criteria = criteria };
            _jobAdSearchAlertsCommand.CreateJobAdSearchAlert(member.Id, search, DateTime.Now.AddDays(-6));

            // Get the email.

            var email = ExecuteTask();

            var expectedJobAds = hasApplied == null
                ? new[] { jobAd1, jobAd2 }
                : hasApplied.Value
                    ? new[] { jobAd2 }
                    : new[] { jobAd1 };
            AssertJobAds(email, expectedJobAds);
        }

        private Member CreateMember()
        {
            var member = CreateMember(0);

            const string fileName = "resume.doc";
            var data = TestResume.Complete.GetData();
            FileReference fileReference;
            using (var stream = new MemoryStream(data))
            {
                fileReference = _filesCommand.SaveFile(FileType.Resume, new StreamFileContents(stream), fileName);
            }

            var resume = _parseResumesCommand.ParseResume(fileReference).Resume;
            var candidate = _candidatesCommand.GetCandidate(member.Id);
            _candidateResumesCommand.CreateResume(candidate, resume);
            return member;
        }

        private JobAdEntry PostJobAd(IEmployer employer)
        {
            var jobAd = employer.CreateTestJobAd(BusinessAnalyst);
            jobAd.CreatedTime = DateTime.Now.AddDays(-1);
            _jobAdsCommand.PostJobAd(jobAd);
            return jobAd;
        }
    }
}
