using System;
using System.Net;
using LinkMe.Apps.Agents.Test.Users.Employers;
using LinkMe.Apps.Agents.Users.Employers.Commands;
using LinkMe.Apps.Asp.Test.Html;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Files;
using LinkMe.Domain.Files.Commands;
using LinkMe.Domain.Roles.JobAds;
using LinkMe.Domain.Roles.JobAds.Commands;
using LinkMe.Domain.Roles.Recruiters.Commands;
using LinkMe.Domain.Roles.Test.Recruiters;
using LinkMe.Domain.Test.Files;
using LinkMe.Domain.Users.Test.Anonymous.JobAds;
using LinkMe.Domain.Users.Test.Employers.JobAds;
using LinkMe.Framework.Utility.Urls;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Employers.JobAds
{
    [TestClass]
    public class LogoTests
        : WebTestClass
    {
        private readonly IEmployerAccountsCommand _employerAccountsCommand = Resolve<IEmployerAccountsCommand>();
        private readonly IOrganisationsCommand _organisationsCommand = Resolve<IOrganisationsCommand>();
        private readonly IJobAdsCommand _jobAdsCommand = Resolve<IJobAdsCommand>();
        private readonly IJobPostersCommand _jobPostersCommand = Resolve<IJobPostersCommand>();
        private readonly IFilesCommand _filesCommand = Resolve<IFilesCommand>();

        [TestMethod]
        public void TestLoggedInLogoFeature()
        {
            var employer = CreateEmployer(0);
            LogIn(employer);

            var jobAd = CreateJobAd(employer, true);
            Assert.IsNotNull(jobAd.LogoId);
            AssertLogo(jobAd.LogoId.Value);
        }

        [TestMethod]
        public void TestLoggedInNoLogoFeature()
        {
            var employer = CreateEmployer(0);
            LogIn(employer);

            var jobAd = CreateJobAd(employer, false);
            Assert.IsNotNull(jobAd.LogoId);
            AssertLogo(jobAd.LogoId.Value);
        }

        [TestMethod]
        public void TestNotLoggedInLogoFeature()
        {
            Get(HomeUrl);
            var anonymousId = GetAnonymousId();

            var jobAd = CreateJobAd(anonymousId, true);
            Assert.IsNotNull(jobAd.LogoId);
            AssertLogo(jobAd.LogoId.Value);
        }

        [TestMethod]
        public void TestNotLoggedInNoLogoFeature()
        {
            Get(HomeUrl);
            var anonymousId = GetAnonymousId();

            var jobAd = CreateJobAd(anonymousId, false);
            Assert.IsNotNull(jobAd.LogoId);
            AssertLogo(jobAd.LogoId.Value);
        }

        [TestMethod]
        public void TestOtherLoggedInLogoFeature()
        {
            var employer0 = CreateEmployer(0);
            var employer1 = CreateEmployer(1);
            LogIn(employer0);

            var jobAd = CreateJobAd(employer1, true);
            Assert.IsNotNull(jobAd.LogoId);
            AssertLogo(jobAd.LogoId.Value);
        }

        [TestMethod]
        public void TestOtherLoggedInNoLogoFeature()
        {
            var employer0 = CreateEmployer(0);
            var employer1 = CreateEmployer(1);
            LogIn(employer0);

            var jobAd = CreateJobAd(employer1, false);
            Assert.IsNotNull(jobAd.LogoId);
            AssertLogo(jobAd.LogoId.Value);
        }

        [TestMethod, ExpectedException(typeof(NotFoundException))]
        public void TestUnknownLogo()
        {
            Get(GetLogoUrl(Guid.NewGuid()));
        }

        private Employer CreateEmployer(int index)
        {
            return _employerAccountsCommand.CreateTestEmployer(index, _organisationsCommand.CreateTestOrganisation(index));
        }

        private JobAd CreateJobAd(IEmployer employer, bool hasLogoFeature)
        {
            var jobAd = _jobAdsCommand.PostTestJobAd(employer);
            AddLogo(jobAd, hasLogoFeature);
            return jobAd;
        }

        private JobAd CreateJobAd(Guid anonymousId, bool hasLogoFeature)
        {
            var poster = new JobPoster { Id = anonymousId };
            _jobPostersCommand.CreateJobPoster(poster);
            var jobAd = _jobAdsCommand.PostTestJobAd(new AnonymousUser { Id = anonymousId });
            AddLogo(jobAd, hasLogoFeature);
            return jobAd;
        }

        private void AddLogo(JobAd jobAd, bool hasLogoFeature)
        {
            var fileReference = _filesCommand.CreateTestPhoto(0, FileType.CompanyLogo);
            jobAd.LogoId = fileReference.Id;
            jobAd.Features = hasLogoFeature ? JobAdFeatures.Logo : JobAdFeatures.None;
            _jobAdsCommand.UpdateJobAd(jobAd);
        }

        private void AssertLogo(Guid fileId)
        {
            Get(GetLogoUrl(fileId));
            Assert.AreEqual(HttpStatusCode.OK, Browser.CurrentStatusCode);
            Assert.AreEqual("image/jpeg", Browser.ResponseHeaders["Content-Type"]);
        }

        private static ReadOnlyUrl GetLogoUrl(Guid fileId)
        {
            return new ReadOnlyApplicationUrl(true, "~/employers/jobads/logos/" + fileId);
        }
    }
}
