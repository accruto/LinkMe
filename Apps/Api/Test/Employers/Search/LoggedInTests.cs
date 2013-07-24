using LinkMe.Query.Search.Members;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Apps.Api.Test.Employers.Search
{
    [TestClass]
    public class LoggedInTests
        : SearchTests
    {
        [TestMethod]
        public void TestLoggedIn()
        {
            var employer = CreateEmployer();
            LogIn(employer);

            var member0 = CreateMember(0);
            var member1 = CreateMember(1);

            var criteria = new MemberSearchCriteria();
            criteria.SetKeywords(BusinessAnalyst);
            var model = Search(criteria);
            AssertMembers(model, member0, member1);
        }
    }
}
