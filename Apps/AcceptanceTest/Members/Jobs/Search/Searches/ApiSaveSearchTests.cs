using System;
using System.Collections.Specialized;
using System.Linq;
using LinkMe.Apps.Agents.Communications.Alerts.Queries;
using LinkMe.Apps.Agents.Test.Users.Members;
using LinkMe.Apps.Agents.Users.Members.Commands;
using LinkMe.Apps.Asp.Json.Models;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Users.Anonymous.Commands;
using LinkMe.Framework.Utility.Urls;
using LinkMe.Query.Search.JobAds.Queries;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Members.Jobs.Search.Searches
{
    [TestClass]
    public class ApiSaveSearchTests
        : SearchTests
    {
        private readonly IMemberAccountsCommand _memberAccountsCommand = Resolve<IMemberAccountsCommand>();
        private readonly IAnonymousUsersCommand _anonymousUsersCommand = Resolve<IAnonymousUsersCommand>();
        private readonly IJobAdSearchesQuery _jobAdSearchesQuery = Resolve<IJobAdSearchesQuery>();
        private readonly IJobAdSearchAlertsQuery _jobAdSearchAlertsQuery = Resolve<IJobAdSearchAlertsQuery>();

        private const string Keywords = "Archeologist";
        private const string EmailAddress = "hsimpson@test.linkme.net.au";
        private const string FirstName = "Homer";
        private const string LastName = "Simpson";
        private const string EmailAddress2 = "bgumble@test.linkme.net.au";
        private const string FirstName2 = "Barney";
        private const string LastName2 = "Gumble";
        private ReadOnlyUrl _apiSaveSearchUrl;

        [TestInitialize]
        public void TestInitialize()
        {
            _apiSaveSearchUrl = new ReadOnlyApplicationUrl("~/members/searches/api/save");
        }

        [TestMethod]
        public void TestMemberSearch()
        {
            var member = CreateMember();
            Assert.AreEqual(0, _jobAdSearchesQuery.GetJobAdSearches(member.Id).Count);

            LogIn(member);

            // Do a search.

            Search(Keywords);

            // Save it.

            AssertJsonSuccess(SaveSearch());
            AssertSearch(Keywords, member.Id);
        }

        [TestMethod]
        public void TestAnonymousSearch()
        {
            Get(HomeUrl);
            var userId = GetAnonymousId();

            // Do a search.

            Search(Keywords);

            // Save it.

            AssertJsonSuccess(SaveSearch(EmailAddress, FirstName, LastName));
            AssertSearch(Keywords, _anonymousUsersCommand.CreateContact(new AnonymousUser { Id = userId }, new ContactDetails { EmailAddress = EmailAddress, FirstName = FirstName, LastName = LastName }).Id);
        }

        [TestMethod]
        public void TestMultipleAnonymousSearches()
        {
            Get(HomeUrl);
            var userId = GetAnonymousId();

            const string keywords1 = Keywords;
            const string keywords2 = Keywords + Keywords;

            // Do a search.

            Search(keywords1);
            AssertJsonSuccess(SaveSearch(EmailAddress, FirstName, LastName));

            // Do another search.

            Search(keywords2);
            AssertJsonSuccess(SaveSearch(EmailAddress, FirstName, LastName));

            var ownerId = _anonymousUsersCommand.CreateContact(new AnonymousUser { Id = userId }, new ContactDetails { EmailAddress = EmailAddress, FirstName = FirstName, LastName = LastName }).Id;
            var searches = _jobAdSearchesQuery.GetJobAdSearches(ownerId);
            Assert.AreEqual(2, searches.Count);
            Assert.IsTrue((from s in searches where s.Criteria.KeywordsExpression.GetUserExpression() == keywords1 select s).Any());
            Assert.IsTrue((from s in searches where s.Criteria.KeywordsExpression.GetUserExpression() == keywords2 select s).Any());
            Assert.IsNotNull(_jobAdSearchAlertsQuery.GetJobAdSearchAlert(searches[0].Id));
            Assert.IsNotNull(_jobAdSearchAlertsQuery.GetJobAdSearchAlert(searches[1].Id));
        }

        [TestMethod]
        public void TestMultipleContactDetails()
        {
            Get(HomeUrl);
            var userId = GetAnonymousId();

            // Do a search.

            Search(Keywords);
            AssertJsonSuccess(SaveSearch(EmailAddress, FirstName, LastName));

            // Do another search.

            Search(Keywords);
            AssertJsonSuccess(SaveSearch(EmailAddress2, FirstName2, LastName2));

            var ownerId1 = _anonymousUsersCommand.CreateContact(new AnonymousUser { Id = userId }, new ContactDetails { EmailAddress = EmailAddress, FirstName = FirstName, LastName = LastName }).Id;
            AssertSearch(Keywords, ownerId1);

            var ownerId2 = _anonymousUsersCommand.CreateContact(new AnonymousUser { Id = userId }, new ContactDetails { EmailAddress = EmailAddress2, FirstName = FirstName2, LastName = LastName2 }).Id;
            AssertSearch(Keywords, ownerId2);
        }

        [TestMethod]
        public void TestAnonymousSearchErrors()
        {
            Get(HomeUrl);
            var userId = GetAnonymousId();

            // Do a search.

            Search(Keywords);

            // Save it.

            AssertJsonError(SaveSearch(null, FirstName, LastName), "EmailAddress", "300", "The email address is required.");
            AssertJsonError(SaveSearch(EmailAddress, null, LastName), "FirstName", "300", "The first name is required.");
            AssertJsonError(SaveSearch(EmailAddress, FirstName, null), "LastName", "300", "The last name is required.");

            AssertNoSearch(userId);
        }

        private void AssertSearch(string keywords, Guid ownerId)
        {
            var searches = _jobAdSearchesQuery.GetJobAdSearches(ownerId);
            Assert.AreEqual(1, searches.Count);
            Assert.AreEqual(keywords, searches[0].Criteria.KeywordsExpression.GetUserExpression());
            Assert.IsNotNull(_jobAdSearchAlertsQuery.GetJobAdSearchAlert(searches[0].Id));
        }

        private void AssertNoSearch(Guid ownerId)
        {
            var searches = _jobAdSearchesQuery.GetJobAdSearches(ownerId);
            Assert.AreEqual(0, searches.Count);
        }

        private JsonResponseModel SaveSearch()
        {
            return Deserialize<JsonResponseModel>(Post(_apiSaveSearchUrl));
        }

        private JsonResponseModel SaveSearch(string emailAddress, string firstName, string lastName)
        {
            return Deserialize<JsonResponseModel>(Post(_apiSaveSearchUrl, GetParameters(emailAddress, firstName, lastName)));
        }

        private static NameValueCollection GetParameters(string emailAddress, string firstName, string lastName)
        {
            return new NameValueCollection
            {
                {"EmailAddress", emailAddress},
                {"FirstName", firstName},
                {"LastName", lastName},
            };
        }

        private Member CreateMember()
        {
            return _memberAccountsCommand.CreateTestMember(0);
        }
    }
}
