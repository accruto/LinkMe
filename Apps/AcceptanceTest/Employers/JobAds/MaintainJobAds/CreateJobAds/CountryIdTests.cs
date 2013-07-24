using System.Globalization;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.JobAds;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Employers.JobAds.MaintainJobAds.CreateJobAds
{
    [TestClass]
    public class CountryIdTests
        : CreateJobAdTests
    {
        protected override void AssertDefaultDisplayValue(IEmployer employer)
        {
            Assert.AreEqual(_locationQuery.GetCountry("Australia").Id.ToString(CultureInfo.InvariantCulture), _countryIdDropDownList.SelectedItem.Value);
        }

        protected override void AssertSavedDefaultValue(IEmployer employer, JobAd jobAd)
        {
            Assert.AreEqual(1, jobAd.Description.Location.Country.Id);
        }

        protected override void SetDisplayValue()
        {
            _countryIdDropDownList.SelectedValue = 2.ToString(CultureInfo.InvariantCulture);
        }

        protected override void AssertSavedValue(JobAd jobAd)
        {
            Assert.AreEqual(2, jobAd.Description.Location.Country.Id);
        }

        protected override void TestErrorValues(bool save, IEmployer employer)
        {
        }
    }
}
