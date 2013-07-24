using System;
using LinkMe.AcceptanceTest.Navigation;
using LinkMe.Apps.Agents.Test.Users.Employers;
using LinkMe.Apps.Agents.Test.Users.Members;
using LinkMe.Apps.Agents.Users.Employers.Commands;
using LinkMe.Apps.Agents.Users.Members.Commands;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Candidates.Commands;
using LinkMe.Domain.Roles.Candidates.Queries;
using LinkMe.Domain.Roles.JobAds;
using LinkMe.Domain.Roles.JobAds.Commands;
using LinkMe.Domain.Roles.Recruiters.Commands;
using LinkMe.Domain.Roles.Test.Candidates;
using LinkMe.Domain.Roles.Test.Recruiters;
using LinkMe.Domain.Users.Test.Employers.JobAds;
using LinkMe.Framework.Utility.Urls;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Members.Jobs
{
    [TestClass]
    public class JobsSwitchTests
        : SwitchTests
    {
        private readonly IEmployerAccountsCommand _employerAccountsCommand = Resolve<IEmployerAccountsCommand>();
        private readonly IOrganisationsCommand _organisationsCommand = Resolve<IOrganisationsCommand>();
        private readonly IJobAdsCommand _jobAdsCommand = Resolve<IJobAdsCommand>();
        private readonly IMemberAccountsCommand _memberAccountsCommand = Resolve<IMemberAccountsCommand>();
        private readonly ICandidatesQuery _candidatesQuery = Resolve<ICandidatesQuery>();
        private readonly ICandidateResumesCommand _candidateResumesCommand = Resolve<ICandidateResumesCommand>();

        [TestMethod]
        public void TestJob()
        {
            var employer = CreateEmployer();
            var jobAd = CreateJobAd(employer);

            Browser.UseMobileUserAgent = true;
            Get(GetJobUrl(jobAd.Id));

            var url = new ReadOnlyUrl(Browser.CurrentUrl);
            Get(url);
            AssertMobile();

            // Switch.

            AssertSwitch(false, url, url);
            AssertWeb();

            // Switch back.

            AssertSwitch(true, url, url);
            AssertMobile();
        }

        [TestMethod]
        public void TestApply()
        {
            var employer = CreateEmployer();
            var jobAd = CreateJobAd(employer);
            var member = CreateMember();

            LogIn(member);
            Browser.UseMobileUserAgent = true;

            Get(GetJobUrl(jobAd.Id));
            var jobUrl = new ReadOnlyUrl(Browser.CurrentUrl);
            var applyUrl = GetApplyUrl(jobAd.Id);

            Get(applyUrl);
            AssertMobile();

            // Switch. No web equivalent.

            AssertSwitch(false, applyUrl, jobUrl);
            AssertWeb();

            // Switch back.

            AssertSwitch(true, jobUrl, jobUrl);
            AssertMobile();
        }

        [TestMethod]
        public void TestEmail()
        {
            var employer = CreateEmployer();
            var jobAd = CreateJobAd(employer);

            Browser.UseMobileUserAgent = true;

            Get(GetJobUrl(jobAd.Id));
            var jobUrl = new ReadOnlyUrl(Browser.CurrentUrl);

            // Email.

            var emailUrl = GetEmailUrl(jobAd.Id);
            Get(emailUrl);
            AssertMobile();

            // Switch. No web equivalent.

            AssertSwitch(false, emailUrl, jobUrl);
            AssertWeb();

            // Switch back.

            AssertSwitch(true, jobUrl, jobUrl);
            AssertMobile();

            // Email sent.

            var emailSentUrl = GetEmailSentUrl(jobAd.Id);
            Get(emailSentUrl);
            AssertMobile();

            // Switch. No web equivalent.

            AssertSwitch(false, emailUrl, jobUrl);
            AssertWeb();

            // Switch back.

            AssertSwitch(true, jobUrl, jobUrl);
            AssertMobile();
        }

        private static ReadOnlyUrl GetJobUrl(Guid jobId)
        {
            return new ReadOnlyApplicationUrl(true, "~/jobs/" + jobId);
        }

        private static ReadOnlyUrl GetApplyUrl(Guid jobAdId)
        {
            return new ReadOnlyApplicationUrl(true, "~/members/jobs/" + jobAdId + "/apply");
        }

        private static ReadOnlyUrl GetEmailUrl(Guid jobAdId)
        {
            return new ReadOnlyApplicationUrl("~/members/jobs/" + jobAdId + "/email");
        }

        private static ReadOnlyUrl GetEmailSentUrl(Guid jobAdId)
        {
            return new ReadOnlyApplicationUrl("~/members/jobs/" + jobAdId + "/emailsent");
        }

        private Employer CreateEmployer()
        {
            return _employerAccountsCommand.CreateTestEmployer(0, _organisationsCommand.CreateTestOrganisation(0));
        }

        private Member CreateMember()
        {
            var member = _memberAccountsCommand.CreateTestMember(0);
            var candidate = _candidatesQuery.GetCandidate(member.Id);
            _candidateResumesCommand.AddTestResume(candidate);
            return member;
        }

        private JobAd CreateJobAd(IEmployer employer)
        {
            return _jobAdsCommand.PostTestJobAd(employer);
        }
    }
}
