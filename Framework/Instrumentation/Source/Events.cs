using LinkMe.Framework.Utility.Event;

namespace LinkMe.Framework.Instrumentation
{
    public class CriticalErrorEvent
		:	Event
	{
		public CriticalErrorEvent()
			:	base(EventInfo, EventDetailFactories)
		{
		}

		static CriticalErrorEvent()
		{
			EventInfo = GetEventInfo(Constants.Events.CriticalError);
		    EventDetailFactories = AllEventDetailFactories;
		}

		private static readonly EventInfo EventInfo;
        private static readonly EventDetailFactories EventDetailFactories;
    }

	public class ErrorEvent
		:	Event
	{
        public ErrorEvent()
			:	base(EventInfo, EventDetailFactories)
		{
		}

		static ErrorEvent()
		{
			EventInfo = GetEventInfo(Constants.Events.Error);
		    EventDetailFactories = AllEventDetailFactories;
		}

		private static readonly EventInfo EventInfo;
        private static readonly EventDetailFactories EventDetailFactories;
    }

    public class NonCriticalErrorEvent
        : Event
    {
        public NonCriticalErrorEvent()
            : base(EventInfo, EventDetailFactories)
        {
        }

        static NonCriticalErrorEvent()
        {
            EventInfo = GetEventInfo(Constants.Events.NonCriticalError);
		    EventDetailFactories = StandardEventDetailFactories;
		}

		private static readonly EventInfo EventInfo;
        private static readonly EventDetailFactories EventDetailFactories;
    }

	public class WarningEvent
		:	Event
	{
        public WarningEvent()
            : base(EventInfo, EventDetailFactories)
		{
		}

		static WarningEvent()
		{
			EventInfo = GetEventInfo(Constants.Events.Warning);
		    EventDetailFactories = StandardEventDetailFactories;
		}

		private static readonly EventInfo EventInfo;
        private static readonly EventDetailFactories EventDetailFactories;
	}

	public class InformationEvent
		:	Event
	{
		public InformationEvent()
            : base(EventInfo, EventDetailFactories)
		{
		}

		static InformationEvent()
		{
			EventInfo = GetEventInfo(Constants.Events.Information);
		    EventDetailFactories = StandardEventDetailFactories;
		}

		private static readonly EventInfo EventInfo;
        private static readonly EventDetailFactories EventDetailFactories;
	}

	public class TraceEvent
		:	Event
	{
		public TraceEvent()
            : base(EventInfo, EventDetailFactories)
		{
		}

		static TraceEvent()
		{
			EventInfo = GetEventInfo(Constants.Events.Trace);
		    EventDetailFactories = StandardEventDetailFactories;
		}

		private static readonly EventInfo EventInfo;
        private static readonly EventDetailFactories EventDetailFactories;
	}

	public class MethodEnterEvent
		:	Event
	{
		public MethodEnterEvent()
            : base(EventInfo, EventDetailFactories)
		{
		}

		static MethodEnterEvent()
		{
			EventInfo = GetEventInfo(Constants.Events.MethodEnter);
		    EventDetailFactories = StandardEventDetailFactories;
		}

		private static readonly EventInfo EventInfo;
        private static readonly EventDetailFactories EventDetailFactories;
	}

	public class MethodExitEvent
		:	Event
	{
		public MethodExitEvent()
            : base(EventInfo, EventDetailFactories)
		{
		}

		static MethodExitEvent()
		{
			EventInfo = GetEventInfo(Constants.Events.MethodExit);
		    EventDetailFactories = StandardEventDetailFactories;
		}

		private static readonly EventInfo EventInfo;
        private static readonly EventDetailFactories EventDetailFactories;
	}

    public class CommunicationTrackingEvent
        : Event
    {
        public CommunicationTrackingEvent()
            : base(EventInfo, EventDetailFactories)
        {
        }

        static CommunicationTrackingEvent()
        {
            EventInfo = GetEventInfo(Constants.Events.CommunicationTracking);
		    EventDetailFactories = StandardEventDetailFactories;
		}

		private static readonly EventInfo EventInfo;
        private static readonly EventDetailFactories EventDetailFactories;
    }

    public class RequestTrackingEvent
        : Event
    {
        public RequestTrackingEvent()
            : base(EventInfo, EventDetailFactories)
        {
        }

        static RequestTrackingEvent()
        {
            EventInfo = GetEventInfo(Constants.Events.RequestTracking);
		    EventDetailFactories = StandardEventDetailFactories;
		}

		private static readonly EventInfo EventInfo;
        private static readonly EventDetailFactories EventDetailFactories;
    }

    public class FlowEvent
        : Event
    {
        public FlowEvent()
            : base(EventInfo, EventDetailFactories)
        {
        }

        static FlowEvent()
        {
            EventInfo = GetEventInfo(Constants.Events.Flow);
		    EventDetailFactories = StandardEventDetailFactories;
		}

		private static readonly EventInfo EventInfo;
        private static readonly EventDetailFactories EventDetailFactories;
    }

    public class FlowEnterEvent
        : Event
    {
        public FlowEnterEvent()
            : base(EventInfo, EventDetailFactories)
        {
        }

        static FlowEnterEvent()
        {
            EventInfo = GetEventInfo(Constants.Events.FlowEnter);
		    EventDetailFactories = StandardEventDetailFactories;
		}

		private static readonly EventInfo EventInfo;
        private static readonly EventDetailFactories EventDetailFactories;
    }

    public class FlowExitEvent
        : Event
    {
        public FlowExitEvent()
            : base(EventInfo, EventDetailFactories)
        {
        }

        static FlowExitEvent()
        {
            EventInfo = GetEventInfo(Constants.Events.FlowExit);
		    EventDetailFactories = StandardEventDetailFactories;
		}

		private static readonly EventInfo EventInfo;
        private static readonly EventDetailFactories EventDetailFactories;
    }
}
