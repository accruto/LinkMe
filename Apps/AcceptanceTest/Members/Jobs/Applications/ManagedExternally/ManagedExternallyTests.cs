using System;
using HtmlAgilityPack;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Contenders;
using LinkMe.Domain.Roles.Integration;
using LinkMe.Domain.Roles.Integration.Commands;
using LinkMe.Domain.Roles.JobAds;
using LinkMe.Domain.Roles.Test.Integration;
using LinkMe.Domain.Test.Data;
using LinkMe.Environment;
using LinkMe.Framework.Utility;
using LinkMe.Framework.Utility.Sql;
using LinkMe.Framework.Utility.Urls;
using LinkMe.Web.Areas.Members.Models.JobAds;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Members.Jobs.Applications.ManagedExternally
{
    [TestClass]
    public abstract class ManagedExternallyTests
        : WebApplyTests
    {
        protected readonly IIntegrationCommand _integrationCommand = Resolve<IIntegrationCommand>();
        private const string ResumeFileName = "Complete.doc";
        protected readonly string _resumeFilePath = FileSystem.GetAbsolutePath(@"Test\Data\Resumes\" + ResumeFileName, RuntimeEnvironment.GetSourceFolder());

        [TestInitialize]
        public void ManagedExternallyTestsInitialize()
        {
            Resolve<IDbConnectionFactory>().DeleteAllTestData();
            _emailServer.ClearEmails();
        }

        protected override JobAd CreateJobAd(IEmployer employer)
        {
            return CreateJobAd(employer, _integrationCommand.CreateTestIntegratorUser());
        }

        protected JobAd CreateJobAd(IEmployer employer, IntegratorUser integratorUser)
        {
            var jobAd = base.CreateJobAd(employer);
            jobAd.Integration.ExternalApplyUrl = "http://www.google.com.au/";
            jobAd.Integration.IntegratorUserId = integratorUser.Id;
            _jobAdsCommand.UpdateJobAd(jobAd);
            return jobAd;
        }

        protected void AssertNotLoggedInView(bool hasQuestions)
        {
            Assert.IsNull(GetAppliedForExternallyNode());
            Assert.IsNull(GetManagedInternallyNode());
            Assert.IsNull(GetLoggedInUserNode());

            Assert.IsNotNull(GetManagedExternallyNode());
            Assert.IsNotNull(hasQuestions ? GetManagedExternallyQuestionsNode() : GetManagedExternallyNoQuestionsNode());
        }

        private HtmlNode GetManagedExternallyQuestionsNode()
        {
            return Browser.CurrentHtml.DocumentNode.SelectSingleNode("//div[@class='apply-button externalexpressapply']");
        }

        private HtmlNode GetManagedExternallyNoQuestionsNode()
        {
            return Browser.CurrentHtml.DocumentNode.SelectSingleNode("//a[@class='apply-button externalexpressapply']");
        }

        protected void AssertLoggedInView(bool hasResume, bool hasResumeFile, bool hasCoverLetter)
        {
            Assert.IsNull(GetAppliedForExternallyNode());
            Assert.IsNull(GetManagedInternallyNode());
            Assert.IsNull(GetManagedExternallyNode());

            Assert.IsNotNull(GetLoggedInUserNode());

            Assert.AreEqual(_coverLetterTextBox.IsVisible, hasCoverLetter);
            Assert.IsTrue(_profileRadioButton.IsVisible);
            Assert.IsTrue(_uploadedRadioButton.IsVisible);
            Assert.IsTrue(_lastUsedRadioButton.IsVisible);

            Assert.AreEqual(hasResume, _profileRadioButton.IsChecked);
            Assert.AreEqual(!hasResume && hasResumeFile, _lastUsedRadioButton.IsChecked);
            Assert.AreEqual(!hasResume && !hasResumeFile, _uploadedRadioButton.IsChecked);
        }

        protected JsonApplicationResponseModel ApplyAnonymously(IEmployer employer, JobAd jobAd, Guid fileReferenceId, string emailAddress, string firstName, string lastName)
        {
            var response = ApiApply(jobAd.Id, fileReferenceId, emailAddress, firstName, lastName);

            GetAppliedUrl(employer, jobAd);
            AssertPageContains("Go back to the job ad");

            return response;
        }

        protected void AssertNoApplication(Guid applicantId, Guid jobAdId)
        {
            AssertNoInternalApplication(applicantId, jobAdId);
        }

        protected InternalApplication AssertApplication(Guid applicationId, JobAd jobAd, Guid applicantId, string coverLetterText)
        {
            return AssertApplication(applicationId, jobAd, applicantId, coverLetterText, true);
        }

        protected InternalApplication AssertApplication(Guid applicationId, JobAd jobAd, Guid applicantId, string coverLetterText, bool hasSubmitted)
        {
            return AssertInternalApplication(applicationId, jobAd, applicantId, true, hasSubmitted, coverLetterText, null);
        }

        protected InternalApplication AssertSubmittedApplication(Guid applicationId, JobAd jobAd, Guid applicantId, string coverLetterText)
        {
            return AssertInternalApplication(applicationId, jobAd, applicantId, false, true, coverLetterText, null);
        }

        protected InternalApplication AssertResumeApplication(Guid applicationId, JobAd jobAd, Guid applicantId, string coverLetterText, Guid resumeId)
        {
            return AssertInternalApplication(applicationId, jobAd, applicantId, true, true, coverLetterText, a => { Assert.AreEqual(resumeId, a.ResumeId); Assert.IsNull(a.ResumeFileId); });
        }

        protected InternalApplication AssertResumeFileApplication(Guid applicationId, JobAd jobAd, Guid applicantId, string coverLetterText, Guid resumeFileId)
        {
            return AssertResumeFileApplication(applicationId, jobAd, applicantId, coverLetterText, resumeFileId, true);
        }

        protected InternalApplication AssertResumeFileApplication(Guid applicationId, JobAd jobAd, Guid applicantId, string coverLetterText, Guid resumeFileId, bool hasSubmitted)
        {
            return AssertInternalApplication(applicationId, jobAd, applicantId, true, hasSubmitted, coverLetterText, a => { Assert.IsNull(a.ResumeId); Assert.AreEqual(resumeFileId, a.ResumeFileId); });
        }

        protected void AssertRedirectToExternal(bool isLoggedIn, Guid applicationId, JobAdEntry jobAd)
        {
            var redirectUrl1 = GetRedirectToExternalUrl(jobAd.Id, applicationId);
            var redirectUrl2 = GetRedirectToExternalUrl(jobAd.Id);
            Get(redirectUrl1);
            AssertUrl(GetRedirectedUrl(jobAd, applicationId));
            Get(redirectUrl2);
            AssertUrl(isLoggedIn ? GetRedirectedUrl(jobAd, applicationId) : GetRedirectedUrl(jobAd));
        }

        private static ReadOnlyUrl GetRedirectToExternalUrl(Guid jobAdId)
        {
            return GetRedirectToExternalUrl(jobAdId, null);
        }

        private static ReadOnlyUrl GetRedirectToExternalUrl(Guid jobAdId, Guid? applicationId)
        {
            return applicationId == null
                ? new ReadOnlyUrl(new ReadOnlyApplicationUrl("~/members/jobs/" + jobAdId + "/redirecttoexternal").AbsoluteUri)
                : new ReadOnlyUrl(new ReadOnlyApplicationUrl("~/members/jobs/" + jobAdId + "/redirecttoexternal", new ReadOnlyQueryString("applicationId", applicationId.Value.ToString())).AbsoluteUri);
        }

        private static ReadOnlyUrl GetRedirectedUrl(JobAdEntry jobAd, Guid applicationId)
        {
            return new ReadOnlyUrl(
                jobAd.Integration.ExternalApplyUrl,
                new ReadOnlyQueryString(
                    "linkmeApplicationId", applicationId.ToString("n"),
                    "linkmeApplicationUri", new ReadOnlyApplicationUrl(true, "~/jobapplication/" + applicationId).AbsoluteUri));
        }

        private static ReadOnlyUrl GetRedirectedUrl(JobAdEntry jobAd)
        {
            return new ReadOnlyUrl(jobAd.Integration.ExternalApplyUrl);
        }

        protected void GetAppliedUrl(IEmployer employer, JobAd jobAd)
        {
            Get(GetAppliedUrl(jobAd.Id));
            AssertAppliedContact(GetAppliedContact(employer, jobAd));
        }

        protected virtual string GetAppliedContact(IEmployer employer, JobAd jobAd)
        {
            return jobAd.ContactDetails.FullName + " at " + employer.Organisation.FullName + ", " + jobAd.ContactDetails.PhoneNumber;
        }
    }
}