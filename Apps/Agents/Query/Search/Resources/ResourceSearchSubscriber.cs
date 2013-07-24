using System;
using LinkMe.Domain.Resources;
using LinkMe.Framework.Utility.PublishedEvents;
using LinkMe.Framework.Utility.Wcf;
using LinkMe.Query.Resources;

namespace LinkMe.Apps.Agents.Query.Search.Resources
{
    public class ResourceSearchSubscriber
    {
        private readonly IChannelManager<IResourceSearchService> _serviceManager;
        private readonly IResourceSearchEngineCommand _resourceSearchEngineCommand;

        public ResourceSearchSubscriber(IChannelManager<IResourceSearchService> serviceManager, IResourceSearchEngineCommand resourceSearchEngineCommand)
        {
            _serviceManager = serviceManager;
            _resourceSearchEngineCommand = resourceSearchEngineCommand;
        }

        [SubscribesTo(PublishedEvents.ResourceViewed)]
        public void OnResourceViewedChanged(object sender, ResourceEventArgs e)
        {
            Update(e.ResourceId);
        }

        [SubscribesTo(PublishedEvents.FaqMarked)]
        public void OnFaqMarked(object sender, ResourceEventArgs e)
        {
            Update(e.ResourceId);
        }

        private void Update(Guid resourceItemId)
        {
            // Update all search engines.

            _resourceSearchEngineCommand.SetModified(resourceItemId);

            // Make sure the local one is updated now.

            var service = _serviceManager.Create();
            try
            {
                service.UpdateItem(resourceItemId);
            }
            catch (Exception)
            {
                _serviceManager.Abort(service);
                throw;
            }
            _serviceManager.Close(service);
        }
    }
}