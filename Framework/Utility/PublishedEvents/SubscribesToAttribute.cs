using System;

namespace LinkMe.Framework.Utility.PublishedEvents
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
    public class SubscribesToAttribute : PublishSubscribeAttribute
    {
        public SubscribesToAttribute(string eventName) : base(eventName)
        {
        }
    }
}
