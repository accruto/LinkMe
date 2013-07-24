using System;
using System.Collections.Generic;

namespace LinkMe.Domain.Users.Employers.Views
{
    public interface IEmployerViewsRepository
    {
        void CreateMemberViewing(MemberViewing viewing);

        bool HasViewedMember(Guid employerId, Guid memberId);
        IList<Guid> GetViewedMemberIds(Guid employerId);
        IList<Guid> GetViewedMemberIds(Guid employerId, IEnumerable<Guid> memberIds);
        IList<MemberViewing> GetMemberViewings(Guid employerId, Guid memberId);

        void CreateMemberAccess(MemberAccess access);
        void CreateMemberAccesses(IEnumerable<MemberAccess> accesses);

        bool HasAccessedMember(Guid employerId, Guid memberId);
        IList<Guid> GetAccessedMemberIds(Guid employerId);
        IList<Guid> GetAccessedMemberIds(Guid employerId, IEnumerable<Guid> memberIds);
        IList<MemberAccess> GetMemberAccesses(Guid employerId, Guid memberId);

        Tuple<int, int> GetMemberAccessCounts(Guid employerId, Guid excludeMemberId, IEnumerable<MemberAccessReason> reasons);
        Tuple<int, int> GetMemberAccessCounts(Guid employerId, IEnumerable<Guid> excludeMemberIds, IEnumerable<MemberAccessReason> reasons);
    }
}