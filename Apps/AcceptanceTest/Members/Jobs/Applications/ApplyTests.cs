using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using LinkMe.Apps.Agents.Test.Users.Employers;
using LinkMe.Apps.Agents.Test.Users.Members;
using LinkMe.Apps.Agents.Users.Employers.Commands;
using LinkMe.Apps.Agents.Users.Members.Commands;
using LinkMe.Apps.Asp.Json.Models;
using LinkMe.Apps.Presentation.Domain;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Files;
using LinkMe.Domain.Files.Commands;
using LinkMe.Domain.Files.Queries;
using LinkMe.Domain.Roles.Candidates;
using LinkMe.Domain.Roles.Candidates.Commands;
using LinkMe.Domain.Roles.Candidates.Queries;
using LinkMe.Domain.Roles.Contenders;
using LinkMe.Domain.Roles.JobAds;
using LinkMe.Domain.Roles.JobAds.Commands;
using LinkMe.Domain.Roles.JobAds.Queries;
using LinkMe.Domain.Roles.Recruiters.Commands;
using LinkMe.Domain.Roles.Resumes;
using LinkMe.Domain.Roles.Resumes.Commands;
using LinkMe.Domain.Roles.Test.Candidates;
using LinkMe.Domain.Roles.Test.Candidates.Mocks;
using LinkMe.Domain.Roles.Test.Communications.Mocks;
using LinkMe.Domain.Roles.Test.Recruiters;
using LinkMe.Domain.Test.Data;
using LinkMe.Domain.Users.Employers.Applicants.Queries;
using LinkMe.Domain.Users.Employers.JobAds.Queries;
using LinkMe.Domain.Users.Members.JobAds.Queries;
using LinkMe.Domain.Users.Members.Queries;
using LinkMe.Domain.Users.Test.Employers.JobAds;
using LinkMe.Framework.Communications;
using LinkMe.Framework.Utility;
using LinkMe.Framework.Utility.Files;
using LinkMe.Framework.Utility.Sql;
using LinkMe.Framework.Utility.Urls;
using LinkMe.Web.Areas.Members.Models.JobAds;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Application=LinkMe.Domain.Roles.Contenders.Application;

namespace LinkMe.AcceptanceTest.Members.Jobs.Applications
{
    [TestClass]
    public abstract class ApplyTests
        : WebTestClass
    {
        protected readonly IJobAdsCommand _jobAdsCommand = Resolve<IJobAdsCommand>();
        private readonly IEmployerAccountsCommand _employerAccountsCommand = Resolve<IEmployerAccountsCommand>();
        private readonly IOrganisationsCommand _organisationsCommand = Resolve<IOrganisationsCommand>();
        protected readonly IMemberAccountsCommand _memberAccountsCommand = Resolve<IMemberAccountsCommand>();
        protected readonly IMembersQuery _membersQuery = Resolve<IMembersQuery>();
        protected readonly ICandidatesQuery _candidatesQuery = Resolve<ICandidatesQuery>();
        private readonly ICandidateResumesCommand _candidateResumesCommand = Resolve<ICandidateResumesCommand>();
        protected readonly ICandidateResumeFilesCommand _candidateResumeFilesCommand = Resolve<ICandidateResumeFilesCommand>();
        protected readonly ICandidateResumeFilesQuery _candidateResumeFilesQuery = Resolve<ICandidateResumeFilesQuery>();
        private readonly ICandidatesRepository _candidatesRepository = Resolve<ICandidatesRepository>();
        private readonly IParseResumesCommand _parseResumesCommand = Resolve<IParseResumesCommand>();
        private readonly IFilesCommand _filesCommand = Resolve<IFilesCommand>();
        protected readonly IFilesQuery _filesQuery = Resolve<IFilesQuery>();
        private readonly IJobAdApplicantsQuery _jobAdApplicantsQuery = Resolve<IJobAdApplicantsQuery>();
        private readonly IJobAdApplicationSubmissionsQuery _jobAdApplicationSubmissionsQuery = Resolve<IJobAdApplicationSubmissionsQuery>();
        protected readonly IJobAdViewsQuery _jobAdViewsQuery = Resolve<IJobAdViewsQuery>();
        private readonly IMemberApplicationsQuery _memberApplicationsQuery = Resolve<IMemberApplicationsQuery>();

        protected const string ContactCompanyName = "The Contact Company";
        protected const string EmployerCompanyName = "The Employer Company";
        protected const string CoverLetterText = "Hi, please let me have a job.";

        private ReadOnlyUrl _apiJoinUrl;
        protected ReadOnlyUrl _previousApplicationsUrl;

        [TestInitialize]
        public void ApplyTestsInitialize()
        {
            Resolve<IDbConnectionFactory>().DeleteAllTestData();
            _emailServer.ClearEmails();

            _apiJoinUrl = new ReadOnlyApplicationUrl(true, "~/join/api");
            _previousApplicationsUrl = new ReadOnlyApplicationUrl(true, "~/ui/registered/networkers/PreviousApplications.aspx");
        }

        protected JsonApplicationResponseModel ApiApplyWithLastUsedResume(Guid jobAdId, string coverLetterText)
        {
            return Deserialize<JsonApplicationResponseModel>(Post(GetApiApplyWithLastUsedResumeUrl(jobAdId), new NameValueCollection { { "CoverLetterText", coverLetterText } }));
        }

        protected JsonApplicationResponseModel ApiApplyWithLastUsedResume(HttpStatusCode expectedStatusCode, Guid jobAdId, string coverLetterText)
        {
            return string.IsNullOrEmpty(coverLetterText)
                ? Deserialize<JsonApplicationResponseModel>(Post(expectedStatusCode, GetApiApplyWithLastUsedResumeUrl(jobAdId)))
                : Deserialize<JsonApplicationResponseModel>(Post(expectedStatusCode, GetApiApplyWithLastUsedResumeUrl(jobAdId), new NameValueCollection { { "CoverLetterText", coverLetterText } }));
        }

        protected JsonApplicationResponseModel ApiApplyWithProfile(Guid jobAdId, string coverLetterText)
        {
            return string.IsNullOrEmpty(coverLetterText)
                ? Deserialize<JsonApplicationResponseModel>(Post(GetApiApplyWithProfileUrl(jobAdId)))
                : Deserialize<JsonApplicationResponseModel>(Post(GetApiApplyWithProfileUrl(jobAdId), new NameValueCollection { { "CoverLetterText", coverLetterText } }));
        }

        protected JsonApplicationResponseModel ApiApplyWithProfile(HttpStatusCode expectedStatusCode, Guid jobAdId, string coverLetterText)
        {
            return Deserialize<JsonApplicationResponseModel>(Post(expectedStatusCode, GetApiApplyWithProfileUrl(jobAdId), new NameValueCollection { { "CoverLetterText", coverLetterText } }));
        }

        protected JsonApplicationResponseModel ApiApplyWithUploadedResume(Guid jobAdId, Guid fileReferenceId, bool useForProfile, string coverLetterText)
        {
            var parameters = new NameValueCollection
            {
                {"FileReferenceId", fileReferenceId.ToString()},
                {"UseForProfile", useForProfile ? "true" : "false"},
            };

            if (!string.IsNullOrEmpty(coverLetterText))
                parameters.Add("CoverLetterText", coverLetterText);

            return Deserialize<JsonApplicationResponseModel>(Post(GetApiApplyWithUploadedResumeUrl(jobAdId), parameters));
        }

        protected JsonApplicationResponseModel ApiApplyWithUploadedResume(HttpStatusCode expectedStatusCode, Guid jobAdId, Guid fileReferenceId, bool useForProfile, string coverLetterText)
        {
            var parameters = new NameValueCollection
            {
                {"FileReferenceId", fileReferenceId.ToString()},
                {"UseForProfile", useForProfile ? "true" : "false"},
                {"CoverLetterText", coverLetterText},
            };
            return Deserialize<JsonApplicationResponseModel>(Post(expectedStatusCode, GetApiApplyWithUploadedResumeUrl(jobAdId), parameters));
        }

        protected JsonApplicationResponseModel ApiApply(Guid jobAdId, Guid fileReferenceId, string emailAddress, string firstName, string lastName)
        {
            var parameters = new NameValueCollection
            {
                {"FileReferenceId", fileReferenceId.ToString()},
                {"EmailAddress", emailAddress},
                {"FirstName", firstName},
                {"LastName", lastName},
            };
            return Deserialize<JsonApplicationResponseModel>(Post(GetApiApplyUrl(jobAdId), parameters));
        }

        protected Guid AssertApply(JsonApplicationResponseModel model)
        {
            AssertJsonSuccess(model);
            return model.Id;
        }

        protected JsonApplicationResponseModel ApiApply(HttpStatusCode expectedStatusCode, Guid jobAdId, Guid fileReferenceId, string emailAddress, string firstName, string lastName)
        {
            var parameters = new NameValueCollection
            {
                {"FileReferenceId", fileReferenceId.ToString()},
                {"EmailAddress", emailAddress},
                {"FirstName", firstName},
                {"LastName", lastName},
            };
            return Deserialize<JsonApplicationResponseModel>(Post(expectedStatusCode, GetApiApplyUrl(jobAdId), parameters));
        }

        protected JsonResponseModel ApiExternallyApplied(Guid jobAdId)
        {
            return Deserialize<JsonResponseModel>(Post(GetApiExternallyAppliedUrl(jobAdId)));
        }

        protected JsonResponseModel ApiViewed(Guid jobAdId)
        {
            return Deserialize<JsonResponseModel>(Post(GetApiViewedUrl(jobAdId)));
        }

        protected JsonResponseModel ApiJoin(string firstName, string lastName, string emailAddress, string password)
        {
            return Deserialize<JsonResponseModel>(Post(_apiJoinUrl, GetJoinParameters(firstName, lastName, emailAddress, password, password, true)));
        }

        private static NameValueCollection GetJoinParameters(string firstName, string lastName, string emailAddress, string password, string confirmPassword, bool acceptTerms)
        {
            return new NameValueCollection
            {
                {"FirstName", firstName},
                {"LastName", lastName},
                {"EmailAddress", emailAddress},
                {"JoinPassword", password},
                {"JoinConfirmPassword", confirmPassword},
                {"AcceptTerms", acceptTerms.ToString()},
            };
        }

        protected static ReadOnlyUrl GetJobAdUrl(Guid jobAdId)
        {
            return new ReadOnlyApplicationUrl("~/jobs/" + jobAdId);
        }

        private static ReadOnlyUrl GetApiApplyWithLastUsedResumeUrl(Guid jobAdId)
        {
            return new ReadOnlyApplicationUrl(true, "~/members/jobs/api/" + jobAdId + "/applywithlastusedresume");
        }

        private static ReadOnlyUrl GetApiApplyWithProfileUrl(Guid jobAdId)
        {
            return new ReadOnlyApplicationUrl(true, "~/members/jobs/api/" + jobAdId + "/applywithprofile");
        }

        private static ReadOnlyUrl GetApiApplyWithUploadedResumeUrl(Guid jobAdId)
        {
            return new ReadOnlyApplicationUrl(true, "~/members/jobs/api/" + jobAdId + "/applywithuploadedresume");
        }

        private static ReadOnlyUrl GetApiApplyUrl(Guid jobAdId)
        {
            return new ReadOnlyApplicationUrl(true, "~/members/jobs/api/" + jobAdId + "/apply");
        }

        protected static ReadOnlyUrl GetApplyUrl(Guid jobAdId)
        {
            return new ReadOnlyApplicationUrl(true, "~/members/jobs/" + jobAdId + "/apply");
        }

        protected static ReadOnlyUrl GetAppliedUrl(Guid jobAdId)
        {
            return new ReadOnlyApplicationUrl(true, "~/members/jobs/" + jobAdId + "/applied");
        }

        protected static ReadOnlyUrl GetQuestionsUrl(Guid jobAdId, Guid applicationId)
        {
            return new ReadOnlyApplicationUrl(true, "~/members/jobs/" + jobAdId + "/questions", new ReadOnlyQueryString("applicationId", applicationId.ToString()));
        }

        private static ReadOnlyUrl GetApiExternallyAppliedUrl(Guid jobAdId)
        {
            return new ReadOnlyApplicationUrl(true, "~/members/jobs/api/" + jobAdId + "/externallyapplied");
        }

        private static ReadOnlyUrl GetApiViewedUrl(Guid jobAdId)
        {
            return new ReadOnlyApplicationUrl(true, "~/members/jobs/api/" + jobAdId + "/viewed");
        }

        protected void View(Guid jobAdId, Action assertView)
        {
            Get(GetJobAdUrl(jobAdId));
            assertView();
            ApiViewed(jobAdId);
        }

        protected void AssertNoView(Guid jobAdId, Guid viewerId)
        {
            Assert.IsFalse(_jobAdViewsQuery.HasViewedJobAd(viewerId, jobAdId));
            Assert.AreEqual(0, _jobAdViewsQuery.GetViewedJobAdIds(viewerId, new List<Guid>{jobAdId}).Count);
        }

        protected void AssertView(Guid jobAdId, Guid viewerId)
        {
            Assert.IsTrue(_jobAdViewsQuery.HasViewedJobAd(viewerId, jobAdId));
            Assert.IsTrue(_jobAdViewsQuery.GetViewedJobAdIds(viewerId, new List<Guid>{jobAdId}).CollectionEqual(new[] { jobAdId }));
        }

        private static void AssertApplication<TApplication>(TApplication application, Guid applicationId, Guid jobAdId, Guid applicantId, Action<TApplication> assertApplication)
            where TApplication : Application
        {
            Assert.IsNotNull(application);
            Assert.AreEqual(applicationId, application.Id);
            Assert.AreEqual(jobAdId, application.PositionId);
            Assert.AreEqual(applicantId, application.ApplicantId);
            Assert.IsTrue(application.CreatedTime > DateTime.Now.AddHours(-1));

            if (assertApplication != null)
                assertApplication(application);
        }

        protected void AssertNoExternalApplication(Guid applicantId, Guid jobAdId)
        {
            Assert.IsNull(_memberApplicationsQuery.GetExternalApplication(applicantId, jobAdId));
        }

        protected ExternalApplication AssertExternalApplication(Guid applicantId, Guid jobAdId)
        {
            var application = _memberApplicationsQuery.GetExternalApplication(applicantId, jobAdId);
            AssertApplication(application, application.Id, jobAdId, applicantId, null);
            return application;
        }

        protected void AssertNoInternalApplication(Guid applicantId, Guid jobAdId)
        {
            Assert.IsNull(_memberApplicationsQuery.GetInternalApplication(applicantId, jobAdId));
            Assert.IsNull(_jobAdApplicantsQuery.GetApplication(applicantId, jobAdId));
        }

        protected InternalApplication AssertInternalApplication(Guid applicationId, JobAdEntry jobAd, Guid applicantId, bool isPending, bool hasSubmitted, string coverLetterText, Action<InternalApplication> assertApplication)
        {
            var application = _memberApplicationsQuery.GetInternalApplication(applicationId);
            AssertApplication(application, applicationId, jobAd.Id, applicantId, a => AssertInternalApplication(a, isPending, coverLetterText, assertApplication));
            Assert.AreEqual(hasSubmitted, _jobAdApplicationSubmissionsQuery.HasSubmittedApplication(applicantId, jobAd.Id));
            return application;
        }

        private static void AssertInternalApplication(InternalApplication application, bool isPending, string coverLetterText, Action<InternalApplication> assertApplication)
        {
            Assert.AreEqual(isPending, application.IsPending);
            Assert.AreEqual(coverLetterText, application.CoverLetterText);
            if (assertApplication != null)
                assertApplication(application);
        }

        protected void AssertStatus(JobAd jobAd, Guid applicantId, bool expectedHasSubmitted, bool expectedHasResume, string expectedLastUsedResumeFileName)
        {
            Assert.AreEqual(expectedHasSubmitted, _jobAdApplicationSubmissionsQuery.HasSubmittedApplication(applicantId, jobAd.Id));

            var candidate = _candidatesQuery.GetCandidate(applicantId);
            Assert.AreEqual(expectedHasResume, candidate != null && candidate.ResumeId != null);

            var lastUserResumeFileName = GetLastUsedResumeFileName(applicantId);
            if (expectedLastUsedResumeFileName == null)
            {
                Assert.IsNull(lastUserResumeFileName);
            }
            else
            {
                Assert.IsNotNull(lastUserResumeFileName);
                Assert.AreEqual(expectedLastUsedResumeFileName, lastUserResumeFileName);
            }
        }

        protected void AssertAppliedContact(string contact)
        {
            var node = Browser.CurrentHtml.DocumentNode.SelectSingleNode("//div[@class='contact']");
            Assert.IsNotNull(node);
            Assert.AreEqual("Please contact <b>" + contact + "</b> directly if you have any questions about the progress of your application.", node.InnerHtml);
        }

        private string GetLastUsedResumeFileName(Guid applicantId)
        {
            var resumeFile = _candidateResumeFilesQuery.GetLastUsedResumeFile(applicantId);
            if (resumeFile == null)
                return null;
            var fileReference = _filesQuery.GetFileReference(resumeFile.FileReferenceId);
            return fileReference == null ? null : fileReference.FileName;
        }

        protected Employer CreateEmployer()
        {
            return _employerAccountsCommand.CreateTestEmployer(0, _organisationsCommand.CreateTestOrganisation(0));
        }

        protected virtual JobAd CreateJobAd(IEmployer employer)
        {
            var jobAd = _jobAdsCommand.PostTestJobAd(employer);
            jobAd.ContactDetails.CompanyName = ContactCompanyName;
            jobAd.Description.CompanyName = EmployerCompanyName;
            _jobAdsCommand.UpdateJobAd(jobAd);
            return jobAd;
        }

        protected Member CreateMember()
        {
            return _memberAccountsCommand.CreateTestMember(0);
        }

        protected Resume AddResume(Guid memberId)
        {
            var fileReference = GetResumeFile();
            var resume = _parseResumesCommand.ParseResume(fileReference).Resume;
            var candidate = _candidatesQuery.GetCandidate(memberId);
            _candidateResumesCommand.CreateResume(candidate, resume);
            return resume;
        }

        protected Resume AddResume(Guid memberId, FileReference fileReference)
        {
            var resume = _parseResumesCommand.ParseResume(fileReference).Resume;
            var candidate = _candidatesQuery.GetCandidate(memberId);
            _candidateResumesCommand.CreateResume(candidate, resume, fileReference);
            return resume;
        }

        protected FileReference GetResumeFile()
        {
            return GetResumeFile(TestResume.Complete);
        }

        protected FileReference GetResumeFile(TestResume testResume)
        {
            const string fileName = "resume.doc";
            var data = testResume.GetData();
            using (var stream = new MemoryStream(data))
            {
                return _filesCommand.SaveFile(FileType.Resume, new StreamFileContents(stream), fileName);
            }
        }

        protected Member CreateMember(bool withResume)
        {
            var member = _memberAccountsCommand.CreateTestMember(0);
            if (withResume)
            {
                var candidate = _candidatesQuery.GetCandidate(member.Id);
                _candidateResumesCommand.AddTestResume(candidate);
            }

            return member;
        }

        protected void AssertNoResumeFile(Guid memberId, Guid resumeId)
        {
            Assert.IsNull(_candidateResumeFilesQuery.GetResumeFile(resumeId));
        }

        protected void AssertResumeFile(Guid memberId, Guid resumeId, Guid fileReferenceId)
        {
            var resumeFile = _candidateResumeFilesQuery.GetResumeFile(resumeId);
            Assert.AreEqual(fileReferenceId, resumeFile.FileReferenceId);
        }

        protected void AssertResumeFiles(Guid memberId, params Guid[] fileReferenceIds)
        {
            var resumeFiles = _candidateResumeFilesQuery.GetResumeFiles(memberId);
            Assert.AreEqual(fileReferenceIds.Length, resumeFiles.Count);
            foreach (var fileReferenceId in fileReferenceIds)
                Assert.IsTrue((from r in resumeFiles where r.FileReferenceId == fileReferenceId select r).Any());

            foreach (var fileReferenceId in fileReferenceIds)
            {
                var resumeFile = _candidateResumeFilesQuery.GetResumeFile(memberId, fileReferenceId);
                Assert.AreEqual(fileReferenceId, resumeFile.FileReferenceId);
            }
        }

        protected void UpdateLastUsedTime(Guid memberId, Guid fileReferenceId, DateTime time)
        {
            var resumeFile = _candidatesRepository.GetResumeFile(memberId, fileReferenceId);
            resumeFile.LastUsedTime = time;
            _candidatesRepository.UpdateResumeFile(resumeFile);
        }

        protected void AssertLastUsedTime(Guid memberId, Guid fileReferenceId, DateTime date)
        {
            var resumeFile = _candidatesRepository.GetResumeFile(memberId, fileReferenceId);
            Assert.AreEqual(date, resumeFile.LastUsedTime.Date);
        }

        protected void AssertPreviousApplication(JobAd jobAd, Application application)
        {
            Get(_previousApplicationsUrl);
            AssertUrl(_previousApplicationsUrl);

            var nodes = Browser.CurrentHtml.DocumentNode.SelectNodes("//table[@id='job-ad-table']//td");
            Assert.AreEqual(4, nodes.Count);

            var a = nodes[0].SelectSingleNode("./a");
            Assert.AreEqual(GetJobText(jobAd), a.InnerText.Trim());
            Assert.AreEqual(application.CreatedTime.ToShortDateString(), nodes[1].SelectSingleNode("./div").InnerText.Trim());
            Assert.AreEqual(GetStatusText(jobAd, application), nodes[2].InnerText.Trim());
        }

        private static string GetJobText(JobAd jobAd)
        {
            var text = jobAd.Title;
            if (jobAd.Description.Salary != null)
            {
                var displayText = jobAd.Description.Salary.GetDisplayText();
                if (!string.IsNullOrEmpty(displayText))
                    text += ", " + displayText;
            }

            return text;
        }

        private static string GetStatusText(JobAdEntry jobAd, Application application)
        {
            if (application is ExternalApplication)
                return "Managed externally";

            if (((InternalApplication) application).IsPending)
            {
                return !string.IsNullOrEmpty(jobAd.Integration.ExternalApplyUrl)
                    ? "Pending (external site)Complete application"
                    : "Managed externally";
            }

            return "New";
        }

        protected void AssertEmail(Member member, Employer employer, string coverLetter, string resumeFileName)
        {
            var email = _emailServer.AssertEmailSent();
            email.AssertAddresses(member, Return, new EmailRecipient(employer.EmailAddress.Address));
            email.AssertHtmlViewContains(coverLetter);
            email.AssertAttachment(resumeFileName, MediaType.Word);
        }
    }
}