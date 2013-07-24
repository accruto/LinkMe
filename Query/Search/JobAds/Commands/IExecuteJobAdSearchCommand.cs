using System;
using LinkMe.Domain.Contacts;
using LinkMe.Framework.Utility.Results;

namespace LinkMe.Query.Search.JobAds.Commands
{
    public interface IExecuteJobAdSearchCommand
    {
        JobAdSearchExecution Search(IMember member, JobAdSearchCriteria criteria, Range range);
        JobAdSearchExecution SearchFlagged(IMember member, JobAdSearchCriteria criteria, Range range);

        JobAdSearchExecution SearchSimilar(IMember member, Guid jobAdId, Range range);
        JobAdSearchExecution SearchSuggested(IMember member, TimeSpan? recency, Range range);

        bool IsSearchable(Guid jobAdId);
    }
}