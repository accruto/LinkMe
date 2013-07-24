using System;
using System.Collections.Generic;
using LinkMe.Domain.Contacts;

namespace LinkMe.Domain.Users.Members.Views
{
    public interface IMemberViewsRepository
    {
        PersonalViews GetPersonalViews(Guid fromId, IEnumerable<Guid> toIds, DateTime minLastSentTime);
        PersonalViews GetPersonalViews(Guid fromId, IEnumerable<Member> tos, DateTime minLastSentTime);
    }
}