using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using System.Xml.XPath;
using LinkMe.Apps.Agents.Users.Employers.Commands;
using LinkMe.Apps.Services.JobAds;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Industries;
using LinkMe.Domain.Industries.Queries;
using LinkMe.Domain.Roles.Integration;
using LinkMe.Domain.Roles.Integration.Commands;
using LinkMe.Domain.Roles.JobAds;
using LinkMe.Domain.Roles.JobAds.Commands;
using LinkMe.Domain.Roles.Recruiters.Commands;
using LinkMe.Domain.Roles.Test.JobAds;
using LinkMe.Domain.Users.Test.Employers.JobAds;
using LinkMe.Framework.Utility;
using LinkMe.Framework.Utility.Urls;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Integration.JobAds.GetJobAds
{
    [TestClass]
    public abstract class GetJobAdsTests
        : IntegrationTests
    {
        protected readonly IEmployerAccountsCommand _employerAccountsCommand = Resolve<IEmployerAccountsCommand>();
        protected readonly IOrganisationsCommand _organisationsCommand = Resolve<IOrganisationsCommand>();
        private readonly IJobAdsCommand _jobAdsCommand = Resolve<IJobAdsCommand>();
        protected readonly IIntegrationCommand _integrationCommand = Resolve<IIntegrationCommand>();
        private readonly IIndustriesQuery _industriesQuery = Resolve<IIndustriesQuery>();

        private ReadOnlyUrl _baseJobUrl;

        [TestInitialize]
        public void TestInitialize()
        {
            ClearSearchIndexes();

            _baseJobUrl = new ReadOnlyApplicationUrl("~/jobs/");
        }

        protected JobAd PostJobAd(IEmployer employer)
        {
            var jobAd = employer.CreateTestJobAd();
            jobAd.Description.Summary = "Summary";
            jobAd.Description.CompanyName = "An Advertiser";
            jobAd.Description.Industries = new List<Industry> {_industriesQuery.GetIndustries()[0]};
            _jobAdsCommand.PostJobAd(jobAd);
            return jobAd;
        }

        protected static JobAdFeedElement GetJobAdFeed(string response)
        {
            JobAdFeedElement jobAdFeed = null;

            var xmlSerializer = new XmlSerializer(typeof(JobAdFeedElement), null, new Type[] { }, null, Apps.Services.Constants.XmlSerializationNamespace);
            var reader = XmlReader.Create(new StringReader(response));
            reader.MoveToContent();
            Assert.AreEqual("GetJobAdsResponse", reader.LocalName);
            reader.ReadStartElement();
            if (reader.IsStartElement())
            {
                Assert.AreEqual("JobAd", reader.LocalName);
                jobAdFeed = (JobAdFeedElement) xmlSerializer.Deserialize(reader);
            }
            reader.Close();

            return jobAdFeed;
        }

        protected void AssertJobAdFeed(IEmployer poster, JobAd expectedJobAd, JobAdFeedElement jobAdFeed)
        {
            Assert.AreEqual(jobAdFeed.Title, expectedJobAd.Title);
            Assert.AreEqual(jobAdFeed.PositionTitle, expectedJobAd.Description.PositionTitle);
            Assert.AreEqual(jobAdFeed.BulletPoints.Length, expectedJobAd.Description.BulletPoints.Count);

            for (var index = 0; index < jobAdFeed.BulletPoints.Length; ++index)
                Assert.AreEqual(jobAdFeed.BulletPoints[index], expectedJobAd.Description.BulletPoints[index]);

            Assert.AreEqual(jobAdFeed.Summary, expectedJobAd.Description.Summary);
            Assert.AreEqual(jobAdFeed.Content, expectedJobAd.Description.Content);
            Assert.AreEqual(jobAdFeed.EmployerCompanyName, expectedJobAd.Description.CompanyName);
            Assert.AreEqual(jobAdFeed.JobTypes, expectedJobAd.Description.JobTypes);
            Assert.AreEqual(jobAdFeed.Location, expectedJobAd.Description.Location != null ? expectedJobAd.Description.Location.ToString() : null);
            Assert.AreEqual(jobAdFeed.Postcode, expectedJobAd.Description.Location != null ? expectedJobAd.Description.Location.Postcode : null);
            Assert.AreEqual(jobAdFeed.Salary.HasLowerBound, expectedJobAd.Description.Salary.HasLowerBound);
            Assert.AreEqual(jobAdFeed.Salary.HasUpperBound, expectedJobAd.Description.Salary.HasUpperBound);
            Assert.AreEqual(jobAdFeed.Salary.IsEmpty, expectedJobAd.Description.Salary.IsEmpty);
            Assert.AreEqual(jobAdFeed.Salary.LowerBound, expectedJobAd.Description.Salary.LowerBound);
            Assert.AreEqual(jobAdFeed.Salary.UpperBound, expectedJobAd.Description.Salary.UpperBound);
            Assert.AreEqual(jobAdFeed.PackageDetails, expectedJobAd.Description.Package);
            Assert.AreEqual(jobAdFeed.ResidencyRequired, expectedJobAd.Description.ResidencyRequired);
            if (expectedJobAd.ContactDetails == null)
            {
                Assert.IsNull(jobAdFeed.ContactDetails);
                Assert.AreEqual(jobAdFeed.RecruiterCompanyName, null);
            }
            else
            {
                Assert.AreEqual(jobAdFeed.ContactDetails.FirstName, expectedJobAd.ContactDetails.FirstName);
                Assert.AreEqual(jobAdFeed.ContactDetails.LastName, expectedJobAd.ContactDetails.LastName);
                Assert.AreEqual(jobAdFeed.ContactDetails.EmailAddress, expectedJobAd.ContactDetails.EmailAddress);
                Assert.AreEqual(jobAdFeed.ContactDetails.FaxNumber, expectedJobAd.ContactDetails.FaxNumber);
                Assert.AreEqual(jobAdFeed.ContactDetails.PhoneNumber, expectedJobAd.ContactDetails.PhoneNumber);

                if (poster == null)
                    Assert.IsNull(jobAdFeed.RecruiterCompanyName);
                else
                    Assert.AreEqual(jobAdFeed.RecruiterCompanyName, poster.Organisation.Name);
            }

            if (expectedJobAd.Description.Industries == null)
            {
                Assert.AreEqual(jobAdFeed.Industries.Count, 0);
            }
            else
            {
                Assert.AreEqual(jobAdFeed.Industries.Count, expectedJobAd.Description.Industries.Count);
                for (var index = 0; index < jobAdFeed.Industries.Count; ++index)
                    Assert.AreEqual(jobAdFeed.Industries[index], expectedJobAd.Description.Industries[index].Name);
            }

            // They are now the same url.

            var viewJobAdUrl = GetJobUrl(expectedJobAd);
            var applyJobAdUrl = GetJobUrl(expectedJobAd);

            Assert.AreEqual(jobAdFeed.ViewJobAdUrl.ToLower(), viewJobAdUrl.AbsoluteUri.ToLower());
            Assert.AreEqual(jobAdFeed.ApplyJobAdUrl.ToLower(), applyJobAdUrl.AbsoluteUri.ToLower());
        }

        private ReadOnlyUrl GetJobUrl(JobAd jobAd)
        {
            var sb = new StringBuilder();

            // Location.

            var location = jobAd.Description.Location != null ? jobAd.Description.Location.ToString() : null;
            if (!string.IsNullOrEmpty(location))
                location = TextUtil.StripExtraWhiteSpace(TextUtil.StripToAlphaNumericAndWhiteSpace(location)).ToLower().Replace(' ', '-');
            if (string.IsNullOrEmpty(location))
                location = "-";
            sb.Append(location);

            // Industry. If there is only one industry then use it.  Do not concatenate more as this can easily lead to
            // urls being longer then url or segment lengths.

            var industrySb = new StringBuilder();
            if (jobAd.Description.Industries != null && jobAd.Description.Industries.Count > 0)
                industrySb.Append(jobAd.Description.Industries[0].UrlName);
            sb.Append("/");
            if (industrySb.Length == 0)
                sb.Append("-");
            else
                sb.Append(industrySb);

            // Job title.

            sb.Append("/");
            sb.Append(TextUtil.StripExtraWhiteSpace(TextUtil.StripToAlphaNumericAndSpace(jobAd.Title)).ToLower().Replace(' ', '-'));

            // Id

            sb.Append("/");
            sb.Append(jobAd.Id.ToString());

            return new ReadOnlyApplicationUrl(true, _baseJobUrl + sb.ToString());
        }

        protected static List<string> GetErrors(string response)
        {
            var document = new XPathDocument(new StringReader(response));
            var navigator = document.CreateNavigator();
            var nsmgr = new XmlNamespaceManager(navigator.NameTable);
            nsmgr.AddNamespace("lm", Apps.Services.Constants.XmlSerializationNamespace);

            var expression = navigator.Compile("//lm:Error");
            expression.SetContext(nsmgr);

            var errors = new List<string>();
            var iterator = navigator.Select(expression);
            while (iterator.MoveNext())
                errors.Add(iterator.Current.Value);
            return errors;
        }

        protected static void AssertError(IList<string> errors, string expectedError)
        {
            Assert.AreEqual(1, errors.Count);
            Assert.IsTrue(errors[0].Contains(expectedError));
        }

        protected static string JobAds(string loginId, string password, string industries, string modifiedSince)
        {
            return Get(Get(industries, modifiedSince), new IntegratorUser { LoginId = loginId }, password, false);
        }

        protected static string JobAds(IntegratorUser user, string industries, string modifiedSince)
        {
            return Get(Get(industries, modifiedSince), user, Password, false);
        }

        protected static string JobAds(IntegratorUser user, string industries)
        {
            return JobAds(user, industries, null);
        }

        protected static ReadOnlyApplicationUrl Get(string industries, string modifiedSince)
        {
            var queryString = new QueryString();
            if (industries != null)
                queryString["industries"] = industries;
            if (modifiedSince != null)
                queryString["modifiedsince"] = modifiedSince;
            return new ReadOnlyApplicationUrl("~/jobads", queryString);
        }
    }
}