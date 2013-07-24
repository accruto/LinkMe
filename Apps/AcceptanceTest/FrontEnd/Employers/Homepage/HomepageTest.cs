using System;
using LinkMe.AcceptanceTest.FrontEnd.Extension;
using LinkMe.Apps.Agents.Test.Users.Employers;
using LinkMe.Apps.Agents.Users.Employers.Commands;
using LinkMe.Domain.Credits;
using LinkMe.Domain.Credits.Commands;
using LinkMe.Domain.Credits.Queries;
using LinkMe.Domain.Roles.Recruiters.Commands;
using LinkMe.Domain.Roles.Test.Recruiters;
using LinkMe.Domain.Test.Data;
using LinkMe.Framework.Utility.Sql;
using LinkMe.Framework.Utility.Unity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WatiN.Core;

namespace LinkMe.AcceptanceTest.FrontEnd.Employers.Homepage
{
    [TestClass]
    [Ignore]
    public class HomeTest : WebTestClass
    {
        private readonly IEmployerAccountsCommand _employerAccountsCommand = Resolve<IEmployerAccountsCommand>();
        private readonly IOrganisationsCommand _organisationsCommand = Resolve<IOrganisationsCommand>();
        private readonly ICreditsQuery _creditsQuery = Resolve<ICreditsQuery>();
        private readonly IAllocationsCommand _allocationsCommand = Resolve<IAllocationsCommand>();

        [TestInitialize]
        public void TestInitialize()
        {
            Resolve<IDbConnectionFactory>().DeleteAllTestData();
            CreateUnlimitEmployer();
        }

        [TestMethod]
        [STAThread]
        public void TestLogin()
        {
            using(var browser = new IE())
            {
                browser.Setup();
                browser.GoToEmployerHomePage();
                //test empty username and password
                browser.Div(Find.ByClass("loginbutton", false)).Click();
                Assert.AreEqual(browser.Div(Find.ByClass("login-error", false)).Style.Display, "block");
                Assert.AreEqual(browser.Eval("$(\".login-error ul li:contains('The username is required.')\").length"), "1");
                Assert.AreEqual(browser.Eval("$(\".login-error ul li:contains('The password is required.')\").length"), "1");
                //test wrong username and password
                browser.TextField(Find.ById("LoginId")).TypeText("abc");
                browser.TextField(Find.ById("Password")).TypeText("def");
                browser.Div(Find.ByClass("loginbutton", false)).Click();
                Assert.AreEqual(browser.Div(Find.ByClass("login-error", false)).Style.Display, "block");
                Assert.AreEqual(browser.Eval("$(\".login-error ul li:contains('Login failed. Please try again.')\").length"), "1");
                //test correct username and password
                browser.TextField(Find.ById("LoginId")).TypeText("employer0");
                browser.TextField(Find.ById("Password")).TypeText("password");
                browser.Div(Find.ByClass("loginbutton", false)).Click();
                Assert.AreEqual(browser.Url, "https://localhost/Trunk/search/candidates");
            }
        }

        [TestMethod]
        [STAThread]
        public void TestKeywordLocationSearch()
        {
            const string keyword = "linkme sales";

            using (var browser = new IE())
            {
                browser.Setup();
                browser.GoToEmployerHomePage();
                //search
                browser.TextField(Find.ById("Keywords")).TypeText(keyword);
                browser.TextField(Find.ById("Location")).TypeText("2089 Neutral Bay NSW");
                browser.Button(Find.ById("search")).Click();
                browser.WaitForComplete();
                //assert keywords
                Assert.AreEqual(browser.Eval("$(\"#search-header-text .keywords_search-criterion .search-criterion-data\").text()"), keyword);
                //assert location
                Assert.AreEqual(browser.Eval("$(\"#search-header-text .location_search-criterion .search-criterion-data\").text()"), "Neutral Bay NSW 2089");
                //assert distance filter is checked
                Assert.AreEqual(browser.Eval("$(\".distance_section .filter_pushcheck\").hasClass(\"pushcheck-checked\")"), "true");
                //assert distance filter is open
                Assert.AreEqual(browser.Eval("$(\".distance_section .section-content:visible\").length"), "1");
            }
        }

        private void CreateUnlimitEmployer()
        {
            var employer = _employerAccountsCommand.CreateTestEmployer(0, _organisationsCommand.CreateTestOrganisation(0));
            var credit = _creditsQuery.GetCredit<ContactCredit>();
            _allocationsCommand.CreateAllocation(new Allocation { CreditId = credit.Id, InitialQuantity = null, OwnerId = employer.Id });
            var jobAdCredit = _creditsQuery.GetCredit<JobAdCredit>();
            _allocationsCommand.CreateAllocation(new Allocation { CreditId = jobAdCredit.Id, InitialQuantity = null, OwnerId = employer.Id });
            var applicantCredit = _creditsQuery.GetCredit<ApplicantCredit>();
            _allocationsCommand.CreateAllocation(new Allocation { CreditId = applicantCredit.Id, InitialQuantity = null, OwnerId = employer.Id });

            return;
        }
    }
}