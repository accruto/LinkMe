using System;
using System.Collections.Generic;

namespace LinkMe.Framework.Utility.PublishedEvents
{
    public class EventArgs<T>
        : EventArgs
    {
        private readonly T _value;

        public EventArgs(T value)
        {
            _value = value;
        }

        public T Value
        {
            get { return _value; }
        }

        public override string ToString()
        {
            return _value.ToString();
        }
    }

    public abstract class PropertyChangedEventArgs
        : EventArgs
    {
    }

    public class PropertiesChangedEventArgs
        : EventArgs
    {
        public Guid InstanceId { get; private set; }
        public IEnumerable<PropertyChangedEventArgs> PropertyChangedEvents { get; private set; }

        public PropertiesChangedEventArgs(Guid instanceId, IEnumerable<PropertyChangedEventArgs> propertyChangedEvents)
        {
            InstanceId = instanceId;
            PropertyChangedEvents = propertyChangedEvents;
        }
    }

    public class PropertyChangedEventArgs<T>
        : PropertyChangedEventArgs
    {
        public T From { get; private set; }
        public T To { get; private set; }

        protected PropertyChangedEventArgs(T from, T to)
        {
            From = from;
            To = to;
        }
    }
}
