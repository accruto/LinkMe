using System;
using System.Collections.Generic;
using System.IO;
using LinkMe.Apps.Agents.Test.Communities;
using LinkMe.Apps.Agents.Test.Users.Employers;
using LinkMe.Domain;
using LinkMe.Domain.Industries.Queries;
using LinkMe.Domain.Location;
using LinkMe.Domain.Roles.Affiliations.Communities.Commands;
using LinkMe.Domain.Roles.Affiliations.Verticals.Commands;
using LinkMe.Domain.Roles.JobAds;
using LinkMe.Domain.Roles.JobAds.Commands;
using LinkMe.Domain.Roles.Recruiters;
using LinkMe.Domain.Roles.Test.Integration;
using LinkMe.Domain.Roles.Test.JobAds;
using LinkMe.Domain.Roles.Test.Recruiters;
using LinkMe.Domain.Users.Test.Employers.JobAds;
using LinkMe.Environment;
using LinkMe.Framework.Utility;
using LinkMe.Query.Search.JobAds;
using LinkMe.Query.Search.JobAds.Commands;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Integration.JobAds.GetJobAds
{
    [TestClass]
    public class JobAdsTests
        : GetJobAdsTests
    {
        private readonly IJobAdsCommand _jobAdsCommand = Resolve<IJobAdsCommand>();
        private readonly IIndustriesQuery _industriesQuery = Resolve<IIndustriesQuery>();
        private readonly ICommunitiesCommand _communitiesCommand = Resolve<ICommunitiesCommand>();
        private readonly IVerticalsCommand _verticalsCommand = Resolve<IVerticalsCommand>();
        private readonly IJobAdSearchesCommand _jobAdSearchesCommand = Resolve<IJobAdSearchesCommand>();

        [TestMethod]
        public void TestAll()
        {
            var integratorUser = _integrationCommand.CreateTestIntegratorUser();
            var employer = _employerAccountsCommand.CreateTestEmployer(0, _organisationsCommand.CreateTestOrganisation(0));
            var jobAd = PostJobAd(employer);

            // Get.

            var response = JobAds(integratorUser, null, null);

            // Assert.

            AssertJobAdFeed(employer, jobAd, GetJobAdFeed(response));
        }

        [TestMethod]
        public void TestJobAd()
        {
            var integratorUser = _integrationCommand.CreateTestIntegratorUser();
            var employer = _employerAccountsCommand.CreateTestEmployer(0, _organisationsCommand.CreateTestOrganisation(0));
            var jobAd = PostJobAd(employer);

            // Get.

            var response = JobAds(integratorUser, jobAd.Description.Industries[0].Name.ToUpper());

            // Assert.

            AssertJobAdFeed(employer, jobAd, GetJobAdFeed(response));
        }

        [TestMethod]
        [Ignore]
        public void TestAddJobAdForCustomCss()
        {
            var cssDir = FileSystem.GetAbsolutePath(@"Apps\Web\ui\styles\Recruiters",
                RuntimeEnvironment.GetSourceFolder());
            if (!Directory.Exists(cssDir))
                throw new DirectoryNotFoundException("The company CSS directory, '" + cssDir + "', does not exist.");

            var cssFiles = Directory.GetFiles(cssDir, "*.css", SearchOption.TopDirectoryOnly);
            var i = 0;
            Guid?[] integratorUserId = { null, _careerOneQuery.GetIntegratorUser().Id, _integrationCommand.CreateTestIntegratorUser().Id };
            foreach (var cssFile in cssFiles)
            {
                try
                {
                    var fileName = Path.GetFileName(cssFile);
                    var parts = fileName.Split(new[] {" - "}, StringSplitOptions.RemoveEmptyEntries);
                    if (parts.Length == 2)
                    {
                        var orgName = parts[0];
                        orgName = orgName.Replace('(', ' ');
                        orgName = orgName.Replace(')', ' ');
                        try
                        {
                            var orgId = new Guid(parts[1].Substring(0, parts[1].LastIndexOf(".css")));
                            var pCode = (int) (new Random(i).NextDouble()*100);
                            var location = _locationQuery.ResolveLocation(_locationQuery.GetCountry("Australia"),
                                "20" + (pCode < 10 ? "0" : "") + pCode);
                            var org = new Organisation
                                          {Id = orgId, Name = orgName, Address = new Address {Location = location}};
                            _organisationsCommand.CreateOrganisation(org);
                            var employer = _employerAccountsCommand.CreateTestEmployer(i, org);
                            var jobAd = employer.CreateTestJobAd(orgName + " test job ads", "test suggest job content, test long text, test ellipsis. test suggest job content, test long text, test ellipsis. test suggest job content, test long text, test ellipsis. test suggest job content, test long text, test ellipsis.", _industriesQuery.GetIndustries()[(int)(new Random(i).NextDouble() * 28)], location);
                            var count = (int) (new Random(i).NextDouble()*5);
                            var jobTypes = new List<JobTypes>();
                            for (var j = 0; j < count; j++)
                                jobTypes.Add(new List<JobTypes> { JobTypes.FullTime, JobTypes.PartTime, JobTypes.Contract, JobTypes.Temp, JobTypes.JobShare }[(int)(new Random(i + j).NextDouble() * 5)]);
                            var jobType = JobTypes.None;
                            foreach (var jt in jobTypes) jobType |= jt;
                            Salary salary;
                            if (i % 2 == 0)
                                salary = new Salary
                                             {
                                                 LowerBound = (int) (new Random(i).NextDouble()*25)*5000,
                                                 UpperBound = (int) ((1 - new Random(i + 1).NextDouble())*50)*5000,
                                                 Rate = SalaryRate.Year,
                                                 Currency = Currency.AUD
                                             };
                            else
                                salary = new Salary
                                {
                                    LowerBound = (int)(new Random(i).NextDouble() * 25) * 5,
                                    UpperBound = (int)((1 - new Random(i + 1).NextDouble()) * 50) * 5,
                                    Rate = SalaryRate.Hour,
                                    Currency = Currency.AUD
                                };
                            jobAd.Description.Salary = salary;
                            jobAd.Description.JobTypes = jobType;
                            jobAd.Integration.IntegratorUserId = integratorUserId[(int)(new Random(i).NextDouble() * 3)];
                            jobAd.Integration.ExternalApplyUrl = (jobAd.Integration.IntegratorUserId == null ? string.Empty : "http://jobview.careerone.com.au/GetJob.aspx?JobID=103216325&WT.mc_n=AFC_linkme");
                            jobAd.CreatedTime = DateTime.Now.AddHours(0 - new Random(i).NextDouble()*100);
                            jobAd.FeatureBoost = (i%2 == 0) ? JobAdFeatureBoost.Low : JobAdFeatureBoost.None;
                            _jobAdsCommand.PostJobAd(jobAd);
                        }
                        catch (FormatException)
                        {
                        }
                    }
                    i++;
                }
                catch (Exception ex)
                {
                    throw new ApplicationException("Failed to process CSS file '" + cssFile + "'.", ex);
                }
            }
            //add a default jobad (without custom stylesheet)
            var anotherOrg = new Organisation
                                 {
                                     Id = new Guid(),
                                     Name = "LinkMe",
                                     Address = new Address
                                                   {
                                                       Location = _locationQuery.ResolveLocation(_locationQuery.GetCountry("Australia"),
                                                           "Neutral Bay NSW 2089")
                                                   }
                                 };
            _organisationsCommand.CreateOrganisation(anotherOrg);
            var anotherEmployer = _employerAccountsCommand.CreateTestEmployer(i, anotherOrg);
            var anotherJobAd = anotherEmployer.CreateTestJobAd("LinkMe test job ads",
                "test suggest job content, test long text, test ellipsis. test suggest job content, test long text, test ellipsis. test suggest job content, test long text, test ellipsis. test suggest job content, test long text, test ellipsis.",
                _industriesQuery.GetIndustries()[(int) (new Random(i).NextDouble()*28)], _locationQuery.ResolveLocation(_locationQuery.GetCountry("Australia"),
                    "Neutral Bay NSW 2089"));
            anotherJobAd.Integration.IntegratorUserId = integratorUserId[1]; //Career One
            anotherJobAd.Integration.ExternalApplyUrl = "http://jobview.careerone.com.au/GetJob.aspx?JobID=103216325&WT.mc_n=AFC_linkme";
            _jobAdsCommand.PostJobAd(anotherJobAd);
            anotherJobAd = anotherEmployer.CreateTestJobAd("LinkMe test job ads - non CareerOne",
                "test suggest job content, test long text, test ellipsis. test suggest job content, test long text, test ellipsis. test suggest job content, test long text, test ellipsis. test suggest job content, test long text, test ellipsis.",
                _industriesQuery.GetIndustries()[(int)(new Random(i).NextDouble() * 28)], _locationQuery.ResolveLocation(_locationQuery.GetCountry("Australia"),
                    "Neutral Bay NSW 2089"));
            _jobAdsCommand.PostJobAd(anotherJobAd);
        }

        [TestMethod]
        public void TestCareerOneJobAd()
        {
            var integratorUser = _integrationCommand.CreateTestIntegratorUser();
            var employer = _employerAccountsCommand.CreateTestEmployer(0, _organisationsCommand.CreateTestOrganisation(0));
            var jobAd = PostJobAd(employer);

            // Get.

            var response = JobAds(integratorUser, jobAd.Description.Industries[0].Name);
            AssertJobAdFeed(employer, jobAd, GetJobAdFeed(response));

            // Make it a CareerOne job ad.

            jobAd.Integration.IntegratorUserId = _careerOneQuery.GetIntegratorUser().Id;
            _jobAdsCommand.UpdateJobAd(jobAd);

            // Should still be returned but contact details are not shown.

            response = JobAds(integratorUser, jobAd.Description.Industries[0].Name);
            jobAd.ContactDetails = null;
            AssertJobAdFeed(employer, jobAd, GetJobAdFeed(response));
        }

        [TestMethod]
        public void TestCommunityJobAd()
        {
            var integratorUser = _integrationCommand.CreateTestIntegratorUser();

            var community = TestCommunity.UniMelbArts.CreateTestCommunity(_communitiesCommand, _verticalsCommand);
            var organisation = _organisationsCommand.CreateTestOrganisation(0);
            organisation.AffiliateId = community.Id;
            _organisationsCommand.UpdateOrganisation(organisation);
            var employer = _employerAccountsCommand.CreateTestEmployer(0, organisation);

            var jobAd = PostJobAd(employer);

            // Should not be returned.

            var response = JobAds(integratorUser, jobAd.Description.Industries[0].Name);
            Assert.IsNull(GetJobAdFeed(response));
        }

        [TestMethod]
        public void TestErrors()
        {
            var integratorUser = _integrationCommand.CreateTestIntegratorUser();
            var employer = _employerAccountsCommand.CreateTestEmployer(0, _organisationsCommand.CreateTestOrganisation(0));
            PostJobAd(employer);

            const string wrongUserName = "blahuser";
            const string wrongPassword = "blahpassword";
            const string industries = "accounting";
            const string modifiedSince = "2007-01-01T00:00:00";

            // No username-password.

            var response = JobAds(null, null, industries, modifiedSince);
            AssertError(GetErrors(response), "No username was specified in the HTTP request.");

            // Wrong username-password.

            response = JobAds(wrongUserName, wrongPassword, industries, modifiedSince);
            AssertError(GetErrors(response), "Web service authorization failed: unknown user '" + wrongUserName + "'.");

            // Correct username-password.

            response = JobAds(integratorUser, industries, modifiedSince);
            Assert.IsTrue(GetErrors(response).Count == 0);

            // Correct username-password as parameters.

            response = Get(Get(industries, modifiedSince), integratorUser, Password, false);
            Assert.IsTrue(GetErrors(response).Count == 0);
        }

        [TestMethod]
        public void TestIndustries()
        {
            var integratorUser = _integrationCommand.CreateTestIntegratorUser();
            var employer = _employerAccountsCommand.CreateTestEmployer(0, _organisationsCommand.CreateTestOrganisation(0));
            var jobAd = PostJobAd(employer);

            const string modifiedSince = "2007-01-01T00:00:00";

            // Non-existing industry.

            var response = JobAds(integratorUser, "accounting|idiots", modifiedSince);
            AssertError(GetErrors(response), "The 'idiots' industry is unknown.");

            // Industry with one job ad.

            response = JobAds(integratorUser, jobAd.Description.Industries[0].Name);
            AssertJobAdFeed(employer, jobAd, GetJobAdFeed(response));

            // Industry with no job ad.

            response = JobAds(integratorUser, _industriesQuery.GetIndustries()[1].Name, modifiedSince);
            Assert.IsNull(GetJobAdFeed(response));
        }

        [TestMethod]
        public void TestIndustriesEmptyModifiedSince()
        {
            var integratorUser = _integrationCommand.CreateTestIntegratorUser();
            var employer = _employerAccountsCommand.CreateTestEmployer(0, _organisationsCommand.CreateTestOrganisation(0));
            var jobAd = PostJobAd(employer);

            const string modifiedSince = "";

            // Non-existing industry.

            var response = JobAds(integratorUser, "accounting|idiots", modifiedSince);
            AssertError(GetErrors(response), "The 'idiots' industry is unknown.");

            // Industry with one job ad.

            response = JobAds(integratorUser, jobAd.Description.Industries[0].Name);
            AssertJobAdFeed(employer, jobAd, GetJobAdFeed(response));

            // Industry with no job ad.

            response = JobAds(integratorUser, _industriesQuery.GetIndustries()[1].Name, modifiedSince);
            Assert.IsNull(GetJobAdFeed(response));
        }

        [TestMethod]
        public void TestIndustryAlias()
        {
            var integratorUser = _integrationCommand.CreateTestIntegratorUser();
            var employer = _employerAccountsCommand.CreateTestEmployer(0, _organisationsCommand.CreateTestOrganisation(0));
            PostJobAd(employer);

            // Primary Industry

            var industries = "Primary Industry";
            const string modifiedSince = "2007-01-01T00:00:00";

            var response = JobAds(integratorUser, industries, modifiedSince);
            Assert.AreEqual(0, GetErrors(response).Count);
            Assert.IsNull(GetJobAdFeed(response));

            // Healthcare & Medical

            industries = "Healthcare & Medical";
            response = JobAds(integratorUser, industries, modifiedSince);
            Assert.AreEqual(0, GetErrors(response).Count);
            Assert.IsNull(GetJobAdFeed(response));
        }

        [TestMethod]
        public void TestModifiedSince()
        {
            var integratorUser = _integrationCommand.CreateTestIntegratorUser();
            var employer = _employerAccountsCommand.CreateTestEmployer(0, _organisationsCommand.CreateTestOrganisation(0));
            var jobAd = PostJobAd(employer);

            var industries = jobAd.Description.Industries[0].Name;
            var modifiedSince = "20070101T000000";

            // Wrong modifiedSince.

            var response = JobAds(integratorUser, industries, modifiedSince);
            AssertError(GetErrors(response), "The 'modifiedSince' parameter is incorrect. Please use the following format for this field: [-]CCYY-MM-DDThh:mm:ssZ");

            // Correct modifiedSince, one job ad found.

            modifiedSince = "2007-01-01T00:00:00Z";
            response = JobAds(integratorUser, industries, modifiedSince);
            AssertJobAdFeed(employer, jobAd, GetJobAdFeed(response));

            // 24hr modifiedSince, one job ad found (some integrators send this).

            modifiedSince = "2007-01-01T24:00:00Z";
            response = JobAds(integratorUser, industries, modifiedSince);
            AssertJobAdFeed(employer, jobAd, GetJobAdFeed(response));

            // Date in future, no job ad found.

            modifiedSince = string.Format("{0}-01-01T00:00:00Z", DateTime.Now.AddYears(1).Year);
            response = JobAds(integratorUser, industries, modifiedSince);
            Assert.IsNull(GetJobAdFeed(response));
        }

        [TestMethod]
        public void TestEmptyIndustriesModifiedSince()
        {
            var integratorUser = _integrationCommand.CreateTestIntegratorUser();
            var employer = _employerAccountsCommand.CreateTestEmployer(0, _organisationsCommand.CreateTestOrganisation(0));
            var jobAd = PostJobAd(employer);

            const string industries = "";
            var modifiedSince = "20070101T000000";

            // Wrong modifiedSince.

            var response = JobAds(integratorUser, industries, modifiedSince);
            AssertError(GetErrors(response), "The 'modifiedSince' parameter is incorrect. Please use the following format for this field: [-]CCYY-MM-DDThh:mm:ssZ");

            // Correct modifiedSince, one job ad found.

            modifiedSince = "2007-01-01T00:00:00Z";
            response = JobAds(integratorUser, industries, modifiedSince);
            AssertJobAdFeed(employer, jobAd, GetJobAdFeed(response));

            // Date in future, no job ad found.

            modifiedSince = string.Format("{0}-01-01T00:00:00Z", DateTime.Now.AddYears(1).Year);
            response = JobAds(integratorUser, industries, modifiedSince);
            Assert.IsNull(GetJobAdFeed(response));
        }

        [TestMethod]
        public void TestNoLocationNoIndustryJobAd()
        {
            var integratorUser = _integrationCommand.CreateTestIntegratorUser();
            var employer = _employerAccountsCommand.CreateTestEmployer(0, _organisationsCommand.CreateTestOrganisation(0));
            var jobAd = PostJobAd(employer);
            jobAd.Description.Location = null;
            jobAd.Description.Industries = null;
            _jobAdsCommand.UpdateJobAd(jobAd);

            // Get.

            var response = JobAds(integratorUser, null);

            // Assert.

            AssertJobAdFeed(employer, jobAd, GetJobAdFeed(response));
        }

        [TestMethod]
        public void TestBadUnstructuredLocationJobAd()
        {
            var integratorUser = _integrationCommand.CreateTestIntegratorUser();
            var employer = _employerAccountsCommand.CreateTestEmployer(0, _organisationsCommand.CreateTestOrganisation(0));
            var jobAd = PostJobAd(employer);
            jobAd.Description.Location = _locationQuery.ResolveLocation(_locationQuery.GetCountry("Australia"), ".");
            _jobAdsCommand.UpdateJobAd(jobAd);

            // Get.

            var response = JobAds(integratorUser, null);

            // Assert.

            AssertJobAdFeed(employer, jobAd, GetJobAdFeed(response));
        }

        [TestMethod]
        public void TestJobAdTitle()
        {
            var integratorUser = _integrationCommand.CreateTestIntegratorUser();
            var employer = _employerAccountsCommand.CreateTestEmployer(0, _organisationsCommand.CreateTestOrganisation(0));
            var jobAd = PostJobAd(employer);
            jobAd.Title = "Quality Assurance Auditor - Mumbai";
            _jobAdsCommand.UpdateJobAd(jobAd);

            // Get.

            var response = JobAds(integratorUser, null);

            // Assert.

            AssertJobAdFeed(employer, jobAd, GetJobAdFeed(response));
        }

        [TestMethod]
        public void TestHideContactDetails()
        {
            var integratorUser = _integrationCommand.CreateTestIntegratorUser();
            var employer = _employerAccountsCommand.CreateTestEmployer(0, _organisationsCommand.CreateTestOrganisation(0));
            var jobAd = PostJobAd(employer);
            jobAd.Title = "Quality Assurance Auditor - Mumbai";
            jobAd.Visibility.HideContactDetails = true;
            _jobAdsCommand.UpdateJobAd(jobAd);

            // Get.

            var response = JobAds(integratorUser, null);

            // Assert.

            jobAd.ContactDetails = null;
            AssertJobAdFeed(employer, jobAd, GetJobAdFeed(response));
        }

        [TestMethod]
        public void TestHideCompany()
        {
            var integratorUser = _integrationCommand.CreateTestIntegratorUser();
            var employer = _employerAccountsCommand.CreateTestEmployer(0, _organisationsCommand.CreateTestOrganisation(0));
            var jobAd = PostJobAd(employer);
            jobAd.Title = "Quality Assurance Auditor - Mumbai";
            jobAd.Visibility.HideCompany = true;
            _jobAdsCommand.UpdateJobAd(jobAd);

            // Get.

            var response = JobAds(integratorUser, null);

            // Assert.

            jobAd.Description.CompanyName = null;
            AssertJobAdFeed(null, jobAd, GetJobAdFeed(response));
        }

        [TestMethod]
        public void TestLimitByCriteria()
        {
            var integratorUser = _integrationCommand.CreateTestIntegratorUser();
            var employer = _employerAccountsCommand.CreateTestEmployer(0, _organisationsCommand.CreateTestOrganisation(0));
            var jobAd = PostJobAd(employer);

            var criteria = new JobAdSearchCriteria { AdvertiserName = "Advertiser" };

            _jobAdSearchesCommand.CreateJobAdSearch(integratorUser.Id, new JobAdSearch { Criteria = criteria });
            // one job ad found.

            var response = JobAds(integratorUser, null, null);
            AssertJobAdFeed(employer, jobAd, GetJobAdFeed(response));
        }

        [TestMethod]
        public void TestLimitByBadCriteria()
        {
            var integratorUser = _integrationCommand.CreateTestIntegratorUser();
            var employer = _employerAccountsCommand.CreateTestEmployer(0, _organisationsCommand.CreateTestOrganisation(0));
            PostJobAd(employer);

            var criteria = new JobAdSearchCriteria { AdvertiserName = "Another" };

            _jobAdSearchesCommand.CreateJobAdSearch(integratorUser.Id, new JobAdSearch { Criteria = criteria });
            // one job ad found.

            var response = JobAds(integratorUser, null, null);
            Assert.IsNull(GetJobAdFeed(response));
        }

        [TestMethod]
        public void TestLimitByCriteriaAndModifiedSince()
        {
            var integratorUser = _integrationCommand.CreateTestIntegratorUser();
            var employer = _employerAccountsCommand.CreateTestEmployer(0, _organisationsCommand.CreateTestOrganisation(0));
            var jobAd = PostJobAd(employer);

            var criteria = new JobAdSearchCriteria {AdvertiserName = "Advertiser"};

            _jobAdSearchesCommand.CreateJobAdSearch(integratorUser.Id, new JobAdSearch{Criteria = criteria});
            // Correct modifiedSince, one job ad found.

            var modifiedSince = "2007-01-01T00:00:00Z";
            var response = JobAds(integratorUser, null, modifiedSince);
            AssertJobAdFeed(employer, jobAd, GetJobAdFeed(response));

            // 24hr modifiedSince, one job ad found (some integrators send this).

            modifiedSince = "2007-01-01T24:00:00Z";
            response = JobAds(integratorUser, null, modifiedSince);
            AssertJobAdFeed(employer, jobAd, GetJobAdFeed(response));

            // Date in future, no job ad found.

            modifiedSince = string.Format("{0}-01-01T00:00:00Z", DateTime.Now.AddYears(1).Year);
            response = JobAds(integratorUser, null, modifiedSince);
            Assert.IsNull(GetJobAdFeed(response));

        }
    }
}