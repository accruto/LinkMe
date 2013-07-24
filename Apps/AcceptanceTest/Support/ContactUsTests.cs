using System.Linq;
using LinkMe.Apps.Agents.Test.Users.Employers;
using LinkMe.Apps.Agents.Test.Users.Members;
using LinkMe.Apps.Agents.Users.Employers.Commands;
using LinkMe.Apps.Agents.Users.Members.Commands;
using LinkMe.Apps.Asp.Test.Html;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Resources.Queries;
using LinkMe.Domain.Roles.Recruiters.Commands;
using LinkMe.Domain.Roles.Test.Recruiters;
using LinkMe.Framework.Utility.Urls;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Support
{
    [TestClass]
    public class ContactUsTests
        : SupportTests
    {
        private readonly IMemberAccountsCommand _memberAccountsCommand = Resolve<IMemberAccountsCommand>();
        private readonly IEmployerAccountsCommand _employerAccountsCommand = Resolve<IEmployerAccountsCommand>();
        private readonly IOrganisationsCommand _organisationsCommand = Resolve<IOrganisationsCommand>();
        private readonly IFaqsQuery _faqsQuery = Resolve<IFaqsQuery>();

        private const string DefaultSubcategoryText = "Report a site issue";

        private ReadOnlyUrl _contactUsUrl;
        private ReadOnlyUrl _siteIssueUrl;
        private ReadOnlyUrl _contactUsPartialUrl;
        private ReadOnlyUrl _faqUrl;

        private HtmlTextBoxTester _nameTextBox;
        private HtmlTextBoxTester _fromTextBox;
        private HtmlTextBoxTester _phoneTextBox;
        private HtmlDropDownListTester _userTypeDropDownList;
        private HtmlDropDownListTester _subcategoryIdDropDownList;
        private HtmlTextAreaTester _messageTextBox;

        [TestInitialize]
        public void TestInitialize()
        {
            _contactUsUrl = new ReadOnlyApplicationUrl("~/contactus");
            _contactUsPartialUrl = new ReadOnlyApplicationUrl("~/contactus/partial");
            _faqUrl = new ReadOnlyApplicationUrl("~/faqs/setting-up-your-profile/09d11385-0213-4157-a5a9-1b2a74e6887e");

            _siteIssueUrl = new ReadOnlyApplicationUrl("~/contactus/partial?enquiryType=Report+a+site+issue");

            _nameTextBox = new HtmlTextBoxTester(Browser, "Name");
            _fromTextBox = new HtmlTextBoxTester(Browser, "From");
            _phoneTextBox = new HtmlTextBoxTester(Browser, "PhoneNumber");
            _userTypeDropDownList = new HtmlDropDownListTester(Browser, "UserType");
            _subcategoryIdDropDownList = new HtmlDropDownListTester(Browser, "MemberSubcategoryId");
            _messageTextBox = new HtmlTextAreaTester(Browser, "Message");
        }
        
        [TestMethod]
        public void TestContactUsRedirect()
        {
            Get(_contactUsUrl);
            AssertUrl(_faqUrl);
        }

        [TestMethod]
        public void TestContactUsMemberLoggedIn()
        {
            var member = _memberAccountsCommand.CreateTestMember(0);
            LogIn(member);

            Get(_contactUsPartialUrl);
            AssertPath(_contactUsPartialUrl);

            AssertContactUs(member.FullName, member.EmailAddresses[0].Address, member.PhoneNumbers[0].Number, UserType.Member, DefaultSubcategoryText, string.Empty);
        }

        [TestMethod]
        public void TestContactUsEmployerLoggedIn()
        {
            var employer = _employerAccountsCommand.CreateTestEmployer(0, _organisationsCommand.CreateTestOrganisation(0));
            LogIn(employer);

            Get(_contactUsPartialUrl);
            AssertPath(_contactUsPartialUrl);

            AssertContactUs(employer.FullName, employer.EmailAddress.Address, employer.PhoneNumber.Number, UserType.Employer, DefaultSubcategoryText, string.Empty);
        }

        [TestMethod]
        public void TestContactUsFromSubCategory()
        {
            var subcategory = _faqsQuery.GetCategory(UserType.Member).Subcategories.Single(s => s.Name == "My LinkMe profile");

            var url = _contactUsPartialUrl.AsNonReadOnly();
            url.QueryString.Add("subcategoryId", subcategory.Id.ToString());
            Get(url);
            AssertPath(url);

            AssertContactUs(string.Empty, string.Empty, string.Empty, UserType.Member, "Report a site issue", string.Empty);
        }

        [TestMethod]
        public void TestContactUsMemberLoggedInFromSubcategory()
        {
            var member = _memberAccountsCommand.CreateTestMember(0);
            LogIn(member);

            var subcategory = _faqsQuery.GetCategory(UserType.Member).Subcategories.Single(s => s.Name == "Applying for jobs");

            var url = _contactUsPartialUrl.AsNonReadOnly();
            url.QueryString.Add("subcategoryId", subcategory.Id.ToString());
            Get(url);
            AssertPath(url);

            AssertContactUs(member.FullName, member.EmailAddresses[0].Address, member.PhoneNumbers[0].Number, UserType.Member, "Report a site issue", string.Empty);
        }

        [TestMethod]
        public void TestContactUsReportSiteIssue()
        {
            Get(_siteIssueUrl);
            AssertPath(_siteIssueUrl);

            AssertContactUs(string.Empty, string.Empty, string.Empty, UserType.Member, "Report a site issue", string.Empty);
        }

        protected override string PageTitleCssClass
        {
            get { return "section-title"; }
        }

        private void AssertContactUs(string name, string emailAddress, string phone, UserType userType, string subcategoryText, string message)
        {
            Assert.AreEqual(name, _nameTextBox.Text);
            Assert.AreEqual(emailAddress, _fromTextBox.Text);
            Assert.AreEqual(phone, _phoneTextBox.Text);
            Assert.AreEqual(userType == UserType.Member ? "a candidate" : "an employer", _userTypeDropDownList.SelectedItem.Text);
            Assert.AreEqual(subcategoryText, _subcategoryIdDropDownList.SelectedItem.Text);
            Assert.AreEqual(message, _messageTextBox.Text.Trim());
        }
    }
}
