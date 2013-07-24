using System;
using System.Net;
using LinkMe.Apps.Asp.Test.Html;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Files;
using LinkMe.Domain.Files.Commands;
using LinkMe.Domain.Roles.JobAds;
using LinkMe.Domain.Test.Files;
using LinkMe.Domain.Users.Test.Employers.JobAds;
using LinkMe.Framework.Utility.Urls;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Members.Jobs
{
    [TestClass]
    public class LogoTests
        : JobsTests
    {
        private readonly IFilesCommand _filesCommand = Resolve<IFilesCommand>();

        [TestMethod]
        public void TestNoLogoNoLogoFeature()
        {
            var employer = CreateEmployer();
            var jobAd = CreateJobAd(employer, false, false);

            // Cannot see the logo.

            Get(HttpStatusCode.NotFound, GetLogoUrl(jobAd.Id));
            Get(GetJobAdUrl(jobAd));
            AssertLogo(false);
        }

        [TestMethod]
        public void TestLogoNoLogoFeature()
        {
            var employer = CreateEmployer();
            var jobAd = CreateJobAd(employer, true, false);

            // Cannot see the logo.

            Get(HttpStatusCode.NotFound, GetLogoUrl(jobAd.Id));
            Get(GetJobAdUrl(jobAd));
            AssertLogo(false);
        }

        [TestMethod]
        public void TestNoLogoLogoFeature()
        {
            var employer = CreateEmployer();
            var jobAd = CreateJobAd(employer, false, true);

            // Cannot see the logo.

            Get(HttpStatusCode.NotFound, GetLogoUrl(jobAd.Id));
            Get(GetJobAdUrl(jobAd));
            AssertLogo(false);
        }

        [TestMethod]
        public void TestLogoLogoFeature()
        {
            var employer = CreateEmployer();
            var jobAd = CreateJobAd(employer, true, true);

            // Can see the logo.

            Get(GetLogoUrl(jobAd.Id));
            Assert.AreEqual(HttpStatusCode.OK, Browser.CurrentStatusCode);
            Assert.AreEqual("image/jpeg", Browser.ResponseHeaders["Content-Type"]);

            Get(GetJobAdUrl(jobAd));
            AssertLogo(true);
        }

        [TestMethod, ExpectedException(typeof(NotFoundException))]
        public void TestUnknownLogo()
        {
            Get(GetLogoUrl(Guid.NewGuid()));
        }

        private JobAd CreateJobAd(IEmployer employer, bool hasLogo, bool hasLogoFeature)
        {
            var jobAd = _jobAdsCommand.PostTestJobAd(employer);

            if (hasLogo)
            {
                var fileReference = _filesCommand.CreateTestPhoto(0, FileType.CompanyLogo);
                jobAd.LogoId = fileReference.Id;
            }

            jobAd.Features = hasLogoFeature ? JobAdFeatures.Logo : JobAdFeatures.None;

            _jobAdsCommand.UpdateJobAd(jobAd);
            return jobAd;
        }

        private static ReadOnlyUrl GetLogoUrl(Guid jobAdId)
        {
            return new ReadOnlyApplicationUrl(true, "~/members/jobs/" + jobAdId + "/logo");
        }

        private void AssertLogo(bool expectedDisplayed)
        {
            var logoItem = Browser.CurrentHtml.DocumentNode.SelectSingleNode("//div[@class='logo-item']");
            Assert.IsNotNull(logoItem);
            var style = logoItem.Attributes["style"];
            if (expectedDisplayed)
            {
                Assert.IsNull(style);
            }
            else
            {
                Assert.IsNotNull(style);
                Assert.AreEqual("display:none;", style.Value);
            }
        }
    }
}
