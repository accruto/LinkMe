using LinkMe.Apps.Agents.Communications.Alerts;
using LinkMe.Query.Search.Members;
using LinkMe.Query.Search.Members.Commands;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Apps.Api.Test.Employers.Searches
{
    [TestClass]
    public class GetSearchesTests
        : SearchesTests
    {
        private readonly IMemberSearchesCommand _memberSearchesCommand = Resolve<IMemberSearchesCommand>();

        [TestMethod]
        public void TestNoSearches()
        {
            var employer = CreateEmployer(0);
            LogIn(employer);
            AssertSearches(Searches());
        }

        [TestMethod]
        public void TestKeywords()
        {
            var employer = CreateEmployer(0);

            var criteria = new MemberSearchCriteria();
            criteria.SetKeywords(Keywords);
            var search = new MemberSearch { Name = string.Format(NameFormat, 0), Criteria = criteria };
            _memberSearchesCommand.CreateMemberSearch(employer, search);

            LogIn(employer);
            AssertSearches(Searches(), MakeTupleTuple(search, AlertType.AppleDevice, false));
        }
    }
}