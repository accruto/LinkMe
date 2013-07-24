using System;
using System.Collections.Generic;
using LinkMe.Domain;
using LinkMe.Domain.Channels;
using LinkMe.Domain.Users.Employers.Views;

namespace LinkMe.Query.Reports.Users.Employers
{
    public interface IEmployerMemberAccessReportsRepository
    {
        // Total viewings.

        int GetMemberViewings(Channel channel, DateTimeRange range);
        int GetDistinctMemberViewings(Channel channel, DateTimeRange timeRange);
        int GetAnonymousMemberViewings(Channel channel, DateTimeRange timeRange);

        // Employer viewings.

        int GetEmployerViewings(Guid employerId, DateTimeRange timeRange);
        IDictionary<Guid, int> GetEmployerViewings(IEnumerable<Guid> employerIds, DateTimeRange timeRange);

        // Member viewings.

        int GetMemberViewings(Guid memberId, DateTimeRange timeRange);

        // Total accesses.

        int GetMemberAccesses(DateTimeRange timeRange);
        int GetMemberAccesses(Channel channel, DateTimeRange timeRange);
        int GetDistinctMemberAccesses(Channel channel, DateTimeRange timeRange);
        int GetMemberAccesses(MemberAccessReason reason, Channel channel, DateTimeRange timeRange);

        // Employer accesses.

        int GetEmployerAccesses(Guid employerId, MemberAccessReason reason, DateTimeRange timeRange);
        IDictionary<Guid, int> GetEmployerAccesses(IEnumerable<Guid> employerIds, MemberAccessReason reason, DateTimeRange timeRange);

        // Member accesses.

        int GetMemberAccesses(Guid memberId);
        int GetMemberAccesses(Guid memberId, MemberAccessReason reason);
        int GetMemberSearchResults(Guid memberId, int rank);
    }
}