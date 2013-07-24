using System;
using LinkMe.Domain.Contacts;
using LinkMe.Framework.Utility.Results;

namespace LinkMe.Query.Search.JobAds.Commands
{
    public interface IExecuteJobAdSortCommand
    {
        JobAdSortExecution SortFolder(IMember member, Guid folderId, JobAdSearchSortCriteria criteria, Range range);
        JobAdSortExecution SortFlagged(IMember member, JobAdSearchSortCriteria criteria, Range range);
        JobAdSortExecution SortBlocked(IMember member, JobAdSearchSortCriteria criteria, Range range);
    }
}