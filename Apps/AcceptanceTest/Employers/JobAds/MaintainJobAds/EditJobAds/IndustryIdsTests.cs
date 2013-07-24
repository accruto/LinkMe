using System;
using System.Collections.Generic;
using System.Linq;
using LinkMe.Domain.Industries;
using LinkMe.Domain.Roles.JobAds;
using LinkMe.Framework.Utility;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Employers.JobAds.MaintainJobAds.EditJobAds
{
    [TestClass]
    public class IndustryIdsTests
        : EditJobAdTests
    {
        private Industry _scienceTechnology;
        private Industry _bankingFinancialServices;
        private Industry _tradesServices;

        [TestInitialize]
        public void TestInitialize()
        {
            _scienceTechnology = _industriesQuery.GetIndustry("Science & Technology");
            _bankingFinancialServices = _industriesQuery.GetIndustry("Banking & Financial Services");
            _tradesServices = _industriesQuery.GetIndustry("Trades & Services");
        }

        protected override void SetValue(JobAd jobAd)
        {
            jobAd.Description.Industries = new[] { _accounting, _administration };
        }

        protected override void SetDisplayValue()
        {
            _industryIdsListBox.SelectedValues = new[] { _scienceTechnology.Id.ToString(), _bankingFinancialServices.Id.ToString(), _tradesServices.Id.ToString() };
        }

        protected override void AssertSavedValue(JobAd jobAd)
        {
            Assert.IsTrue(new[] { _scienceTechnology.Id, _bankingFinancialServices.Id, _tradesServices.Id }.CollectionEqual(jobAd.Description.Industries.Select(i => i.Id)));
        }

        protected override void TestErrorValues(bool save, Guid jobAdId)
        {
            AssertErrorValue(save, jobAdId, () => _industryIdsListBox.SelectedValues = new List<string>(), "The industry is required.");
        }
    }
}
