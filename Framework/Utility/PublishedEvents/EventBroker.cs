using System.Collections.Generic;
using System.Reflection;
using LinkMe.Framework.Utility.Unity;

namespace LinkMe.Framework.Utility.PublishedEvents
{
    public class EventBroker
    {
        private readonly IContainerEventSource _eventSource;
        private readonly Dictionary<string, PublishedEvent> _publishedEvents = new Dictionary<string, PublishedEvent>();

        public EventBroker(IContainerEventSource eventSource)
        {
            _eventSource = eventSource;
        }

        public void RegisterPublisher(string publishedEventName, object publisher, EventInfo eventInfo)
        {
            lock (_publishedEvents)
            {
                var publishedEvent = GetEvent(publishedEventName);
                publishedEvent.AddPublisher(publisher, eventInfo);
                CleanUp();
            }
        }

        public void RegisterSubscriber(string publishedEventName, object subscriber, MethodInfo methodInfo)
        {
            lock (_publishedEvents)
            {
                var publishedEvent = GetEvent(publishedEventName);
                publishedEvent.AddSubscriber(subscriber, methodInfo);
                CleanUp();
            }
        }

        private PublishedEvent GetEvent(string eventName)
        {
            if (!_publishedEvents.ContainsKey(eventName))
                _publishedEvents[eventName] = new PublishedEvent(eventName, _eventSource);

            return _publishedEvents[eventName];
        }

        private void CleanUp()
        {
            var deadEvents = new List<string>();
            foreach (var publishedEvent in _publishedEvents)
            {
                if (!publishedEvent.Value.HasPublishers && !publishedEvent.Value.HasSubscribers)
                    deadEvents.Add(publishedEvent.Key);
            }

            deadEvents.ForEach(eventName => _publishedEvents.Remove(eventName));
        }
    }
}