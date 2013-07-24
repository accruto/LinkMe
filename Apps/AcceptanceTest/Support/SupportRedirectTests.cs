using LinkMe.AcceptanceTest.Navigation;
using LinkMe.Framework.Utility.Urls;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Support
{
    [TestClass]
    public class SupportRedirectTests
        : RedirectTests
    {
        [TestMethod]
        public void TestFaqs()
        {
            var redirectUrl = new ReadOnlyApplicationUrl("~/faqs");

            // Some bots have urls without ids.  Simply redirect back to to FAQs page.

            AssertRedirect(new ReadOnlyApplicationUrl("~/faq/About-LinkMe/What-is-LinkMe"), redirectUrl, redirectUrl);
            AssertRedirect(new ReadOnlyApplicationUrl("~/faqs/saved-searches-and-email-alerts/how-do-i-stop-saved-search-alert-emails-from-coming-to-me"), redirectUrl, redirectUrl);
        }

        [TestMethod]
        public void TestContactUs()
        {
            var redirectUrl = new ReadOnlyApplicationUrl("~/contactus");
            var faqUrl = new ReadOnlyApplicationUrl("~/faqs/setting-up-your-profile/09d11385-0213-4157-a5a9-1b2a74e6887e");
            AssertRedirect(new ReadOnlyApplicationUrl("~/ui/unregistered/common/ContactUsForm.aspx"), redirectUrl, faqUrl);
        }
    }
}
