using LinkMe.Framework.Utility.Urls;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Support
{
    [TestClass]
    public class TermsDetailTests
        : SupportTests
    {
        private const string PageTitle = "General terms and conditions";
        private const string MemberPageTitle = "Member terms and conditions";
        private const string EmployerPageTitle = "Employer terms and conditions";

        [TestMethod]
        public void TestTermsDetail()
        {
            var url = new ReadOnlyApplicationUrl("~/termsdetail");
            AssertUrl(url, url, PageTitle);
            AssertUrl(new ReadOnlyApplicationUrl("~/TermsAndConditionsDetail.aspx"), url, PageTitle);
        }

        [TestMethod]
        public void TestMemberTermsDetail()
        {
            var url = new ReadOnlyApplicationUrl("~/members/termsdetail");
            AssertUrl(url, url, MemberPageTitle);
        }

        [TestMethod]
        public void TestEmployerTermsDetail()
        {
            var url = new ReadOnlyApplicationUrl("~/employers/termsdetail");
            AssertUrl(url, url, EmployerPageTitle);
        }

        protected override string PageTitleCssClass
        {
            get { return "page-title terms-detail"; }
        }
    }
}
