using System;
using System.Collections.Generic;
using LinkMe.Domain.Contacts;

namespace LinkMe.Domain.Users.Members.Views.Queries
{
    public interface IMemberViewsQuery
    {
        PersonalView GetPersonalView(Guid? fromId, Guid toId);
        PersonalView GetPersonalView(Guid? fromId, Member toMember);
        PersonalViews GetPersonalViews(Guid? fromId, IEnumerable<Guid> toIds);
        PersonalViews GetPersonalViews(Guid? fromId, IEnumerable<Member> toMembers);
    }
}