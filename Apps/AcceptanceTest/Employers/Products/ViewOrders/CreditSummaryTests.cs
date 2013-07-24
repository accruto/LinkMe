using System;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Credits;
using LinkMe.Domain.Credits.Commands;
using LinkMe.Domain.Credits.Queries;
using LinkMe.Framework.Utility.Urls;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Employers.Products.ViewOrders
{
    [TestClass]
    public class CreditSummaryTests
        : ProductsTests
    {
        private readonly ICreditsQuery _creditsQuery = Resolve<ICreditsQuery>();
        private readonly IAllocationsCommand _allocationsCommand = Resolve<IAllocationsCommand>();

        private ContactCredit _contactCredit;
        private JobAdCredit _jobAdCredit;
        private ApplicantCredit _applicantCredit;

        private ReadOnlyUrl _openJobAdsUrl;

        [TestInitialize]
        public void TestInitialize()
        {
            _contactCredit = _creditsQuery.GetCredit<ContactCredit>();
            _jobAdCredit = _creditsQuery.GetCredit<JobAdCredit>();
            _applicantCredit = _creditsQuery.GetCredit<ApplicantCredit>();

            _openJobAdsUrl = new ReadOnlyApplicationUrl(true, "~/ui/registered/employers/EmployerJobAds.aspx?mode=Open");
        }

        [TestMethod]
        public void TestNoAllocations()
        {
            var employer = CreateEmployer(0, true);

            AssertCreditSummary(
                employer,
                new Allocation { CreditId = _contactCredit.Id, RemainingQuantity = 0 });
        }

        [TestMethod]
        public void TestZeroContactCredits()
        {
            var employer = CreateEmployer(0, true);
            var allocation = new Allocation { CreditId = _contactCredit.Id, InitialQuantity = 0, ExpiryDate = DateTime.Now.AddDays(10), OwnerId = employer.Id };
            _allocationsCommand.CreateAllocation(allocation);

            AssertCreditSummary(
                employer,
                allocation);
        }

        [TestMethod]
        public void TestFiniteContactCredits()
        {
            var employer = CreateEmployer(0, true);
            var allocation = new Allocation { CreditId = _contactCredit.Id, InitialQuantity = 10, ExpiryDate = DateTime.Now.AddDays(10), OwnerId = employer.Id };
            _allocationsCommand.CreateAllocation(allocation);

            AssertCreditSummary(
                employer,
                allocation);
        }

        [TestMethod]
        public void TestInfiniteContactCredits()
        {
            var employer = CreateEmployer(0, true);
            var allocation = new Allocation { CreditId = _contactCredit.Id, InitialQuantity = null, ExpiryDate = DateTime.Now.AddDays(10), OwnerId = employer.Id };
            _allocationsCommand.CreateAllocation(allocation);

            AssertCreditSummary(
                employer);
        }

        [TestMethod]
        public void TestZeroJobAdCredits()
        {
            var employer = CreateEmployer(0, true);
            var allocation = new Allocation { CreditId = _jobAdCredit.Id, InitialQuantity = 0, ExpiryDate = DateTime.Now.AddDays(10), OwnerId = employer.Id };
            _allocationsCommand.CreateAllocation(allocation);

            AssertCreditSummary(
                employer,
                new Allocation { CreditId = _contactCredit.Id, RemainingQuantity = 0 });
        }

        [TestMethod]
        public void TestFiniteJobAdCredits()
        {
            var employer = CreateEmployer(0, true);
            var allocation = new Allocation { CreditId = _jobAdCredit.Id, InitialQuantity = 10, ExpiryDate = DateTime.Now.AddDays(10), OwnerId = employer.Id };
            _allocationsCommand.CreateAllocation(allocation);

            AssertCreditSummary(
                employer,
                new Allocation { CreditId = _contactCredit.Id, RemainingQuantity = 0 },
                allocation);
        }

        [TestMethod]
        public void TestInfiniteJobAdCredits()
        {
            var employer = CreateEmployer(0, true);
            var allocation = new Allocation { CreditId = _jobAdCredit.Id, InitialQuantity = null, ExpiryDate = DateTime.Now.AddDays(10), OwnerId = employer.Id };
            _allocationsCommand.CreateAllocation(allocation);

            AssertCreditSummary(
                employer,
                new Allocation { CreditId = _contactCredit.Id, RemainingQuantity = 0 });
        }

        [TestMethod]
        public void TestZeroApplicantCredits()
        {
            var employer = CreateEmployer(0, true);
            var allocation = new Allocation { CreditId = _applicantCredit.Id, InitialQuantity = 0, ExpiryDate = DateTime.Now.AddDays(10), OwnerId = employer.Id };
            _allocationsCommand.CreateAllocation(allocation);

            AssertCreditSummary(
                employer,
                new Allocation { CreditId = _contactCredit.Id, RemainingQuantity = 0 });
        }

        [TestMethod]
        public void TestFiniteApplicantCredits()
        {
            var employer = CreateEmployer(0, true);
            var allocation = new Allocation { CreditId = _applicantCredit.Id, InitialQuantity = 10, ExpiryDate = DateTime.Now.AddDays(10), OwnerId = employer.Id };
            _allocationsCommand.CreateAllocation(allocation);

            AssertCreditSummary(
                employer,
                new Allocation { CreditId = _contactCredit.Id, RemainingQuantity = 0 },
                allocation);
        }

        [TestMethod]
        public void TestInfiniteApplicantCredits()
        {
            var employer = CreateEmployer(0, true);
            var allocation = new Allocation { CreditId = _applicantCredit.Id, InitialQuantity = null, ExpiryDate = DateTime.Now.AddDays(10), OwnerId = employer.Id };
            _allocationsCommand.CreateAllocation(allocation);

            AssertCreditSummary(
                employer,
                new Allocation { CreditId = _contactCredit.Id, RemainingQuantity = 0 });
        }

        private void AssertCreditSummary(IUser employer, params Allocation[] allocations)
        {
            LogIn(employer);
            AssertCreditSummary(allocations);
            Get(_openJobAdsUrl);
            AssertCreditSummary(allocations);
        }

        private void AssertCreditSummary(params Allocation[] allocations)
        {
            var trNodes = Browser.CurrentHtml.DocumentNode.SelectNodes("//table[@class='credit-summary']//tr");
            if (allocations.Length == 0)
            {
                Assert.IsNull(trNodes);
            }
            else
            {
                Assert.IsNotNull(trNodes);
                Assert.AreEqual(allocations.Length, trNodes.Count);

                for (var index = 0; index < allocations.Length; ++index)
                {
                    var allocation = allocations[index];
                    var credit = _creditsQuery.GetCredit(allocation.CreditId);
                    var trNode = trNodes[index];

                    var description = trNode.SelectSingleNode("th[@class='credit-item']/a").InnerText;
                    Assert.AreEqual(credit.Description, description);
                    Assert.AreEqual(allocation.RemainingQuantity + " credits", trNode.SelectSingleNode("td[@class='credits']").InnerText);

                    if (allocation.RemainingQuantity != 0)
                        Assert.AreEqual("Expires " + (allocation.ExpiryDate == null ? "never" : allocation.ExpiryDate.Value.ToShortDateString()), trNode.SelectSingleNode("td[@class='expiry']").InnerText.Trim());
                    else
                        Assert.AreEqual("", trNode.SelectSingleNode("td[@class='expiry']").InnerText.Trim());
                }
            }
        }
    }
}
