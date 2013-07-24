using System;
using LinkMe.Apps.Asp.Test.Html;
using LinkMe.Domain.Roles.JobAds;
using LinkMe.Domain.Roles.Orders;
using LinkMe.Domain.Roles.Orders.Queries;
using LinkMe.Domain.Roles.Test.Communications.Mocks;
using LinkMe.Domain.Users.Employers.Orders.Queries;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Employers.JobAds.MaintainJobAds.NewJobAdFlow
{
    [TestClass]
    public class ZeroCreditsNewJobAdFlowTests
        : EmployerNewJobAdFlowTests
    {
        private readonly IOrdersQuery _ordersQuery = Resolve<IOrdersQuery>();
        private readonly IEmployerOrdersQuery _employerOrdersQuery = Resolve<IEmployerOrdersQuery>();

        private HtmlTextBoxTester _cardNumberTextBox;
        private HtmlTextBoxTester _cvvTextBox;
        private HtmlTextBoxTester _cardHolderNameTextBox;
        private HtmlCheckBoxTester _authoriseCreditCardCheckBox;
        private HtmlButtonTester _purchaseButton;

        private const string CreditCardNumber = "4444333322221111";
        private const string Cvv = "123";
        private const string CardHolderName = "Marge Simpson";

        [TestInitialize]
        public void ZeroCreditsNewJobAdFlowTestInitialize()
        {
            _cardNumberTextBox = new HtmlTextBoxTester(Browser, "CardNumber");
            _cvvTextBox = new HtmlTextBoxTester(Browser, "Cvv");
            _cardHolderNameTextBox = new HtmlTextBoxTester(Browser, "CardHolderName");
            _authoriseCreditCardCheckBox = new HtmlCheckBoxTester(Browser, "authoriseCreditCard");
            _purchaseButton = new HtmlButtonTester(Browser, "purchase");
        }

        protected override int? GetEmployerCredits()
        {
            return 0;
        }

        protected override bool ShouldFeaturePacksShow
        {
            get { return true; }
        }

        [TestMethod]
        public void TestLoggedInFeaturePack1()
        {
            var credits = GetEmployerCredits();
            var employer = CreateEmployer(credits);
            LogIn(employer);

            // Job ad.

            Get(GetJobAdUrl(null));

            AssertJobAdPage(true);

            _titleTextBox.Text = DefaultTitle;
            _contentTextBox.Text = DefaultContent;
            _industryIdsListBox.SelectedValues = new[] { _accounting.Id.ToString() };
            _fullTimeCheckBox.IsChecked = true;
            _locationTextBox.Text = DefaultLocation;

            // Preview.

            _previewButton.Click();

            var jobAdId = AssertJobAd(employer.Id, JobAdStatus.Draft, JobAdFeatures.None).Id;
            AssertPreviewPage(jobAdId, true, credits);

            // Publish.

            _featurePack1.IsChecked = true;
            _publishButton.Click();

            AssertJobAd(employer.Id, JobAdStatus.Draft, JobAdFeatures.None);
            AssertPaymentPage(jobAdId, JobAdFeaturePack.FeaturePack1);

            // Pay.

            _cardNumberTextBox.Text = CreditCardNumber;
            _cvvTextBox.Text = Cvv;
            _cardHolderNameTextBox.Text = CardHolderName;
            _authoriseCreditCardCheckBox.IsChecked = true;
            _purchaseButton.Click();

            var jobAd = AssertJobAd(employer.Id, JobAdStatus.Open, JobAdFeatures.Logo | JobAdFeatures.ExtendedExpiry);
            var order = AssertOrder(employer.Id, JobAdFeaturePack.FeaturePack1);
            AssertReceiptPage(jobAd, order);

            _emailServer.AssertNoEmailSent();
        }

        [TestMethod]
        public void TestLoggedInFeaturePack2()
        {
            var credits = GetEmployerCredits();
            var employer = CreateEmployer(credits);
            LogIn(employer);

            // Job ad.

            Get(GetJobAdUrl(null));

            AssertJobAdPage(true);

            _titleTextBox.Text = DefaultTitle;
            _contentTextBox.Text = DefaultContent;
            _industryIdsListBox.SelectedValues = new[] { _accounting.Id.ToString() };
            _fullTimeCheckBox.IsChecked = true;
            _locationTextBox.Text = DefaultLocation;

            // Preview.

            _previewButton.Click();

            var jobAdId = AssertJobAd(employer.Id, JobAdStatus.Draft, JobAdFeatures.None).Id;
            AssertPreviewPage(jobAdId, true, credits);

            // Publish.

            _featurePack2.IsChecked = true;
            _publishButton.Click();

            AssertJobAd(employer.Id, JobAdStatus.Draft, JobAdFeatures.None);
            AssertPaymentPage(jobAdId, JobAdFeaturePack.FeaturePack2);

            // Pay.

            _cardNumberTextBox.Text = CreditCardNumber;
            _cvvTextBox.Text = Cvv;
            _cardHolderNameTextBox.Text = CardHolderName;
            _authoriseCreditCardCheckBox.IsChecked = true;
            _purchaseButton.Click();

            var jobAd = AssertJobAd(employer.Id, JobAdStatus.Open, JobAdFeatures.Logo | JobAdFeatures.ExtendedExpiry | JobAdFeatures.Highlight | JobAdFeatures.Refresh);
            var order = AssertOrder(employer.Id, JobAdFeaturePack.FeaturePack2);
            AssertReceiptPage(jobAd, order);

            _emailServer.AssertNoEmailSent();
        }

        private Order AssertOrder(Guid employerId, JobAdFeaturePack featurePack)
        {
            var orders = _ordersQuery.GetOrders(employerId);
            Assert.AreEqual(1, orders.Count);
            var order = orders[0];
            Assert.AreEqual(featurePack == JobAdFeaturePack.FeaturePack1 ? 10m : 20m, order.Price);
            Assert.AreEqual(featurePack == JobAdFeaturePack.FeaturePack1 ? 11m : 22m, order.AdjustedPrice);
            Assert.AreEqual(employerId, order.OwnerId);
            Assert.AreEqual(employerId, order.PurchaserId);

            Assert.AreEqual(1, order.Adjustments.Count);
            var adjustment = order.Adjustments[0];
            Assert.AreEqual(featurePack == JobAdFeaturePack.FeaturePack1 ? 11m : 22m, adjustment.AdjustedPrice);
            Assert.AreEqual(featurePack == JobAdFeaturePack.FeaturePack1 ? 10m : 20m, adjustment.InitialPrice);

            Assert.AreEqual(1, order.Items.Count);
            var item = order.Items[0];
            Assert.AreEqual(featurePack == JobAdFeaturePack.FeaturePack1 ? 10m : 20m, item.Price);

            var product = _employerOrdersQuery.GetJobAdFeaturePackProduct(featurePack);
            Assert.AreEqual(product.Id, item.ProductId);

            return order;
        }
    }
}
