using System;
using System.Collections.Generic;
using System.Linq;
using LinkMe.Apps.Asp.Mvc.Models;
using LinkMe.Apps.Presentation.Domain.Search;
using LinkMe.Domain;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Users.Employers.Views;
using LinkMe.Domain.Users.Employers.Views.Queries;
using LinkMe.Domain.Users.Members.Status.Queries;
using LinkMe.Domain.Users.Members.Views;
using LinkMe.Framework.Utility.Results;
using LinkMe.Query.Search.Members;
using LinkMe.Query.Search.Members.Commands;
using LinkMe.Web.Areas.Employers.Models;
using LinkMe.Web.Areas.Employers.Models.Search;

namespace LinkMe.Web.Areas.Employers.Controllers
{
    public abstract class CandidateListController
        : EmployersController
    {
        protected readonly IExecuteMemberSearchCommand _executeMemberSearchCommand;
        private readonly IEmployerMemberViewsQuery _employerMemberViewsQuery;
        private readonly IMemberStatusQuery _memberStatusQuery;

        protected static readonly RecencyModel[] Recencies = (from d in Reference.RecencyDays select new RecencyModel { Days = d, Description = new TimeSpan(d, 0, 0, 0).GetRecencyDisplayText() }).ToArray();

        protected enum SearchContext
        {
            NewSearch,
            Filter,
            Current,
            Saved,
        }

        protected CandidateListController(IExecuteMemberSearchCommand executeMemberSearchCommand, IEmployerMemberViewsQuery employerMemberViewsQuery, IMemberStatusQuery memberStatusQuery)
        {
            _executeMemberSearchCommand = executeMemberSearchCommand;
            _employerMemberViewsQuery = employerMemberViewsQuery;
            _memberStatusQuery = memberStatusQuery;
        }

        protected static CandidatesPresentationModel PreparePresentationModel(CandidatesPresentationModel model)
        {
            // Ensure that the pagination values are always set.

            if (model == null)
                model = new CandidatesPresentationModel();
            if (model.Pagination == null)
                model.Pagination = new Pagination();
            if (model.Pagination.Page == null)
                model.Pagination.Page = 1;
            if (model.Pagination.Items == null)
                model.Pagination.Items = Reference.DefaultItemsPerPage;
            model.ItemsPerPage = Reference.ItemsPerPage;
            model.DefaultItemsPerPage = Reference.DefaultItemsPerPage;

            return model;
        }

        protected TListModel Search<TListModel>(IEmployer employer, MemberSearchCriteria criteria, CandidatesPresentationModel presentation)
            where TListModel : CandidateListModel, new()
        {
            return Search<TListModel>(employer, presentation, (e, r) => _executeMemberSearchCommand.Search(e, criteria, r));
        }

        protected TListModel Search<TListModel>(IEmployer employer, CandidatesPresentationModel presentation, Func<IEmployer, Range, MemberSearchExecution> search)
            where TListModel : CandidateListModel, new()
        {
            presentation = PreparePresentationModel(presentation);

            // Search.

            var execution = search(employer, presentation.Pagination.ToRange());

            var model = new TListModel
            {
                Criteria = execution.Criteria,
                Presentation = presentation,
                Results = new CandidateListResultsModel
                {
                    TotalCandidates = execution.Results.TotalMatches,
                    CandidateIds = execution.Results.MemberIds,
                    CandidateStatusHits = GetEnumHitsDictionary(execution.Results.CandidateStatusHits, CandidateStatus.NotLooking),
                    IndustryHits = execution.Results.IndustryHits,
                    DesiredJobTypeHits = GetEnumHitsDictionary(execution.Results.DesiredJobTypeHits, JobTypes.All, JobTypes.None),
                },
            };

            // Only get details for those in the range.

            model.Results.Views = _employerMemberViewsQuery.GetEmployerMemberViews(employer, model.Results.CandidateIds);
            model.Results.LastUpdatedTimes = GetLastUpdatedTimes(model.Results.CandidateIds, model.Results.Views);
            return model;
        }

        private IDictionary<Guid, DateTime> GetLastUpdatedTimes(IEnumerable<Guid> candidateIds, ProfessionalViewCollection<EmployerMemberView> views)
        {
            return (from i in candidateIds
                    let view = views[i]
                    select new
                    {
                        id = i,
                        lastUpdatedTime = _memberStatusQuery.GetLastUpdatedTime(view, view, view.Resume)
                    }).ToDictionary(x => x.id, x => x.lastUpdatedTime);
        }

        protected TListModel CreateEmptyList<TListModel>(IEmployer employer, MemberSearchCriteria criteria, CandidatesPresentationModel presentation)
            where TListModel : CandidateListModel, new()
        {
            var model = new TListModel
            {
                Criteria = criteria,
                Presentation = presentation,
                Results = new CandidateListResultsModel
                {
                    TotalCandidates = 0,
                    CandidateIds = new List<Guid>(),
                    CandidateStatusHits = GetEnumHitsDictionary(new List<KeyValuePair<CandidateStatus, int>>(), CandidateStatus.NotLooking),
                    IndustryHits = new List<KeyValuePair<Guid, int>>(),
                    DesiredJobTypeHits = GetEnumHitsDictionary( new List<KeyValuePair<JobTypes, int>>(), JobTypes.All, JobTypes.None),
                },
            };

            // Only get details for those in the range.

            model.Results.Views = _employerMemberViewsQuery.GetEmployerMemberViews(employer, model.Results.CandidateIds);
            model.Results.LastUpdatedTimes = GetLastUpdatedTimes(model.Results.CandidateIds, model.Results.Views);
            return model;
        }

        private static IDictionary<T, int> GetEnumHitsDictionary<T>(IEnumerable<KeyValuePair<T, int>> hitList, params T[] ignore)
        {
            // The list may not have counts for all values so ensure they are set to 0.

            var hits = new Dictionary<T, int>();
            foreach (var value in Enum.GetValues(typeof(T)))
            {
                if (!ignore.Contains((T) value))
                    hits[(T) value] = 0;
            }

            foreach (var hit in hitList)
            {
                if (!ignore.Contains(hit.Key))
                    hits[hit.Key] = hit.Value;
            }

            return hits;
        }
    }
}
