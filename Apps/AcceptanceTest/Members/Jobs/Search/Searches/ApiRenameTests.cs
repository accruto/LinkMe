using System;
using System.Collections.Specialized;
using System.Linq;
using LinkMe.Apps.Agents.Test.Users.Members;
using LinkMe.Apps.Agents.Users.Members.Commands;
using LinkMe.Apps.Asp.Json.Models;
using LinkMe.Domain.Contacts;
using LinkMe.Framework.Utility.Urls;
using LinkMe.Query.Search.JobAds;
using LinkMe.Query.Search.JobAds.Commands;
using LinkMe.Query.Search.JobAds.Queries;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Members.Jobs.Search.Searches
{
    [TestClass]
    public class ApiRenameTests
        : SearchTests
    {
        private readonly IMemberAccountsCommand _memberAccountsCommand = Resolve<IMemberAccountsCommand>();
        private readonly IJobAdSearchesCommand _jobAdSearchesCommand = Resolve<IJobAdSearchesCommand>();
        private readonly IJobAdSearchesQuery _jobAdSearchesQuery = Resolve<IJobAdSearchesQuery>();

        private const string Keywords = "Archeologist";
        private const string SearchName = "My search";
        private const string OtherName = "My other search";
        private const string SearchNewName = "My renamed search";

        private ReadOnlyUrl _apiRenameSearchUrl;

        [TestInitialize]
        public void TestInitialize()
        {
            _apiRenameSearchUrl = new ReadOnlyApplicationUrl(true, "~/members/searches/api/rename");
        }

        [TestMethod]
        public void TestRenameNoName()
        {
            var member = CreateMember();
            LogIn(member);

            // Save a search.

            var criteria = new JobAdSearchCriteria();
            criteria.SetKeywords(Keywords);
            var search = new JobAdSearch { Criteria = criteria };
            _jobAdSearchesCommand.CreateJobAdSearch(member.Id, search);

            // Rename.

            AssertJsonSuccess(RenameSearch(search.Id, SearchNewName));

            // Check.

            var searches = _jobAdSearchesQuery.GetJobAdSearches(member.Id);
            Assert.AreEqual(1, searches.Count);
            Assert.AreEqual(SearchNewName, searches[0].Name);
            Assert.AreEqual(Keywords, searches[0].Criteria.KeywordsExpression.GetUserExpression());
        }

        [TestMethod]
        public void TestRenameName()
        {
            var member = CreateMember();
            LogIn(member);

            // Save a search.

            var criteria = new JobAdSearchCriteria();
            criteria.SetKeywords(Keywords);
            var search = new JobAdSearch { Criteria = criteria, Name = SearchName };
            _jobAdSearchesCommand.CreateJobAdSearch(member.Id, search);

            // Rename.

            AssertJsonSuccess(RenameSearch(search.Id, SearchNewName));

            // Check.

            var searches = _jobAdSearchesQuery.GetJobAdSearches(member.Id);
            Assert.AreEqual(1, searches.Count);
            Assert.AreEqual(SearchNewName, searches[0].Name);
            Assert.AreEqual(Keywords, searches[0].Criteria.KeywordsExpression.GetUserExpression());
        }

        [TestMethod]
        public void TestRenameToOtherName()
        {
            var member = CreateMember();
            LogIn(member);

            // Save a search.

            var criteria = new JobAdSearchCriteria();
            criteria.SetKeywords(Keywords);
            var search1 = new JobAdSearch { Criteria = criteria, Name = OtherName };
            _jobAdSearchesCommand.CreateJobAdSearch(member.Id, search1);

            criteria = new JobAdSearchCriteria();
            criteria.SetKeywords(Keywords);
            var search2 = new JobAdSearch { Criteria = criteria, Name = SearchName };
            _jobAdSearchesCommand.CreateJobAdSearch(member.Id, search2);

            // Rename.

            AssertJsonError(RenameSearch(search2.Id, OtherName), "Name", "300", "The name is already being used.");

            // Check.

            var searches = _jobAdSearchesQuery.GetJobAdSearches(member.Id);
            Assert.AreEqual(2, searches.Count);
            Assert.AreEqual(OtherName, (from s in searches where s.Id == search1.Id select s.Name).Single());
            Assert.AreEqual(SearchName, (from s in searches where s.Id == search2.Id select s.Name).Single());
        }

        [TestMethod]
        public void TestRenameToNoName()
        {
            var member = CreateMember();
            LogIn(member);

            // Save a search.

            var criteria = new JobAdSearchCriteria();
            criteria.SetKeywords(Keywords);
            var search = new JobAdSearch { Criteria = criteria, Name = SearchName };
            _jobAdSearchesCommand.CreateJobAdSearch(member.Id, search);

            // Rename, can actually remove the name.

            AssertJsonSuccess(RenameSearch(search.Id, null));

            // Check.

            var searches = _jobAdSearchesQuery.GetJobAdSearches(member.Id);
            Assert.AreEqual(1, searches.Count);
            Assert.IsNull(searches[0].Name);
        }

        private JsonResponseModel RenameSearch(Guid searchId, string newName)
        {
            return Deserialize<JsonResponseModel>(Post(_apiRenameSearchUrl, GetParameters(searchId, newName)));
        }

        private static NameValueCollection GetParameters(Guid searchId, string newName)
        {
            return new NameValueCollection
            {
                {"searchId", searchId.ToString()},
                {"newName", newName},
            };
        }

        private Member CreateMember()
        {
            return _memberAccountsCommand.CreateTestMember(0);
        }
    }
}
