using LinkMe.Apps.Agents.Test.Users.Employers;
using LinkMe.Apps.Asp.Test.Html;
using LinkMe.Framework.Utility.Urls;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Employers.Candidates.Folders
{
    [TestClass]
    public class AccessTests
        : FoldersTests
    {
        private ReadOnlyUrl _loginUrl;
        private HtmlTextBoxTester _loginIdTextBox;
        private HtmlPasswordTester _passwordTextBox;
        private HtmlButtonTester _loginButton;

        [TestInitialize]
        public void TestInitialize()
        {
            _loginUrl = new ReadOnlyApplicationUrl(true, "~/employers/login");
            _loginIdTextBox = new HtmlTextBoxTester(Browser, "LoginId");
            _passwordTextBox = new HtmlPasswordTester(Browser, "Password");
            _loginButton = new HtmlButtonTester(Browser, "login");
        }

        [TestMethod]
        public void TestAnonymousAccess()
        {
            // Create employer.

            var employer = CreateEmployer();

            // Access anonymously.

            var foldersUrl = GetFoldersUrl();
            Get(foldersUrl);

            // Should be redirected.

            var loginUrl = _loginUrl.AsNonReadOnly();
            loginUrl.QueryString["returnUrl"] = foldersUrl.PathAndQuery;
            AssertUrl(loginUrl);

            // Log in.

            _loginIdTextBox.Text = employer.GetLoginId();
            _passwordTextBox.Text = employer.GetPassword();
            _loginButton.Click();

            AssertUrl(foldersUrl);
        }
    }
}
