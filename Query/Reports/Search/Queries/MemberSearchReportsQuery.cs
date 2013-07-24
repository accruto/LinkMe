using System;
using System.Collections.Generic;
using LinkMe.Domain;
using LinkMe.Domain.Channels;

namespace LinkMe.Query.Reports.Search.Queries
{
    public class MemberSearchReportsQuery
        : IMemberSearchReportsQuery
    {
        private readonly IMemberSearchReportsRepository _repository;

        public MemberSearchReportsQuery(IMemberSearchReportsRepository repository)
        {
            _repository = repository;
        }

        int IMemberSearchReportsQuery.GetAllMemberSearches(DateTimeRange timeRange)
        {
            return _repository.GetAllMemberSearches(timeRange);
        }

        int IMemberSearchReportsQuery.GetMemberSearches(Channel channel, DateTimeRange timeRange)
        {
            return _repository.GetMemberSearches(channel, timeRange);
        }

        int IMemberSearchReportsQuery.GetFilterMemberSearches(Channel channel, DateTimeRange timeRange)
        {
            return _repository.GetFilterMemberSearches(channel, timeRange);
        }

        int IMemberSearchReportsQuery.GetSavedMemberSearches(Channel channel, DateTimeRange timeRange)
        {
            return _repository.GetSavedMemberSearches(channel, timeRange);
        }

        int IMemberSearchReportsQuery.GetAnonymousMemberSearches(Channel channel, DateTimeRange timeRange)
        {
            return _repository.GetAnonymousMemberSearches(channel, timeRange);
        }

        int IMemberSearchReportsQuery.GetMemberSearches(Guid searcherId, DateTimeRange timeRange)
        {
            return _repository.GetMemberSearches(searcherId, timeRange);
        }

        IDictionary<Guid, int> IMemberSearchReportsQuery.GetMemberSearches(IEnumerable<Guid> searcherIds, DateTimeRange timeRange)
        {
            return _repository.GetMemberSearches(searcherIds, timeRange);
        }

        int IMemberSearchReportsQuery.GetMemberSearchAlerts()
        {
            return _repository.GetMemberSearchAlerts();
        }

        int IMemberSearchReportsQuery.GetMemberSearchAlerts(Guid searcherId)
        {
            return _repository.GetMemberSearchAlerts(searcherId);
        }

        IDictionary<Guid, int> IMemberSearchReportsQuery.GetMemberSearchAlerts(IEnumerable<Guid> searcherIds)
        {
            return _repository.GetMemberSearchAlerts(searcherIds);
        }
    }
}