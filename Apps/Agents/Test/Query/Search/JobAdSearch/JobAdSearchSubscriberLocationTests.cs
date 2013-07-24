using System.Collections.Generic;
using LinkMe.Apps.Agents.Users.Employers.Commands;
using LinkMe.Apps.Agents.Test.Users.Employers;
using LinkMe.Apps.Mocks.Hosts;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Industries.Queries;
using LinkMe.Domain.Location;
using LinkMe.Domain.Location.Queries;
using LinkMe.Domain.Roles.JobAds;
using LinkMe.Domain.Roles.JobAds.Commands;
using LinkMe.Domain.Roles.Recruiters.Commands;
using LinkMe.Domain.Roles.Test.JobAds;
using LinkMe.Domain.Roles.Test.Recruiters;
using LinkMe.Domain.Test.Data;
using LinkMe.Domain.Users.Test.Employers.JobAds;
using LinkMe.Framework.Utility.Sql;
using LinkMe.Query.Search.JobAds;
using LinkMe.Query.Search.JobAds.Commands;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Apps.Agents.Test.Query.Search.JobAdSearch
{
    [TestClass]
    public class JobAdSearchSubscriberLocationTests
        : TestClass
    {
        private static readonly ILocationQuery LocationQuery = Resolve<ILocationQuery>();
        private readonly IIndustriesQuery _industriesQuery = Resolve<IIndustriesQuery>();
        private readonly IEmployerAccountsCommand _employerAccountsCommand = Resolve<IEmployerAccountsCommand>();
        private readonly IOrganisationsCommand _organisationsCommand = Resolve<IOrganisationsCommand>();
        private readonly IJobAdsCommand _jobAdsCommand = Resolve<IJobAdsCommand>();
        private readonly IExecuteJobAdSearchCommand _executeJobAdSearchCommand = Resolve<IExecuteJobAdSearchCommand>();

        private static Country _australia;
        private static Country _newZealand;

        private const string MelbourneVic3000 = "Melbourne VIC 3000";
        private const string ArmadaleVic3143 = "Armadale VIC 3143";
        private const string NorlaneVic3214 = "Norlane VIC 3214";
        private const string SydneyNsw2000 = "Sydney NSW 2000";
        private const string PerthWa6000 = "Perth WA 6000";

        private const string Melbourne = "Melbourne";
        private const string Sydney = "Sydney";
        private const string Perth = "Perth";

        private const string Vic = "VIC";
        private const string Nsw = "NSW";
        private const string Wa = "WA";

        [TestInitialize]
        public void TestInitialize()
        {
            Resolve<IDbConnectionFactory>().DeleteAllTestData();
            JobAdSearchHost.ClearIndex();
        }

        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            _australia = LocationQuery.GetCountry("Australia");
            _newZealand = LocationQuery.GetCountry("New Zealand");
        }

        [TestMethod]
        public void TestSearchLocality()
        {
            var titles = new List<string>();
            var location = new LocationReference();

            // Create jobs.

            PostJobAds();

            // Search within 50km of Melbourne VIC 3000.

            LocationQuery.ResolveLocation(location, _australia, MelbourneVic3000);
            var criteria = new JobAdSearchCriteria { Location = location };
            titles.Clear();
            AddTitles(titles, _australia, MelbourneVic3000);
            AddTitles(titles, _australia, ArmadaleVic3143);
            AddTitles(titles, _australia, Melbourne);
            TestSearch(criteria, titles);

            // Search within 50km of Norlane VIC 3124.

            LocationQuery.ResolveLocation(location, _australia, NorlaneVic3214);
            criteria = new JobAdSearchCriteria { Location = location };
            titles.Clear();
            AddTitles(titles, _australia, NorlaneVic3214);
            TestSearch(criteria, titles);

            // Search within 50km of Sydney NSW 2000.

            LocationQuery.ResolveLocation(location, _australia, SydneyNsw2000);
            criteria = new JobAdSearchCriteria { Location = location };
            titles.Clear();
            AddTitles(titles, _australia, SydneyNsw2000);
            AddTitles(titles, _australia, Sydney);
            TestSearch(criteria, titles);

            // Search within 50km of Perth WA 6000.

            LocationQuery.ResolveLocation(location, _australia, PerthWa6000);
            criteria = new JobAdSearchCriteria { Location = location };
            titles.Clear();
            TestSearch(criteria, titles);
        }

        [TestMethod]
        public void TestSearchLocalityDistance()
        {
            var titles = new List<string>();
            var location = new LocationReference();

            // Create jobs.

            PostJobAds();

            // Simple search in Melbourne VIC 3000 (default distance = 50).

            LocationQuery.ResolveLocation(location, _australia, MelbourneVic3000);
            var criteria = new JobAdSearchCriteria { Location = location };
            titles.Clear();
            AddTitles(titles, _australia, MelbourneVic3000);
            AddTitles(titles, _australia, ArmadaleVic3143);
            AddTitles(titles, _australia, Melbourne);
            TestSearch(criteria, titles);

            // Advanced search within 0km which means use the default of 50km.

            criteria = new JobAdSearchCriteria { Location = location };
            TestSearch(criteria, titles);

            // Search within 5 km: no Armadale jobs.

            criteria = new JobAdSearchCriteria { Location = location, Distance = 5 };
            titles.Clear();
            AddTitles(titles, _australia, MelbourneVic3000);
            TestSearch(criteria, titles);

            // Search within 10 km: Armadale back in.

            criteria = new JobAdSearchCriteria { Location = location, Distance = 10 };
            titles.Clear();
            AddTitles(titles, _australia, MelbourneVic3000);
            AddTitles(titles, _australia, ArmadaleVic3143);
            TestSearch(criteria, titles);

            // Search within 100 km: Norlane included.

            criteria = new JobAdSearchCriteria { Location = location, Distance = 100 };
            titles.Clear();
            AddTitles(titles, _australia, MelbourneVic3000);
            AddTitles(titles, _australia, ArmadaleVic3143);
            AddTitles(titles, _australia, NorlaneVic3214);
            AddTitles(titles, _australia, Melbourne);
            TestSearch(criteria, titles);

            // Simple search in Norlane VIC 3124 (default distance = 50).

            LocationQuery.ResolveLocation(location, _australia, NorlaneVic3214);
            criteria = new JobAdSearchCriteria { Location = location };
            titles.Clear();
            AddTitles(titles, _australia, NorlaneVic3214);
            TestSearch(criteria, titles);

            // Advanced search within 0km which means use the default of 50km.

            criteria = new JobAdSearchCriteria { Location = location };
            TestSearch(criteria, titles);

            // Search within 5 km.

            criteria = new JobAdSearchCriteria { Location = location, Distance = 5 };
            TestSearch(criteria, titles);

            // Search within 10 km.

            criteria = new JobAdSearchCriteria { Location = location, Distance = 10 };
            TestSearch(criteria, titles);

            // Search within 100 km.

            criteria = new JobAdSearchCriteria { Location = location, Distance = 100 };
            titles.Clear();
            AddTitles(titles, _australia, MelbourneVic3000);
            AddTitles(titles, _australia, ArmadaleVic3143);
            AddTitles(titles, _australia, NorlaneVic3214);
            //AddTitles(titles, Australia, Vic);
            TestSearch(criteria, titles);
        }

        [TestMethod]
        public void TestSearchRegion()
        {
            var titles = new List<string>();
            var location = new LocationReference();

            // Create jobs.

            PostJobAds();

            // Search within Melbourne.

            LocationQuery.ResolveLocation(location, _australia, Melbourne);
            var criteria = new JobAdSearchCriteria { Location = location, Distance = 0};
            titles.Clear();
            AddTitles(titles, _australia, MelbourneVic3000);
            AddTitles(titles, _australia, ArmadaleVic3143);
            AddTitles(titles, _australia, Melbourne);
            TestSearch(criteria, titles);

            // Search within Sydney.

            LocationQuery.ResolveLocation(location, _australia, Sydney);
            criteria = new JobAdSearchCriteria { Location = location, Distance = 0};
            titles.Clear();
            AddTitles(titles, _australia, SydneyNsw2000);
            AddTitles(titles, _australia, Sydney);
            TestSearch(criteria, titles);

            // Search within Perth.

            LocationQuery.ResolveLocation(location, _australia, Perth);
            criteria = new JobAdSearchCriteria { Location = location, Distance = 0};
            titles.Clear();
            TestSearch(criteria, titles);
        }

        [TestMethod]
        public void TestSearchCountrySubdivision()
        {
            var titles = new List<string>();
            var location = new LocationReference();

            // Create jobs.

            PostJobAds();

            // Search within VIC.

            LocationQuery.ResolveLocation(location, _australia, Vic);
            var criteria = new JobAdSearchCriteria { Location = location };
            titles.Clear();
            AddTitles(titles, _australia, MelbourneVic3000);
            AddTitles(titles, _australia, ArmadaleVic3143);
            AddTitles(titles, _australia, NorlaneVic3214);
            AddTitles(titles, _australia, Melbourne);
            AddTitles(titles, _australia, Vic);
            TestSearch(criteria, titles);

            // Search within NSW.

            LocationQuery.ResolveLocation(location, _australia, Nsw);
            criteria = new JobAdSearchCriteria { Location = location };
            titles.Clear();
            AddTitles(titles, _australia, SydneyNsw2000);
            AddTitles(titles, _australia, Sydney);
            AddTitles(titles, _australia, Nsw);
            TestSearch(criteria, titles);

            // Search within WA.

            LocationQuery.ResolveLocation(location, _australia, Wa);
            criteria = new JobAdSearchCriteria { Location = location };
            titles.Clear();
            TestSearch(criteria, titles);
        }

        [TestMethod]
        public void TestSearchCountry()
        {
            var titles = new List<string>();
            var location = new LocationReference();

            // Create jobs.

            PostJobAds();

            // Search within Australia.

            LocationQuery.ResolveLocation(location, _australia, "");
            var criteria = new JobAdSearchCriteria { Location = location };
            titles.Clear();
            AddTitles(titles, _australia, MelbourneVic3000);
            AddTitles(titles, _australia, ArmadaleVic3143);
            AddTitles(titles, _australia, NorlaneVic3214);
            AddTitles(titles, _australia, Melbourne);
            AddTitles(titles, _australia, Vic);
            AddTitles(titles, _australia, SydneyNsw2000);
            AddTitles(titles, _australia, Sydney);
            AddTitles(titles, _australia, Nsw);
            AddTitles(titles, _australia, "");
            TestSearch(criteria, titles);

            // Search within New Zealand.

            LocationQuery.ResolveLocation(location, _newZealand, "");
            criteria = new JobAdSearchCriteria { Location = location };
            titles.Clear();
            AddTitles(titles, _newZealand, "");
            TestSearch(criteria, titles);
        }

        private void PostJobAds()
        {
            var employer = _employerAccountsCommand.CreateTestEmployer(0, _organisationsCommand.CreateTestOrganisation(0));

            // Create jobs in the localities.

            PostJobAds(employer, _australia, MelbourneVic3000);
            PostJobAds(employer, _australia, ArmadaleVic3143);
            PostJobAds(employer, _australia, NorlaneVic3214);
            PostJobAds(employer, _australia, SydneyNsw2000);

            // Create jobs in the regions.

            PostJobAds(employer, _australia, Melbourne);
            PostJobAds(employer, _australia, Sydney);

            // Create jobs in the subdivisions.

            PostJobAds(employer, _australia, Vic);
            PostJobAds(employer, _australia, Nsw);

            // Create jobs in the countries.

            PostJobAds(employer, _australia, "");
            PostJobAds(employer, _newZealand, "");
        }

        private static int GetJobCount(LocationReference location)
        {
            switch (location.ToString())
            {
                case MelbourneVic3000:
                case ArmadaleVic3143:
                case SydneyNsw2000:
                    return 2;

                case Vic:
                case Nsw:
                    return 3;

                case "":
                    return 4;

                default:
                    return 1;
            }
        }

        private static void AddTitles(ICollection<string> titles, Country country, string location)
        {
            var locationReference = new LocationReference();
            LocationQuery.ResolveLocation(locationReference, country, location);
            AddTitles(titles, locationReference);
        }

        private static void AddTitles(ICollection<string> titles, LocationReference location)
        {
            for (var index = 0; index < GetJobCount(location); ++index)
                titles.Add(GetJobTitle(index, location));
        }

        private void PostJobAds(IEmployer employer, Country country, string location)
        {
            var locationReference = new LocationReference();
            LocationQuery.ResolveLocation(locationReference, country, location);
            PostJobAds(employer, locationReference);
        }

        private void PostJobAds(IEmployer employer, LocationReference location)
        {
            for (var index = 0; index < GetJobCount(location); ++index)
                PostJobAd(employer, index, location);
        }

        private void PostJobAd(IEmployer employer, int jobIndex, LocationReference location)
        {
            var industry = _industriesQuery.GetIndustries()[0];
            var jobAd = employer.CreateTestJobAd(GetJobTitle(jobIndex, location), "The content for job #" + jobIndex + " in " + location, industry, location);
            _jobAdsCommand.PostJobAd(jobAd);
        }

        private static string GetJobTitle(int jobIndex, LocationReference location)
        {
            return "Job #" + jobIndex + " in " + location;
        }

        private void TestSearch(JobAdSearchCriteria criteria, ICollection<string> expectedTitles)
        {
            var execution = _executeJobAdSearchCommand.Search(null, criteria, null);

            Assert.AreEqual(expectedTitles.Count, execution.Results.TotalMatches);
            Assert.AreEqual(expectedTitles.Count, execution.Results.JobAdIds.Count);

            foreach (var expectedTitle in expectedTitles)
            {
                var found = false;
                foreach (var jobAdId in execution.Results.JobAdIds)
                {
                    var jobAd = _jobAdsCommand.GetJobAd<JobAdEntry>(jobAdId);
                    if (jobAd.Title == expectedTitle)
                    {
                        found = true;
                        break;
                    }
                }

                if (!found)
                    Assert.Fail("Could not find job with title '" + expectedTitle + "'.");
            }
        }
    }
}