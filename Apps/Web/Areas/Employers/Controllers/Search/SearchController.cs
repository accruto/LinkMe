using System;
using System.Linq;
using System.Web.Mvc;
using LinkMe.Apps.Asp.Mvc.Controllers;
using LinkMe.Apps.Asp.Mvc.Models;
using LinkMe.Apps.Asp.Routing;
using LinkMe.Apps.Presentation.Errors;
using LinkMe.Apps.Presentation.Query.Search.Members;
using LinkMe.Domain;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Credits;
using LinkMe.Domain.Industries.Queries;
using LinkMe.Domain.Location;
using LinkMe.Domain.Location.Queries;
using LinkMe.Domain.Roles.Affiliations.Communities.Queries;
using LinkMe.Domain.Roles.Affiliations.Verticals.Queries;
using LinkMe.Domain.Users.Employers.Candidates;
using LinkMe.Domain.Users.Employers.Candidates.Commands;
using LinkMe.Domain.Users.Employers.Credits.Queries;
using LinkMe.Domain.Users.Employers.Views.Queries;
using LinkMe.Domain.Users.Members.Status.Queries;
using LinkMe.Framework.Instrumentation;
using LinkMe.Framework.Utility;
using LinkMe.Query.Members;
using LinkMe.Query.Search.Members;
using LinkMe.Query.Search.Members.Commands;
using LinkMe.Query.Search.Members.Queries;
using LinkMe.Web.Areas.Employers.Models;
using LinkMe.Web.Areas.Employers.Models.Search;
using LinkMe.Web.Areas.Employers.Routes;
using LinkMe.Web.Areas.Public.Routes;
using LinkMe.Web.Context;
using LinkMe.Web.Controllers;
using LinkMe.Web.Models;

namespace LinkMe.Web.Areas.Employers.Controllers.Search
{
    public class SearchController
        : CandidateListController
    {
        private static readonly EventSource EventSource = new EventSource<SearchController>();

        private readonly IMemberSearchSuggestionsQuery _memberSearchSuggestionsQuery;
        private readonly IMemberSearchesCommand _memberSearchesCommand;
        private readonly IMemberSearchesQuery _memberSearchesQuery;
        private readonly ICandidateListsCommand _candidateListsCommand;
        private readonly IEmployerCreditsQuery _employerCreditsQuery;
        private readonly ILocationQuery _locationQuery;
        private readonly ICommunitiesQuery _communitiesQuery;
        private readonly IVerticalsQuery _verticalsQuery;
        private readonly IIndustriesQuery _industriesQuery;

        private static readonly MemberSortOrder[] SortOrders = new[] { MemberSortOrder.Relevance, MemberSortOrder.DateUpdated, MemberSortOrder.Distance, MemberSortOrder.Salary, MemberSortOrder.Flagged, MemberSortOrder.Availability, MemberSortOrder.FirstName }.OrderBy(o => o.ToString()).ToArray();

        public SearchController(IExecuteMemberSearchCommand executeMemberSearchCommand, IMemberSearchesCommand memberSearchesCommand, IEmployerMemberViewsQuery employerMemberViewsQuery, IMemberStatusQuery memberStatusQuery, IMemberSearchSuggestionsQuery memberSearchSuggestionsQuery, IMemberSearchesQuery memberSearchesQuery, ICandidateListsCommand candidateListsCommand, IEmployerCreditsQuery employerCreditsQuery, ILocationQuery locationQuery, ICommunitiesQuery communitiesQuery, IVerticalsQuery verticalsQuery, IIndustriesQuery industriesQuery)
            : base(executeMemberSearchCommand, employerMemberViewsQuery, memberStatusQuery)
        {
            _memberSearchSuggestionsQuery = memberSearchSuggestionsQuery;
            _memberSearchesCommand = memberSearchesCommand;
            _memberSearchesQuery = memberSearchesQuery;
            _candidateListsCommand = candidateListsCommand;
            _employerCreditsQuery = employerCreditsQuery;
            _locationQuery = locationQuery;
            _communitiesQuery = communitiesQuery;
            _verticalsQuery = verticalsQuery;
            _industriesQuery = industriesQuery;
        }

        [EnsureHttps]
        public ActionResult LocationSalaryBandCandidates(string locationSegment, string salarySegment)
        {
            return LocationSalaryBandCandidates(locationSegment, salarySegment, null);
        }

        [EnsureHttps]
        public ActionResult PagedLocationSalaryBandCandidates(string locationSegment, string salarySegment, int? page)
        {
            return LocationSalaryBandCandidates(locationSegment, salarySegment, page);
        }

        [EnsureHttps]
        public ActionResult Search(MemberSearchCriteria criteria, CandidatesPresentationModel presentation, bool? createEmailAlert)
        {
            var employer = CurrentEmployer;

            // Prepare.

            var criteriaIsEmpty = PrepareCriteria(employer, criteria);
            presentation = PreparePresentationModel(presentation);

            if (criteriaIsEmpty || !ModelState.IsValid)
            {
                // Set up defaults.

                SetDefaults(criteria);

                if (CurrentEmployer != null && EmployerContext.ShowUpdatedTermsReminder())
                    ModelState.AddModelConfirmation("We've made some changes to our terms and conditions. You can review them <a href=\"" + SupportRoutes.Terms.GenerateUrl() + "\">here</a>.");
                
                return View(GetSearchList(employer, createEmailAlert, criteria, presentation));
            }

            // On a new search reset.

            EmployerContext.CurrentSearch = new MemberSearchNavigation(criteria, presentation);
            ResetSearch(employer, null);

            return createEmailAlert != null
                ? RedirectToRoute(SearchRoutes.Results, new { createEmailAlert })
                : RedirectToRoute(SearchRoutes.Results);
        }

        public ActionResult PartialSearch(MemberSearchCriteria criteria, CandidatesPresentationModel presentation)
        {
            var employer = CurrentEmployer;

            // Prepare.

            var criteriaIsEmpty = PrepareCriteria(employer, criteria);
            presentation = PreparePresentationModel(presentation);

            if (criteriaIsEmpty)
            {
                // Set up defaults.

                SetDefaults(criteria);
                return View("CandidateList", CreateEmptyList<SearchListModel>(employer, criteria, presentation));
            }

            return PartialView("CandidateList", Search(SearchContext.Filter, employer, null, criteria, presentation, null));
        }

        public ActionResult Tips()
        {
            return View();
        }

        public ActionResult Results(bool? createEmailAlert)
        {
            var search = EmployerContext.CurrentSearch;
            if (search == null)
                return View("NoCurrentSearch");

            var presentation = new CandidatesPresentationModel
            {
                DetailLevel = search.DetailLevel == null ? default(DetailLevel) : search.DetailLevel.Value,
                Pagination = search.Pagination
            };

            var employer = CurrentEmployer;

            if (EmployerContext.IsNewSearch)
            {
                // Do a search based on the criteria.

                var searchList = Search(
                    SearchContext.NewSearch,
                    employer,
                    createEmailAlert,
                    search.Criteria,
                    presentation,
                    null);

                // Get suggestions as needed.

                searchList.IsNewSearch = true;
                searchList.ShowHelpPrompt = ShowHelpPrompt(employer);
                searchList.Recovery = GetRecovery(searchList);

                return View("Results", searchList);
            }
            
            // Not a new search.

            var model = Search(
                SearchContext.Current,
                employer,
                createEmailAlert,
                search.Criteria,
                PreparePresentationModel(presentation),
                null);

            return View("Results", model);
        }

        [EnsureHttps, EnsureAuthorized(UserType.Employer)]
        public ActionResult Saved(Guid savedSearchId)
        {
            var search = _memberSearchesQuery.GetMemberSearch(savedSearchId);
            if (search == null)
                return NotFound("search", "id", savedSearchId);

            var employer = CurrentEmployer;
            ResetSearch(employer, search.Name);

            // Do a search based on the search's criteria.

            var criteria = search.Criteria;
            PrepareCriteria(employer, criteria);

            var searchList = Search(
                SearchContext.Saved,
                employer,
                null,
                criteria,
                PreparePresentationModel(null),
                savedSearchId);
            searchList.SavedSearch = search.Name;

            return View("Results", searchList);
        }

        private SearchListModel Search(SearchContext context, IEmployer employer, bool? createEmailAlert, MemberSearchCriteria criteria, CandidatesPresentationModel presentation, Guid? savedSearchId)
        {
            var canSearchByName = CanSearchByName(employer);
            var canSelectCommunity = CanSelectCommunity(employer);

            var searchList = GetSearchList(
                Search<SearchListModel>(employer, PrepareCriteriaForSearch(criteria, canSelectCommunity), presentation),
                canSearchByName,
                canSelectCommunity,
                createEmailAlert);

            SaveSearch(context, employer, searchList, presentation.Pagination.Page.Value, savedSearchId);

            // Save the basis for the search.

            EmployerContext.CurrentSearch = new MemberSearchNavigation(searchList.Criteria, searchList.Presentation);
            return searchList;
        }

        private BrowseListModel Browse(IEmployer employer, MemberSearchCriteria criteria, CandidatesPresentationModel presentation)
        {
            var canSearchByName = CanSearchByName(employer);
            var canSelectCommunity = CanSelectCommunity(employer);

            var browseList = GetSearchList(
                Search<BrowseListModel>(employer, PrepareCriteriaForSearch(criteria, canSelectCommunity), presentation),
                canSearchByName,
                canSelectCommunity,
                null);

            // Save the basis for the search.

            EmployerContext.CurrentCandidates = new BrowseCandidatesNavigation((IUrlNamedLocation)browseList.Criteria.Location.NamedLocation, browseList.Criteria.Salary, browseList.Presentation);
            return browseList;
        }

        private ActionResult LocationSalaryBandCandidates(string locationSegment, string salarySegment, int? page)
        {
            var salary = salarySegment.GetSalaryByUrlSegment(CandidatesRoutes.SegmentSuffix);
            var location = _locationQuery.GetLocationByUrlSegment(ActivityContext.Location.Country, locationSegment, CandidatesRoutes.SegmentSuffix);

            if (location == null && salary == null)
                return RedirectToUrl(CandidatesRoutes.BrowseCandidates.GenerateUrl(), true);
            if (location == null)
                return RedirectToUrl(salary.GenerateCandidatesUrl(), true);
            if (salary == null)
                return RedirectToUrl(location.GenerateCandidatesUrl(), true);

            // Check url.

            var result = EnsureUrl(location.GenerateCandidatesUrl(salary, page));
            if (result != null)
                return result;

            if (page.HasValue && page.Value < 2)
            {
                //on index 0 or 1; should be index <blank>
                return RedirectToUrl(location.GenerateCandidatesUrl(salary, null), true);
            }

            //construct a criteria and presentation and return results

            var criteria = new MemberSearchCriteria
            {
                Location = _locationQuery.ResolveLocation(_locationQuery.GetCountry("Australia"), location.Name),
                Salary = new Salary { LowerBound = salary.LowerBound, UpperBound = salary.UpperBound, Currency = Currency.AUD, Rate = SalaryRate.Year },
                ExcludeNoSalary = true,
                SortCriteria = new MemberSearchSortCriteria{ReverseSortOrder = false, SortOrder = MemberSortOrder.DateUpdated},
            };

            var presentation = new CandidatesPresentationModel
            {
                Pagination = new Pagination
                {
                    Page = page.HasValue ? page : 1,
                },
            };

            var employer = CurrentEmployer;

            PrepareCriteria(employer, criteria);
            PreparePresentationModel(presentation);

            var browseList = Browse(employer, criteria, presentation);

            if ((browseList.Results.TotalCandidates <= (presentation.Pagination.Page - 1) * Reference.DefaultItemsPerPage) && presentation.Pagination.Page > 1)
            {
                //on some page that doesn't exist (anymore) - redirect to page one
                return RedirectToUrl(location.GenerateCandidatesUrl(salary, null));
            }

            browseList.SalaryBand = criteria.Salary;
            browseList.Location = location;

            return View("Results", browseList);
        }

        private bool PrepareCriteria(IEmployer employer, MemberSearchCriteria criteria)
        {
            // If trying to do a search by name ensure that they have unlimited credits.

            if (!string.IsNullOrEmpty(criteria.Name))
            {
                if (!HasUnlimitedCredits(employer))
                {
                    // Remove the criteria but allow the search to proceed.

                    criteria.Name = null;
                    criteria.IncludeSimilarNames = MemberSearchCriteria.DefaultIncludeSimilarNames;
                }
            }

            // Prepare the criteria after doing the check for empty as preparing it may set things.

            var criteriaIsEmpty = criteria.IsEmpty;
            criteria.PrepareCriteria();

            return criteriaIsEmpty;
        }

        private MemberSearchCriteria PrepareCriteriaForSearch(MemberSearchCriteria criteria, bool canSelectCommunity)
        {
            // If within a community site then restrict who is returned.

            if (!canSelectCommunity)
                criteria.CommunityId = ActivityContext.Community.Id;

            return criteria;
        }

        private void SaveSearch(SearchContext context, IHasId<Guid> employer, CandidateListModel searchList, int page, Guid? savedSearchId)
        {
            try
            {
                var userAgent = HttpContext.Request.UserAgent;

                //never log monitoring calls
                if (!string.IsNullOrEmpty(userAgent) && userAgent.Contains("LinkmeMonitoring"))
                    return;

                var app = ActivityContext.Channel.App;
                var execution = new MemberSearchExecution
                {
                    Context = context.ToString(),
                    Criteria = searchList.Criteria.Clone(),
                    Results = new MemberSearchResults
                    {
                        MemberIds = page == 1 ? searchList.Results.CandidateIds : new Guid[0],
                        TotalMatches = searchList.Results.TotalCandidates
                    },
                    SearcherId = employer == null ? (Guid?) null : employer.Id,
                    SearchId = savedSearchId,
                    ChannelId = app.ChannelId,
                    AppId = app.Id,
                    StartTime = DateTime.Now,
                };

                _memberSearchesCommand.CreateMemberSearchExecution(execution);
            }
            catch (Exception)
            {
                // Never fail for this.
            }
        }

        private void SetDefaults(MemberSearchCriteria criteria)
        {
            criteria.Location = _locationQuery.ResolveLocation(ActivityContext.Location.Country, null);
        }

        private SearchListModel GetSearchList(IEmployer employer, bool? createEmailAlert, MemberSearchCriteria criteria, CandidatesPresentationModel presentation)
        {
            return GetSearchList(
                new SearchListModel { Criteria = criteria, Presentation = presentation },
                CanSearchByName(employer),
                CanSelectCommunity(employer),
                createEmailAlert);
        }

        private TListModel GetSearchList<TListModel>(TListModel searchList, bool canSearchByName, bool canSelectCommunity, bool? createEmailAlert)
            where TListModel : SearchListModel
        {
            // Only include communities for which verticals are not deleted.

            searchList.SortOrders = SortOrders;
            searchList.Verticals = _verticalsQuery.GetVerticals().ToDictionary(v => v.Id, v => v);
            searchList.Communities = (from c in _communitiesQuery.GetCommunities() where searchList.Verticals.ContainsKey(c.Id) select c).ToDictionary(c => c.Id, c => c);

            searchList.Countries = _locationQuery.GetCountries();
            searchList.CanSearchByName = canSearchByName;
            searchList.CanSelectCommunity = canSelectCommunity;
            searchList.CreateEmailAlert = createEmailAlert ?? false;
            searchList.IsFirstSearch = EmployerContext.Searches <= 1;
            searchList.ShowHelpPrompt = false;

            searchList.Industries = _industriesQuery.GetIndustries();
            searchList.Distances = Reference.Distances;
            searchList.DefaultDistance = MemberSearchCriteria.DefaultDistance;
            searchList.DefaultCountry = ActivityContext.Location.Country;
            searchList.Recencies = Recencies;
            searchList.DefaultRecency = MemberSearchCriteria.DefaultRecency;
            searchList.MinSalary = MemberSearchCriteria.MinSalary;
            searchList.MaxSalary = MemberSearchCriteria.MaxSalary;
            searchList.StepSalary = MemberSearchCriteria.StepSalary;

            return searchList;
        }

        private void ResetSearch(IEmployer employer, string savedSearchName)
        {
            // Empty the temporary block list.

            if (employer != null)
                _candidateListsCommand.RemoveAllCandidatesFromBlockList(employer, BlockListType.Temporary);

            EmployerContext.SavedSearchName = savedSearchName;

            if (string.IsNullOrEmpty(savedSearchName))
            {
                EmployerContext.IsNewSearch = true;

                // Determine whether the prompt can be shown just the once.

                var searches = EmployerContext.Searches;
                if (searches == 0)
                    EmployerContext.CanShowHelpPrompt = !HasUnlimitedCredits(employer);

                EmployerContext.Searches = searches + 1;
            }
        }

        private CandidateListRecoveryModel GetRecovery(CandidateListModel searchList)
        {
            const string method = "GetRecovery";

            try
            {
                // Get spelling suggestions if needed.

                var spellingSuggestions = searchList.Results.TotalCandidates <= Reference.SpellingThreshold
                    ? _memberSearchSuggestionsQuery.GetSpellingSuggestions(searchList.Criteria)
                    : null;

                // Get more results only if there are no spelling suggestions.

                var moreResultsSuggestions = searchList.Results.TotalCandidates <= Reference.MoreResultsThreshold && spellingSuggestions.IsNullOrEmpty()
                    ? _memberSearchSuggestionsQuery.GetMoreResultsSuggestions(searchList.Criteria)
                    : null;

                return !spellingSuggestions.IsNullOrEmpty() || !moreResultsSuggestions.IsNullOrEmpty()
                    ? new CandidateListRecoveryModel {SpellingSuggestions = spellingSuggestions, MoreResultsSuggestions = moreResultsSuggestions}
                    : null;
            }
            catch (Exception ex)
            {
                // Don't let a recovery failure cause the entire request to fail.

                EventSource.Raise(Event.Warning, method, "Cannot get the recovery for a search.", ex, new StandardErrorHandler());
                return null;
            }
        }

        private bool HasUnlimitedCredits(IEmployer employer)
        {
            if (employer == null)
                return false;

            var allocation = _employerCreditsQuery.GetEffectiveActiveAllocation<ContactCredit>(CurrentEmployer);
            return allocation.IsUnlimited;
        }

        private bool CanSearchByName(IEmployer employer)
        {
            return HasUnlimitedCredits(employer);
        }

        private bool CanSelectCommunity(IEmployer employer)
        {
            var communityId = ActivityContext.Community.Id;
            if (communityId == null)
                return true;

            var community = _communitiesQuery.GetCommunity(communityId.Value);
            if (community == null)
                return true;

            // If the community does not have any members then can.

            if (!community.HasMembers)
                return true;

            // If the community explicitly allows full search then can.

            if (community.OrganisationsCanSearchAllMembers)
                return true;

            // An anonymous employer cannot.

            if (employer == null)
                return false;

            // An employer with no affiliation to any community can.

            if (employer.Organisation.AffiliateId == null)
                return true;

            // An employer with an affiliation to this community cannot.

            return false;
        }

        private bool ShowHelpPrompt(IEmployer employer)
        {
            // Don't show it again if already shown.

            if (!EmployerContext.CanShowHelpPrompt || EmployerContext.ShownHelpPrompt)
                return false;

            var searchesPrompt = employer == null ? 5 : 10;
            var viewingsPrompt = employer == null ? 2 : 3;

            if (EmployerContext.Searches >= searchesPrompt || EmployerContext.Viewings >= viewingsPrompt)
            {
                EmployerContext.ShownHelpPrompt = true;
                return true;
            }

            return false;
        }
    }
}