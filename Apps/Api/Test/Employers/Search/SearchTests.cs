using LinkMe.Apps.Api.Areas.Employers.Models.Candidates;
using LinkMe.Apps.Presentation.Converters;
using LinkMe.Domain.Industries.Queries;
using LinkMe.Domain.Location.Queries;
using LinkMe.Framework.Utility.Urls;
using LinkMe.Query.Members;
using LinkMe.Query.Search.Members;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Apps.Api.Test.Employers.Search
{
    [TestClass]
    public abstract class SearchTests
        : CandidateListsTests
    {
        private readonly ILocationQuery _locationQuery = Resolve<ILocationQuery>();
        private readonly IIndustriesQuery _industriesQuery = Resolve<IIndustriesQuery>();

        private ReadOnlyUrl _searchUrl;
        protected const string BusinessAnalyst = "business analyst";

        [TestInitialize]
        public void SearchTestsInitialize()
        {
            ClearSearchIndexes();
            _searchUrl = new ReadOnlyApplicationUrl("~/v1/search/candidates");
        }

        private CandidatesResponseModel Search(ReadOnlyUrl url)
        {
            return Deserialize<CandidatesResponseModel>(Get(url), new CandidateModelJavaScriptConverter());
        }

        private ReadOnlyUrl GetUrl(MemberSearchCriteria criteria)
        {
            var url = _searchUrl.AsNonReadOnly();
            url.QueryString.Add(new QueryStringGenerator(new MemberSearchCriteriaConverter(_locationQuery, _industriesQuery)).GenerateQueryString(criteria));
            return url;
        }

        protected CandidatesResponseModel Search(MemberSearchCriteria criteria)
        {
            return Search(GetUrl(criteria));
        }

        protected CandidatesResponseModel Search(MemberSearchCriteria criteria, MemberSortOrder sortOrder)
        {
            return Search(GetUrl(GetUrl(criteria), sortOrder));
        }

        protected CandidatesResponseModel Search(MemberSearchCriteria criteria, MemberSortOrder sortOrder, int? page, int? items)
        {
            if (page == null && items == null)
                return Search(criteria, sortOrder);
            return Search(GetUrl(GetUrl(GetUrl(criteria), sortOrder), page, items));
        }

        protected ReadOnlyUrl GetSearchUrl()
        {
            return _searchUrl;
        }
    }
}