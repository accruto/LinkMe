using System;
using LinkMe.Apps.Agents.Users;
using LinkMe.Apps.Asp.Json.Models;
using LinkMe.Apps.Services.External.CareerOne.Queries;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.JobAds;
using LinkMe.Framework.Utility;
using LinkMe.Framework.Utility.Unity;
using LinkMe.Framework.Utility.Urls;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Members.Jobs.Overlays
{
    [TestClass]
    public class OverlaysTests
        : JobsTests
    {
        private readonly ICareerOneQuery _careerOneQuery = Resolve<ICareerOneQuery>();
        private readonly int[] _occasionalPrompts = new[] { 3, 10, 25 };
        private readonly int[] _casualPrompts = new[] { 3, 10 };

        [TestMethod]
        public void TestOccasionalOverlay()
        {
            var employer = CreateEmployer();

            // Less than the first prompt is a new visitor.

            var view = 0;
            for (; view < _occasionalPrompts[0]; ++view)
                TestView(employer, null);

            // Prompt on the next view.

            TestView(employer, VisitorFrequency.Occasional);
            ++view;

            // Look for next prompts.

            var prompt = 1;
            for (; prompt < _occasionalPrompts.Length; ++prompt)
            {
                // No prompt.

                for (; view < _occasionalPrompts[prompt]; ++view)
                    TestView(employer, null);

                // Prompt.

                TestView(employer, VisitorFrequency.Occasional);
                ++view;
            }

            // After that no prompt.

            for (; view < 100; ++view)
                TestView(employer, null);
        }

        [TestMethod]
        public void TestCasualOverlay()
        {
            var employer = CreateEmployer();

            // Less than the first prompt is a new visitor.

            var application = 0;
            for (; application < _casualPrompts[0] - 1; ++application)
                TestApply(employer, null);

            // Prompt on the next application.

            TestApply(employer, VisitorFrequency.Casual);
            ++application;

            // Look for next prompts.

            var prompt = 1;
            for (; prompt < _casualPrompts.Length; ++prompt)
            {
                // No prompt.

                for (; application < _casualPrompts[prompt] - 1; ++application)
                    TestApply(employer, null);

                // Prompt.

                TestApply(employer, VisitorFrequency.Casual);
                ++application;
            }

            // After that no prompt.

            for (; application < 100; ++application)
                TestApply(employer, null);
        }

        private void TestView(IEmployer employer, VisitorFrequency? frequency)
        {
            var jobAd = CreateJobAd(employer);
            Get(GetJobAdUrl(jobAd.Id));

            // Look for the overlay.

            if (frequency == null)
            {
                AssertNoOverlay();
            }
            else
            {
                if (frequency.Value == VisitorFrequency.Occasional)
                {
                    // Both get shown for occasional users.

                    Assert.IsFalse(Browser.CurrentHtml.DocumentNode.SelectNodes("//div[@class='Occasional CallToActionOverlay']").IsNullOrEmpty());
                    Assert.IsFalse(Browser.CurrentHtml.DocumentNode.SelectNodes("//div[@class='Casual CallToActionOverlay']").IsNullOrEmpty());
                }
                else if (frequency.Value == VisitorFrequency.Casual)
                {
                    Assert.IsTrue(Browser.CurrentHtml.DocumentNode.SelectNodes("//div[@class='Occasional CallToActionOverlay']").IsNullOrEmpty());
                    Assert.IsFalse(Browser.CurrentHtml.DocumentNode.SelectNodes("//div[@class='Casual CallToActionOverlay']").IsNullOrEmpty());
                }
            }

            AssertJsonSuccess(Deserialize<JsonResponseModel>(Post(GetViewedUrl(jobAd.Id))));
        }

        private void TestApply(IEmployer employer, VisitorFrequency? frequency)
        {
            // Apply for the job.

            var jobAd = CreateJobAd(employer);
            AssertJsonSuccess(Deserialize<JsonResponseModel>(Post(GetExternallyAppliedUrl(jobAd.Id))));

            Get(GetJobAdUrl(jobAd.Id));
            if (frequency == null)
            {
                AssertNoOverlay();
            }
            else
            {
                if (frequency.Value == VisitorFrequency.Occasional)
                {
                    Assert.IsFalse(Browser.CurrentHtml.DocumentNode.SelectNodes("//div[@class='Occasional CallToActionOverlay']").IsNullOrEmpty());
                    Assert.IsTrue(Browser.CurrentHtml.DocumentNode.SelectNodes("//div[@class='Casual CallToActionOverlay']").IsNullOrEmpty());
                }
                else if (frequency.Value == VisitorFrequency.Casual)
                {
                    Assert.IsTrue(Browser.CurrentHtml.DocumentNode.SelectNodes("//div[@class='Occasional CallToActionOverlay']").IsNullOrEmpty());
                    Assert.IsFalse(Browser.CurrentHtml.DocumentNode.SelectNodes("//div[@class='Casual CallToActionOverlay']").IsNullOrEmpty());
                }
            }
        }

        private void AssertNoOverlay()
        {
            Assert.IsTrue(Browser.CurrentHtml.DocumentNode.SelectNodes("//div[@class='Occasional CallToActionOverlay']").IsNullOrEmpty());
            Assert.IsTrue(Browser.CurrentHtml.DocumentNode.SelectNodes("//div[@class='Casual CallToActionOverlay']").IsNullOrEmpty());
        }

        private static ReadOnlyUrl GetExternallyAppliedUrl(Guid jobAdId)
        {
            return new ReadOnlyApplicationUrl("~/members/jobs/api/" + jobAdId + "/externallyapplied");
        }

        private static ReadOnlyUrl GetViewedUrl(Guid jobAdId)
        {
            return new ReadOnlyApplicationUrl("~/members/jobs/api/" + jobAdId + "/viewed");
        }

        protected override JobAd CreateJobAd(IEmployer employer)
        {
            var jobAd = base.CreateJobAd(employer);
            jobAd.Integration.IntegratorUserId = _careerOneQuery.GetIntegratorUser().Id;
            jobAd.Integration.ExternalApplyUrl = "http://google.com";
            _jobAdsCommand.UpdateJobAd(jobAd);
            return jobAd;
        }
    }
}
