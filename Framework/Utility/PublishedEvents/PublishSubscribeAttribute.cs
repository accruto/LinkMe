using System;

namespace LinkMe.Framework.Utility.PublishedEvents
{
    /// <summary>
    /// Base class for the two publish / subscribe attributes. Stores
    /// the event name to be published or subscribed to.
    /// </summary>
    public abstract class PublishSubscribeAttribute : Attribute
    {
        protected PublishSubscribeAttribute(string eventName)
        {
            EventName = eventName;
        }

        public string EventName { get; private set; }
    }
}
