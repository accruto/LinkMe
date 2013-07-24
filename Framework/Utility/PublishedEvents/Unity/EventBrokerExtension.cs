using LinkMe.Framework.Utility.Unity;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.ObjectBuilder;

namespace LinkMe.Framework.Utility.PublishedEvents.Unity
{
    public class EventBrokerExtension
        : UnityContainerExtension
    {
        private readonly EventBroker _broker;

        public EventBrokerExtension(IContainerEventSource eventSource)
        {
            _broker = new EventBroker(eventSource);
        }

        protected override void Initialize()
        {
            // Only add if not already there.

            if (Context.Locator.Get(typeof(EventBroker)) == null)
            {
                Context.Locator.Add(typeof (EventBroker), _broker);
                Context.Strategies.AddNew<EventBrokerReflectionStrategy>(UnityBuildStage.PreCreation);
                Context.Strategies.AddNew<EventBrokerWireupStrategy>(UnityBuildStage.Initialization);
            }
        }
    }
}
