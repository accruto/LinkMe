using LinkMe.Framework.Instrumentation.Message;
using LinkMe.Framework.Utility.Event;

namespace LinkMe.Framework.Instrumentation
{
    public struct EventArg
    {
        private readonly string _name;
        private readonly object _value;

        internal EventArg(string name, object value)
        {
            _name = name;
            _value = value;
        }

        internal string Name
        {
            get { return _name; }
        }

        internal object Value
        {
            get { return _value; }
        }
    }

	public abstract class Event
	{
        private static readonly CriticalErrorEvent CriticalErrorEvent = new CriticalErrorEvent();
        private static readonly ErrorEvent ErrorEvent = new ErrorEvent();
        private static readonly WarningEvent WarningEvent = new WarningEvent();
        private static readonly NonCriticalErrorEvent NonCriticalErrorEvent = new NonCriticalErrorEvent();
        private static readonly InformationEvent InformationEvent = new InformationEvent();
        private static readonly TraceEvent TraceEvent = new TraceEvent();
        private static readonly MethodEnterEvent MethodEnterEvent = new MethodEnterEvent();
        private static readonly MethodExitEvent MethodExitEvent = new MethodExitEvent();
        private static readonly CommunicationTrackingEvent CommunicationTrackingEvent = new CommunicationTrackingEvent();
        private static readonly RequestTrackingEvent RequestTrackingEvent = new RequestTrackingEvent();
        private static readonly FlowEvent FlowEvent = new FlowEvent();
        private static readonly FlowEnterEvent FlowEnterEvent = new FlowEnterEvent();
        private static readonly FlowExitEvent FlowExitEvent = new FlowExitEvent();

	    private static EventDetailFactories _allEventDetailFactories;
        private static EventDetailFactories _standardEventDetailFactories;

	    protected static EventDetailFactories AllEventDetailFactories
	    {
            get
            {
                if (_allEventDetailFactories == null)
                {
                    var allEventDetailFactories = new EventDetailFactories();
                    allEventDetailFactories.Add(new MessageDiagnosticDetailFactory());
                    allEventDetailFactories.Add(new MessageHttpDetailFactory());
                    allEventDetailFactories.Add(new MessageNetDetailFactory());
                    allEventDetailFactories.Add(new MessageProcessDetailFactory());
                    allEventDetailFactories.Add(new MessageSecurityDetailFactory());
                    allEventDetailFactories.Add(new MessageThreadDetailFactory());
                    _allEventDetailFactories = allEventDetailFactories;
                }
                return _allEventDetailFactories;
            }
	    }

        protected static EventDetailFactories StandardEventDetailFactories
        {
            get
            {
                if (_standardEventDetailFactories == null)
                {
                    var standardEventDetailFactories = new EventDetailFactories();
                    standardEventDetailFactories.Add(new MessageHttpDetailFactory());
                    standardEventDetailFactories.Add(new MessageNetDetailFactory());
                    standardEventDetailFactories.Add(new MessageProcessDetailFactory());
                    standardEventDetailFactories.Add(new MessageSecurityDetailFactory());
                    standardEventDetailFactories.Add(new MessageThreadDetailFactory());
                    _standardEventDetailFactories = standardEventDetailFactories;
                }
                return _standardEventDetailFactories;
            }
        }

        public static CriticalErrorEvent CriticalError
        {
            get { return CriticalErrorEvent; }
        }

        public static ErrorEvent Error
        {
            get { return ErrorEvent; }
        }

        public static WarningEvent Warning
        {
            get { return WarningEvent; }
        }

        public static NonCriticalErrorEvent NonCriticalError
        {
            get { return NonCriticalErrorEvent; }
        }

        public static InformationEvent Information
        {
            get { return InformationEvent; }
        }

        public static TraceEvent Trace
        {
            get { return TraceEvent; }
        }

        public static MethodEnterEvent MethodEnter
        {
            get { return MethodEnterEvent; }
        }

        public static MethodExitEvent MethodExit
        {
            get { return MethodExitEvent; }
        }

        public static CommunicationTrackingEvent CommunicationTracking
        {
            get { return CommunicationTrackingEvent; }
        }

        public static RequestTrackingEvent RequestTracking
        {
            get { return RequestTrackingEvent; }
        }

        public static FlowEvent Flow
        {
            get { return FlowEvent; }
        }

        public static FlowEnterEvent FlowEnter
        {
            get { return FlowEnterEvent; }
        }

        public static FlowExitEvent FlowExit
        {
            get { return FlowExitEvent; }
        }

        /// <summary>
        /// Initializes an event parameter with the passed parameters and 
        /// returns the event parameter object.
        /// </summary>
        /// <param name="name">The name of the parameter.</param>
        /// <param name="value">The value of the parameter.</param>
        /// <returns>The event parameter object.</returns>
        public static EventArg Arg(string name, object value)
        {
            return new EventArg(name, value);
        }

		protected Event(EventInfo eventInfo, EventDetailFactories eventDetailFactories)
		{
			_eventInfo = eventInfo;
		    _eventDetailFactories = eventDetailFactories;
		}

		public string Name
		{
			get { return _eventInfo.Name; }
		}

		internal int Index
		{
			get { return _eventInfo.Index; }
		}

		internal EventDetails CreateEventDetails()
		{
			return _eventDetailFactories.CreateDetails();
		}

		protected static EventInfo GetEventInfo(string name)
		{
			return InstrumentationManager.GetEventInfo(name);
		}

		private readonly EventInfo _eventInfo;
	    private readonly EventDetailFactories _eventDetailFactories;
	}
}
