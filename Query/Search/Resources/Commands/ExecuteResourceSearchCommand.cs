using System;
using LinkMe.Framework.Utility.Results;
using LinkMe.Framework.Utility.Wcf;
using LinkMe.Query.Resources;

namespace LinkMe.Query.Search.Resources.Commands
{
    public class ExecuteResourceSearchCommand
        : IExecuteResourceSearchCommand
    {
        private readonly IChannelManager<IResourceSearchService> _serviceManager;

        public ExecuteResourceSearchCommand(IChannelManager<IResourceSearchService> serviceManager)
        {
            _serviceManager = serviceManager;
        }

        #region Implementation of IExecuteResourceSearchCommand

        /// <summary>
        /// Execute a search on the Lucene resource index
        /// </summary>
        /// <param name="criteria">The criteria the search results need to meet</param>
        /// <param name="range">The number of results to return. Null means include all resources</param>
        /// <returns>A list of resourceItemIds meeting the criteria</returns>
        ResourceSearchExecution IExecuteResourceSearchCommand.Search(ResourceSearchCriteria criteria, Range range)
        {
            ResourceSearchResults results;
            var service = _serviceManager.Create();
            try
            {
                results = service.Search(criteria.GetSearchQuery(range), true);
            }
            catch (Exception)
            {
                _serviceManager.Abort(service);
                throw;
            }
            _serviceManager.Close(service);

            return new ResourceSearchExecution
            {
                Criteria = criteria,
                Results = results,
            };
        }
        
        #endregion
    }
}
