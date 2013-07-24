using System;

namespace LinkMe.Framework.Utility.PublishedEvents
{
    [AttributeUsage(AttributeTargets.Event, Inherited = true)]
    public class PublishesAttribute : PublishSubscribeAttribute
    {
        public PublishesAttribute(string publishedEventName) : base(publishedEventName)
        {
        }
    }
}
