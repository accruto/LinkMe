using LinkMe.Framework.Utility.Urls;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Support
{
    [TestClass]
    public class TermsTests
        : SupportTests
    {
        private const string PageTitle = "General terms and conditions";
        private const string MemberPageTitle = "Member terms and conditions";
        private const string EmployerPageTitle = "Employer terms and conditions";

        [TestMethod]
        public void TestTerms()
        {
            var url = new ReadOnlyApplicationUrl("~/terms");
            AssertUrl(url, url, PageTitle);
            AssertUrl(new ReadOnlyApplicationUrl("~/TermsAndConditions.aspx"), url, PageTitle);
            AssertUrl(new ReadOnlyApplicationUrl("~/ui/unregistered/common/TermsAndConditions.aspx"), url, PageTitle);
            AssertUrl(new ReadOnlyApplicationUrl("~/ui/unregistered/common/termsandconditionsform.aspx"), url, PageTitle);
        }

        [TestMethod]
        public void TestMemberTerms()
        {
            var url = new ReadOnlyApplicationUrl("~/members/terms");
            AssertUrl(url, url, MemberPageTitle);
        }

        [TestMethod]
        public void TestEmployerTerms()
        {
            var url = new ReadOnlyApplicationUrl("~/employers/terms");
            AssertUrl(url, url, EmployerPageTitle);
        }
    }
}
