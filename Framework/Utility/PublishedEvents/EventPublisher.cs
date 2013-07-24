using System;
using System.Reflection;

namespace LinkMe.Framework.Utility.PublishedEvents
{
    internal class EventPublisher
    {
        readonly PublishedEvent _publishedEvent;
        readonly WeakReference _publisher;

        public EventPublisher(PublishedEvent publishedEvent, object publisher, EventInfo eventInfo)
        {
            _publishedEvent = publishedEvent;
            _publisher = new WeakReference(publisher);

            var methodInfo = GetType().GetMethod("HandleEvent");
            var method = Delegate.CreateDelegate(eventInfo.EventHandlerType, this, methodInfo);
            eventInfo.AddEventHandler(publisher, method);
        }

        public object Target
        {
            get { return _publisher.Target; }
        }

        public void HandleEvent(object sender, EventArgs e)
        {
            _publishedEvent.Fire(sender, e);
        }
    }
}
