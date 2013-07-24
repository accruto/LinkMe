using System;
using LinkMe.Domain.Roles.JobAds;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Employers.JobAds.MaintainJobAds.EditJobAds
{
    [TestClass]
    public class LocationTests
        : EditJobAdTests
    {
        private const string Location = "Norlane VIC 3214";
        private const string ChangedLocation = "Camberwell VIC 3124";

        protected override void SetValue(JobAd jobAd)
        {
            jobAd.Description.Location = _locationQuery.ResolveLocation(_locationQuery.GetCountry(1), Location);
        }

        protected override void SetDisplayValue()
        {
            _locationTextBox.Text = ChangedLocation;
        }

        protected override void AssertSavedValue(JobAd jobAd)
        {
            Assert.AreEqual(ChangedLocation, jobAd.Description.Location.ToString());
        }

        protected override void TestErrorValues(bool save, Guid jobAdId)
        {
            AssertErrorValue(save, jobAdId, () => _locationTextBox.Text = "", "The location is required.");
        }
    }
}
