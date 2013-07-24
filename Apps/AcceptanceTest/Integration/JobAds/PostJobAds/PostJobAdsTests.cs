using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Xml;
using System.Xml.Serialization;
using LinkMe.Apps.Agents.Test.Users.Employers;
using LinkMe.Apps.Agents.Users.Employers.Commands;
using LinkMe.Apps.Services.JobAds;
using LinkMe.Domain;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Credits;
using LinkMe.Domain.Credits.Commands;
using LinkMe.Domain.Credits.Queries;
using LinkMe.Domain.Industries;
using LinkMe.Domain.Roles.Integration;
using LinkMe.Domain.Roles.Integration.Commands;
using LinkMe.Domain.Roles.JobAds;
using LinkMe.Domain.Roles.JobAds.Queries;
using LinkMe.Domain.Roles.Recruiters.Commands;
using LinkMe.Domain.Roles.Test.Integration;
using LinkMe.Domain.Roles.Test.Recruiters;
using LinkMe.Domain.Test.Data;
using LinkMe.Framework.Utility;
using LinkMe.Framework.Utility.Sql;
using LinkMe.Framework.Utility.Urls;
using LinkMe.Web.Configuration;
using LinkMe.Web.Service;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Constants = LinkMe.Apps.Services.Constants;

namespace LinkMe.AcceptanceTest.Integration.JobAds.PostJobAds
{
    [TestClass]
    public abstract class PostJobAdsTests
        : IntegrationTests
    {
        protected readonly IEmployerAccountsCommand _employerAccountsCommand = Resolve<IEmployerAccountsCommand>();
        protected readonly IOrganisationsCommand _organisationsCommand = Resolve<IOrganisationsCommand>();
        protected readonly IJobAdsQuery _jobAdsQuery = Resolve<IJobAdsQuery>();
        protected readonly IJobAdIntegrationQuery _jobAdIntegrationQuery = Resolve<IJobAdIntegrationQuery>();
        private readonly IIntegrationCommand _integrationCommand = Resolve<IIntegrationCommand>();
        protected readonly IAllocationsCommand _allocationsCommand = Resolve<IAllocationsCommand>();
        protected readonly IAllocationsQuery _allocationsQuery = Resolve<IAllocationsQuery>();
        protected readonly ICreditsQuery _creditsQuery = Resolve<ICreditsQuery>();
        private static readonly XmlSerializer Serializer = new XmlSerializer(typeof(JobAdElement), null, new Type[] { }, null, Constants.XmlSerializationNamespace);

        private ReadOnlyUrl _jobAdsUrl;
        private ReadOnlyUrl _closeUrl;
        protected ReadOnlyUrl _searchUrl;
        protected ReadOnlyUrl _searchResultsUrl;

        [TestInitialize]
        public void PostJobAdsTestsInitialize()
        {
            Resolve<IDbConnectionFactory>().DeleteAllTestData();
            ClearSearchIndexes();

            _jobAdsUrl = new ReadOnlyApplicationUrl("~/jobads");
            _closeUrl = new ReadOnlyApplicationUrl("~/jobads/close");
            _searchUrl = new ReadOnlyApplicationUrl("~/search/jobs");
            _searchResultsUrl = new ReadOnlyApplicationUrl("~/search/jobs/results");
        }

        #region Static methods

        protected IntegratorUser CreateIntegratorUser(int index, IntegratorPermissions permissions)
        {
            return _integrationCommand.CreateTestIntegratorUser("IntegratorUser" + index, "password", permissions);
        }

        protected IntegratorUser CreateIntegratorUser(int index)
        {
            return _integrationCommand.CreateTestIntegratorUser("IntegratorUser" + index, "password", IntegratorPermissions.PostJobAds);
        }

        protected Employer CreateEmployer(int index)
        {
            var employer = _employerAccountsCommand.CreateTestEmployer(index, _organisationsCommand.CreateTestOrganisation(index));
            _allocationsCommand.CreateAllocation(new Allocation { CreditId = _creditsQuery.GetCredit<JobAdCredit>().Id, InitialQuantity = null, OwnerId = employer.Id });
            _allocationsCommand.CreateAllocation(new Allocation { CreditId = _creditsQuery.GetCredit<ApplicantCredit>().Id, InitialQuantity = null, OwnerId = employer.Id });
            return employer;
        }

        protected static JobAdElement CreateJobAd(int index)
        {
            return CreateJobAd(index, null);
        }

        protected static JobAdElement CreateJobAd(int index, Action<JobAdElement> action)
        {
            var jobAd = new JobAdElement
            {
                Content = "Original test ad content " + index,
                ContactDetails = new ContactDetails
                {
                    FirstName = "Contact",
                    LastName = "Person",
                    EmailAddress = "original" + index + "@contact.com",
                    FaxNumber = "02 1234 " + index.ToString("0000"),
                    PhoneNumber = "02 3456 " + index.ToString("0000")
                },
                Status = null,
                Title = "Original test ad title " + index,
                PositionTitle = "Test position " + index,
                ResidencyRequired = true,
                JobTypes = JobTypes.PartTime | JobTypes.Contract,
                Industries = new List<string> { "Accounting", "Other" },
                EmployerCompanyName = "Great Employer " + index,
                Summary = "Original ad summary " + index,
                Salary = new Salary { LowerBound = 70000, UpperBound = 90000, Rate = SalaryRate.Year, Currency = Currency.AUD },
                PackageDetails = "Company car",
                ExternalReferenceId = "TEST" + index.ToString("0000"),
                BulletPoints = new[] { "One point", "Two points" },
                Location = "Sydney",
                Postcode = "2000"
            };

            if (action != null)
                action(jobAd);

            return jobAd;
        }

        protected JobAd AssertJobAd(IntegratorUser integratorUser, Guid employerId, string externalReferenceId, JobAdStatus expectedStatus)
        {
            var jobAds = _jobAdsQuery.GetJobAds<JobAd>(_jobAdIntegrationQuery.GetJobAdIds(integratorUser.Id, employerId, externalReferenceId));
            Assert.AreEqual(1, jobAds.Count);
            return AssertJobAd(jobAds[0], integratorUser, externalReferenceId, expectedStatus);
        }

        protected JobAd AssertJobAd(JobAd jobAd, IntegratorUser integratorUser, string externalReferenceId, JobAdStatus expectedStatus)
        {
            Assert.IsNotNull(jobAd);
            Assert.AreEqual(integratorUser.Id, jobAd.Integration.IntegratorUserId);
            Assert.AreEqual(externalReferenceId, jobAd.Integration.ExternalReferenceId);
            Assert.AreEqual(expectedStatus, jobAd.Status);
            return jobAd;
        }

        private static XmlDocument CreateDocument(string xml)
        {
            var xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(xml);
            return xmlDocument;
        }

        private static XmlNamespaceManager CreateNamespaceManager(XmlDocument xmlDocument)
        {
            var xmlNsMgr = new XmlNamespaceManager(xmlDocument.NameTable);
            xmlNsMgr.AddNamespace("lm", WebConstants.WEB_SERVICE_NAMESPACE);
            return xmlNsMgr;
        }

        private static XmlNode SelectResponse(XmlNode xmlDocument, XmlNamespaceManager xmlNsMgr, string rootElementName, WebServiceReturnCode returnCode)
        {
            // Select the node.

            var xmlResponse = xmlDocument.SelectSingleNode("lm:" + rootElementName, xmlNsMgr);
            Assert.IsNotNull(xmlResponse);

            // Assert the return code.

            var xmlReturnCode = xmlResponse.SelectSingleNode("lm:ReturnCode", xmlNsMgr);
            Assert.IsNotNull(xmlReturnCode);

            Assert.AreEqual(
                returnCode.ToString(),
                xmlReturnCode.InnerText,
                string.Format("The expected return code was '{0}', but the actual return code was '{1}'. Response XML: {2}", returnCode, xmlReturnCode.InnerText, xmlDocument.InnerXml));

            return xmlResponse;
        }

        private static void AssertResponse(string responseXml, string rootElementName, int added, int updated, int closed, int failed, string[] errorSubstrings)
        {
            // Load the XML.

            var xmlDocument = CreateDocument(responseXml);
            var xmlNsMgr = CreateNamespaceManager(xmlDocument);

            // Select the top level response element.

            var returnCode = errorSubstrings.IsNullOrEmpty() ? WebServiceReturnCode.Success : WebServiceReturnCode.Errors;
            var xmlResponse = SelectResponse(xmlDocument, xmlNsMgr, rootElementName, returnCode);

            // Assert stats.

            var xmlJobAds = xmlResponse.SelectSingleNode("lm:JobAds", xmlNsMgr);
            Assert.IsNotNull(xmlJobAds);
            if (added != -1)
                AssertAttributeValue(xmlJobAds, "added", added.ToString(CultureInfo.InvariantCulture));
            if (updated != -1)
                AssertAttributeValue(xmlJobAds, "updated", updated.ToString(CultureInfo.InvariantCulture));
            AssertAttributeValue(xmlJobAds, "closed", closed.ToString(CultureInfo.InvariantCulture));
            AssertAttributeValue(xmlJobAds, "failed", failed.ToString(CultureInfo.InvariantCulture));

            // Assert errors.

            if (returnCode == WebServiceReturnCode.Errors)
                AssertErrors(xmlResponse, xmlNsMgr, errorSubstrings);
        }

        private static void AssertFailure(string responseXml, string rootElementName, string[] errorSubstrings)
        {
            // Load the XML.

            var xmlDocument = CreateDocument(responseXml);
            var xmlNsMgr = CreateNamespaceManager(xmlDocument);

            // Select the top level response element.

            const WebServiceReturnCode returnCode = WebServiceReturnCode.Failure;
            var xmlResponse = SelectResponse(xmlDocument, xmlNsMgr, rootElementName, returnCode);

            // Assert errors.

            AssertErrors(xmlResponse, xmlNsMgr, errorSubstrings);
        }

        private static void AssertErrors(XmlNode xmlResponse, XmlNamespaceManager xmlNsMgr, string[] errorSubstrings)
        {
            // Select the error nodes.

            var xmlErrors = xmlResponse.SelectNodes("lm:Errors/lm:Error", xmlNsMgr);
            Assert.IsNotNull(xmlErrors);
            Assert.IsTrue(xmlErrors.Count > 0);

            // Look for all errors.

            var errorsToFind = new ArrayList(errorSubstrings);
            foreach (XmlNode xmlError in xmlErrors)
            {
                for (var i = 0; i < errorsToFind.Count; i++)
                {
                    if (xmlError.InnerText.IndexOf((string)errorsToFind[i], StringComparison.Ordinal) != -1)
                    {
                        errorsToFind.RemoveAt(i);
                        break;
                    }
                }
            }

            if (errorsToFind.Count > 0)
            {
                Assert.Fail("The following error sub-strings were not found in the response XML: '"
                    + string.Join("', '", (string[])errorsToFind.ToArray(typeof(string)))
                        + "'. The error XML was: " + xmlResponse.OuterXml);
            }
        }

        private static void AssertAttributeValue(XmlNode xmlElement, string attributeName, string expected)
        {
            var xmlAttribute = xmlElement.Attributes.GetNamedItem(attributeName);
            Assert.IsNotNull(xmlAttribute);
            Assert.AreEqual(expected, xmlAttribute.InnerText, "XML attribute '" + attributeName + "' value was '" + xmlAttribute.InnerText + "', but the expected value was '" + expected + "'.");
        }

        protected void PostJobAds(IntegratorUser integratorUser, Employer jobPoster, bool closeAllOtherAds, IEnumerable<JobAdElement> jobAds, int added, int updated, int closed, int failed, params string[] errorSubStrings)
        {
            // Create request.

            var requestXml = CreatePostRequestXml(jobPoster, closeAllOtherAds, jobAds);

            // Invoke.

            var responseXml = Post(_jobAdsUrl, integratorUser, Password, requestXml);

            // Assert.

            AssertResponse(responseXml, "PostJobAdsResponse", added, updated, closed, failed, errorSubStrings);
        }

        protected void PostJobAdsFailure(IntegratorUser integratorUser, Employer jobPoster, bool closeAllOtherAds, IEnumerable<JobAdElement> jobAds, params string[] errorSubStrings)
        {
            // Create request.

            var requestXml = CreatePostRequestXml(jobPoster, closeAllOtherAds, jobAds);

            // Invoke.

            var responseXml = Post(_jobAdsUrl, integratorUser, Password, requestXml);

            // Assert.

            AssertFailure(responseXml, "PostJobAdsResponse", errorSubStrings);
        }

        protected void CloseJobAds(IntegratorUser integratorUser, Employer jobPoster, IEnumerable<JobAdElement> jobAds, int closed, int failed, params string[] errorSubStrings)
        {
            // Create request.

            var requestXml = CreateCloseRequestXml(jobPoster.GetLoginId(), jobAds);

            // Invoke.

            var responseXml = Post(_closeUrl, integratorUser, Password, requestXml);

            // Assert.

            AssertResponse(responseXml, "CloseJobAdsResponse", -1, -1, closed, failed, errorSubStrings);
        }

        private static string CreatePostRequestXml(Employer jobPoster, bool closeAllOtherAds, IEnumerable<JobAdElement> ads)
        {
            var writer = GetXmlStringWriter();
            var xmlWriter = new XmlTextWriter(writer);

            xmlWriter.WriteStartDocument();

            xmlWriter.WriteStartElement("PostJobAdsRequest", WebConstants.WEB_SERVICE_NAMESPACE);
            xmlWriter.WriteAttributeString("version", "1.1");

            xmlWriter.WriteStartElement("JobAds");
            if (jobPoster != null)
                xmlWriter.WriteAttributeString("jobPosterUserId", jobPoster.GetLoginId());
            xmlWriter.WriteAttributeString("closeAllOtherAds", XmlConvert.ToString(closeAllOtherAds));

            foreach (var ad in ads)
            {
                Serializer.Serialize(xmlWriter, ad);
            }

            xmlWriter.WriteEndDocument();
            xmlWriter.Flush();

            return writer.ToString();
        }

        private static string CreateCloseRequestXml(string jobPosterUserId, IEnumerable<JobAdElement> jobAds)
        {
            var writer = GetXmlStringWriter();
            var xmlWriter = new XmlTextWriter(writer);

            xmlWriter.WriteStartDocument();

            xmlWriter.WriteStartElement("CloseJobAdsRequest", WebConstants.WEB_SERVICE_NAMESPACE);
            xmlWriter.WriteAttributeString("version", "1.0");

            xmlWriter.WriteStartElement("JobAds");
            xmlWriter.WriteAttributeString("jobPosterUserId", jobPosterUserId);

            foreach (var jobAd in jobAds)
            {
                xmlWriter.WriteStartElement("JobAd", Constants.XmlSerializationNamespace);
                xmlWriter.WriteAttributeString("externalReferenceId", jobAd.ExternalReferenceId);
                xmlWriter.WriteEndElement();
            }

            xmlWriter.WriteEndDocument();
            xmlWriter.Flush();

            return writer.ToString();
        }

        protected void AssertJobAd(JobAdElement original, JobAd jobAd)
        {
            // BrandingLogo is ignored
            Assert.IsTrue(original.BulletPoints.SequenceEqual(jobAd.Description.BulletPoints));

            if (original.ContactDetails == null)
            {
                Assert.IsNull(jobAd.ContactDetails);
            }
            else
            {
                Assert.IsNotNull(jobAd.ContactDetails);
                Assert.AreEqual(original.ContactDetails.FirstName, jobAd.ContactDetails.FirstName);
                Assert.AreEqual(original.ContactDetails.LastName, jobAd.ContactDetails.LastName);
                Assert.AreEqual(original.ContactDetails.EmailAddress, jobAd.ContactDetails.EmailAddress);
                Assert.AreEqual(original.ContactDetails.FaxNumber, jobAd.ContactDetails.FaxNumber);
                Assert.AreEqual(original.ContactDetails.PhoneNumber, jobAd.ContactDetails.PhoneNumber);
            }

            Assert.AreEqual(original.Content, jobAd.Description.Content);
            // CreatedTime is ignored //Assert.AreEqual(original.CreatedTime, jobAd.CreatedTime);
            Assert.AreEqual(original.EmployerCompanyName, jobAd.Description.CompanyName);
            // ExpiryTime is ignored //Assert.AreEqual(original.ExpiryTime, jobAd.ExpiryTime);
            Assert.AreEqual(original.ExternalReferenceId, jobAd.Integration.ExternalReferenceId);

            if (original.Industries == null)
            {
                Assert.IsTrue(jobAd.Description.Industries == null || jobAd.Description.Industries.Count == 0);
            }
            else
            {
                Assert.IsNotNull(jobAd.Description.Industries);
                Assert.AreEqual(original.Industries.Count, jobAd.Description.Industries.Count);

                // Different order for industries is acceptable.

                foreach (var oIndustry in original.Industries)
                {
                    var dIndustry = FindIndustry(jobAd.Description.Industries, oIndustry);
                    Assert.IsNotNull(dIndustry);
                    Assert.AreEqual(oIndustry, dIndustry.Name);
                }
            }

            // Id is ignored
            // InternalReferenceId is ignored
            // JobPoster is ignored
            Assert.AreEqual(original.JobTypes, jobAd.Description.JobTypes);
            Assert.AreEqual(original.PackageDetails, jobAd.Description.Package);
            Assert.AreEqual(original.PositionTitle, jobAd.Description.PositionTitle);

            // Publishers is ignored
            Assert.AreEqual(original.ResidencyRequired, jobAd.Description.ResidencyRequired);
            Assert.AreEqual(original.Salary, jobAd.Description.Salary);
            // Status is ignored
            Assert.AreEqual(original.Summary, jobAd.Description.Summary);
            Assert.AreEqual(original.Title, jobAd.Title);

            // The location gets resolved.

            var location = original.Location + " "
                + (original.Postcode != null && _locationQuery.GetPostalCode(_locationQuery.GetCountry("Australia"), original.Postcode) != null ? original.Postcode : "");
            Assert.AreEqual(_locationQuery.ResolveLocation(_locationQuery.GetCountry("Australia"), location).ToString(), jobAd.Description.Location.ToString());
        }

        protected static void AssertJobAd(JobAd original, JobAd jobAd)
        {
            // BrandingLogo is ignored
            Assert.IsTrue(original.Description.BulletPoints.SequenceEqual(jobAd.Description.BulletPoints));

            if (original.ContactDetails == null)
            {
                Assert.IsNull(jobAd.ContactDetails);
            }
            else
            {
                Assert.IsNotNull(jobAd.ContactDetails);
                Assert.AreEqual(original.ContactDetails.FirstName, jobAd.ContactDetails.FirstName);
                Assert.AreEqual(original.ContactDetails.LastName, jobAd.ContactDetails.LastName);
                Assert.AreEqual(original.ContactDetails.EmailAddress, jobAd.ContactDetails.EmailAddress);
                Assert.AreEqual(original.ContactDetails.FaxNumber, jobAd.ContactDetails.FaxNumber);
                Assert.AreEqual(original.ContactDetails.PhoneNumber, jobAd.ContactDetails.PhoneNumber);
            }

            Assert.AreEqual(original.Description.Content, jobAd.Description.Content);
            // CreatedTime is ignored //Assert.AreEqual(original.CreatedTime, jobAd.CreatedTime);
            Assert.AreEqual(original.Description.CompanyName, jobAd.Description.CompanyName);
            // ExpiryTime is ignored //Assert.AreEqual(original.ExpiryTime, jobAd.ExpiryTime);
            Assert.AreEqual(original.Integration.ExternalReferenceId, jobAd.Integration.ExternalReferenceId);

            if (original.Description.Industries == null)
            {
                Assert.IsTrue(jobAd.Description.Industries == null || jobAd.Description.Industries.Count == 0);
            }
            else
            {
                Assert.IsNotNull(jobAd.Description.Industries);
                Assert.AreEqual(original.Description.Industries.Count, jobAd.Description.Industries.Count);

                // Different order for industries is acceptable.

                foreach (var oIndustry in original.Description.Industries)
                {
                    var dIndustry = FindIndustry(jobAd.Description.Industries, oIndustry.Name);
                    Assert.IsNotNull(dIndustry);
                    Assert.AreEqual(oIndustry, dIndustry);
                }
            }

            // Id is ignored
            // InternalReferenceId is ignored
            // JobPoster is ignored
            Assert.AreEqual(original.Description.JobTypes, jobAd.Description.JobTypes);
            // LastUpdatedTimeis ignored //Assert.AreEqual(original.LastUpdatedTime, jobAd.LastUpdatedTime);
            Assert.AreEqual(original.Description.Location, jobAd.Description.Location);
            Assert.AreEqual(original.Description.Package, jobAd.Description.Package);
            Assert.AreEqual(original.Description.PositionTitle, jobAd.Description.PositionTitle);

            // Publishers is ignored
            Assert.AreEqual(original.Description.ResidencyRequired, jobAd.Description.ResidencyRequired);
            Assert.AreEqual(original.Description.Salary, jobAd.Description.Salary);
            // Status is ignored
            Assert.AreEqual(original.Description.Summary, jobAd.Description.Summary);
            Assert.AreEqual(original.Title, jobAd.Title);
        }

        private static Industry FindIndustry(IEnumerable<Industry> toSearch, string toFind)
        {
            return toSearch.FirstOrDefault(industry => industry.Name == toFind);
        }

        #endregion
    }
}