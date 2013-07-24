using System.Collections.Generic;
using System.Reflection;
using Microsoft.Practices.ObjectBuilder2;

namespace LinkMe.Framework.Utility.PublishedEvents.Unity
{
    /// <summary>
    /// This policy interface allows access to saved publication and
    /// subscription information.
    /// </summary>
    internal interface IEventBrokerInfoPolicy
        : IBuilderPolicy
    {
        IEnumerable<PublicationInfo> Publications { get;}
        IEnumerable<SubscriptionInfo> Subscriptions { get; }
    }

    internal struct PublicationInfo
    {
        public string PublishedEventName { get; private set; }
        public EventInfo Publisher { get; private set; }

        public PublicationInfo(string publishedEventName, EventInfo publisher)
            : this()
        {
            PublishedEventName = publishedEventName;
            Publisher = publisher;
        }
    }

    internal struct SubscriptionInfo
    {
        public string PublishedEventName { get; private set; }
        public MethodInfo Subscriber { get; private set; }

        public SubscriptionInfo(string publishedEventName, MethodInfo subscriber)
            : this()
        {
            PublishedEventName = publishedEventName;
            Subscriber = subscriber;
        }
    }
}
