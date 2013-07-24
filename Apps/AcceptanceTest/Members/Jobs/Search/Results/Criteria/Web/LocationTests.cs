using LinkMe.Domain.Contacts;
using LinkMe.Domain.Location;
using LinkMe.Domain.Roles.JobAds;
using LinkMe.Domain.Roles.Test.JobAds;
using LinkMe.Domain.Users.Test.Employers.JobAds;
using LinkMe.Query.Search.JobAds;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Members.Jobs.Search.Results.Criteria.Web
{
    [TestClass]
    public abstract class LocationTests
        : CriteriaTests
    {
        protected LocationReference _australia;
        protected LocationReference _newZealand;

        protected LocationReference _melbourneVic3000;
        protected LocationReference _armadaleVic3143;
        protected LocationReference _norlaneVic3214;
        protected LocationReference _sydneyNsw2000;
        protected LocationReference _perthWa6000;

        protected LocationReference _melbourne;
        protected LocationReference _sydney;
        protected LocationReference _perth;
        protected LocationReference _goldCoast;

        protected LocationReference _vic;
        protected LocationReference _nsw;
        protected LocationReference _wa;

        protected LocationReference _auckland;
        protected LocationReference _northIsland;

        protected JobAd _australiaJobAd;
        protected JobAd _newZealandJobAd;

        protected JobAd _melbourneVic3000JobAd;
        protected JobAd _armadaleVic3143JobAd;
        protected JobAd _norlaneVic3214JobAd;
        protected JobAd _sydneyNsw2000JobAd;

        protected JobAd _melbourneJobAd;
        protected JobAd _sydneyJobAd;

        protected JobAd _vicJobAd;
        protected JobAd _nswJobAd;

        protected JobAd _aucklandJobAd;
        protected JobAd _northIslandJobAd;

        [TestInitialize]
        public void TestInitialize()
        {
            var australiaCountry = _locationQuery.GetCountry("Australia");
            var newZealandCountry = _locationQuery.GetCountry("New Zealand");

            _australia = _locationQuery.ResolveLocation(australiaCountry, null);
            _newZealand = _locationQuery.ResolveLocation(newZealandCountry, null);

            _melbourneVic3000 = _locationQuery.ResolveLocation(australiaCountry, "Melbourne VIC 3000");
            _armadaleVic3143 = _locationQuery.ResolveLocation(australiaCountry, "Armadale VIC 3143");
            _norlaneVic3214 = _locationQuery.ResolveLocation(australiaCountry, "Norlane VIC 3214");
            _sydneyNsw2000 = _locationQuery.ResolveLocation(australiaCountry, "Sydney NSW 2000");
            _perthWa6000 = _locationQuery.ResolveLocation(australiaCountry, "Perth WA 6000");

            _melbourne = _locationQuery.ResolveLocation(australiaCountry, "Melbourne");
            _sydney = _locationQuery.ResolveLocation(australiaCountry, "Sydney");
            _perth = _locationQuery.ResolveLocation(australiaCountry, "Perth");
            _goldCoast = _locationQuery.ResolveLocation(australiaCountry, "Gold Coast");

            _vic = _locationQuery.ResolveLocation(australiaCountry, "VIC");
            _nsw = _locationQuery.ResolveLocation(australiaCountry, "NSW");
            _wa = _locationQuery.ResolveLocation(australiaCountry, "WA");

            _auckland = _locationQuery.ResolveLocation(newZealandCountry, "Auckland");
            _northIsland = _locationQuery.ResolveLocation(newZealandCountry, "North Island");
        }

        protected void PostJobAds()
        {
            var employer = CreateEmployer(0);

            // Create jobs in the localities.

            _melbourneVic3000JobAd = PostJobAd(employer, _melbourneVic3000);
            _armadaleVic3143JobAd = PostJobAd(employer, _armadaleVic3143);
            _norlaneVic3214JobAd = PostJobAd(employer, _norlaneVic3214);
            _sydneyNsw2000JobAd = PostJobAd(employer, _sydneyNsw2000);

            // Create jobs in the regions.

            _melbourneJobAd = PostJobAd(employer, _melbourne);
            _sydneyJobAd = PostJobAd(employer, _sydney);

            // Create jobs in the subdivisions.

            _vicJobAd = PostJobAd(employer, _vic);
            _nswJobAd = PostJobAd(employer, _nsw);

            // Create jobs in the countries.

            _australiaJobAd = PostJobAd(employer, _australia);
            _newZealandJobAd = PostJobAd(employer, _newZealand);

            // Create some New Zealand jobs.

            _aucklandJobAd = PostJobAd(employer, _auckland);
            _northIslandJobAd = PostJobAd(employer, _northIsland);
        }

        private JobAd PostJobAd(IEmployer employer, LocationReference locationReference)
        {
            var location = locationReference.IsCountry ? locationReference.Country.Name : locationReference.ToString();
            var jobAd = employer.CreateTestJobAd("Job in " + location + " title", "Job in " + location + " content", null, locationReference);
            _jobAdsCommand.PostJobAd(jobAd);
            return jobAd;
        }

        protected void TestResults(LocationReference location, int? distance, params JobAd[] expectedJobAds)
        {
            var criteria = new JobAdSearchCriteria { Location = location, Distance = distance };
            Get(GetSearchUrl(criteria));
            AssertResults(false, expectedJobAds);

            // Assert.

            var model = ApiSearch(criteria);
            AssertApiResults(model, false, expectedJobAds);
        }
    }
}