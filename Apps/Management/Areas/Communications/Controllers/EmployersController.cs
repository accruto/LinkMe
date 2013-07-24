using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using LinkMe.Apps.Agents.Security.Queries;
using LinkMe.Apps.Management.Areas.Communications.Models;
using LinkMe.Apps.Management.Areas.Communications.Models.Employers;
using LinkMe.Apps.Presentation.Domain.Users.Members.Search;
using LinkMe.Domain;
using LinkMe.Domain.Accounts.Queries;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Recruiters.Queries;
using LinkMe.Domain.Users.Employers.Queries;
using LinkMe.Domain.Users.Employers.Views;
using LinkMe.Framework.Utility.Linq;
using LinkMe.Framework.Utility.Results;
using LinkMe.Query.Members;
using LinkMe.Query.Reports.Roles.Candidates.Queries;
using LinkMe.Query.Reports.Search.Queries;
using LinkMe.Query.Reports.Users.Employers.Queries;
using LinkMe.Query.Search.Members;
using LinkMe.Query.Search.Members.Commands;
using LinkMe.Query.Search.Members.Queries;

namespace LinkMe.Apps.Management.Areas.Communications.Controllers
{
    public class EmployersController
        : CommunicationsController
    {
        private const int MaxPreviousSearches = 10;

        private readonly IEmployersQuery _employersQuery;
        private readonly ILoginCredentialsQuery _loginCredentialsQuery;
        private readonly ICandidateReportsQuery _candidateReportsQuery;
        private readonly IMemberSearchesQuery _memberSearchesQuery;
        private readonly IExecuteMemberSearchCommand _executeMemberSearchCommand;
        private readonly IOrganisationsQuery _organisationsQuery;
        private readonly IRecruitersQuery _recruitersQuery;
        private readonly IUserAccountsQuery _userAccountsQuery;
        private readonly IMemberSearchReportsQuery _memberSearchReportsQuery;
        private readonly IEmployerMemberAccessReportsQuery _employerMemberAccessReportsQuery;

        private static readonly string[] Keywords =
        {
            "Engineer",
            "Sales executive",
            "Software engineer",
            "Project manager",
            "General manager",
            "Receptionist",
            "Personal assistant",
            "Accountant",
            "Nurse",
            "Mechanic",
            "Forklift driver"
        };

        public EmployersController(IEmployersQuery employersQuery, ILoginCredentialsQuery loginCredentialsQuery, ICandidateReportsQuery candidateReportsQuery, IMemberSearchesQuery memberSearchesQuery, IExecuteMemberSearchCommand executeMemberSearchCommand, IOrganisationsQuery organisationsQuery, IRecruitersQuery recruitersQuery, IUserAccountsQuery userAccountsQuery, IMemberSearchReportsQuery memberSearchReportsQuery, IEmployerMemberAccessReportsQuery employerMemberAccessReportsQuery)
        {
            _employersQuery = employersQuery;
            _loginCredentialsQuery = loginCredentialsQuery;
            _candidateReportsQuery = candidateReportsQuery;
            _memberSearchesQuery = memberSearchesQuery;
            _executeMemberSearchCommand = executeMemberSearchCommand;
            _organisationsQuery = organisationsQuery;
            _recruitersQuery = recruitersQuery;
            _userAccountsQuery = userAccountsQuery;
            _memberSearchReportsQuery = memberSearchReportsQuery;
            _employerMemberAccessReportsQuery = employerMemberAccessReportsQuery;
        }

        public ActionResult Newsletter(CommunicationsContext context)
        {
            var employer = _employersQuery.GetEmployer(context.UserId);
            if (employer == null)
                return HttpNotFound();

            // Other enabled employers in the same organisation.

            var organisationHierarchy = _organisationsQuery.GetOrganisationHierarchy(employer.Organisation.Id);
            var employerIds = _recruitersQuery.GetRecruiters(organisationHierarchy);
            employerIds = _userAccountsQuery.GetEnabledAccountIds(employerIds);

            var lastLastLastMonth = new MonthRange(DateTime.Now.AddMonths(-3));
            var lastLastMonth = new MonthRange(DateTime.Now.AddMonths(-2));
            var lastMonth = new MonthRange(DateTime.Now.AddMonths(-1));

            var model = CreateModel<NewsletterModel>(context);
            model.LoginId = _loginCredentialsQuery.GetLoginId(employer.Id);
            model.Employer = employer;
            model.SampleSearches = GetCachedSampleSearches();
            model.PreviousSearches = GetPreviousSearches(employer, employerIds, lastMonth);
            model.Ranks = GetRanks(employer.Id, employerIds, lastLastLastMonth, lastLastMonth, lastMonth);
            return View(model);
        }

        public ActionResult IosLaunch(CommunicationsContext context)
        {
            return View(CreateModel<CommunicationsModel>(context));
        }

        private IList<NewsletterRankModel> GetRanks(Guid employerId, IEnumerable<Guid> employerIds, DateTimeRange lastLastLastMonth, DateTimeRange lastLastMonth, DateTimeRange lastMonth)
        {
            return new List<NewsletterRankModel>
            {
                GetSearches(employerId, employerIds, lastLastLastMonth, lastLastMonth, lastMonth),
                GetMemberViews(employerId, employerIds, lastLastLastMonth, lastLastMonth, lastMonth),
                GetMemberAccesses(employerId, employerIds, lastLastLastMonth, lastLastMonth, lastMonth),
                GetMemberSearchAlerts(employerId, employerIds),
            };
        }

        private NewsletterRankModel GetMemberSearchAlerts(Guid employerId, IEnumerable<Guid> employerIds)
        {
            var alerts = _memberSearchReportsQuery.GetMemberSearchAlerts(employerIds);

            return new NewsletterRankModel
            {
                Description = "Candidate alerts",
                PreviousMonths = new List<int>
                {
                    alerts[employerId],
                },
                TopInOrganisation = GetTopInOrganisation(alerts),
                Rank = GetRank(alerts, employerId),
            };
        }

        private NewsletterRankModel GetMemberAccesses(Guid employerId, IEnumerable<Guid> employerIds, DateTimeRange lastLastLastMonth, DateTimeRange lastLastMonth, DateTimeRange lastMonth)
        {
            var lastLastLastMonthContacts = _employerMemberAccessReportsQuery.GetEmployerAccesses(employerId, MemberAccessReason.PhoneNumberViewed, lastLastLastMonth)
                + _employerMemberAccessReportsQuery.GetEmployerAccesses(employerId, MemberAccessReason.MessageSent, lastLastLastMonth);

            var lastLastMonthContacts = _employerMemberAccessReportsQuery.GetEmployerAccesses(employerId, MemberAccessReason.PhoneNumberViewed, lastLastMonth)
                + _employerMemberAccessReportsQuery.GetEmployerAccesses(employerId, MemberAccessReason.MessageSent, lastLastMonth);

            var lastMonthContacts = Add(_employerMemberAccessReportsQuery.GetEmployerAccesses(employerIds, MemberAccessReason.PhoneNumberViewed, lastMonth),
                _employerMemberAccessReportsQuery.GetEmployerAccesses(employerIds, MemberAccessReason.MessageSent, lastMonth));

            return new NewsletterRankModel
            {
                Description = "Candidate contacts",
                PreviousMonths = new List<int>
                {
                    lastLastLastMonthContacts,
                    lastLastMonthContacts,
                    lastMonthContacts[employerId],
                },
                TopInOrganisation = GetTopInOrganisation(lastMonthContacts),
                Rank = GetRank(lastMonthContacts, employerId),
            };
        }

        private static IDictionary<Guid, int> Add(IEnumerable<KeyValuePair<Guid, int>> dictionary1, IEnumerable<KeyValuePair<Guid, int>> dictionary2)
        {
            var dictionary = dictionary1.ToDictionary(p => p.Key, p => p.Value);
            foreach (var pair in dictionary2)
            {
                if (dictionary.ContainsKey(pair.Key))
                    dictionary[pair.Key] = dictionary[pair.Key] + pair.Value;
                else
                    dictionary[pair.Key] = pair.Value;
            }

            return dictionary;
        }

        private NewsletterRankModel GetMemberViews(Guid employerId, IEnumerable<Guid> employerIds, DateTimeRange lastLastLastMonth, DateTimeRange lastLastMonth, DateTimeRange lastMonth)
        {
            var lastLastLastMonthViews = _employerMemberAccessReportsQuery.GetEmployerViewings(employerId, lastLastLastMonth);
            var lastLastMonthViews = _employerMemberAccessReportsQuery.GetEmployerViewings(employerId, lastLastMonth);
            var lastMonthViews = _employerMemberAccessReportsQuery.GetEmployerViewings(employerIds, lastMonth);

            return new NewsletterRankModel
            {
                Description = "Resume views",
                PreviousMonths = new List<int>
                {
                    lastLastLastMonthViews,
                    lastLastMonthViews,
                    lastMonthViews[employerId],
                },
                TopInOrganisation = GetTopInOrganisation(lastMonthViews),
                Rank = GetRank(lastMonthViews, employerId),
            };
        }

        private NewsletterRankModel GetSearches(Guid employerId, IEnumerable<Guid> employerIds, DateTimeRange lastLastLastMonth, DateTimeRange lastLastMonth, DateTimeRange lastMonth)
        {
            var lastLastLastMonthSearches = _memberSearchReportsQuery.GetMemberSearches(employerId, lastLastLastMonth);
            var lastLastMonthSearches = _memberSearchReportsQuery.GetMemberSearches(employerId, lastLastMonth);
            var lastMonthSearches = _memberSearchReportsQuery.GetMemberSearches(employerIds, lastMonth);

            return new NewsletterRankModel
            {
                Description = "Searches",
                PreviousMonths = new List<int>
                {
                    lastLastLastMonthSearches,
                    lastLastMonthSearches,
                    lastMonthSearches[employerId],
                },
                TopInOrganisation = GetTopInOrganisation(lastMonthSearches),
                Rank = GetRank(lastMonthSearches, employerId),
            };
        }

        private static int GetRank(IDictionary<Guid, int> data, Guid employerId)
        {
            // If they have done nothing then put them last.

            if (data[employerId] == 0)
                return data.Count;

            return data.OrderByDescending(p => p.Value).IndexOf(p => p.Key, employerId) + 1;
        }

        private static int GetTopInOrganisation(IDictionary<Guid, int> data)
        {
            return data.Values.Max();
        }

        private IList<NewsletterSearchModel> GetPreviousSearches(IEmployer employer, IEnumerable<Guid> employerIds, DateTimeRange lastMonth)
        {
            var searches = new List<NewsletterSearchModel>();

            // Start with saved searches.

            var memberSearches = _memberSearchesQuery.GetMemberSearches(new[] {employer.Id}, new Range(0, MaxPreviousSearches));
            AddPreviousSearches(searches, employer, from s in memberSearches select s.Criteria, lastMonth);

            // Previous searches.

            if (searches.Count < MaxPreviousSearches)
            {
                var previousSearches = _memberSearchesQuery.GetMemberSearchExecutions(new[] { employer.Id }, new Range(0, MaxPreviousSearches - searches.Count));
                AddPreviousSearches(searches, employer, from s in previousSearches select s.Criteria, lastMonth);
            }

            // Organisation searches.

            if (searches.Count < MaxPreviousSearches)
            {
                // Do not repeat the employer's searches.

                employerIds = employerIds.Except(new[] { employer.Id }).ToList();

                // Saved searches.

                memberSearches = _memberSearchesQuery.GetMemberSearches(employerIds, new Range(0, MaxPreviousSearches - searches.Count));
                AddPreviousSearches(searches, employer, from s in memberSearches select s.Criteria, lastMonth);

                // Previous searches.

                if (searches.Count < MaxPreviousSearches)
                {
                    var previousSearches = _memberSearchesQuery.GetMemberSearchExecutions(employerIds, new Range(0, MaxPreviousSearches - searches.Count));
                    AddPreviousSearches(searches, employer, from s in previousSearches select s.Criteria, lastMonth);
                }
            }

            return searches.Take(MaxPreviousSearches).ToList();
        }

        private void AddPreviousSearches(ICollection<NewsletterSearchModel> searches, IEmployer employer, IEnumerable<MemberSearchCriteria> criterias, DateTimeRange lastMonth)
        {
            foreach (var criteria in criterias)
            {
                var html = criteria.GetDisplayHtml();

                // Make sure the same search is not repeated.

                var found = false;
                foreach (var search in searches)
                {
                    if (search.Criteria == html)
                    {
                        found = true;
                        break;
                    }
                }

                if (!found)
                    AddSearch(searches, employer, criteria, lastMonth);
            }
        }

        private void AddSearch(ICollection<NewsletterSearchModel> searches, IEmployer employer, MemberSearchCriteria criteria, DateTimeRange lastMonth)
        {
            var results = GetSearchResults(employer, criteria.Clone());
            AddSearch(searches, criteria.GetDisplayHtml(), results, lastMonth);
        }

        private void AddSearch(ICollection<NewsletterSearchModel> searches, string criteria, ICollection<Guid> results, DateTimeRange lastMonth)
        {
            // Only add if there are in fact results.

            if (results.Count > 0)
                searches.Add(new NewsletterSearchModel
                {
                    Criteria = criteria,
                    Results = results.Count,
                    NewLastMonth = _userAccountsQuery.GetCreatedAccountIds(results, lastMonth).Count
                });
        }

        private void AddSearch(ICollection<NewsletterSearchModel> searches, string criteria, MemberSearchResults results, DateTimeRange lastMonth)
        {
            // Only add if there are in fact results.

            if (results.TotalMatches > 0)
                searches.Add(new NewsletterSearchModel
                {
                    Criteria = criteria,
                    Results = results.TotalMatches,
                    NewLastMonth = _userAccountsQuery.GetCreatedAccountIds(results.MemberIds, lastMonth).Count
                });
        }

        private IList<NewsletterSearchModel> GetCachedSampleSearches()
        {
            return GetCachedItem("SampleSearches", GetSampleSearches);
        }

        private IList<NewsletterSearchModel> GetSampleSearches()
        {
            var lastMonth = new MonthRange(DateTime.Now.AddMonths(-1));
            var availableNow = _candidateReportsQuery.GetCandidateStatuses(CandidateStatus.AvailableNow);
            var activelyLooking = _candidateReportsQuery.GetCandidateStatuses(CandidateStatus.ActivelyLooking);
            var indigenous = (_candidateReportsQuery.GetEthnicStatuses(EthnicStatus.Aboriginal).Concat(_candidateReportsQuery.GetEthnicStatuses(EthnicStatus.TorresIslander)).Distinct()).ToList();

            // Statuses.

            var searches = new List<NewsletterSearchModel>();
            AddSearch(searches, "Immediately available", availableNow, lastMonth);
            AddSearch(searches, "Actively looking", activelyLooking, lastMonth);
            AddSearch(searches, "Indigenous", indigenous, lastMonth);

            // Keywords.

            foreach (var keywords in Keywords)
            {
                var criteria = new MemberSearchCriteria();
                criteria.SetKeywords(keywords);
                AddSearch(searches, null, criteria, lastMonth);
            }

            return searches;
        }

        private MemberSearchResults GetSearchResults(IEmployer employer, MemberSearchCriteria criteria)
        {
            return _executeMemberSearchCommand.Search(employer, criteria, null).Results;
        }
    }
}
