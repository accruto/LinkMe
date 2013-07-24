using System;
using LinkMe.Domain.Contacts;
using LinkMe.Framework.Utility;
using LinkMe.Framework.Utility.Results;
using LinkMe.Framework.Utility.Wcf;
using LinkMe.Query.JobAds;

namespace LinkMe.Query.Search.JobAds.Commands
{
    public class ExecuteJobAdSearchCommand
        : IExecuteJobAdSearchCommand
    {
        private readonly IChannelManager<IJobAdSearchService> _serviceManager;

        public ExecuteJobAdSearchCommand(IChannelManager<IJobAdSearchService> serviceManager)
        {
            _serviceManager = serviceManager;
        }

        #region Implementation of IExecuteJobAdSearchCommand

        JobAdSearchExecution IExecuteJobAdSearchCommand.Search(IMember member, JobAdSearchCriteria criteria, Range range)
        {
            return Search(member, criteria, range, (s, m, q) => s.Search(m, q));
        }

        JobAdSearchExecution IExecuteJobAdSearchCommand.SearchFlagged(IMember member, JobAdSearchCriteria criteria, Range range)
        {
            return Search(member, criteria, range, (s, m, q) => s.SearchFlagged(m, q));
        }

        #endregion

        JobAdSearchExecution IExecuteJobAdSearchCommand.SearchSimilar(IMember member, Guid jobAdId, Range range)
        {
            var criteria = new JobAdSearchCriteria();
            return Search(member, criteria, range, (s, m, q) => s.SearchSimilar(m, jobAdId, q));
        }

        JobAdSearchExecution IExecuteJobAdSearchCommand.SearchSuggested(IMember member, TimeSpan? recency, Range range)
        {
            var criteria = new JobAdSearchCriteria
            {
                Recency = recency
            };
            return Search(member, criteria, range, (s, m, q) => s.SearchSuggested(m, q));
        }

        bool IExecuteJobAdSearchCommand.IsSearchable(Guid jobAdId)
        {
            bool isIndexed;
            var service = _serviceManager.Create();
            try
            {
                isIndexed = service.IsIndexed(jobAdId);
            }
            catch (Exception)
            {
                _serviceManager.Abort(service);
                throw;
            }
            _serviceManager.Close(service);

            return isIndexed;
        }

        private JobAdSearchExecution Search(IHasId<Guid> member, JobAdSearchCriteria criteria, Range range, Func<IJobAdSearchService, Guid?, JobAdSearchQuery, JobAdSearchResults> search)
        {
            JobAdSearchResults results;
            var service = _serviceManager.Create();
            try
            {
                results = search(service, member == null ? (Guid?)null : member.Id, criteria.GetSearchQuery(range));
            }
            catch (Exception)
            {
                _serviceManager.Abort(service);
                throw;
            }
            _serviceManager.Close(service);

            return new JobAdSearchExecution
            {
                Criteria = criteria,
                Results = results,
            };
        }
    }
}
