using System;
using Microsoft.Practices.ObjectBuilder2;

namespace LinkMe.Framework.Utility.PublishedEvents.Unity
{
    public class EventBrokerWireupStrategy
        : BuilderStrategy
    {
        public override void PreBuildUp(IBuilderContext context)
        {
            if (context.Existing != null)
            {
                var policy = context.Policies.Get<IEventBrokerInfoPolicy>(context.BuildKey);

                if (policy != null)
                {
                    var broker = GetBroker(context);

                    foreach (var pub in policy.Publications)
                        broker.RegisterPublisher(pub.PublishedEventName, context.Existing, pub.Publisher);

                    foreach (var sub in policy.Subscriptions)
                        broker.RegisterSubscriber(sub.PublishedEventName, context.Existing, sub.Subscriber);
                }
            }
        }

        private static EventBroker GetBroker(IBuilderContext context)
        {
            var broker = context.Locator.Get<EventBroker>(typeof(EventBroker));
            if (broker == null)
                throw new InvalidOperationException("No event broker available");
            return broker;
        }
    }
}
