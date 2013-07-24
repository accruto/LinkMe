using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace LinkMe.Framework.Utility.PublishedEvents
{
    [Serializable]
    public class EventBrokerException
        : Exception
    {
        private readonly List<Exception> _exceptions;

        public EventBrokerException(IEnumerable<Exception> exceptions)
            : base(null, (exceptions.Any() ? exceptions.First() : null))
        {
            _exceptions = new List<Exception>(exceptions);
        }

        protected EventBrokerException()
        {
        }

        protected EventBrokerException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        public IList<Exception> Exceptions
        {
            get { return _exceptions; }
        }

        public override string Message
        {
            get
            {
                switch (_exceptions.Count)
                {
                    case 0:
                        return "Some exceptions were thrown by event broker sinks - but we don't have them!";

                    case 1:
                        return "An exception was thrown by event broker sinks.";

                    default:
                        return _exceptions.Count + " exceptions were thrown by event broker sinks -"
                            + " see inner exception for the first of them.";
                }
            }
        }
    }
}
