using LinkMe.Domain.Contacts;
using LinkMe.Framework.Utility.Urls;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Employers.Search.BackToSearch
{
    [TestClass]
    public class FlagListBackToSearchTests
        : BackToSearchTests
    {
        private ReadOnlyUrl _flagListUrl;

        [TestInitialize]
        public void TestInitialize()
        {
            _flagListUrl = new ReadOnlyApplicationUrl(true, "~/employers/candidates/flaglist");
        }

        protected override ReadOnlyUrl GetBackToSearchUrl(IEmployer employer)
        {
            return _flagListUrl;
        }
    }
}