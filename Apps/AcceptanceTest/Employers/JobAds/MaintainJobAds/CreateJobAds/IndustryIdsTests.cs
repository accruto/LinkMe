using System.Collections.Generic;
using System.Linq;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.JobAds;
using LinkMe.Framework.Utility;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Employers.JobAds.MaintainJobAds.CreateJobAds
{
    [TestClass]
    public class IndustryIdsTests
        : CreateJobAdTests
    {
        protected override void AssertDefaultDisplayValue(IEmployer employer)
        {
            Assert.AreEqual(0, _industryIdsListBox.SelectedItems.Count);
        }

        protected override void AssertSavedDefaultValue(IEmployer employer, JobAd jobAd)
        {
            Assert.IsTrue(jobAd.Description.Industries.Select(i => i.Id).CollectionEqual(new[] { DefaultIndustry.Id }));
        }

        protected override void SetDisplayValue()
        {
            _industryIdsListBox.SelectedValues = new[] { _accounting.Id.ToString(), _administration.Id.ToString() };
        }

        protected override void AssertSavedValue(JobAd jobAd)
        {
            Assert.IsTrue(jobAd.Description.Industries.Select(i => i.Id).CollectionEqual(new[] { _accounting.Id, _administration.Id }));
        }

        protected override void TestErrorValues(bool save, IEmployer employer)
        {
            AssertErrorValue(save, employer, () => _industryIdsListBox.SelectedValues = new List<string>(), "The industry is required.");
        }
    }
}
