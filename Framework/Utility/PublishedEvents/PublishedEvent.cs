using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using LinkMe.Framework.Utility.Unity;

namespace LinkMe.Framework.Utility.PublishedEvents
{
    internal class PublishedEvent
    {
        private readonly List<EventPublisher> _publishers;
        private readonly List<EventSubscriber> _subscribers;
        private readonly string _eventName;
        private readonly IContainerEventSource _eventSource;

        public PublishedEvent(string eventName, IContainerEventSource eventSource)
        {
            _eventName = eventName;
            _eventSource = eventSource;
            _publishers = new List<EventPublisher>();
            _subscribers = new List<EventSubscriber>();
        }

        public void Fire(object sender, EventArgs e)
        {
            const string method = "Fire";

            EventSubscriber[] subscribers;
            lock (_subscribers)
            {
                subscribers = _subscribers.ToArray();
            }

            if (subscribers.Length == 0)
            {
                _eventSource.RaiseError(method, "A '" + _eventName + "' event is being fired by the '" + sender.GetType() + "' type but there are no subscribers.");
            }
            else
            {
                var exceptions = new List<Exception>();

                foreach (var sink in subscribers)
                {
                    var ex = sink.Invoke(sender, e);

                    if (ex != null)
                        exceptions.Add(ex);
                }

                if (exceptions.Count > 0)
                    throw new EventBrokerException(exceptions);
            }
        }

        public bool HasPublishers
        {
            get
            {
                return _publishers.Any(p => (p.Target != null));
            }
        }

        public bool HasSubscribers
        {
            get
            {
                lock (_subscribers)
                {
                    return _subscribers.Any(s => (s.Target != null));
                }
            }
        }

        public void AddPublisher(object publisher, EventInfo targetEvent)
        {
            CleanUpPublishers();
            _publishers.Add(new EventPublisher(this, publisher, targetEvent));
        }

        public void AddSubscriber(object subscriber, MethodInfo methodInfo)
        {
            lock (_subscribers)
            {
                CleanUpSubscribers();
                _subscribers.Add(new EventSubscriber(subscriber, methodInfo));
            }
        }

        private void CleanUpPublishers()
        {
            _publishers.RemoveAll(p => p.Target == null);
        }

        private void CleanUpSubscribers()
        {
            _subscribers.RemoveAll(s => s.Target == null);
        }
    }
}
