using System;
using System.Globalization;
using LinkMe.Domain.Roles.JobAds;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Employers.JobAds.MaintainJobAds.EditJobAds
{
    [TestClass]
    public class CountryIdTests
        : EditJobAdTests
    {
        private const string Location = "Somewhere";

        protected override void SetValue(JobAd jobAd)
        {
            jobAd.Description.Location = _locationQuery.ResolveLocation(_locationQuery.GetCountry(2), Location);
        }

        protected override void SetDisplayValue()
        {
            _countryIdDropDownList.SelectedValue = 3.ToString(CultureInfo.InvariantCulture);
        }

        protected override void AssertSavedValue(JobAd jobAd)
        {
            Assert.AreEqual(3, jobAd.Description.Location.Country.Id);
        }

        protected override void TestErrorValues(bool save, Guid jobAdId)
        {
        }
    }
}
