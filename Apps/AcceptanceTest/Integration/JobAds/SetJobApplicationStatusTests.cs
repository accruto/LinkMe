using System;
using System.Xml;
using LinkMe.Apps.Agents.Test.Users.Employers;
using LinkMe.Apps.Agents.Test.Users.Members;
using LinkMe.Apps.Agents.Users.Employers.Commands;
using LinkMe.Apps.Agents.Users.Members.Commands;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Contenders;
using LinkMe.Domain.Roles.Integration;
using LinkMe.Domain.Roles.Integration.Commands;
using LinkMe.Domain.Roles.JobAds;
using LinkMe.Domain.Roles.JobAds.Commands;
using LinkMe.Domain.Roles.Recruiters.Commands;
using LinkMe.Domain.Roles.Test.Integration;
using LinkMe.Domain.Roles.Test.JobAds;
using LinkMe.Domain.Roles.Test.Recruiters;
using LinkMe.Domain.Test.Data;
using LinkMe.Domain.Users.Employers.Applicants.Queries;
using LinkMe.Domain.Users.Employers.JobAds.Commands;
using LinkMe.Domain.Users.Members.JobAds.Queries;
using LinkMe.Domain.Users.Test.Employers.JobAds;
using LinkMe.Framework.Utility.Sql;
using LinkMe.Framework.Utility.Urls;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Application=LinkMe.Domain.Roles.Contenders.Application;

namespace LinkMe.AcceptanceTest.Integration.JobAds
{
    [TestClass]
    public class SetJobApplicationStatusTests
        : IntegrationTests
    {
        private readonly IMemberAccountsCommand _memberAccountsCommand = Resolve<IMemberAccountsCommand>();
        private readonly IEmployerAccountsCommand _employerAccountsCommand = Resolve<IEmployerAccountsCommand>();
        private readonly IOrganisationsCommand _organisationsCommand = Resolve<IOrganisationsCommand>();
        private readonly IJobAdsCommand _jobAdsCommand = Resolve<IJobAdsCommand>();
        private readonly IJobAdApplicantsQuery _jobAdApplicantsQuery = Resolve<IJobAdApplicantsQuery>();
        private readonly IJobAdApplicationSubmissionsCommand _jobAdApplicationSubmissionsCommand = Resolve<IJobAdApplicationSubmissionsCommand>();
        private readonly IMemberApplicationsQuery _memberApplicationsQuery = Resolve<IMemberApplicationsQuery>();
        private readonly IIntegrationCommand _integrationCommand = Resolve<IIntegrationCommand>();
            
        private ReadOnlyUrl _statusUrl;

        [TestInitialize]
        public void TestInitialize()
        {
            Resolve<IDbConnectionFactory>().DeleteAllTestData();
            _statusUrl = new ReadOnlyApplicationUrl("~/jobapplications/status");
        }

        [TestMethod]
        public void TestSetStatus()
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
            AssertStatus(application.Id, true, ApplicantStatus.New);

            // Set status.

            AssertSuccess(SetJobApplicationsStatus(integratorUser, application), 1, 0);
            AssertStatus(application.Id, false, ApplicantStatus.New);

            // Set status again.

            AssertSuccess(SetJobApplicationsStatus(integratorUser, application), 1, 0);
            AssertStatus(application.Id, false, ApplicantStatus.New);
        }

        [TestMethod]
        public void TestSetMultipleStatuses()
        {
            // Post job ad.

            var jobPoster = _employerAccountsCommand.CreateTestEmployer(0, _organisationsCommand.CreateTestOrganisation(0));
            var integratorUser = _integrationCommand.CreateTestIntegratorUser();
            var jobAd = PostJobAd(jobPoster, integratorUser);

            // Apply.

            var applicant0 = _memberAccountsCommand.CreateTestMember(0);
            var application0 = new InternalApplication { PositionId = jobAd.Id, ApplicantId = applicant0.Id };
            _jobAdApplicationSubmissionsCommand.CreateApplication(jobAd, application0);
            _jobAdApplicationSubmissionsCommand.SubmitApplication(jobAd, application0);
            AssertStatus(application0.Id, true, ApplicantStatus.New);

            // Apply.

            var applicant1 = _memberAccountsCommand.CreateTestMember(1);
            var application1 = new InternalApplication { PositionId = jobAd.Id, ApplicantId = applicant1.Id };
            _jobAdApplicationSubmissionsCommand.CreateApplication(jobAd, application1);
            _jobAdApplicationSubmissionsCommand.SubmitApplication(jobAd, application1);
            AssertStatus(application1.Id, true, ApplicantStatus.New);

            // Set status.

            AssertSuccess(SetJobApplicationsStatus(integratorUser, application0, application1), 2, 0);
            AssertStatus(application0.Id, false, ApplicantStatus.New);
            AssertStatus(application1.Id, false, ApplicantStatus.New);

            // Set status again.

            AssertSuccess(SetJobApplicationsStatus(integratorUser, application0), 1, 0);
            AssertStatus(application0.Id, false, ApplicantStatus.New);

            AssertSuccess(SetJobApplicationsStatus(integratorUser, application1), 1, 0);
            AssertStatus(application1.Id, false, ApplicantStatus.New);
        }

        [TestMethod]
        public void TestUnknownApplication()
        {
            var integratorUser = _integrationCommand.CreateTestIntegratorUser();

            // Apply.

            var application = new InternalApplication
            {
                Id = Guid.NewGuid(),
            };

            // Set status.

            AssertErrors(SetJobApplicationsStatus(integratorUser, application), 0, 1, "There is no job application with ID {" + application.Id + "}.");
        }

        [TestMethod]
        public void TestUnknownStatus()
        {
            // Post job ad.

            var jobPoster = _employerAccountsCommand.CreateTestEmployer(0, _organisationsCommand.CreateTestOrganisation(0));
            var integratorUser = _integrationCommand.CreateTestIntegratorUser();
            var jobAd = PostJobAd(jobPoster, integratorUser);

            // Apply.

            var applicant = _memberAccountsCommand.CreateTestMember(0);
            var application = new InternalApplication
            {
                PositionId = jobAd.Id,
                ApplicantId = applicant.Id,
            };
            _jobAdApplicationSubmissionsCommand.CreateApplication(jobAd, application);
            _jobAdApplicationSubmissionsCommand.SubmitApplication(jobAd, application);
            AssertStatus(application.Id, true, ApplicantStatus.New);

            // Set status.

            AssertErrors(SetJobApplicationsStatus(integratorUser, "Pending", application), 0, 0, "The value of the 'status' attribute, 'Pending', is not a valid application status.");
        }

        private void AssertStatus(Guid applicationId, bool isPending, ApplicantStatus status)
        {
            var application = _memberApplicationsQuery.GetInternalApplication(applicationId);
            Assert.AreEqual(isPending, application.IsPending);
            Assert.AreEqual(status, _jobAdApplicantsQuery.GetApplicantStatus(application.Id));
        }

        private JobAd PostJobAd(IEmployer jobPoster, IntegratorUser integratorUser)
        {
            var jobAd = jobPoster.CreateTestJobAd();
            jobAd.Integration.IntegratorUserId = integratorUser.Id;
            jobAd.Integration.ExternalApplyUrl = "http://test.external/ad";
            _jobAdsCommand.PostJobAd(jobAd);
            return jobAd;
        }

        private string SetJobApplicationsStatus(IntegratorUser integratorUser, params Application[] applications)
        {
            return SetJobApplicationsStatus(integratorUser, "Submitted", applications);
        }

        private string SetJobApplicationsStatus(IntegratorUser integratorUser, string status, params Application[] applications)
        {
            return Post(_statusUrl, integratorUser, "password", CreateRequestXml(status, applications));
        }

        private static string CreateRequestXml(string status, params Application[] applications)
        {
            var writer = GetXmlStringWriter();
            var xmlWriter = new XmlTextWriter(writer);

            xmlWriter.WriteStartDocument();
            xmlWriter.WriteStartElement("SetJobApplicationStatusRequest", "http://www.linkme.com.au/");
            xmlWriter.WriteAttributeString("version", "1.0");

            xmlWriter.WriteStartElement("JobApplications");
            foreach (var application in applications)
            {
                xmlWriter.WriteStartElement("JobApplication");
                xmlWriter.WriteAttributeString("id", application.Id.ToString("n"));
                xmlWriter.WriteAttributeString("status", status);
                xmlWriter.WriteEndElement();
            }

            xmlWriter.WriteEndDocument();
            xmlWriter.Flush();
            return writer.ToString();
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

        private static void AssertSuccess(string xml, int updated, int failed)
        {
            AssertResponse(xml, "Success", updated, failed);
        }

        private static void AssertErrors(string xml, int updated, int failed, params string[] expectedErrors)
        {
            AssertResponse(xml, "Errors", updated, failed, expectedErrors);
        }
    }
}