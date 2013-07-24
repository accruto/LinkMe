using System;
using LinkMe.Framework.Utility.Results;
using LinkMe.Framework.Utility.Wcf;
using LinkMe.Query.Resources;

namespace LinkMe.Query.Search.Resources.Commands
{
    public class ExecuteFaqSearchCommand
        : IExecuteFaqSearchCommand
    {
        private readonly IChannelManager<IResourceSearchService> _serviceManager;

        public ExecuteFaqSearchCommand(IChannelManager<IResourceSearchService> serviceManager)
        {
            _serviceManager = serviceManager;
        }

        #region Implementation of IExecuteResourceSearchCommand

        FaqSearchExecution IExecuteFaqSearchCommand.Search(FaqSearchCriteria criteria, Range range)
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

            return new FaqSearchExecution
            {
                Criteria = criteria,
                Results = results
            };
        }
        #endregion
    }
}
