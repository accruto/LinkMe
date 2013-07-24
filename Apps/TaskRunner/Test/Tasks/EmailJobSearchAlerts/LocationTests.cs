using System;
using System.Collections.Generic;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Industries;
using LinkMe.Domain.Location;
using LinkMe.Domain.Roles.Test.Communications.Mocks;
using LinkMe.Domain.Roles.Test.JobAds;
using LinkMe.Domain.Users.Test.Employers.JobAds;
using LinkMe.Query.Search.JobAds;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.TaskRunner.Test.Tasks.EmailJobSearchAlerts
{
    [TestClass]
    public class LocationTests
        : EmailJobSearchAlertsTaskTests
    {
        private Country _australia;
        private Country _newZealand;

        private const string MelbourneVic3000 = "Melbourne VIC 3000";
        private const string ArmadaleVic3143 = "Armadale VIC 3143";
        private const string NorlaneVic3214 = "Norlane VIC 3214";
        private const string SydneyNsw2000 = "Sydney NSW 2000";

        private const string Melbourne = "Melbourne";
        private const string Sydney = "Sydney";

        private const string Vic = "VIC";
        private const string Nsw = "NSW";

        private const string Auckland = "Auckland";
        private const string NorthIsland = "North Island";

        private const int MaximumResults = 20;

        [TestInitialize]
        public void TestInitialize()
        {
            _australia = _locationQuery.GetCountry("Australia");
            _newZealand = _locationQuery.GetCountry("New Zealand");
        }

        [TestMethod]
        public void TestSearchLocality()
        {
            var index = 0;
            IList<string> titles = new List<string>();

            // Create jobs.

            PostJobAds();

            // Search within 50km of Melbourne VIC 3000.

            titles.Clear();
            AddTitles(titles, _australia, MelbourneVic3000);
            AddTitles(titles, _australia, ArmadaleVic3143);
            AddTitles(titles, _australia, Melbourne);
            TestTask(ref index, _australia, null, MelbourneVic3000, titles);

            // Search within 50km of Norlane VIC 3124.

            titles.Clear();
            AddTitles(titles, _australia, NorlaneVic3214);
            TestTask(ref index, _australia, null, NorlaneVic3214, titles);

            // Search within 50km of Sydney NSW 2000.

            titles.Clear();
            AddTitles(titles, _australia, SydneyNsw2000);
            AddTitles(titles, _australia, Sydney);
            TestTask(ref index, _australia, null, SydneyNsw2000, titles);
        }

        [TestMethod]
        public void TestSearchLocalityDistance()
        {
            var index = 0;
            IList<string> titles = new List<string>();

            // Create jobs.

            PostJobAds();

            // Simple search in Melbourne VIC 3000 (default distance = 50).

            titles.Clear();
            AddTitles(titles, _australia, MelbourneVic3000);
            AddTitles(titles, _australia, ArmadaleVic3143);
            AddTitles(titles, _australia, Melbourne);
            TestTask(ref index, _australia, null, MelbourneVic3000, titles);

            // Search within 5 km: no Armadale jobs.

            titles.Clear();
            AddTitles(titles, _australia, MelbourneVic3000);
            TestTask(ref index, _australia, 5, MelbourneVic3000, titles);

            // Search within 10 km: Armadale back in.

            titles.Clear();
            AddTitles(titles, _australia, MelbourneVic3000);
            AddTitles(titles, _australia, ArmadaleVic3143);
            TestTask(ref index, _australia, 10, MelbourneVic3000, titles);

            // Search within 100 km: Norlane included.

            titles.Clear();
            AddTitles(titles, _australia, MelbourneVic3000);
            AddTitles(titles, _australia, ArmadaleVic3143);
            AddTitles(titles, _australia, NorlaneVic3214);
            AddTitles(titles, _australia, Melbourne);
            TestTask(ref index, _australia, 100, MelbourneVic3000, titles);

            // Simple search in Norlane VIC 3124 (default distance = 50).

            titles.Clear();
            AddTitles(titles, _australia, NorlaneVic3214);
            TestTask(ref index, _australia, null, NorlaneVic3214, titles);

            // Search within 5 km.

            TestTask(ref index, _australia, 5, NorlaneVic3214, titles);

            // Search within 10 km.

            TestTask(ref index, _australia, 10, NorlaneVic3214, titles);

            // Search within 100 km.

            titles.Clear();
            AddTitles(titles, _australia, MelbourneVic3000);
            AddTitles(titles, _australia, ArmadaleVic3143);
            AddTitles(titles, _australia, NorlaneVic3214);
            TestTask(ref index, _australia, 100, NorlaneVic3214, titles);
        }

        [TestMethod]
        public void TestSearchRegion()
        {
            var index = 0;
            IList<string> titles = new List<string>();

            // Create jobs.

            PostJobAds();

            // Search within Melbourne.

            titles.Clear();
            AddTitles(titles, _australia, MelbourneVic3000);
            AddTitles(titles, _australia, ArmadaleVic3143);
            AddTitles(titles, _australia, Melbourne);
            TestTask(ref index, _australia, null, Melbourne, titles);

            // Search within Sydney.

            titles.Clear();
            AddTitles(titles, _australia, SydneyNsw2000);
            AddTitles(titles, _australia, Sydney);
            TestTask(ref index, _australia, null, Sydney, titles);
        }

        [TestMethod]
        public void TestSearchCountrySubdivision()
        {
            var index = 0;
            IList<string> titles = new List<string>();

            // Create jobs.

            PostJobAds();

            // Search within VIC.

            titles.Clear();
            AddTitles(titles, _australia, MelbourneVic3000);
            AddTitles(titles, _australia, ArmadaleVic3143);
            AddTitles(titles, _australia, NorlaneVic3214);
            AddTitles(titles, _australia, Melbourne);
            AddTitles(titles, _australia, Vic);
            TestTask(ref index, _australia, null, Vic, titles);

            // Search within NSW.

            titles.Clear();
            AddTitles(titles, _australia, SydneyNsw2000);
            AddTitles(titles, _australia, Sydney);
            AddTitles(titles, _australia, Nsw);
            TestTask(ref index, _australia, null, Nsw, titles);
        }

        [TestMethod]
        public void TestSearchCountry()
        {
            var index = 0;
            IList<string> titles = new List<string>();

            // Create jobs.

            PostJobAds();

            // Search within Australia.

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
            TestTask(ref index, _australia, null, "", titles);

            // Search within New Zealand.

            titles.Clear();
            AddTitles(titles, _newZealand, "");
            AddTitles(titles, _newZealand, Auckland);
            AddTitles(titles, _newZealand, NorthIsland);
            TestTask(ref index, _newZealand, null, "", titles);

            // No matter what you put in for New Zealand the same jobs will come up.

            TestTask(ref index, _newZealand, null, "xyz", titles);
        }

        [TestMethod]
        public void TestSearchCount()
        {
            int index;
            IList<string> titles = new List<string>();

            var employer = CreateEmployer();
            var locationReference = new LocationReference();
            _locationQuery.ResolveLocation(locationReference, _australia, MelbourneVic3000);

            for (index = 0; index < 2 * MaximumResults; ++index)
                PostJobAd(employer, index, _australia, locationReference);

            // Should only see some of the results.

            for (index = 2 * MaximumResults - 1; index >= MaximumResults; --index)
                titles.Add(GetJobTitle(index, _australia, locationReference));

            TestTask(ref index, _australia, null, "", titles, MaximumResults, false);
        }

        private void PostJobAds()
        {
            var employer = CreateEmployer();

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

            // Create some New Zealand jobs.

            PostJobAds(employer, _newZealand, Auckland);
            PostJobAds(employer, _newZealand, NorthIsland);
        }

        private void PostJobAds(IEmployer employer, Country country, string location)
        {
            var locationReference = new LocationReference();
            _locationQuery.ResolveLocation(locationReference, country, location);
            PostJobAd(employer, 0, country, locationReference);
        }

        private void PostJobAd(IEmployer employer, int jobIndex, Country country, LocationReference location)
        {
            var jobAd = employer.CreateTestJobAd(GetJobTitle(jobIndex, country, location), "The content for job #" + jobIndex + " in " + location, GetIndustry(), location);
            _jobAdsCommand.PostJobAd(jobAd);
        }

        private static string GetJobTitle(int index, Country country, LocationReference location)
        {
            return ((location.NamedLocation is CountrySubdivision && location.NamedLocation.Name == null) ? country.Name : location.ToString()) + ": " + index;
        }

        private Industry GetIndustry()
        {
            return _industriesQuery.GetIndustries()[0];
        }

        private void AddTitles(ICollection<string> titles, Country country, string location)
        {
            var locationReference = new LocationReference();
            _locationQuery.ResolveLocation(locationReference, country, location);
            titles.Add(GetJobTitle(0, country, locationReference));
        }

        private static void AssertTitles(MockEmail email, ICollection<string> locations, int totalMatches, bool checkEachLocation)
        {
            if (totalMatches == 1)
                email.AssertHtmlViewContains("<strong>1 new job</strong>");
            else
                email.AssertHtmlViewContains("<strong>" + totalMatches + " new jobs</strong>");

            if (locations.Count != totalMatches)
            {
                var check = "top " + locations.Count + " out of total";
                email.AssertHtmlViewContains(check);
            }

            if (checkEachLocation)
            {
                foreach (var location in locations)
                    email.AssertHtmlViewContains(location);
            }
        }

        private void TestTask(ref int index, Country country, int? distance, string location, ICollection<string> titles)
        {
            TestTask(ref index, country, distance, location, titles, null, true);
        }

        private void TestTask(ref int index, Country country, int? distance, string location, ICollection<string> titles, int? totalMatches, bool checkEachLocation)
        {
            // Create member.

            var member = CreateMember(++index);

            // Create a job search.

            var criteria = new JobAdSearchCriteria
            {
                AdTitle = null,
                Location = _locationQuery.ResolveLocation(country, location),
                Distance = distance
            };

            var search = new JobAdSearch { OwnerId = member.Id, Criteria = criteria };
            _jobAdSearchAlertsCommand.CreateJobAdSearchAlert(member.Id, search, DateTime.Now.AddDays(-1));

            // Get the email.

            var email = ExecuteTask();
            AssertTitles(email, titles, totalMatches == null ? titles.Count : totalMatches.Value, checkEachLocation);
        }
    }
}