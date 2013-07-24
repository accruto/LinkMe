using System;
using LinkMe.Apps.Agents.Users.Employers.Commands;
using LinkMe.Apps.Agents.Test.Users.Employers;
using LinkMe.Apps.Asp.Test.Html;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Products;
using LinkMe.Domain.Products.Commands;
using LinkMe.Domain.Products.Queries;
using LinkMe.Domain.Roles.Recruiters.Commands;
using LinkMe.Domain.Roles.Test.Recruiters;
using LinkMe.Framework.Utility.Urls;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Employers.Products
{
    [TestClass]
    public abstract class ProductsTests
        : WebTestClass
    {
        protected ReadOnlyUrl _newOrderUrl;
        private ReadOnlyUrl _chooseUrl;
        private ReadOnlyUrl _accountUrl;
        private ReadOnlyUrl _paymentUrl;
        private ReadOnlyUrl _receiptUrl;
        protected ReadOnlyUrl _creditsUrl;
        protected ReadOnlyUrl _ordersUrl;
        protected ReadOnlyUrl _orderUrl;
        protected ReadOnlyUrl _searchUrl;

        protected HtmlButtonTester _joinButton;
        protected HtmlButtonTester _loginButton;
        protected HtmlButtonTester _purchaseButton;
        protected HtmlButtonTester _backButton;
        protected HtmlButtonTester _cancelButton;

        protected HtmlDropDownListTester _contactProductIdDropDownList;

        protected HtmlTextBoxTester _loginIdTextBox;
        protected HtmlPasswordTester _passwordTextBox;
        protected HtmlCheckBoxTester _rememberMeCheckBox;
        protected HtmlTextBoxTester _joinLoginIdTextBox;
        protected HtmlPasswordTester _joinPasswordTextBox;
        protected HtmlPasswordTester _joinConfirmPasswordTextBox;
        protected HtmlTextBoxTester _firstNameTextBox;
        protected HtmlTextBoxTester _lastNameTextBox;
        protected HtmlTextBoxTester _emailAddressTextBox;
        protected HtmlTextBoxTester _phoneNumberTextBox;
        protected HtmlTextBoxTester _organisationNameTextBox;
        protected HtmlTextBoxTester _locationTextBox;
        protected HtmlCheckBoxTester _acceptTermsCheckBox;

        protected HtmlTextBoxTester _cardNumberTextBox;
        protected HtmlTextBoxTester _cvvTextBox;
        protected HtmlTextBoxTester _cardHolderNameTextBox;
        protected HtmlDropDownListTester _cardTypeDropDownList;
        protected HtmlDropDownListTester _expiryMonthDropDownList;
        protected HtmlDropDownListTester _expiryYearDropDownList;
        protected HtmlCheckBoxTester _authoriseCreditCardCheckBox;

        protected readonly IEmployerAccountsCommand _employerAccountsCommand = Resolve<IEmployerAccountsCommand>();
        protected readonly IOrganisationsCommand _organisationsCommand = Resolve<IOrganisationsCommand>();
        protected readonly IProductsQuery _productsQuery = Resolve<IProductsQuery>();
        protected readonly IProductsCommand _productsCommand = Resolve<IProductsCommand>();

        protected const string LoginId = "tester";
        protected const string Password = "password";
        protected const string FirstName = "Marge";
        protected const string LastName = "Simpson";
        protected const string EmailAddress = "tester@test.linkme.net.au";
        protected const string PhoneNumber = "0399998888";
        protected const string OrganisationName = "Acme";
        protected const string Location = "Camberwell VIC 3124";
        protected const string CreditCardNumber = "4444333322221111";
        protected const string Cvv = "123";
        protected const string CardHolderName = "Marge Simpson";

        [TestInitialize]
        public void ProductsTestsInitialize()
        {
            _newOrderUrl = new ReadOnlyApplicationUrl(true, "~/employers/products/neworder");
            _chooseUrl = new ReadOnlyApplicationUrl(true, "~/employers/products/choose");
            _accountUrl = new ReadOnlyApplicationUrl(true, "~/employers/products/account");
            _paymentUrl = new ReadOnlyApplicationUrl(true, "~/employers/products/payment");
            _receiptUrl = new ReadOnlyApplicationUrl(true, "~/employers/products/receipt");
            _creditsUrl = new ReadOnlyApplicationUrl(true, "~/employers/products/credits");
            _ordersUrl = new ReadOnlyApplicationUrl(true, "~/employers/products/orders");
            _orderUrl = new ReadOnlyApplicationUrl(true, "~/employers/products/order");
            _searchUrl = new ReadOnlyApplicationUrl(true, "~/search/candidates");

            _joinButton = new HtmlButtonTester(Browser, "join");
            _loginButton = new HtmlButtonTester(Browser, "login");
            _purchaseButton = new HtmlButtonTester(Browser, "purchase");
            _backButton = new HtmlButtonTester(Browser, "back");
            _cancelButton = new HtmlButtonTester(Browser, "cancel");

            _contactProductIdDropDownList = new HtmlDropDownListTester(Browser, "ContactProductId");

            _loginIdTextBox = new HtmlTextBoxTester(Browser, "LoginId");
            _passwordTextBox = new HtmlPasswordTester(Browser, "Password");
            _rememberMeCheckBox = new HtmlCheckBoxTester(Browser, "RememberMe");

            _joinLoginIdTextBox = new HtmlTextBoxTester(Browser, "JoinLoginId");
            _joinPasswordTextBox = new HtmlPasswordTester(Browser, "JoinPassword");
            _joinConfirmPasswordTextBox = new HtmlPasswordTester(Browser, "JoinConfirmPassword");
            _firstNameTextBox = new HtmlTextBoxTester(Browser, "FirstName");
            _lastNameTextBox = new HtmlTextBoxTester(Browser, "LastName");
            _emailAddressTextBox = new HtmlTextBoxTester(Browser, "EmailAddress");
            _phoneNumberTextBox = new HtmlTextBoxTester(Browser, "PhoneNumber");
            _organisationNameTextBox = new HtmlTextBoxTester(Browser, "OrganisationName");
            _locationTextBox = new HtmlTextBoxTester(Browser, "Location");
            _acceptTermsCheckBox = new HtmlCheckBoxTester(Browser, "AcceptTerms");

            _cardNumberTextBox = new HtmlTextBoxTester(Browser, "CardNumber");
            _cvvTextBox = new HtmlTextBoxTester(Browser, "Cvv");
            _cardHolderNameTextBox = new HtmlTextBoxTester(Browser, "CardHolderName");
            _cardTypeDropDownList = new HtmlDropDownListTester(Browser, "CardType");
            _expiryMonthDropDownList = new HtmlDropDownListTester(Browser, "ExpiryMonth");
            _expiryYearDropDownList = new HtmlDropDownListTester(Browser, "ExpiryYear");
            _authoriseCreditCardCheckBox = new HtmlCheckBoxTester(Browser, "authoriseCreditCard");
        }

        protected ReadOnlyUrl GetChooseUrl()
        {
            return _chooseUrl;
        }

        protected ReadOnlyUrl GetChooseUrl(Guid instanceId)
        {
            return GetUrl(_chooseUrl, instanceId);
        }

        protected ReadOnlyUrl GetAccountUrl(Guid? instanceId)
        {
            return GetUrl(_accountUrl, instanceId);
        }

        protected ReadOnlyUrl GetPaymentUrl(Guid? instanceId)
        {
            return GetUrl(_paymentUrl, instanceId);
        }

        protected ReadOnlyUrl GetReceiptUrl(Guid? instanceId)
        {
            return GetUrl(_receiptUrl, instanceId);
        }

        private static ReadOnlyUrl GetUrl(ReadOnlyUrl baseUrl, Guid? instanceId)
        {
            if (instanceId == null)
                return baseUrl;

            var url = baseUrl.AsNonReadOnly();
            url.QueryString["instanceId"] = instanceId.ToString();
            return url;
        }

        protected Guid GetInstanceId()
        {
            var url = new ReadOnlyUrl(Browser.CurrentUrl);
            return new Guid(url.QueryString["instanceId"]);
        }

        protected Employer CreateEmployer(int index, bool verified)
        {
            var organisation = verified
                ? _organisationsCommand.CreateTestVerifiedOrganisation(index, null, Guid.NewGuid())
                : _organisationsCommand.CreateTestOrganisation(index);
            return _employerAccountsCommand.CreateTestEmployer(index, organisation);
        }

        protected Employer CreateEmployer(string loginId, bool verified)
        {
            var organisation = verified
                ? _organisationsCommand.CreateTestVerifiedOrganisation(0, null, Guid.NewGuid())
                : _organisationsCommand.CreateTestOrganisation(0);
            return _employerAccountsCommand.CreateTestEmployer(loginId, organisation);
        }

        protected Employer LogIn(bool verified)
        {
            var employer = CreateEmployer(1, verified);
            LogIn(employer);
            return employer;
        }

        protected ReadOnlyApplicationUrl GetOrderUrl(Guid orderId)
        {
            var url = _orderUrl.AsNonReadOnly();
            url.Path = url.Path + "/";
            return new ReadOnlyApplicationUrl(url, orderId.ToString());
        }

        protected void Select(HtmlDropDownListTester tester, Product product)
        {
            tester.SelectedValue = product.Id.ToString();
        }

        protected void SelectContactProduct(Product product)
        {
            Select(_contactProductIdDropDownList, product);
        }

        protected Guid? GetSelectedProductId(HtmlDropDownListTester tester)
        {
            try
            {
                return new Guid(tester.SelectedItem.Value);
            }
            catch (Exception)
            {
                return null;
            }
        }

        protected Guid? GetSelectedContactProductId()
        {
            return GetSelectedProductId(_contactProductIdDropDownList);
        }
    }
}
