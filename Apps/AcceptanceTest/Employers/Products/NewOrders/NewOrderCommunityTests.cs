using LinkMe.Apps.Agents.Test.Communities;
using LinkMe.Apps.Agents.Users.Employers.Queries;
using LinkMe.Domain.Roles.Affiliations.Communities;
using LinkMe.Domain.Roles.Affiliations.Communities.Commands;
using LinkMe.Domain.Roles.Affiliations.Verticals.Commands;
using LinkMe.Domain.Roles.Test.Affiliations.Communities;
using LinkMe.Framework.Content;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Employers.Products.NewOrders
{
    [TestClass]
    public class NewOrderCommunityTests
        : NewOrderTests
    {
        private readonly IEmployerAccountsQuery _employerAccountsQuery = Resolve<IEmployerAccountsQuery>();
        private readonly ICommunitiesCommand _communitiesCommand = Resolve<ICommunitiesCommand>();
        private readonly IVerticalsCommand _verticalsCommand = Resolve<IVerticalsCommand>();
        private readonly IContentEngine _contentEngine = Resolve<IContentEngine>();

        [TestMethod]
        public void TestJoinCommunityWithNewOrder()
        {
            TestJoinWithNewOrder(TestCommunity.BusinessSpectator, true);
        }

        [TestMethod]
        public void TestJoinCommunityWithNoOrganisationsWithNewOrder()
        {
            TestJoinWithNewOrder(TestCommunity.Rcsa, false);
        }

        private void TestJoinWithNewOrder(TestCommunity testCommunity, bool shouldBeJoined)
        {
            var data = testCommunity.GetCommunityTestData();
            var community = data.CreateTestCommunity(_communitiesCommand, _verticalsCommand, _contentEngine);

            // Hit the landing page.

            var url = _verticalsCommand.GetCommunityPathUrl(community, "employers/Employer.aspx");
            Get(url);

            // Choose.

            var host = Browser.CurrentUrl.Host;
            var chooseUrl = GetChooseUrl().AsNonReadOnly();
            chooseUrl.Host = host;
            Get(chooseUrl);

            SelectContactProduct(_productsQuery.GetProduct("Contacts40"));
            _purchaseButton.Click();
            var instanceId = GetInstanceId();

            // Join.

            var accountUrl = GetAccountUrl(instanceId).AsNonReadOnly();
            accountUrl.Host = host;
            AssertUrl(accountUrl);

            _joinLoginIdTextBox.Text = LoginId;
            _joinPasswordTextBox.Text = Password;
            _joinConfirmPasswordTextBox.Text = Password;
            _firstNameTextBox.Text = FirstName;
            _lastNameTextBox.Text = LastName;
            _emailAddressTextBox.Text = EmailAddress;
            _phoneNumberTextBox.Text = PhoneNumber;
            _organisationNameTextBox.Text = OrganisationName;
            _locationTextBox.Text = Location;
            _acceptTermsCheckBox.IsChecked = true;
            _joinButton.Click();

            // Payment.

            var paymentUrl = GetPaymentUrl(instanceId).AsNonReadOnly();
            paymentUrl.Host = host;
            AssertUrl(paymentUrl);

            _cardNumberTextBox.Text = CreditCardNumber;
            _cvvTextBox.Text = Cvv;
            _cardHolderNameTextBox.Text = CardHolderName;
            _authoriseCreditCardCheckBox.IsChecked = true;
            _purchaseButton.Click();

            // Receipt.

            var receiptUrl = GetReceiptUrl(instanceId).AsNonReadOnly();
            receiptUrl.Host = host;
            AssertUrl(receiptUrl);

            AssertCommunity(community, LoginId, shouldBeJoined);
        }

        private void AssertCommunity(Community community, string loginId, bool shouldBeJoined)
        {
            var employer = _employerAccountsQuery.GetEmployer(loginId);

            if (shouldBeJoined)
                Assert.AreEqual(community.Id, employer.Organisation.AffiliateId);
            else
                Assert.AreEqual(null, employer.Organisation.AffiliateId);
        }
    }
}