using System;
using System.ComponentModel;

namespace LinkMe.Framework.Instrumentation
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public class EventSourceAttribute
        : Attribute
    {
        private readonly bool m_isEventSource;
        private readonly System.Type m_sourceType;

        public EventSourceAttribute(System.Type sourceType)
        {
            m_isEventSource = true;
            m_sourceType = sourceType;
        }

        public EventSourceAttribute()
        {
            m_sourceType = null;
        }

        public EventSourceAttribute(bool isEventSource)
        {
            m_isEventSource = isEventSource;
            m_sourceType = null;
        }

        public bool IsEventSource
        {
            get { return m_isEventSource; }
        }

        public System.Type SourceType
        {
            get { return m_sourceType; }
        }
    }

	public enum AutoInstrumentationState
	{
		/// <summary>
		/// The assembly, type or member does not need to be auto-instrumented.
		/// </summary>
		NotInstrumented = 0,
		/// <summary>
		/// The assembly, type or member needs to be auto-instrumented, which has not been done yet.
		/// </summary>
		Requested = 1,
		/// <summary>
		/// The assembly, type or member has already been auto-instrumented. This value should only be set by the
		/// tool that performs the instrumentation.
		/// </summary>
		Done = 2
	}

	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Method | AttributeTargets.Constructor)]
	public class AutoInstrumentEnterExitAttribute
        : Attribute
	{
		private readonly AutoInstrumentationState m_state;

		/// <summary>
		/// Creates a new instance of the AutoInstrumentEnterExitAttribute attribute.
		/// </summary>
		/// <param name="autoInstrument">True to automatically instrument the target, false to skip it.</param>
		public AutoInstrumentEnterExitAttribute(bool autoInstrument)
		{
			m_state = (autoInstrument ? AutoInstrumentationState.Requested : AutoInstrumentationState.NotInstrumented);
		}

		/// <summary>
		/// Do not use this constructor. It is provided for use by the automatic instrumentation tool.
		/// </summary>
		[EditorBrowsable(EditorBrowsableState.Never)]
		public AutoInstrumentEnterExitAttribute(AutoInstrumentationState state)
		{
			m_state = state;
		}

		public AutoInstrumentationState State
		{
			get { return m_state; }
		}
	}
}
