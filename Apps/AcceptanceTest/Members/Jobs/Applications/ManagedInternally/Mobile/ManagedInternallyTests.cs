using System;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Contenders;
using LinkMe.Domain.Roles.JobAds;
using LinkMe.Environment;
using LinkMe.Framework.Utility;
using LinkMe.Web.Areas.Members.Models.JobAds;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Members.Jobs.Applications.ManagedInternally.Mobile
{
    [TestClass]
    public abstract class ManagedInternallyTests
        : MobileApplyTests
    {
        private const string ResumeFileName = "Complete.doc";
        protected readonly string _resumeFilePath = FileSystem.GetAbsolutePath(@"Test\Data\Resumes\" + ResumeFileName, RuntimeEnvironment.GetSourceFolder());

        protected JsonApplicationResponseModel Apply(IEmployer employer, JobAd jobAd, bool useProfile, string coverLetterText)
        {
            Get(GetApplyUrl(jobAd.Id));
            var div = Browser.CurrentHtml.DocumentNode.SelectSingleNode("//div[@class='button apply']");
            Assert.IsNotNull(div);

            var response = useProfile
                ? ApiApplyWithProfile(jobAd.Id, coverLetterText)
                : ApiApplyWithLastUsedResume(jobAd.Id, coverLetterText);

            Get(GetAppliedUrl(jobAd.Id));
            AssertPageContains("Your application has been submitted");
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