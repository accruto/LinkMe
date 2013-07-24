using System;
using System.Collections.Generic;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Users.Members.Views;

namespace LinkMe.Domain.Users.Employers.Views.Queries
{
    public interface IEmployerMemberViewsQuery
    {
        ProfessionalView GetProfessionalView(IEmployer employer, Guid memberId);
        ProfessionalView GetProfessionalView(IEmployer employer, Member member);
        ProfessionalViews GetProfessionalViews(IEmployer employer, IEnumerable<Guid> memberIds);
        ProfessionalViews GetProfessionalViews(IEmployer employer, IEnumerable<Member> members);

        EmployerMemberView GetEmployerMemberView(IEmployer employer, Guid memberId);
        EmployerMemberView GetEmployerMemberView(IEmployer employer, Member member);
        EmployerMemberViews GetEmployerMemberViews(IEmployer employer, IEnumerable<Guid> memberIds);
        EmployerMemberViews GetEmployerMemberViews(IEmployer employer, IEnumerable<Member> members);

        bool HasViewedMember(IEmployer employer, Guid memberId);
        IList<Guid> GetViewedMemberIds(IEmployer employer);

        CanContactStatus CanContact(IEmployer employer, Member member);

        bool HasAccessedMember(IEmployer employer, Guid memberId);
        IList<Guid> GetAccessedMemberIds(IEmployer employer);
        IList<MemberAccess> GetMemberAccesses(IEmployer employer, Guid memberId);
    }
}