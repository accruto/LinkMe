using LinkMe.Apps.Agents.Test.Users.Employers;
using LinkMe.Apps.Agents.Users.Employers.Commands;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Recruiters.Commands;
using LinkMe.Domain.Roles.Test.Recruiters;
using LinkMe.Domain.Test.Data;
using LinkMe.Framework.Utility.Sql;
using LinkMe.Framework.Utility.Urls;
using LinkMe.Query.Search.Members;
using LinkMe.Query.Search.Members.Commands;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Employers.Searches
{
    [TestClass]
    public class SearchesTests
        : WebTestClass
    {
        private readonly IEmployerAccountsCommand _employerAccountsCommand = Resolve<IEmployerAccountsCommand>();
        private readonly IOrganisationsCommand _organisationsCommand = Resolve<IOrganisationsCommand>();
        private readonly IMemberSearchesCommand _memberSearchesCommand = Resolve<IMemberSearchesCommand>();

        private const string NameFormat = "My search{0}";
        private const string JobTitleFormat = "Architect{0}";

        private ReadOnlyUrl _searchesUrl;

        [TestInitialize]
        public void TestInitialize()
        {
            Resolve<IDbConnectionFactory>().DeleteAllTestData();
            _searchesUrl = new ReadOnlyApplicationUrl("~/employers/searches");
        }

        [TestMethod]
        public void TestNamedSearch()
        {
            var employer = _employerAccountsCommand.CreateTestEmployer(0, _organisationsCommand.CreateTestVerifiedOrganisation(0));

            // Create a named search.

            var search = CreateMemberSearch(employer, 0);

            // Access the page.

            LogIn(employer);
            Get(_searchesUrl);

            AssertPageContains(search.Name);
        }

        [TestMethod]
        public void TestMultipleNamedSearches()
        {
            var employer = _employerAccountsCommand.CreateTestEmployer(0, _organisationsCommand.CreateTestVerifiedOrganisation(0));

            // Create named searches.

            const int count = 20;
            var searches = new MemberSearch[count];
            for (var index = 0; index < 20; ++index)
                searches[index] = CreateMemberSearch(employer, index);

            // Access the page.

            LogIn(employer);
            Get(_searchesUrl);

            foreach (var search in searches)
                AssertPageContains(search.Name);
        }

        private MemberSearch CreateMemberSearch(IUser employer, int index)
        {
            var search = new MemberSearch
            {
                Name = string.Format(NameFormat, index),
                Criteria = new MemberSearchCriteria
                {
                    JobTitle = string.Format(JobTitleFormat, index),
                },
            };
            _memberSearchesCommand.CreateMemberSearch(employer, search);
            return search;
        }
    }
}
