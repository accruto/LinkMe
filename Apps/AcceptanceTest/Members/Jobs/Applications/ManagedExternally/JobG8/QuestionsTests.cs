using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Test.Communications.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Members.Jobs.Applications.ManagedExternally.JobG8
{
    [TestClass]
    public abstract class QuestionsTests
        : JobG8Tests
    {
        const string NoQuestionsApplyForm = "<?xml version=\"1.0\" encoding=\"iso-8859-1\"?><Application JobReference=\"JS081021171146/AMY LEE/350723\" JobBoardID=\"52\" ResponseURI=\"http://www.jobg8.com/WebServices/ApplicationResponse.asmx\"><Question ID=\"11\" FormatID=\"15\"><Required>true</Required><Name>Email</Name><Text>Email Address</Text><Type>TEXTBOX</Type><Size>100</Size><Width>200</Width><Rows>0</Rows></Question><Question ID=\"CoverLetter\" FormatID=\"54\"><Required>false</Required><Name>Cover Letter Upload</Name><Text>Upload a covering letter.</Text><Type>FILEUPLOAD</Type><Size>255</Size><Width>275</Width><Rows>0</Rows></Question><Question ID=\"CV\" FormatID=\"53\"><Required>true</Required><Name>CV Upload</Name><Text>Upload your CV.</Text><Type>FILEUPLOAD</Type><Size>255</Size><Width>275</Width><Rows>0</Rows></Question><Question ID=\"2\" FormatID=\"2\"><Required>true</Required><Name>First Name</Name><Text>First Name</Text><Type>TEXTBOX</Type><Size>100</Size><Width>200</Width><Rows>0</Rows></Question><Question ID=\"3\" FormatID=\"5\"><Required>true</Required><Name>Last Name</Name><Text>Surname</Text><Type>TEXTBOX</Type><Size>100</Size><Width>200</Width><Rows>0</Rows></Question></Application>";

        [TestMethod]
        public void TestRequiredCoverLetter()
        {
            var jobAd = CreateJobAd(CreateEmployer());

            // Create a member and log in.

            var member = CreateMember(true);
            LogIn(member);

            AssertNoView(jobAd.Id, member.Id);
            AssertNoApplication(member.Id, jobAd.Id);

            // Apply for the job.

            View(jobAd.Id, () => AssertLoggedInView(true, true, true));
            var applicationId = AssertApply(ApiApplyWithProfile(jobAd.Id, null));
            var questionsUrl = GetQuestionsUrl(jobAd.Id, applicationId);
            Get(questionsUrl);

            // Phone number should be required.

            AnswerQuestions(null, VisaValue, null, MobileValue, AvailabilityValue, SalaryValue);
            AssertUrl(questionsUrl);
            AssertErrorMessage("Cover letter is required.");
            _emailServer.AssertNoEmailSent();
            AssertNoJobG8Request();

            // Supply the answer.

            AnswerQuestions(CoverLetterText, VisaValue, PhoneNumberValue, MobileValue, AvailabilityValue, SalaryValue);

            AssertView(jobAd.Id, member.Id);
            var application = AssertApplication(applicationId, jobAd, member.Id, CoverLetterText);
            _emailServer.AssertNoEmailSent();

            AssertJobG8Request(member, jobAd, application);
        }

        [TestMethod]
        public void TestRequiredQuestion()
        {
            var jobAd = CreateJobAd(CreateEmployer());

            // Create a member and log in.

            var member = CreateMember(true);
            LogIn(member);

            AssertNoView(jobAd.Id, member.Id);
            AssertNoApplication(member.Id, jobAd.Id);

            // Apply for the job.

            View(jobAd.Id, () => AssertLoggedInView(true, true, true));
            var applicationId = AssertApply(ApiApplyWithProfile(jobAd.Id, CoverLetterText));
            var questionsUrl = GetQuestionsUrl(jobAd.Id, applicationId);
            Get(questionsUrl);

            // Phone number should be required.

            AnswerQuestions(VisaValue, null, MobileValue, AvailabilityValue, SalaryValue);
            AssertUrl(questionsUrl);
            AssertErrorMessage("Home telephone no. is required.");
            _emailServer.AssertNoEmailSent();
            AssertNoJobG8Request();

            // Supply the answer.

            AnswerQuestions(VisaValue, PhoneNumberValue, MobileValue, AvailabilityValue, SalaryValue);

            AssertView(jobAd.Id, member.Id);
            var application = AssertApplication(applicationId, jobAd, member.Id, CoverLetterText);
            _emailServer.AssertNoEmailSent();

            AssertJobG8Request(member, jobAd, application);
        }

        [TestMethod]
        public void TestNotRequiredQuestion()
        {
            var jobAd = CreateJobAd(CreateEmployer());

            // Create a member and log in.

            var member = CreateMember(true);
            LogIn(member);

            AssertNoView(jobAd.Id, member.Id);
            AssertNoApplication(member.Id, jobAd.Id);

            // Apply for the job.

            View(jobAd.Id, () => AssertLoggedInView(true, true, true));
            var applicationId = AssertApply(ApiApplyWithProfile(jobAd.Id, CoverLetterText));
            var questionsUrl = GetQuestionsUrl(jobAd.Id, applicationId);
            Get(questionsUrl);

            // Mobile is not required.

            AnswerQuestions(VisaValue, PhoneNumberValue, null, AvailabilityValue, SalaryValue);

            AssertView(jobAd.Id, member.Id);
            var application = AssertApplication(applicationId, jobAd, member.Id, CoverLetterText);
            _emailServer.AssertNoEmailSent();

            AssertJobG8Request(member, jobAd, application);
        }

        [TestMethod]
        public void TestNoQuestions()
        {
            var jobAd = CreateJobAd(CreateEmployer(), NoQuestionsApplyForm);

            // Create a member and log in.

            var member = CreateMember(true);
            LogIn(member);

            AssertNoView(jobAd.Id, member.Id);
            AssertNoApplication(member.Id, jobAd.Id);

            // Apply for the job.

            View(jobAd.Id, () => AssertLoggedInView(true, true, true));
            var applicationId = AssertApply(ApiApplyWithProfile(jobAd.Id, CoverLetterText));
            var questionsUrl = GetQuestionsUrl(jobAd.Id, applicationId);
            Get(questionsUrl);

            // Since there are no questions should be moved on automatically.

            AssertAppliedUrl(jobAd, applicationId, member.GetBestEmailAddress().Address);

            AssertView(jobAd.Id, member.Id);
            var application = AssertApplication(applicationId, jobAd, member.Id, CoverLetterText);
            _emailServer.AssertNoEmailSent();

            AssertJobG8Request(member, jobAd, application);
        }
    }
}