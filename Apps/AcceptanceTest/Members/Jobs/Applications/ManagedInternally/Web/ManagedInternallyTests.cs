using System;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Contenders;
using LinkMe.Domain.Roles.JobAds;
using LinkMe.Domain.Test.Data;
using LinkMe.Environment;
using LinkMe.Framework.Utility;
using LinkMe.Framework.Utility.Sql;
using LinkMe.Web.Areas.Members.Models.JobAds;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Members.Jobs.Applications.ManagedInternally.Web
{
    [TestClass]
    public abstract class ManagedInternallyTests
        : WebApplyTests
    {
        private const string ResumeFileName = "Complete.doc";
        protected readonly string _resumeFilePath = FileSystem.GetAbsolutePath(@"Test\Data\Resumes\" + ResumeFileName, RuntimeEnvironment.GetSourceFolder());

        [TestInitialize]
        public void TestInitialize()
        {
            Resolve<IDbConnectionFactory>().DeleteAllTestData();
            _emailServer.ClearEmails();
        }

        protected void AssertView()
        {
            Assert.IsNull(GetAppliedForExternallyNode());
            Assert.IsNull(GetManagedExternallyNode());

            Assert.IsNotNull(GetManagedInternallyNode());
            Assert.IsNull(GetLoggedInUserNode());
        }

        protected void AssertView(bool hasResume, bool hasResumeFile)
        {
            Assert.IsNull(GetAppliedForExternallyNode());
            Assert.IsNull(GetManagedExternallyNode());

            Assert.IsNull(GetManagedInternallyNode());
            Assert.IsNotNull(GetLoggedInUserNode());

            Assert.IsTrue(_coverLetterTextBox.IsVisible);
            Assert.IsTrue(_profileRadioButton.IsVisible);
            Assert.IsTrue(_uploadedRadioButton.IsVisible);
            Assert.IsTrue(_lastUsedRadioButton.IsVisible);

            Assert.AreEqual(hasResume, _profileRadioButton.IsChecked);
            Assert.AreEqual(!hasResume && hasResumeFile, _lastUsedRadioButton.IsChecked);
            Assert.AreEqual(!hasResume && !hasResumeFile, _uploadedRadioButton.IsChecked);
        }

        protected JsonApplicationResponseModel Apply(IEmployer employer, JobAd jobAd, bool useProfile, string coverLetterText)
        {
            var response = useProfile
                ? ApiApplyWithProfile(jobAd.Id, coverLetterText)
                : ApiApplyWithLastUsedResume(jobAd.Id, coverLetterText);

            Get(GetAppliedUrl(jobAd.Id));
            AssertPageContains("Go back to the job ad");
            AssertAppliedContact(jobAd.ContactDetails.FullName + " at " + employer.Organisation.FullName + ", " + jobAd.ContactDetails.PhoneNumber);

            return response;
        }

        protected JsonApplicationResponseModel Apply(IEmployer employer, JobAd jobAd, Guid fileReferenceId, bool useForProfile, string coverLetterText)
        {
            var response = ApiApplyWithUploadedResume(jobAd.Id, fileReferenceId, useForProfile, coverLetterText);

            Get(GetAppliedUrl(jobAd.Id));
            AssertPageContains("Go back to the job ad");
            AssertAppliedContact(jobAd.ContactDetails.FullName + " at " + employer.Organisation.FullName + ", " + jobAd.ContactDetails.PhoneNumber);

            return response;
        }

        protected string GetProfileResumeFileName(IRegisteredUser member)
        {
            return member.FullName + ".doc";
        }

        protected void AssertNoApplication(Guid applicantId, Guid jobAdId)
        {
            AssertNoInternalApplication(applicantId, jobAdId);
        }

        protected InternalApplication AssertApplication(Guid applicationId, JobAdEntry jobAd, Guid applicantId, string coverLetterText)
        {
            return AssertInternalApplication(applicationId, jobAd, applicantId, false, true, coverLetterText, null);
        }
    }
}