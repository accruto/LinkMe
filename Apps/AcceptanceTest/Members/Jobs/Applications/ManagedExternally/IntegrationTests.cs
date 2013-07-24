using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Xml;
using LinkMe.Apps.Presentation.Domain.Roles.Candidates;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Files;
using LinkMe.Domain.Roles.Candidates;
using LinkMe.Domain.Roles.Integration;
using LinkMe.Domain.Roles.Resumes;
using LinkMe.Domain.Roles.Resumes.Queries;
using LinkMe.Domain.Roles.Test.Candidates.Mocks;
using LinkMe.Domain.Roles.Test.Integration;
using LinkMe.Domain.Users.Anonymous.Queries;
using LinkMe.Domain.Users.Employers.Views.Queries;
using LinkMe.Framework.Utility.Urls;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Members.Jobs.Applications.ManagedExternally
{
    [TestClass]
    public class IntegrationTests
        : ManagedExternallyTests
    {
        private readonly IAnonymousUsersQuery _anonymousUsersQuery = Resolve<IAnonymousUsersQuery>();
        private readonly IResumesQuery _resumesQuery = Resolve<IResumesQuery>();
        private readonly IResumeFilesQuery _resumeFilesQuery = Resolve<IResumeFilesQuery>();
        private readonly IEmployerMemberViewsQuery _employerMemberViewsQuery = Resolve<IEmployerMemberViewsQuery>();

        private const string EmailAddress = "hsimpson@test.linkme.net.au";
        private const string FirstName = "Homer";
        private const string LastName = "Simpson";
        private static readonly Encoding Encoding = Encoding.GetEncoding(28591); // Latin-1

        private ReadOnlyUrl _statusUrl;

        [TestInitialize]
        public void TestInitialize()
        {
            _statusUrl = new ReadOnlyApplicationUrl("~/jobapplications/status");
        }

        [TestMethod]
        public void TestWithProfile()
        {
            var integratorUser = _integrationCommand.CreateTestIntegratorUser();
            var employer = CreateEmployer();
            var jobAd = CreateJobAd(employer, integratorUser);

            // Member with no resume file.

            var member = CreateMember();
            var resume = AddResume(member.Id);

            // Apply.

            LogIn(member);
            AssertStatus(jobAd, member.Id, false, true, null);
            var applicationId = AssertApply(ApiApplyWithProfile(jobAd.Id, null));
            AssertStatus(jobAd, member.Id, true, true, null);

            // Check the application.

            AssertResumeApplication(applicationId, jobAd, member.Id, null, GetResumeId(member.Id));
            AssertNoResumeFile(member.Id, resume.Id);
            AssertResumeFiles(member.Id);

            // Get the application.

            LogOut();
            var resumeUrl = AssertSuccess(GetJobApplication(integratorUser, applicationId), member, resume, null);
            AssertApplication(applicationId, jobAd, member.Id, null);
            AssertStatus(jobAd, member.Id, true, true, null);

            // Get the resume file.

            var resumeFileContents = GetResumeFileContents(resumeUrl, integratorUser);
            Assert.AreEqual(GetResumeFile(employer, member).Contents, resumeFileContents);
            AssertApplication(applicationId, jobAd, member.Id, null);
            AssertStatus(jobAd, member.Id, true, true, null);

            // Set status.

            AssertSuccess(SetJobApplicationsStatus(integratorUser, applicationId), 1, 0);
            AssertSubmittedApplication(applicationId, jobAd, member.Id, null);
            AssertStatus(jobAd, member.Id, true, true, null);
        }

        [TestMethod]
        public void TestWithUploadedResume()
        {
            var integratorUser = _integrationCommand.CreateTestIntegratorUser();
            var jobAd = CreateJobAd(CreateEmployer(), integratorUser);

            // Member with resume file to be uploaded.

            var member = CreateMember();
            var resume = AddResume(member.Id);
            var fileReference = GetResumeFile(TestResume.Complete);

            // Apply.

            LogIn(member);
            AssertStatus(jobAd, member.Id, false, true, null);
            var applicationId = AssertApply(ApiApplyWithUploadedResume(jobAd.Id, fileReference.Id, false, null));
            AssertStatus(jobAd, member.Id, true, true, fileReference.FileName);

            // Check the application.

            AssertResumeFileApplication(applicationId, jobAd, member.Id, null, fileReference.Id);
            AssertNoResumeFile(member.Id, resume.Id);
            AssertResumeFiles(member.Id, fileReference.Id);

            // Get the application.

            LogOut();
            var resumeUrl = AssertSuccess(GetJobApplication(integratorUser, applicationId), member, resume, fileReference);
            AssertApplication(applicationId, jobAd, member.Id, null);
            AssertStatus(jobAd, member.Id, true, true, fileReference.FileName);

            // Get the resume file.

            var resumeFileData = GetResumeFileData(resumeUrl, integratorUser);
            Assert.IsTrue(TestResume.Complete.GetData().SequenceEqual(resumeFileData));
            AssertApplication(applicationId, jobAd, member.Id, null);
            AssertStatus(jobAd, member.Id, true, true, fileReference.FileName);

            // Set status.

            AssertSuccess(SetJobApplicationsStatus(integratorUser, applicationId), 1, 0);
            AssertSubmittedApplication(applicationId, jobAd, member.Id, null);
            AssertStatus(jobAd, member.Id, true, true, fileReference.FileName);
        }

        [TestMethod]
        public void TestWithLastUsedResume()
        {
            var integratorUser = _integrationCommand.CreateTestIntegratorUser();
            var jobAd = CreateJobAd(CreateEmployer(), integratorUser);

            // Member with a resume file.

            var member = CreateMember();
            var resume = AddResume(member.Id);
            var fileReference = GetResumeFile(TestResume.Complete);
            _candidateResumeFilesCommand.CreateResumeFile(member.Id, new ResumeFileReference { FileReferenceId = fileReference.Id });

            // Apply.

            LogIn(member);
            AssertStatus(jobAd, member.Id, false, true, fileReference.FileName);
            var applicationId = AssertApply(ApiApplyWithLastUsedResume(jobAd.Id, null));
            AssertStatus(jobAd, member.Id, true, true, fileReference.FileName);

            // Check the application.

            AssertResumeFileApplication(applicationId, jobAd, member.Id, null, fileReference.Id);
            AssertNoResumeFile(member.Id, resume.Id);
            AssertResumeFiles(member.Id, fileReference.Id);

            // Get the application.

            LogOut();
            var resumeUrl = AssertSuccess(GetJobApplication(integratorUser, applicationId), member, resume, fileReference);
            AssertApplication(applicationId, jobAd, member.Id, null);
            AssertStatus(jobAd, member.Id, true, true, fileReference.FileName);

            // Get the resume file.

            var resumeFileData = GetResumeFileData(resumeUrl, integratorUser);
            Assert.IsTrue(TestResume.Complete.GetData().SequenceEqual(resumeFileData));
            AssertApplication(applicationId, jobAd, member.Id, null);
            AssertStatus(jobAd, member.Id, true, true, fileReference.FileName);

            // Set status.

            AssertSuccess(SetJobApplicationsStatus(integratorUser, applicationId), 1, 0);
            AssertSubmittedApplication(applicationId, jobAd, member.Id, null);
            AssertStatus(jobAd, member.Id, true, true, fileReference.FileName);
        }

        [TestMethod]
        public void TestAnonymously()
        {
            var integratorUser = _integrationCommand.CreateTestIntegratorUser();
            var employer = CreateEmployer();
            var jobAd = CreateJobAd(employer, integratorUser);

            Get(HomeUrl);
            var anonymousId = GetAnonymousId();

            // Apply.

            var fileReference = GetResumeFile(TestResume.Complete);
            View(jobAd.Id, () => AssertNotLoggedInView(false));
            var applicationId = AssertApply(ApplyAnonymously(employer, jobAd, fileReference.Id, EmailAddress, FirstName, LastName));

            // Check the application.

            var contact = _anonymousUsersQuery.GetContact(new AnonymousUser { Id = anonymousId }, new ContactDetails { EmailAddress = EmailAddress, FirstName = FirstName, LastName = LastName });
            AssertView(jobAd.Id, anonymousId);
            AssertApplication(applicationId, jobAd, contact.Id, null);

            // Get the application.

            var member = new Member
            {
                FirstName = FirstName,
                LastName = LastName,
                EmailAddresses = new List<EmailAddress> { new EmailAddress { Address = EmailAddress } }
            };

            LogOut();
            var resumeUrl = AssertSuccess(GetJobApplication(integratorUser, applicationId), member, null, fileReference);
            AssertApplication(applicationId, jobAd, contact.Id, null);

            // Get the resume file.

            var resumeFileData = GetResumeFileData(resumeUrl, integratorUser);
            Assert.IsTrue(TestResume.Complete.GetData().SequenceEqual(resumeFileData));
            AssertApplication(applicationId, jobAd, contact.Id, null);

            // Set status.

            AssertSuccess(SetJobApplicationsStatus(integratorUser, applicationId), 1, 0);
            AssertSubmittedApplication(applicationId, jobAd, contact.Id, null);
        }

        private Guid GetResumeId(Guid memberId)
        {
            return _candidatesQuery.GetCandidate(memberId).ResumeId.Value;
        }

        private static string GetJobApplication(IntegratorUser integratorUser, Guid applicationId)
        {
            return Get(GetJobApplicationUrl(applicationId), integratorUser, "password");
        }

        private static string GetResumeFileContents(ReadOnlyUrl resumeUrl, IntegratorUser integratorUser)
        {
            return Get(resumeUrl, integratorUser, "password");
        }

        private static byte[] GetResumeFileData(ReadOnlyUrl resumeUrl, IntegratorUser integratorUser)
        {
            return GetData(resumeUrl, integratorUser, "password");
        }

        private static ReadOnlyUrl AssertSuccess(string xml, IMember member, IResume resume, FileReference fileReference)
        {
            return AssertResponse(xml, "Success", member, resume, fileReference);
        }

        private static ReadOnlyUrl GetJobApplicationUrl(Guid applicationId)
        {
            return new ReadOnlyApplicationUrl("~/jobapplication/" + applicationId);
        }

        private static string Get(ReadOnlyUrl url, IntegratorUser user, string password)
        {
            var request = (HttpWebRequest)WebRequest.Create(url.AbsoluteUri);
            request.Method = "GET";
            request.UserAgent = "Mozilla/4.0 (compatible; MSIE 6.0)";
            request.ContentLength = 0;
            request.Expect = "";
            request.Timeout = Debugger.IsAttached ? -1 : 300000;
            request.Headers.Add("X-LinkMeUsername", user.LoginId);
            request.Headers.Add("X-LinkMePassword", password);

            using (var response = (HttpWebResponse)request.GetResponse())
            {
                var encoding = string.IsNullOrEmpty(response.ContentEncoding) ? Encoding : Encoding.GetEncoding(response.ContentEncoding);
                using (var reader = new StreamReader(response.GetResponseStream(), encoding))
                {
                    return reader.ReadToEnd();
                }
            }
        }

        private static byte[] GetData(ReadOnlyUrl url, IntegratorUser user, string password)
        {
            var request = (HttpWebRequest)WebRequest.Create(url.AbsoluteUri);
            request.Method = "GET";
            request.UserAgent = "Mozilla/4.0 (compatible; MSIE 6.0)";
            request.ContentLength = 0;
            request.Expect = "";
            request.Timeout = Debugger.IsAttached ? -1 : 300000;
            request.Headers.Add("X-LinkMeUsername", user.LoginId);
            request.Headers.Add("X-LinkMePassword", password);

            using (var response = (HttpWebResponse)request.GetResponse())
            {
                var responseStream = response.GetResponseStream();
                var stream = new MemoryStream();
                var buffer = new byte[65536];
                int read;
                do
                {
                    read = responseStream.Read(buffer, 0, buffer.Length);
                    stream.Write(buffer, 0, read);
                } while (read != 0);
                stream.Seek(0, SeekOrigin.Begin);

                var data = new byte[stream.Length];
                stream.Read(data, 0, (int)stream.Length);
                return data;
            }
        }

        private static ReadOnlyUrl AssertResponse(string xml, string returnCode, IMember member, IResume resume, FileReference fileReference, params string[] expectedErrors)
        {
            ReadOnlyUrl resumeUrl = null;

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
                    resumeUrl = AssertResume(xmlResume, fileReference);
                }
                else if (resume != null)
                {
                    var xmlResume = xmlJobApplication.SelectSingleNode("lm:Resume", xmlNsMgr);
                    resumeUrl = AssertResume(xmlResume, member);
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

            return resumeUrl;
        }

        private static ReadOnlyUrl AssertResume(XmlNode xmlResume, IRegisteredUser member)
        {
            var url = new ReadOnlyApplicationUrl(true, "~/resume/" + member.Id.ToString("n") + "/file/rtf");
            Assert.AreEqual(url.AbsoluteUri.ToLower(), xmlResume.Attributes["uri"].Value.ToLower());
            Assert.AreEqual(member.LastName + "_" + member.FirstName + "_LinkMeCV.doc", xmlResume.Attributes["name"].Value);
            return url;
        }

        private static ReadOnlyUrl AssertResume(XmlNode xmlResume, FileReference fileReference)
        {
            var url = new ReadOnlyApplicationUrl(true, "~/file/" + fileReference.Id + "/" + fileReference.FileName);
            Assert.IsTrue(url.AbsoluteUri.Equals(xmlResume.Attributes["uri"].Value, StringComparison.CurrentCultureIgnoreCase));
            Assert.AreEqual(fileReference.FileName, xmlResume.Attributes["name"].Value);
            return url;
        }

        private static void AssertApplicant(XmlNode xmlApplicant, XmlNamespaceManager xmlNsMgr, IMember member)
        {
            Assert.AreEqual(member.FirstName, xmlApplicant.Attributes["firstName"].Value);
            Assert.AreEqual(member.LastName, xmlApplicant.Attributes["lastName"].Value);
            Assert.AreEqual(member.GetBestEmailAddress().Address, xmlApplicant.Attributes["email"].Value);
            if (member.PhoneNumbers == null || member.PhoneNumbers.Count == 0)
                Assert.IsNull(xmlApplicant.Attributes["contactPhoneNumber"]);
            else
                Assert.AreEqual(member.GetBestPhoneNumber().Number, xmlApplicant.Attributes["contactPhoneNumber"].Value);

            var xmlAddress = xmlApplicant.SelectSingleNode("lm:Address", xmlNsMgr);
            if (member.Address == null || member.Address.Location == null)
            {
                Assert.IsNull(xmlAddress);
            }
            else
            {
                Assert.IsNotNull(xmlAddress);
                Assert.AreEqual(member.Address.Location.CountrySubdivision.ShortName, xmlAddress.Attributes["state"].Value);
                Assert.AreEqual(member.Address.Location.Postcode, xmlAddress.Attributes["postcode"].Value);
            }
        }

        private ResumeFile GetResumeFile(IEmployer employer, Member member)
        {
            var candidate = _candidatesQuery.GetCandidate(member.Id);
            var resume = candidate.ResumeId == null ? null : _resumesQuery.GetResume(candidate.ResumeId.Value);
            return _resumeFilesQuery.GetResumeFile(_employerMemberViewsQuery.GetEmployerMemberView(employer, member), resume);
        }

        private string SetJobApplicationsStatus(IntegratorUser integratorUser, params Guid[] applicationIds)
        {
            return SetJobApplicationsStatus(integratorUser, "Submitted", applicationIds);
        }

        private string SetJobApplicationsStatus(IntegratorUser integratorUser, string status, params Guid[] applicationIds)
        {
            return Post(_statusUrl, integratorUser, "password", CreateRequestXml(status, applicationIds));
        }

        private static string CreateRequestXml(string status, params Guid[] applicationIds)
        {
            var writer = GetXmlStringWriter();
            var xmlWriter = new XmlTextWriter(writer);

            xmlWriter.WriteStartDocument();
            xmlWriter.WriteStartElement("SetJobApplicationStatusRequest", "http://www.linkme.com.au/");
            xmlWriter.WriteAttributeString("version", "1.0");

            xmlWriter.WriteStartElement("JobApplications");
            foreach (var applicationId in applicationIds)
            {
                xmlWriter.WriteStartElement("JobApplication");
                xmlWriter.WriteAttributeString("id", applicationId.ToString("n"));
                xmlWriter.WriteAttributeString("status", status);
                xmlWriter.WriteEndElement();
            }

            xmlWriter.WriteEndDocument();
            xmlWriter.Flush();
            return writer.ToString();
        }

        private string Post(ReadOnlyUrl url, IntegratorUser user, string password, string requestXml)
        {
            Browser.RequestHeaders = new WebHeaderCollection
            {
                {"X-LinkMeUsername", user.LoginId},
                {"X-LinkMePassword", password}
            };

            return Post(url, "text/xml", requestXml);
        }

        private static void AssertSuccess(string xml, int updated, int failed)
        {
            AssertResponse(xml, "Success", updated, failed);
        }

        private static void AssertResponse(string xml, string returnCode, int updated, int failed, params string[] expectedErrors)
        {
            var xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(xml);

            var xmlNsMgr = new XmlNamespaceManager(xmlDoc.NameTable);
            xmlNsMgr.AddNamespace("lm", "http://www.linkme.com.au/");

            var xmlResponse = xmlDoc.SelectSingleNode("lm:SetJobApplicationStatusResponse", xmlNsMgr);
            Assert.IsNotNull(xmlResponse);

            var xmlReturnCode = xmlResponse.SelectSingleNode("lm:ReturnCode", xmlNsMgr);
            Assert.IsNotNull(xmlReturnCode);
            Assert.AreEqual(returnCode, xmlReturnCode.InnerText);

            var xmlJobApplications = xmlResponse.SelectSingleNode("lm:JobApplications", xmlNsMgr);
            if (xmlJobApplications == null)
            {
                Assert.AreEqual(0, updated);
                Assert.AreEqual(0, failed);
            }
            else
            {
                Assert.AreEqual(updated.ToString(), xmlJobApplications.Attributes["updated"].InnerText);
                Assert.AreEqual(failed.ToString(), xmlJobApplications.Attributes["failed"].InnerText);
            }

            var xmlErrors = xmlResponse.SelectNodes("lm:Errors/lm:Error", xmlNsMgr);
            Assert.IsNotNull(xmlErrors);
            Assert.AreEqual(expectedErrors.Length, xmlErrors.Count);
            for (var index = 0; index < expectedErrors.Length; ++index)
                Assert.AreEqual(expectedErrors[index], xmlErrors[index].InnerText);
        }
    }
}
