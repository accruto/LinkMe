using Microsoft.Practices.ObjectBuilder2;

namespace LinkMe.Framework.Utility.PublishedEvents.Unity
{
    public class EventBrokerReflectionStrategy
        : BuilderStrategy
    {
        public override void PreBuildUp(IBuilderContext context)
        {
            if (context.Policies.Get<IEventBrokerInfoPolicy>(context.BuildKey) == null)
            {
                var policy = new EventBrokerInfoPolicy();
                context.Policies.Set<IEventBrokerInfoPolicy>(policy, context.BuildKey);

                AddPublicationsToPolicy(context.BuildKey, policy);
                AddSubscriptionsToPolicy(context.BuildKey, policy);
            }
        }

        private static void AddPublicationsToPolicy(object buildKey, EventBrokerInfoPolicy policy)
        {
            var t = BuildKey.GetType(buildKey);
            foreach (var eventInfo in t.GetEvents())
            {
                var attrs = (PublishesAttribute[])eventInfo.GetCustomAttributes(typeof(PublishesAttribute), true);
                foreach (var attr in attrs)
                    policy.AddPublication(attr.EventName, eventInfo);
            }
        }

        private static void AddSubscriptionsToPolicy(object buildKey, EventBrokerInfoPolicy policy)
        {
            var t = BuildKey.GetType(buildKey);
            foreach (var method in t.GetMethods())
            {
                var attrs = (SubscribesToAttribute[])method.GetCustomAttributes(typeof(SubscribesToAttribute), true);
                foreach (var attr in attrs)
                    policy.AddSubscription(attr.EventName, method);
            }
        }
    }
}
