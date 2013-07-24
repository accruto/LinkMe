using HtmlAgilityPack;
using LinkMe.Apps.Asp.Test.Html;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Members.Jobs.Applications
{
    [TestClass]
    public abstract class WebApplyTests
        : ApplyTests
    {
        protected HtmlTextBoxTester _coverLetterTextBox;
        protected HtmlRadioButtonTester _profileRadioButton;
        protected HtmlRadioButtonTester _uploadedRadioButton;
        protected HtmlRadioButtonTester _lastUsedRadioButton;

        [TestInitialize]
        public void WebApplyTestsInitialize()
        {
            _coverLetterTextBox = new HtmlTextBoxTester(Browser, "CoverLetter");
            _profileRadioButton = new HtmlRadioButtonTester(Browser, "Profile");
            _uploadedRadioButton = new HtmlRadioButtonTester(Browser, "Uploaded");
            _lastUsedRadioButton = new HtmlRadioButtonTester(Browser, "LastUsed");
        }

        protected HtmlNode GetAppliedForExternallyNode()
        {
            return Browser.CurrentHtml.DocumentNode.SelectSingleNode("//div[@class='careeroneapply']");
        }

        protected HtmlNode GetManagedInternallyNode()
        {
            return Browser.CurrentHtml.DocumentNode.SelectSingleNode("//div[@class='managedinternallyapply']");
        }

        protected HtmlNode GetManagedExternallyNode()
        {
            return Browser.CurrentHtml.DocumentNode.SelectSingleNode("//div[@class='managedexternallyapply']");
        }

        protected HtmlNode GetLoggedInUserNode()
        {
            return Browser.CurrentHtml.DocumentNode.SelectSingleNode("//div[@class='loggedinuserapply']");
        }
    }
}
