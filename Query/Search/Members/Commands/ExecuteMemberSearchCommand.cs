using System;
using LinkMe.Domain;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Contenders;
using LinkMe.Framework.Instrumentation;
using LinkMe.Framework.Utility.Results;
using LinkMe.Framework.Utility.Wcf;
using LinkMe.Query.Members;

namespace LinkMe.Query.Search.Members.Commands
{
    public class ExecuteMemberSearchCommand
        : IExecuteMemberSearchCommand
    {
        private readonly IChannelManager<IMemberSearchService> _serviceManager;
        private static readonly EventSource EventSource = new EventSource<ExecuteMemberSearchCommand>();

        private const MemberSortOrder FolderDefaultSortOrder = MemberSortOrder.DateUpdated;
        private const MemberSortOrder FlagListDefaultSortOrder = MemberSortOrder.DateUpdated;
        private const MemberSortOrder BlockListDefaultSortOrder = MemberSortOrder.DateUpdated;
        private const MemberSortOrder ManagedCandidatesDefaultSortOrder = MemberSortOrder.DisplayName;

        public ExecuteMemberSearchCommand(IChannelManager<IMemberSearchService> serviceManager)
        {
            _serviceManager = serviceManager;
        }

        #region Implementation of IExecuteMemberSearchCommand

        MemberSearchExecution IExecuteMemberSearchCommand.Search(IEmployer employer, MemberSearchCriteria criteria, Range range)
        {
            return Search(employer, criteria, range, (s, e, o, q) => s.Search(e, o, q));
        }

        MemberSearchExecution IExecuteMemberSearchCommand.SearchFolder(IEmployer employer, Guid folderId, MemberSearchSortCriteria sortCriteria, Range range)
        {
            var criteria = new MemberSearchCriteria
            {
                SortCriteria = sortCriteria ?? new MemberSearchSortCriteria { SortOrder = FolderDefaultSortOrder },
                CandidateStatusFlags = null,
            };

            return Search(employer, criteria, range, (s, e, o, q) => s.SearchFolder(e, o, folderId, q));
        }

        MemberSearchExecution IExecuteMemberSearchCommand.SearchFlagged(IEmployer employer, MemberSearchSortCriteria sortCriteria, Range range)
        {
            var criteria = new MemberSearchCriteria
            {
                SortCriteria = sortCriteria ?? new MemberSearchSortCriteria { SortOrder = FlagListDefaultSortOrder },
            };

            return Search(employer, criteria, range, (s, e, o, q) => s.SearchFlagged(e, o, q));
        }

        MemberSearchExecution IExecuteMemberSearchCommand.SearchFlagged(IEmployer employer, MemberSearchCriteria criteria, Range range)
        {
            return Search(employer, criteria, range, (s, e, o, q) => s.SearchFlagged(e, o, q));
        }

        MemberSearchExecution IExecuteMemberSearchCommand.SearchBlockList(IEmployer employer, Guid blockListId, MemberSearchSortCriteria sortCriteria, Range range)
        {
            var criteria = new MemberSearchCriteria
            {
                SortCriteria = sortCriteria ?? new MemberSearchSortCriteria { SortOrder = BlockListDefaultSortOrder },
                HasResume = null,
                CandidateStatusFlags = CandidateStatusFlags.All,
            };

            return Search(employer, criteria, range, (s, e, o, q) => s.SearchBlockList(e, o, blockListId, q));
        }

        MemberSearchExecution IExecuteMemberSearchCommand.SearchSuggested(IEmployer employer, Guid jobAdId, MemberSearchCriteria criteria, Range range)
        {
            return Search(employer, criteria, range, (s, e, o, q) => s.SearchSuggested(e, o, jobAdId, q));
        }

        MemberSearchExecution IExecuteMemberSearchCommand.SearchManaged(IEmployer employer, Guid jobAdId, ApplicantStatus status, MemberSearchSortCriteria sortCriteria, Range range)
        {
            var criteria = new MemberSearchCriteria
            {
                SortCriteria = sortCriteria ?? new MemberSearchSortCriteria { SortOrder = ManagedCandidatesDefaultSortOrder },
                HasResume = null,
                IsActivated = null,
                IsContactable = null,
                CandidateStatusFlags = CandidateStatusFlags.All,
            };

            return Search(employer, criteria, range, (s, e, o, q) => s.SearchManaged(e, o, jobAdId, status, q));
        }

        bool IExecuteMemberSearchCommand.IsSearchable(Guid memberId)
        {
            bool isIndexed;
            var service = _serviceManager.Create();
            try
            {
                isIndexed = service.IsIndexed(memberId);
            }
            catch (Exception)
            {
                _serviceManager.Abort(service);
                throw;
            }
            _serviceManager.Close(service);

            return isIndexed;
        }

        #endregion

        private MemberSearchExecution Search(IEmployer employer, MemberSearchCriteria criteria, Range range, Func<IMemberSearchService, Guid?, Guid?, MemberSearchQuery, MemberSearchResults> search)
        {
            const string method = "Search";

            MemberSearchResults results;
            var service = _serviceManager.Create();
            try
            {
                results = search(service, employer == null ? (Guid?)null : employer.Id, employer == null ? (Guid?)null : employer.Organisation.Id, criteria.GetSearchQuery(range));

                if (EventSource.IsEnabled(Event.Trace))
                    EventSource.Raise(Event.Trace, method, "Results retrieved.", Event.Arg("total hits", results.TotalMatches), Event.Arg("result count", results.MemberIds.Count));
            }
            catch (Exception)
            {
                _serviceManager.Abort(service);
                throw;
            }
            _serviceManager.Close(service);

            return new MemberSearchExecution
            {
                Criteria = criteria,
                Results = results,
            };
        }
    }
}