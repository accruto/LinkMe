using System.Globalization;
using System.Linq;
using System.Web.Script.Serialization;
using LinkMe.Apps.Agents.Test.Users.Employers;
using LinkMe.Apps.Agents.Test.Users.Members;
using LinkMe.Apps.Agents.Users.Employers.Commands;
using LinkMe.Apps.Agents.Users.Members.Commands;
using LinkMe.Apps.Presentation.Converters;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Industries.Queries;
using LinkMe.Domain.Roles.JobAds.Commands;
using LinkMe.Domain.Roles.Recruiters.Commands;
using LinkMe.Domain.Roles.Test.Recruiters;
using LinkMe.Framework.Utility;
using LinkMe.Framework.Utility.Urls;
using LinkMe.Query.Search.JobAds;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Members.Jobs.Search
{
    [TestClass]
    public abstract class SearchTests
        : WebTestClass
    {
        protected readonly IEmployerAccountsCommand _employerAccountsCommand = Resolve<IEmployerAccountsCommand>();
        protected readonly IOrganisationsCommand _organisationsCommand = Resolve<IOrganisationsCommand>();
        protected readonly IJobAdsCommand _jobAdsCommand = Resolve<IJobAdsCommand>();
        private readonly IMemberAccountsCommand _memberAccountsCommand = Resolve<IMemberAccountsCommand>();
        protected IIndustriesQuery _industriesQuery = Resolve<IIndustriesQuery>();

        private ReadOnlyUrl _searchUrl;
        private ReadOnlyUrl _leftSideUrl;
        private ReadOnlyUrl _resultsUrl;
        private ReadOnlyUrl _apiSearchUrl;

        [TestInitialize]
        public void SearchTestsInitialize()
        {
            _searchUrl = new ReadOnlyApplicationUrl("~/search/jobs");
            _leftSideUrl = new ReadOnlyApplicationUrl("~/search/jobs/leftside");
            _resultsUrl = new ReadOnlyApplicationUrl("~/search/jobs/results");
            _apiSearchUrl = new ReadOnlyApplicationUrl("~/search/jobs/api");
        }

        protected Employer CreateEmployer(int index)
        {
            return _employerAccountsCommand.CreateTestEmployer(index, _organisationsCommand.CreateTestVerifiedOrganisation(index));
        }

        protected Member CreateMember(int index)
        {
            return _memberAccountsCommand.CreateTestMember(index);
        }

        protected ReadOnlyUrl GetResultsUrl()
        {
            return _resultsUrl;
        }

        protected ReadOnlyUrl GetSearchUrl()
        {
            return _searchUrl;
        }

        protected ReadOnlyUrl GetLeftSideUrl()
        {
            return _leftSideUrl;
        }

        protected ReadOnlyUrl GetApiSearchUrl()
        {
            return _apiSearchUrl;
        }

        protected ReadOnlyUrl GetSearchUrl(JobAdSearchCriteria criteria, int? page, int? items)
        {
            return Get(GetSearchUrl(), criteria, page, items);
        }

        protected ReadOnlyUrl GetSearchUrl(JobAdSearchCriteria criteria)
        {
            return Get(GetSearchUrl(), criteria, null, null);
        }

        protected ReadOnlyUrl GetApiSearchUrl(JobAdSearchCriteria criteria, int? page, int? items)
        {
            return Get(GetApiSearchUrl(), criteria, page, items);
        }

        protected ReadOnlyUrl GetApiSearchUrl(JobAdSearchCriteria criteria)
        {
            return Get(GetApiSearchUrl(), criteria, null, null);
        }

        protected void Search(string keywords)
        {
            var criteria = new JobAdSearchCriteria();
            criteria.SetKeywords(keywords);
            Get(GetSearchUrl(criteria));
        }

        protected JsonSearchResponseModel ApiSearch(string keywords)
        {
            var criteria = new JobAdSearchCriteria();
            criteria.SetKeywords(keywords);
            return ApiSearch(criteria);
        }

        protected JsonSearchResponseModel ApiSearch(JobAdSearchCriteria criteria, int? page, int? items)
        {
            var url = GetApiSearchUrl(criteria, page, items);
            var response = Get(url);
            var model = new JavaScriptSerializer().Deserialize<JsonSearchResponseModel>(response);
            AssertJsonSuccess(model);
            return model;
        }

        protected JsonSearchResponseModel ApiSearch(JobAdSearchCriteria criteria)
        {
            return ApiSearch(criteria, null, null);
        }

        private ReadOnlyUrl Get(ReadOnlyUrl baseUrl, JobAdSearchCriteria criteria, int? page, int? items)
        {
            var url = baseUrl.AsNonReadOnly();
            url.QueryString.Add(new QueryStringGenerator(new JobAdSearchCriteriaConverter(_locationQuery, _industriesQuery)).GenerateQueryString(criteria));

            // In general if all industries are supplied then they are not included in the query string.  However, to simulate the UI
            // which does not make this distinction manually include them here.

            if (criteria.IndustryIds != null && criteria.IndustryIds.CollectionContains(_industriesQuery.GetIndustries().Select(i => i.Id)))
            {
                foreach (var industryId in criteria.IndustryIds)
                    url.QueryString.Add("IndustryIds", industryId.ToString());
            }

            if (page != null)
                url.QueryString["Page"] = page.Value.ToString(CultureInfo.InvariantCulture);
            if (items != null)
                url.QueryString["Items"] = items.Value.ToString(CultureInfo.InvariantCulture);

            return url;
        }
    }
}
