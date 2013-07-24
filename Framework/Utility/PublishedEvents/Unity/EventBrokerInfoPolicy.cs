using System.Collections.Generic;
using System.Reflection;

namespace LinkMe.Framework.Utility.PublishedEvents.Unity
{
    internal class EventBrokerInfoPolicy
        : IEventBrokerInfoPolicy
    {
        private readonly List<PublicationInfo> _publications = new List<PublicationInfo>();
        private readonly List<SubscriptionInfo> _subscriptions = new List<SubscriptionInfo>();

        public void AddPublication(string publishedEventName, EventInfo publisher)
        {
            _publications.Add(new PublicationInfo(publishedEventName, publisher));
        }

        public void AddSubscription(string publishedEventName, MethodInfo subscriber)
        {
            _subscriptions.Add(new SubscriptionInfo(publishedEventName, subscriber));
        }

        public IEnumerable<PublicationInfo> Publications
        {
            get { return _publications; }
        }

        public IEnumerable<SubscriptionInfo> Subscriptions
        {
            get { return _subscriptions; }
        }
    }
}
