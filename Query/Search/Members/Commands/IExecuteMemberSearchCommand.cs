using System;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Contenders;
using LinkMe.Framework.Utility.Results;

namespace LinkMe.Query.Search.Members.Commands
{
    public interface IExecuteMemberSearchCommand
    {
        MemberSearchExecution Search(IEmployer employer, MemberSearchCriteria criteria, Range range);

        MemberSearchExecution SearchFolder(IEmployer employer, Guid folderId, MemberSearchSortCriteria sortCriteria, Range range);
        MemberSearchExecution SearchBlockList(IEmployer employer, Guid blockListId, MemberSearchSortCriteria sortCriteria, Range range);

        MemberSearchExecution SearchFlagged(IEmployer employer, MemberSearchSortCriteria sortCriteria, Range range);
        MemberSearchExecution SearchFlagged(IEmployer employer, MemberSearchCriteria criteria, Range range);

        MemberSearchExecution SearchSuggested(IEmployer employer, Guid jobAdId, MemberSearchCriteria criteria, Range range);
        MemberSearchExecution SearchManaged(IEmployer employer, Guid jobAdId, ApplicantStatus status, MemberSearchSortCriteria sortCriteria, Range range);

        bool IsSearchable(Guid memberId);
    }
}