using log4net.Appender;
using log4net.Core;

namespace LinkMe.Framework.Instrumentation.Adaptors
{
    public class Log4NetAppender
        : AppenderSkeleton
    {
        private readonly EventSource _eventSource;

        public Log4NetAppender(System.Type type)
        {
            _eventSource = new EventSource(type);
        }

        protected override void Append(LoggingEvent loggingEvent)
        {
            _eventSource.Raise(MapEventType(loggingEvent.Level), loggingEvent.LoggerName, loggingEvent.RenderedMessage, loggingEvent.ExceptionObject);
        }

        private static Event MapEventType(Level level)
        {
            if (level >= Level.Critical)
                return Event.CriticalError;

            if (level >= Level.Error)
                return Event.Error;

            if (level == Level.Warn)
                return Event.Warning;

            if (level == Level.Info)
                return Event.Information;
    
            return Event.Trace;
        }
    }
}
