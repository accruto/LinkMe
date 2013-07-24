using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using LinkMe.Apps.Asp.Mvc.Controllers;
using LinkMe.Apps.Asp.Mvc.Models;
using LinkMe.Apps.Asp.Routing;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Industries.Queries;
using LinkMe.Domain.Location.Queries;
using LinkMe.Domain.Roles.Affiliations.Communities.Queries;
using LinkMe.Domain.Roles.Affiliations.Verticals.Queries;
using LinkMe.Domain.Roles.JobAds.Queries;
using LinkMe.Domain.Users.Employers.JobAds.Queries;
using LinkMe.Domain.Users.Employers.Queries;
using LinkMe.Domain.Users.Members.JobAds;
using LinkMe.Domain.Users.Members.JobAds.Commands;
using LinkMe.Domain.Users.Members.JobAds.Queries;
using LinkMe.Framework.Utility;
using LinkMe.Framework.Utility.Results;
using LinkMe.Query.JobAds;
using LinkMe.Query.Search;
using LinkMe.Query.Search.JobAds;
using LinkMe.Query.Search.JobAds.Commands;
using LinkMe.Query.Search.JobAds.Queries;
using LinkMe.Web.Areas.Members.Models;
using LinkMe.Web.Areas.Members.Models.Search;
using LinkMe.Web.Areas.Members.Routes;
using LinkMe.Web.Areas.Public.Routes;
using LinkMe.Web.Context;
using LinkMe.Web.Controllers;
using LinkMe.Web.Models;

namespace LinkMe.Web.Areas.Members.Controllers.Search
{
    public class SearchController
        : JobAdSearchListController
    {
        private readonly IJobAdSearchesCommand _jobAdSearchesCommand;
        private readonly IJobAdSearchesQuery _jobAdSearchesQuery;

        private readonly ILocationQuery _locationQuery;
        private readonly ICommunitiesQuery _communitiesQuery;
        private readonly IVerticalsQuery _verticalsQuery;
        private readonly IIndustriesQuery _industriesQuery;

        private const int MaxSuggestedJobs = 5;

        private static readonly JobAdSortOrder[] SortOrders = new[] { JobAdSortOrder.CreatedTime, JobAdSortOrder.Distance, JobAdSortOrder.Flagged, JobAdSortOrder.JobType, JobAdSortOrder.Relevance, JobAdSortOrder.Salary }.OrderBy(o => o.ToString()).ToArray();

        public SearchController(IJobAdFoldersCommand jobAdFoldersCommand, IExecuteJobAdSearchCommand executeJobAdSearchCommand, IJobAdSearchesCommand jobAdSearchesCommand, IJobAdSearchesQuery jobAdSearchesQuery, IJobAdsQuery jobAdsQuery, ILocationQuery locationQuery, ICommunitiesQuery communitiesQuery, IVerticalsQuery verticalsQuery, IIndustriesQuery industriesQuery, IJobAdFoldersQuery jobAdFoldersQuery, IJobAdFlagListsQuery jobAdFlagListsQuery, IMemberJobAdViewsQuery memberJobAdViewsQuery, IJobAdProcessingQuery jobAdProcessingQuery, IJobAdBlockListsQuery jobAdBlockListsQuery, IEmployersQuery employersQuery)
            : base(executeJobAdSearchCommand, jobAdsQuery, jobAdFlagListsQuery, memberJobAdViewsQuery, jobAdProcessingQuery, jobAdFoldersQuery, jobAdFoldersCommand, jobAdBlockListsQuery, employersQuery)
        {
            _jobAdSearchesCommand = jobAdSearchesCommand;
            _jobAdSearchesQuery = jobAdSearchesQuery;
            _locationQuery = locationQuery;
            _communitiesQuery = communitiesQuery;
            _verticalsQuery = verticalsQuery;
            _industriesQuery = industriesQuery;
        }

        public ActionResult Search(JobAdSearchCriteria criteria, JobAdsPresentationModel presentation)
        {
            var member = CurrentMember;

            // Prepare.

            var criteriaIsEmpty = PrepareCriteria(criteria);
            presentation = PreparePresentationModel(presentation);

            if (criteriaIsEmpty || !ModelState.IsValid)
            {
                // Set up defaults.

                SetDefaults(criteria);

                if (member != null && MemberContext.ShowUpdatedTermsReminder())
                    ModelState.AddModelConfirmation("We've made some changes to our terms and conditions. You can review them <a href=\"" + SupportRoutes.Terms.GenerateUrl() + "\">here</a>.");

                return View(GetSearchModel(criteria, presentation));
            }

            MemberContext.CurrentSearch = new JobAdSearchNavigation(criteria, presentation);
            MemberContext.IsNewSearch = true;

            return RedirectToRoute(SearchRoutes.Results);
        }

        public ActionResult PartialSearch(JobAdSearchCriteria criteria, JobAdsPresentationModel presentation)
        {
            var member = CurrentMember;

            // Prepare.

            var criteriaIsEmpty = PrepareCriteria(criteria);
            presentation = PreparePresentationModel(presentation);
            SearchListModel model;
            if (criteriaIsEmpty)
            {
                // Set up defaults.

                SetDefaults(criteria);
                model = CreateEmptyList<SearchListModel>(member, criteria, presentation, JobAdListType.SearchResult);
                return View("JobAdList", model);
            }

            model = Search(SearchContext.Filter, member, criteria, presentation, null, JobAdListType.SearchResult);
            return PartialView("JobAdList", model);
        }

        public ActionResult LeftSide()
        {
            var search = MemberContext.CurrentSearch;

            if (search == null)
            {
                //There should always be a curent search but it's possible for a spider to hit this link
                return RedirectToRoute(SearchRoutes.Search);
            }

            var presentation = new JobAdsPresentationModel
            {
                Pagination = search.Pagination
            };

            var member = CurrentMember;

            // Not a new search.
            return View("LeftSide", Search(SearchContext.Current, member, search.Criteria, PreparePresentationModel(presentation), null, JobAdListType.SearchResult));
        }

        public ActionResult LocationIndustryJobAds(string locationSegment, string industrySegment)
        {
            return LocationIndustryJobAds(locationSegment, industrySegment, null);
        }

        public ActionResult PagedLocationIndustryJobAds(string locationSegment, string industrySegment, int? page)
        {
            return LocationIndustryJobAds(locationSegment, industrySegment, page);
        }

        [EnsureHttps, EnsureAuthorized(UserType.Member)]
        public ActionResult Saved(Guid savedSearchId)
        {
            var search = _jobAdSearchesQuery.GetJobAdSearch(savedSearchId);
            if (search == null)
                return NotFound("search", "id", savedSearchId);

            var member = CurrentMember;

            // Do a search based on the search's criteria.

            var searchList = Search(SearchContext.Saved, member, search.Criteria, PreparePresentationModel(null), savedSearchId, JobAdListType.SearchResult);

            return View("Results", searchList);
        }

        [EnsureHttps, EnsureAuthorized(UserType.Member)]
        public ActionResult Recent(Guid recentSearchId)
        {
            var search = _jobAdSearchesQuery.GetJobAdSearchExecution(recentSearchId);
            if (search == null)
                return NotFound("search", "id", recentSearchId);

            var member = CurrentMember;

            // Do a search based on the search's criteria.

            var searchList = Search(SearchContext.Saved, member, search.Criteria, PreparePresentationModel(null), null, JobAdListType.SearchResult);

            return View("Results", searchList);
        }

        public ActionResult Results()
        {
            var search = MemberContext.CurrentSearch;
            if (search == null)
                return View("NoCurrentSearch");

            var presentation = new JobAdsPresentationModel
            {
                Pagination = search.Pagination
            };

            var member = CurrentMember;

            if (MemberContext.IsNewSearch)
            {
                // Do a search based on the criteria.

                var searchList = Search(SearchContext.NewSearch, member, search.Criteria, presentation, null, JobAdListType.SearchResult);

                // Get suggestions as needed.

                return View("Results", searchList);
            }

            // Not a new search.

            return View("Results", Search(SearchContext.Current, member, search.Criteria, PreparePresentationModel(presentation), null, JobAdListType.SearchResult));
        }

        private SearchListModel Search(SearchContext context, IMember member, JobAdSearchCriteria criteria, JobAdsPresentationModel presentation, Guid? savedSearchId, JobAdListType listType)
        {
            var searchList = GetSearchList(Search<SearchListModel>(member, PrepareCriteriaForSearch(criteria), presentation, listType));

            SaveSearch(context, member, searchList, presentation.Pagination.Page.Value, savedSearchId);

            // Save the basis for the search.

            MemberContext.CurrentSearch = new JobAdSearchNavigation(searchList.Criteria, searchList.Presentation);
            return searchList;
        }

        private BrowseListModel Browse(IMember member, JobAdSearchCriteria criteria, JobAdsPresentationModel presentation, JobAdListType listType)
        {
            var browseList = GetSearchList(Search<BrowseListModel>(member, PrepareCriteriaForSearch(criteria), presentation, listType));

            // Save the basis for the search.

            MemberContext.CurrentSearch = new JobAdSearchNavigation(browseList.Criteria, browseList.Presentation);
            return browseList;
        }

        private ActionResult LocationIndustryJobAds(string locationSegment, string industrySegment, int? page)
        {
            var industry = _industriesQuery.GetIndustryByUrlSegment(industrySegment, JobAdsRoutes.SegmentSuffix);
            var location = _locationQuery.GetLocationByUrlSegment(ActivityContext.Location.Country, locationSegment, JobAdsRoutes.SegmentSuffix);
            if (industry == null && location == null)
                return RedirectToUrl(JobAdsRoutes.BrowseJobAds.GenerateUrl(), true);
            if (location == null)
                return RedirectToUrl(industry.GenerateJobAdsUrl(), true);
            if (industry == null)
                return RedirectToUrl(location.GenerateJobAdsUrl(), true);

            // Check url.

            var result = EnsureUrl(location.GenerateJobAdsUrl(industry, page));
            if (result != null)
                return result;

            if (page.HasValue && page.Value < 2)
            {
                //on page 0 or 1; should be page <blank>
                return RedirectToUrl(location.GenerateJobAdsUrl(industry, null), true);
            }

            //construct a criteria and presentation and return results
            var criteria = new JobAdSearchCriteria
            {
                Location = _locationQuery.ResolveLocation(ActivityContext.Location.Country, location.Name),
                IndustryIds = new List<Guid> { industry.Id },
                SortCriteria = new JobAdSearchSortCriteria { ReverseSortOrder = false, SortOrder = JobAdSortOrder.CreatedTime },
            };

            var presentation = new JobAdsPresentationModel
            {
                Pagination = new Pagination { Page = page.HasValue ? page : 1 },
            };

            criteria.PrepareCriteria();
            PreparePresentationModel(presentation);

            var browseList = Browse(CurrentMember, criteria, presentation, JobAdListType.BrowseResult);
            if ((browseList.Results.TotalJobAds <= (presentation.Pagination.Page - 1) * Reference.DefaultItemsPerPage) && presentation.Pagination.Page > 1)
            {
                //on some page that doesn't exist (anymore) - redirect to page one
                return RedirectToUrl(location.GenerateJobAdsUrl(industry, null));
            }

            browseList.Industry = industry;
            browseList.Location = location;

            return View("Results", browseList);
        }

        private static bool PrepareCriteria(JobAdSearchCriteria criteria)
        {
            // Prepare the criteria after doing the check for empty as preparing it may set things.

            var criteriaIsEmpty = criteria.IsEmpty;
            criteria.PrepareCriteria();

            return criteriaIsEmpty;
        }

        private static JobAdSearchCriteria PrepareCriteriaForSearch(JobAdSearchCriteria criteria)
        {
            return criteria;
        }

        private void SaveSearch(SearchContext context, IHasId<Guid> member, JobAdSearchListModel searchList, int page, Guid? savedSearchId)
        {
            try
            {
                var userAgent = HttpContext.Request.UserAgent;

                //never log monitoring calls
                if (!string.IsNullOrEmpty(userAgent) && userAgent.Contains("LinkmeMonitoring"))
                    return;

                var app = ActivityContext.Channel.App;
                var execution = new JobAdSearchExecution
                {
                    Context = context.ToString(),
                    Criteria = searchList.Criteria.Clone(),
                    Results = new JobAdSearchResults
                    {
                        JobAdIds = page == 1 ? searchList.Results.JobAdIds : new List<Guid>(),
                        TotalMatches = searchList.Results.TotalJobAds
                    },
                    SearcherId = member == null ? (Guid?)null : member.Id,
                    SearchId = savedSearchId,
                    ChannelId = app.ChannelId,
                    AppId = app.Id,
                    StartTime = DateTime.Now,
                };

                _jobAdSearchesCommand.CreateJobAdSearchExecution(execution);
            }
            catch (Exception)
            {
                // Never fail for this.
            }
        }

        private void SetDefaults(JobAdSearchCriteria criteria)
        {
            criteria.Location = _locationQuery.ResolveLocation(ActivityContext.Location.Country, null);
        }

        private SearchModel GetSearchModel(JobAdSearchCriteria criteria, JobAdsPresentationModel presentationModel)
        {
            var suggestedJobs = GetSuggestedJobs(CurrentMember);

            return new SearchModel
            {
                Criteria = criteria,
                Presentation = presentationModel,
                ListType = JobAdListType.SearchResult,
                AncillaryData = GetAncillaryData(),
                SuggestedJobs = suggestedJobs,
            };
        }

        private TListModel GetSearchList<TListModel>(TListModel searchList)
            where TListModel: SearchListModel
        {
            searchList.AncillaryData = GetAncillaryData();
            
            // Only include communities for which verticals are not deleted.
            
            searchList.SortOrders = SortOrders;
            searchList.Verticals = _verticalsQuery.GetVerticals().ToDictionary(v => v.Id, v => v);
            searchList.Communities = (from c in _communitiesQuery.GetCommunities() where searchList.Verticals.ContainsKey(c.Id) select c).ToDictionary(c => c.Id, c => c);
            searchList.Folders = GetFoldersModel();
            searchList.BlockLists = GetBlockListsModel();
            searchList.Industries = _industriesQuery.GetIndustries();

            searchList.IsFirstSearch = MemberContext.IsFirstSearch;
            if (searchList.IsFirstSearch)
                MemberContext.IsFirstSearch = false;

            searchList.IsSavedSearch = false;

            return searchList;
        }

        private SearchAncillaryModel GetAncillaryData()
        {
            var country = ActivityContext.Location.Country;

            var ancillaryModel = new SearchAncillaryModel
                                     {
                                         Countries = _locationQuery.GetCountries(),
                                         Industries = _industriesQuery.GetIndustries(),
                                         Distances = Reference.Distances,
                                         DefaultDistance = JobAdSearchCriteria.DefaultDistance,
                                         DefaultCountry = ActivityContext.Location.Country,
                                         Recencies = Recencies,
                                         DefaultRecency = JobAdSearchCriteria.DefaultRecency,
                                         MaxRecency = JobAdSearchCriteria.MaxRecency,
                                         MinSalary = JobAdSearchCriteria.MinSalary,
                                         MaxSalary = JobAdSearchCriteria.MaxSalary,
                                         StepSalary = JobAdSearchCriteria.StepSalary,
                                         MinHourlySalary = JobAdSearchCriteria.MinHourlySalary,
                                         MaxHourlySalary = JobAdSearchCriteria.MaxHourlySalary,
                                         StepHourlySalary = JobAdSearchCriteria.StepHourlySalary,
                                         CountrySubdivisions = (from s in _locationQuery.GetCountrySubdivisions(country)
                                         where !s.IsCountry
                                         select s).ToList(),
                                         Regions = _locationQuery.GetRegions(country)
                                     };
            return ancillaryModel;
        }

        private IList<MemberJobAdView> GetSuggestedJobs(IMember member)
        {
            if (member == null)
                return new List<MemberJobAdView>();

            try
            {
                var execution = _executeJobAdSearchCommand.SearchSuggested(member, null, new Range(0, MaxSuggestedJobs));
                return _memberJobAdViewsQuery.GetMemberJobAdViews(member, execution.Results.JobAdIds);
            }
            catch (Exception)
            {
                // If this fails then don't fail the call.

                return new List<MemberJobAdView>();
            }
        }
    }
}
