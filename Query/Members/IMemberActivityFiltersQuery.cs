using System;
using System.Collections.Generic;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Contenders;

namespace LinkMe.Query.Members
{
    public interface IMemberActivityFiltersQuery
    {
        IList<Guid> GetIncludeMemberIds(IEmployer employer, MemberSearchQuery query);
        IList<Guid> GetFolderIncludeMemberIds(IEmployer employer, Guid folderId, MemberSearchQuery query);
        IList<Guid> GetFlaggedIncludeMemberIds(IEmployer employer, MemberSearchQuery query);
        IList<Guid> GetBlockListIncludeMemberIds(IEmployer employer, Guid blockListId, MemberSearchQuery query);
        IList<Guid> GetSuggestedIncludeMemberIds(IEmployer employer, Guid jobAdId, MemberSearchQuery query);
        IList<Guid> GetManagedIncludeMemberIds(IEmployer employer, Guid jobAdId, ApplicantStatus status, MemberSearchQuery query);

        IList<Guid> GetExcludeMemberIds(IEmployer employer, MemberSearchQuery query);
        IList<Guid> GetFolderExcludeMemberIds(IEmployer employer, Guid folderId, MemberSearchQuery query);
        IList<Guid> GetFlaggedExcludeMemberIds(IEmployer employer, MemberSearchQuery query);
        IList<Guid> GetBlockListExcludeMemberIds(IEmployer employer, Guid blockListId, MemberSearchQuery query);
        IList<Guid> GetSuggestedExcludeMemberIds(IEmployer employer, Guid jobAdId, MemberSearchQuery query);
        IList<Guid> GetManagedExcludeMemberIds(IEmployer employer, Guid jobAdId, ApplicantStatus status, MemberSearchQuery query);
    }
}