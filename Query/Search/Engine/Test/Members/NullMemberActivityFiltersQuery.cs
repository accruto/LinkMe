using System;
using System.Collections.Generic;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Contenders;
using LinkMe.Query.Members;

namespace LinkMe.Query.Search.Engine.Test.Members
{
    class NullMemberActivityFiltersQuery
        : IMemberActivityFiltersQuery
    {
        #region Implementation of IMemberActivityFiltersQuery

        IList<Guid> IMemberActivityFiltersQuery.GetIncludeMemberIds(IEmployer employer, MemberSearchQuery query)
        {
            return null;
        }

        IList<Guid> IMemberActivityFiltersQuery.GetFolderIncludeMemberIds(IEmployer employer, Guid folderId, MemberSearchQuery query)
        {
            throw new NotImplementedException();
        }

        IList<Guid> IMemberActivityFiltersQuery.GetFlaggedIncludeMemberIds(IEmployer employer, MemberSearchQuery query)
        {
            throw new NotImplementedException();
        }

        IList<Guid> IMemberActivityFiltersQuery.GetBlockListIncludeMemberIds(IEmployer employer, Guid blockListId, MemberSearchQuery query)
        {
            throw new NotImplementedException();
        }

        IList<Guid> IMemberActivityFiltersQuery.GetSuggestedIncludeMemberIds(IEmployer employer, Guid jobAdId, MemberSearchQuery query)
        {
            throw new NotImplementedException();
        }

        IList<Guid> IMemberActivityFiltersQuery.GetManagedIncludeMemberIds(IEmployer employer, Guid jobAdId, ApplicantStatus status, MemberSearchQuery query)
        {
            throw new NotImplementedException();
        }

        IList<Guid> IMemberActivityFiltersQuery.GetExcludeMemberIds(IEmployer employer, MemberSearchQuery query)
        {
            return null;
        }

        IList<Guid> IMemberActivityFiltersQuery.GetFolderExcludeMemberIds(IEmployer employer, Guid folderId, MemberSearchQuery query)
        {
            throw new NotImplementedException();
        }

        IList<Guid> IMemberActivityFiltersQuery.GetFlaggedExcludeMemberIds(IEmployer employer, MemberSearchQuery query)
        {
            throw new NotImplementedException();
        }

        IList<Guid> IMemberActivityFiltersQuery.GetBlockListExcludeMemberIds(IEmployer employer, Guid blockListId, MemberSearchQuery query)
        {
            throw new NotImplementedException();
        }

        IList<Guid> IMemberActivityFiltersQuery.GetSuggestedExcludeMemberIds(IEmployer employer, Guid jobAdId, MemberSearchQuery query)
        {
            throw new NotImplementedException();
        }

        IList<Guid> IMemberActivityFiltersQuery.GetManagedExcludeMemberIds(IEmployer employer, Guid jobAdId, ApplicantStatus status, MemberSearchQuery query)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
