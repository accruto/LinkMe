using LinkMe.Domain.Location;
using LinkMe.Domain.Location.Queries;
using LinkMe.Domain.Roles.JobAds;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Domain.Users.Test.Employers.JobAds.Update
{
    [TestClass]
    public class LocationUpdateJobAdsTests
        : UpdateJobAdsTests
    {
        private readonly ILocationQuery _locationQuery = Resolve<ILocationQuery>();

        private const string TitleFormat = "This is the title {0}";
        private const string ContentFormat = "This is the content {0}";
        private const string Location1 = "Melbourne VIC 3000";
        private const string Location2 = "Sydney NSW 2000";

        [TestMethod]
        public void TestAddLocation()
        {
            var employer = CreateEmployer();
            var country = _locationQuery.GetCountry("Australia");

            // No location.

            var jobAd = new JobAd
            {
                PosterId = employer.Id,
                Title = string.Format(TitleFormat, 0),
                Description = 
                {
                    Content = string.Format(ContentFormat, 1),
                }
            };
            _jobAdsCommand.CreateJobAd(jobAd);

            AssertLocation(jobAd.Description.Location, _jobAdsQuery.GetJobAd<JobAd>(jobAd.Id).Description.Location);

            // Add it.

            jobAd.Description.Location = _locationQuery.ResolveLocation(country, Location2);
            _jobAdsCommand.UpdateJobAd(jobAd);

            AssertLocation(jobAd.Description.Location, _jobAdsQuery.GetJobAd<JobAd>(jobAd.Id).Description.Location);
        }

        [TestMethod]
        public void TestRemoveLocation()
        {
            var employer = CreateEmployer();
            var country = _locationQuery.GetCountry("Australia");

            // Create with location.

            var jobAd = new JobAd
            {
                PosterId = employer.Id,
                Title = string.Format(TitleFormat, 0),
                Description = 
                {
                    Content = string.Format(ContentFormat, 1),
                    Location = _locationQuery.ResolveLocation(country, Location1),
                }
            };
            _jobAdsCommand.CreateJobAd(jobAd);

            AssertLocation(jobAd.Description.Location, _jobAdsQuery.GetJobAd<JobAd>(jobAd.Id).Description.Location);

            // Remove it.

            jobAd.Description.Location = null;
            _jobAdsCommand.UpdateJobAd(jobAd);

            AssertLocation(jobAd.Description.Location, _jobAdsQuery.GetJobAd<JobAd>(jobAd.Id).Description.Location);
        }

        [TestMethod]
        public void TestUpdateLocation()
        {
            var employer = CreateEmployer();
            var country = _locationQuery.GetCountry("Australia");

            // Create with location.

            var jobAd = new JobAd
            {
                PosterId = employer.Id,
                Title = string.Format(TitleFormat, 0),
                Description = 
                {
                    Content = string.Format(ContentFormat, 1),
                    Location = _locationQuery.ResolveLocation(country, Location1),
                }
            };
            _jobAdsCommand.CreateJobAd(jobAd);

            AssertLocation(jobAd.Description.Location, _jobAdsQuery.GetJobAd<JobAd>(jobAd.Id).Description.Location);

            // Update it.

            jobAd.Description.Location = _locationQuery.ResolveLocation(country, Location2);
            _jobAdsCommand.UpdateJobAd(jobAd);

            AssertLocation(jobAd.Description.Location, _jobAdsQuery.GetJobAd<JobAd>(jobAd.Id).Description.Location);
        }

        private static void AssertLocation(LocationReference expectedLocation, LocationReference location)
        {
            if (expectedLocation == null)
            {
                Assert.IsNull(location);
            }
            else
            {
                Assert.IsNotNull(location);
                Assert.AreEqual(expectedLocation, location);
            }
        }
    }
}