using System;
using System.Collections.Generic;
using LinkMe.Domain;
using LinkMe.Domain.Channels;
using LinkMe.Domain.Users.Employers.Views;

namespace LinkMe.Query.Reports.Users.Employers.Queries
{
    public class EmployerMemberAccessReportsQuery
        : IEmployerMemberAccessReportsQuery
    {
        private readonly IEmployerMemberAccessReportsRepository _repository;

        public EmployerMemberAccessReportsQuery(IEmployerMemberAccessReportsRepository repository)
        {
            _repository = repository;
        }

        int IEmployerMemberAccessReportsQuery.GetEmployerViewings(Guid employerId, DateTimeRange timeRange)
        {
            return _repository.GetEmployerViewings(employerId, timeRange);
        }

        IDictionary<Guid, int> IEmployerMemberAccessReportsQuery.GetEmployerViewings(IEnumerable<Guid> employerIds, DateTimeRange timeRange)
        {
            return _repository.GetEmployerViewings(employerIds, timeRange);
        }

        int IEmployerMemberAccessReportsQuery.GetMemberViewings(Guid memberId, DateTimeRange timeRange)
        {
            return _repository.GetMemberViewings(memberId, timeRange);
        }

        EmployerMemberViewingReport IEmployerMemberAccessReportsQuery.GetEmployerMemberViewingReport(Channel channel, DateTimeRange timeRange)
        {
            return new EmployerMemberViewingReport
                       {
                           TotalViewings = _repository.GetMemberViewings(channel, timeRange),
                           DistinctViewings = _repository.GetDistinctMemberViewings(channel, timeRange),
                           AnonymousViewings = _repository.GetAnonymousMemberViewings(channel, timeRange),
                       };
        }

        int IEmployerMemberAccessReportsQuery.GetMemberAccesses(DateTimeRange timeRange)
        {
            return _repository.GetMemberAccesses(timeRange);
        }

        int IEmployerMemberAccessReportsQuery.GetEmployerAccesses(Guid employerId, MemberAccessReason reason, DateTimeRange timeRange)
        {
            return _repository.GetEmployerAccesses(employerId, reason, timeRange);
        }

        IDictionary<Guid, int> IEmployerMemberAccessReportsQuery.GetEmployerAccesses(IEnumerable<Guid> employerIds, MemberAccessReason reason, DateTimeRange timeRange)
        {
            return _repository.GetEmployerAccesses(employerIds, reason, timeRange);
        }

        EmployerMemberAccessReport IEmployerMemberAccessReportsQuery.GetEmployerMemberAccessReport(Channel channel, DateTimeRange timeRange)
        {
            return new EmployerMemberAccessReport
                       {
                           TotalAccesses = _repository.GetMemberAccesses(channel, timeRange),
                           DistinctAccesses = _repository.GetDistinctMemberAccesses(channel, timeRange),
                           MessagesSent = _repository.GetMemberAccesses(MemberAccessReason.MessageSent, channel, timeRange),
                           PhoneNumbersViewed = _repository.GetMemberAccesses(MemberAccessReason.PhoneNumberViewed, channel, timeRange),
                           ResumesDownloaded = _repository.GetMemberAccesses(MemberAccessReason.ResumeDownloaded, channel, timeRange),
                           ResumesSent = _repository.GetMemberAccesses(MemberAccessReason.ResumeSent, channel, timeRange),
                           Unlockings = _repository.GetMemberAccesses(MemberAccessReason.Unlock, channel, timeRange),
                       };
        }

        int IEmployerMemberAccessReportsQuery.GetMemberAccesses(Guid memberId)
        {
            return _repository.GetMemberAccesses(memberId);
        }

        int IEmployerMemberAccessReportsQuery.GetMemberAccesses(Guid memberId, MemberAccessReason reason)
        {
            return _repository.GetMemberAccesses(memberId, reason);
        }

        MemberAccessReport IEmployerMemberAccessReportsQuery.GetMemberAccessReport(Guid memberId)
        {
            return new MemberAccessReport
                       {
                           Top10SearchResults = _repository.GetMemberSearchResults(memberId, 10),
                           Viewed = _repository.GetMemberViewings(memberId, null),
                           EmailsSent = _repository.GetMemberAccesses(memberId, MemberAccessReason.MessageSent),
                           PhoneNumberViewed = _repository.GetMemberAccesses(memberId, MemberAccessReason.PhoneNumberViewed),
                       };
        }
    }
}