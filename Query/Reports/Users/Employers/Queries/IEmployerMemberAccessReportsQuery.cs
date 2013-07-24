using System;
using System.Collections.Generic;
using LinkMe.Domain;
using LinkMe.Domain.Channels;
using LinkMe.Domain.Users.Employers.Views;

namespace LinkMe.Query.Reports.Users.Employers.Queries
{
    public interface IEmployerMemberAccessReportsQuery
    {
        int GetEmployerViewings(Guid employerId, DateTimeRange timeRange);
        IDictionary<Guid, int> GetEmployerViewings(IEnumerable<Guid> employerIds, DateTimeRange timeRange);
        int GetMemberViewings(Guid memberId, DateTimeRange timeRange);

        EmployerMemberViewingReport GetEmployerMemberViewingReport(Channel channel, DateTimeRange timeRange);

        int GetMemberAccesses(DateTimeRange timeRange);
        int GetEmployerAccesses(Guid employerId, MemberAccessReason reason, DateTimeRange timeRange);
        IDictionary<Guid, int> GetEmployerAccesses(IEnumerable<Guid> employerIds, MemberAccessReason reason, DateTimeRange timeRange);

        EmployerMemberAccessReport GetEmployerMemberAccessReport(Channel channel, DateTimeRange timeRange);

        int GetMemberAccesses(Guid memberId);
        int GetMemberAccesses(Guid memberId, MemberAccessReason reason);
        MemberAccessReport GetMemberAccessReport(Guid memberId);
    }
}