using LinkMe.Domain.Test.Data;
using LinkMe.Domain.Users.Employers.Candidates.Queries;
using LinkMe.Framework.Utility.Sql;
using LinkMe.Framework.Utility.Urls;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Employers.Candidates.FlagLists
{
    [TestClass]
    public abstract class FlagListsTests
        : CandidateListsTests
    {
        protected readonly ICandidateFlagListsQuery _candidateFlagListsQuery = Resolve<ICandidateFlagListsQuery>();

        private ReadOnlyUrl _flagListUrl;

        [TestInitialize]
        public void TestInitialize()
        {
            Resolve<IDbConnectionFactory>().DeleteAllTestData();
            _flagListUrl = new ReadOnlyApplicationUrl(true, "~/employers/candidates/flaglist");
        }

        protected ReadOnlyUrl GetFlagListUrl()
        {
            return GetFlagListUrl(null, null);
        }

        protected ReadOnlyUrl GetFlagListUrl(int? page, int? items)
        {
            if (page == null && items == null)
                return _flagListUrl;

            var queryString = new QueryString();
            if (page != null)
                queryString.Add("page", page.Value.ToString());
            if (items != null)
                queryString.Add("items", items.Value.ToString());

            var url = _flagListUrl.AsNonReadOnly();
            url.QueryString.Add(queryString);
            return url;
        }
    }
}