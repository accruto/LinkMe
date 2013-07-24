using System;
using LinkMe.Domain.Contacts;
using LinkMe.Framework.Utility;
using LinkMe.Framework.Utility.Results;
using LinkMe.Framework.Utility.Wcf;
using LinkMe.Query.JobAds;

namespace LinkMe.Query.Search.JobAds.Commands
{
    public class ExecuteJobAdSortCommand
        : IExecuteJobAdSortCommand
    {
        private readonly IChannelManager<IJobAdSortService> _serviceManager;

        public ExecuteJobAdSortCommand(IChannelManager<IJobAdSortService> serviceManager)
        {
            _serviceManager = serviceManager;
        }

        #region Implementation of IExecuteJobAdSortCommand

        JobAdSortExecution IExecuteJobAdSortCommand.SortFolder(IMember member, Guid folderId, JobAdSearchSortCriteria sortCriteria, Range range)
        {
            return Sort(member, sortCriteria, range, (s, m, q) => s.SortFolder(m, folderId, q));
        }

        JobAdSortExecution IExecuteJobAdSortCommand.SortFlagged(IMember member, JobAdSearchSortCriteria sortCriteria, Range range)
        {
            return Sort(member, sortCriteria, range, (s, m, q) => s.SortFlagged(m, q));
        }

        JobAdSortExecution IExecuteJobAdSortCommand.SortBlocked(IMember member, JobAdSearchSortCriteria sortCriteria, Range range)
        {
            return Sort(member, sortCriteria, range, (s, m, q) => s.SortBlocked(m, q));
        }

        private JobAdSortExecution Sort(IHasId<Guid> member, JobAdSearchSortCriteria sortCriteria, Range range, Func<IJobAdSortService, Guid?, JobAdSortQuery, JobAdSearchResults> sort)
        {
            var criteria = new JobAdSortCriteria { SortCriteria = sortCriteria };
            
            JobAdSearchResults results;
            var service = _serviceManager.Create();
            try
            {
                results = sort(service, member == null ? (Guid?) null : member.Id, criteria.GetSortQuery(range));
            }
            catch (Exception)
            {
                _serviceManager.Abort(service);
                throw;
            }
            _serviceManager.Close(service);

            return new JobAdSortExecution
            {
                Criteria = criteria,
                Results = results,
            };
        }

        #endregion
    }
}
