using System;
using System.IO;
using System.Xml;
using LinkMe.Apps.Agents.Test.Users.Employers;
using LinkMe.Apps.Agents.Test.Users.Members;
using LinkMe.Apps.Agents.Users.Employers.Commands;
using LinkMe.Apps.Agents.Users.Members.Commands;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Files;
using LinkMe.Domain.Files.Commands;
using LinkMe.Domain.Roles.Candidates.Commands;
using LinkMe.Domain.Roles.Candidates.Queries;
using LinkMe.Domain.Roles.Contenders;
using LinkMe.Domain.Roles.Integration;
using LinkMe.Domain.Roles.Integration.Commands;
using LinkMe.Domain.Roles.JobAds;
using LinkMe.Domain.Roles.JobAds.Commands;
using LinkMe.Domain.Roles.Recruiters.Commands;
using LinkMe.Domain.Roles.Resumes;
using LinkMe.Domain.Roles.Resumes.Commands;
using LinkMe.Domain.Roles.Test.Candidates.Mocks;
using LinkMe.Domain.Roles.Test.Integration;
using LinkMe.Domain.Roles.Test.JobAds;
using LinkMe.Domain.Roles.Test.Recruiters;
using LinkMe.Domain.Test.Data;
using LinkMe.Domain.Users.Employers.JobAds.Commands;
using LinkMe.Domain.Users.Test.Employers.JobAds;
using LinkMe.Framework.Utility.Sql;
using LinkMe.Framework.Utility.Unity;
using LinkMe.Framework.Utility.Urls;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Integration.JobAds
{
    [TestClass]
    public class JobApplicationTests
        : IntegrationTests
    {
        private readonly IMemberAccountsCommand _memberAccountsCommand = Resolve<IMemberAccountsCommand>();
        private readonly IEmployerAccountsCommand _employerAccountsCommand = Resolve<IEmployerAccountsCommand>();
        private readonly IOrganisationsCommand _organisationsCommand = Resolve<IOrganisationsCommand>();
        private readonly IJobAdsCommand _jobAdsCommand = Resolve<IJobAdsCommand>();
        private readonly IJobAdApplicationSubmissionsCommand _jobAdApplicationSubmissionsCommand = Resolve<IJobAdApplicationSubmissionsCommand>();
        private readonly IIntegrationCommand _integrationCommand = Resolve<IIntegrationCommand>();
        private readonly ICandidatesQuery _candidatesQuery = Resolve<ICandidatesQuery>();
        private readonly ICandidateResumesCommand _candidateResumesCommand = Resolve<ICandidateResumesCommand>();
        private readonly IParseResumesCommand _parseResumesCommand = Resolve<IParseResumesCommand>();
        private readonly IFilesCommand _filesCommand = Resolve<IFilesCommand>();

        [TestInitialize]
        public void TestInitialize()
        {
            Resolve<IDbConnectionFactory>().DeleteAllTestData();
        }

        [TestMethod]
        public void TestGetJobApplication()
        {
            // Post job ad.

            var jobPoster = _employerAccountsCommand.CreateTestEmployer(0, _organisationsCommand.CreateTestOrganisation(0));
            var integratorUser = _integrationCommand.CreateTestIntegratorUser();
            var jobAd = PostJobAd(jobPoster, integratorUser);

            // Apply.

            var applicant = _memberAccountsCommand.CreateTestMember(0);
            var application = new InternalApplication { PositionId = jobAd.Id, ApplicantId = applicant.Id };
            _jobAdApplicationSubmissionsCommand.CreateApplication(jobAd, application);
            _jobAdApplicationSubmissionsCommand.SubmitApplication(jobAd, application);

            // Get the application.

            AssertSuccess(GetJobApplication(integratorUser, application.Id), applicant, null, null);
        }

        [TestMethod]
        public void TestGetJobApplicationResume()
        {
            // Post job ad.

            var jobPoster = _employerAccountsCommand.CreateTestEmployer(0, _organisationsCommand.CreateTestOrganisation(0));
            var integratorUser = _integrationCommand.CreateTestIntegratorUser();
            var jobAd = PostJobAd(jobPoster, integratorUser);

            // Apply.

            var applicant = _memberAccountsCommand.CreateTestMember(0);
            var resume = AddResume(applicant.Id);

            var application = new InternalApplication { PositionId = jobAd.Id, ApplicantId = applicant.Id, ResumeId = resume.Id };
            _jobAdApplicationSubmissionsCommand.CreateApplication(jobAd, application);
            _jobAdApplicationSubmissionsCommand.SubmitApplication(jobAd, application);

            // Get the application.

            AssertSuccess(GetJobApplication(integratorUser, application.Id), applicant, resume, null);
        }

        [TestMethod]
        public void TestGetJobApplicationResumeFile()
        {
            // Post job ad.

            var jobPoster = _employerAccountsCommand.CreateTestEmployer(0, _organisationsCommand.CreateTestOrganisation(0));
            var integratorUser = _integrationCommand.CreateTestIntegratorUser();
            var jobAd = PostJobAd(jobPoster, integratorUser);

            // Apply.

            var applicant = _memberAccountsCommand.CreateTestMember(0);
            var fileReference = GetResumeFile();
            var resume = AddResume(applicant.Id, fileReference);

            var application = new InternalApplication { PositionId = jobAd.Id, ApplicantId = applicant.Id, ResumeFileId = fileReference.Id };
            _jobAdApplicationSubmissionsCommand.CreateApplication(jobAd, application);
            _jobAdApplicationSubmissionsCommand.SubmitApplication(jobAd, application);

            // Get the application.

            AssertSuccess(GetJobApplication(integratorUser, application.Id), applicant, resume, fileReference);
        }

        [TestMethod]
        public void TestUnknownJobApplication()
        {
            var integratorUser = _integrationCommand.CreateTestIntegratorUser();
            var applicationId = Guid.NewGuid();
            AssertErrors(GetJobApplication(integratorUser, applicationId), "There is no job application with ID '" + applicationId + "'.");
        }

        private JobAd PostJobAd(IEmployer jobPoster, IntegratorUser integratorUser)
        {
            var jobAd = jobPoster.CreateTestJobAd();
            jobAd.Integration.IntegratorUserId = integratorUser.Id;
            jobAd.Integration.ExternalApplyUrl = "http://test.external/ad";
            _jobAdsCommand.PostJobAd(jobAd);
            return jobAd;
        }

        private static string GetJobApplication(IntegratorUser integratorUser, Guid applicationId)
        {
            return Get(GetJobApplicationUrl(applicationId), integratorUser, "password", false);
        }

        protected Resume AddResume(Guid memberId)
        {
            var fileReference = GetResumeFile();
            var resume = _parseResumesCommand.ParseResume(fileReference).Resume;
            var candidate = _candidatesQuery.GetCandidate(memberId);
            _candidateResumesCommand.CreateResume(candidate, resume);
            return resume;
        }

        private Resume AddResume(Guid memberId, FileReference fileReference)
        {
            var resume = _parseResumesCommand.ParseResume(fileReference).Resume;
            var candidate = _candidatesQuery.GetCandidate(memberId);
            _candidateResumesCommand.CreateResume(candidate, resume, fileReference);
            return resume;
        }

        private FileReference GetResumeFile()
        {
            const string fileName = "resume.doc";
            var data = TestResume.Complete.GetData();
            using (var stream = new MemoryStream(data))
            {
                return _filesCommand.SaveFile(FileType.Resume, new StreamFileContents(stream), fileName);
            }
        }

        private static ReadOnlyUrl GetJobApplicationUrl(Guid applicationId)
        {
            return new ReadOnlyApplicationUrl("~/jobapplication/" + applicationId);
        }

        private static void AssertResponse(string xml, string returnCode, IMember member, IResume resume, FileReference fileReference, params string[] expectedErrors)
        {
            var xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(xml);

            var xmlNsMgr = new XmlNamespaceManager(xmlDoc.NameTable);
            xmlNsMgr.AddNamespace("lm", "http://www.linkme.com.au/");

            var xmlResponse = xmlDoc.SelectSingleNode("lm:GetJobApplicationResponse", xmlNsMgr);
            Assert.IsNotNull(xmlResponse);

            var xmlReturnCode = xmlResponse.SelectSingleNode("lm:ReturnCode", xmlNsMgr);
            Assert.IsNotNull(xmlReturnCode);
            Assert.AreEqual(returnCode, xmlReturnCode.InnerText);

            var xmlJobApplication = xmlResponse.SelectSingleNode("lm:JobApplication", xmlNsMgr);
            if (member != null)
            {
                Assert.IsNotNull(xmlJobApplication);
                var xmlApplicant = xmlJobApplication.SelectSingleNode("lm:Applicant", xmlNsMgr);
                AssertApplicant(xmlApplicant, xmlNsMgr, member);

                if (fileReference != null)
                {
                    var xmlResume = xmlJobApplication.SelectSingleNode("lm:Resume", xmlNsMgr);
                    AssertResume(xmlResume, fileReference);
                }
                else if (resume != null)
                {
                    var xmlResume = xmlJobApplication.SelectSingleNode("lm:Resume", xmlNsMgr);
                    AssertResume(xmlResume, member);
                }
            }
            else
            {
                Assert.IsNull(xmlJobApplication);
            }

            var xmlErrors = xmlResponse.SelectNodes("lm:Errors/lm:Error", xmlNsMgr);
            Assert.IsNotNull(xmlErrors);
            Assert.AreEqual(expectedErrors.Length, xmlErrors.Count);
            for (var index = 0; index < expectedErrors.Length; ++index)
                Assert.AreEqual(expectedErrors[index], xmlErrors[index].InnerText);
        }

        private static void AssertResume(XmlNode xmlResume, IRegisteredUser member)
        {
            Assert.AreEqual(new ReadOnlyApplicationUrl(true, "~/resume/" + member.Id.ToString("n") + "/file/rtf").AbsoluteUri.ToLower(), xmlResume.Attributes["uri"].Value.ToLower());
            Assert.AreEqual(member.LastName + "_" + member.FirstName + "_LinkMeCV.doc", xmlResume.Attributes["name"].Value);
        }

        private static void AssertResume(XmlNode xmlResume, FileReference fileReference)
        {
            Assert.IsTrue(new ReadOnlyApplicationUrl(true, "~/file/" + fileReference.Id + "/" + fileReference.FileName).AbsoluteUri.Equals(xmlResume.Attributes["uri"].Value, StringComparison.CurrentCultureIgnoreCase));
            Assert.AreEqual(fileReference.FileName, xmlResume.Attributes["name"].Value);
        }

        private static void AssertApplicant(XmlNode xmlApplicant, XmlNamespaceManager xmlNsMgr, IMember member)
        {
            Assert.AreEqual(member.FirstName, xmlApplicant.Attributes["firstName"].Value);
            Assert.AreEqual(member.LastName, xmlApplicant.Attributes["lastName"].Value);
            Assert.AreEqual(member.GetBestEmailAddress().Address, xmlApplicant.Attributes["email"].Value);
            Assert.AreEqual(member.GetBestPhoneNumber().Number, xmlApplicant.Attributes["contactPhoneNumber"].Value);

            var xmlAddress = xmlApplicant.SelectSingleNode("lm:Address", xmlNsMgr);
            Assert.IsNotNull(xmlAddress);
            Assert.AreEqual(member.Address.Location.CountrySubdivision.ShortName, xmlAddress.Attributes["state"].Value);
            Assert.AreEqual(member.Address.Location.Postcode, xmlAddress.Attributes["postcode"].Value);
        }

        private static void AssertSuccess(string xml, IMember member, IResume resume, FileReference fileReference)
        {
            AssertResponse(xml, "Success", member, resume, fileReference);
        }

        private static void AssertErrors(string xml, params string[] expectedErrors)
        {
            AssertResponse(xml, "Errors", null, null, null, expectedErrors);
        }
    }
}