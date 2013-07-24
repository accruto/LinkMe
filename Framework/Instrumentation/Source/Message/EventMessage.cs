using System;
using System.Collections;
using System.Diagnostics;
using System.IO;
using System.Runtime.Serialization;
using System.Xml;
using System.Xml.Schema;
using LinkMe.Framework.Type;
using LinkMe.Framework.Utility;
using LinkMe.Framework.Utility.Event;
using LinkMe.Framework.Utility.Exceptions;
using LinkMe.Framework.Utility.Validation;
using LinkMe.Framework.Utility.Xml;
using DateTime=LinkMe.Framework.Type.DateTime;
using TimeZone=LinkMe.Framework.Type.TimeZone;

namespace LinkMe.Framework.Instrumentation.Message
{
	/// <summary>
	/// An Instrumentation event message.
	/// </summary>
	[Serializable]
	public class EventMessage
		:	ISerializable,
			ICloneable,
			IXmlSerializable,
			IBinarySerializable,
			IInternable
	{
		#region Constructors

		public EventMessage()
			:	this(null, null, null, null, (string) null, null, null)
		{
		}

        internal EventMessage(InstrumentationSource source, Event instrumentationEvent, System.Type type)
			:	this(source, instrumentationEvent, type, null, (string) null, null, null)
		{
		}

		internal EventMessage(InstrumentationSource source, Event instrumentationEvent, System.Type type, string method)
			:	this(source, instrumentationEvent, type, method, (string) null, null, null)
		{
		}

        internal EventMessage(InstrumentationSource source, Event instrumentationEvent, System.Type type, string method, string message)
            : this(source, instrumentationEvent, type, method, message, null, null)
        {
        }

        internal EventMessage(InstrumentationSource source, Event instrumentationEvent, System.Type type, string method, string message, Exception exception, IErrorHandler errorHandler)
			:	this(source, instrumentationEvent, type, method, message, exception, errorHandler, null)
		{
		}

        internal EventMessage(InstrumentationSource source, Event instrumentationEvent, System.Type type, string method, Exception exception, IErrorHandler errorHandler, EventParameter[] parameters)
            : this(source, instrumentationEvent, type, method, null, exception, errorHandler, parameters)
		{
		}

		/// <summary>
		/// Initializes an Event Message with the passed parameters
		/// </summary>
		internal EventMessage(InstrumentationSource source, Event instrumentationEvent, System.Type type, string method, string message, Exception exception, IErrorHandler errorHandler, EventParameter[] parameters)
		{
			try
			{
				// Set members.

				_source = source == null ? string.Empty : source.FullyQualifiedReference;
				_event = instrumentationEvent == null ? string.Empty : instrumentationEvent.Name;
				_type = type == null ? string.Empty : type.AssemblyQualifiedName;
				_method = method ?? string.Empty;
				_message = message ?? string.Empty;
                _parameters = parameters;
                _exception = (exception == null ? null : new ExceptionInfo(exception, errorHandler));

				// Store the time in UTC.

				_time = System.DateTime.UtcNow;

				if ( instrumentationEvent != null )
					_details = instrumentationEvent.CreateEventDetails();
			}
			catch (Exception ex)
			{
				// Don't propagate anything outside Instrumentation. If an exception is thrown send a
				// message to the internal message handler, but only up to a maximum of 10 times.

				if (_errorCount < Constants.Errors.MaximumErrorsToLog)
				{
					_errorCount++;
					string errorMessage = "The following error occurred while trying to create an EventMessage:"
						+ Environment.NewLine + ex;

					if (_errorCount >= Constants.Errors.MaximumErrorsToLog)
					{
						errorMessage += Environment.NewLine + Environment.NewLine
							+ "No further EventMessage constructor errors will be logged.";
					}

					// Use the simplest (private) EventMessage constructor to minimise the chance of
					// another exception occurring and avoid a loop.

					InstrumentationManager.GetInternalMessageHandler().HandleEventMessage(
						new EventMessage(Constants.Events.Warning, typeof(EventMessage).Name,
                        typeof(EventMessage).AssemblyQualifiedName, ".ctor", errorMessage,
                        System.DateTime.UtcNow, new ExceptionInfo(ex, null)));
				}
			}
		}

		internal EventMessage(string eventName, string source, string type, string method, string message, System.DateTime time, ExceptionInfo exception)
		{
			_event = eventName;
			_source = source;
			_type = type;
			_method = method;
			_message = message;
			_time = time;
            _exception = exception;
		}

		#endregion

		#region Properties

		/// <summary>
		/// Gets the name of the event source.
		/// </summary>
		public string Source
		{
			get { return _source; }
		}

		/// <summary>
		/// Gets ot Sets the Event Type.
		/// </summary>
		public string Event
		{
			get { return _event; }
            set { _event = value; }
		}

		/// <summary>
		/// Gets ot Sets the Type.
		/// </summary>
		public string Type
		{
			get { return _type; }
		}

		/// <summary>
		/// Gets the Method Name.
		/// </summary>
		public string Method
		{
			get { return _method; }
		}

		/// <summary>
		/// Gets the Logged message.
		/// </summary>
		public string Message
		{
			get { return _message; }
		}

		/// <summary>
		/// The data and time of the message in UTC.
		/// </summary>
		public System.DateTime Time
		{
			// LinkMe.Framework.Type.DateTime is overkill in this case - the system one works fine. We also want to use
			// its Ticks property to persist it to SQL, because the SQL datetime type loses precision.

			get { return _time; }
		}

        /// <summary>
        /// The exception associated with the message, if any.
        /// </summary>
	    public virtual ExceptionInfo Exception
	    {
	        get { return _exception; }
	    }

	    /// <summary>
		/// The sequence number used to maintain the order of the messages in a repository when multiple
		/// messages have the same time.
		/// </summary>
		/// <remarks>This sequence number should be set by a message handler before writing the message
		/// to a repository, unless it's already set (the value is non-zero).</remarks>
		public int Sequence
		{
			get { return _sequence; }
		}

		public virtual EventDetails Details
		{
			get { return _details ?? new EventDetails(); }
		}

		/// <summary>
		/// Gets the additional paramters which have to be logged.
		/// </summary>
		public virtual EventParameters Parameters
		{
			get { return new EventParameters(_parameters ?? new EventParameter[0]); }
		}

		#endregion

		#region Equals

		public override bool Equals(object other)
		{
			if ( !(other is EventMessage) )
				return false;

            var otherMessage = (EventMessage) other;

            if ( !StandardEquals(otherMessage) )
				return false;
			if ( !EventDetailsEquals(otherMessage) )
				return false;
			if ( !ParametersEquals(otherMessage) )
				return false;
			return true;
		}

		public override int GetHashCode()
		{
			return _source.GetHashCode()
				^ _event.GetHashCode()
				^ _type.GetHashCode()
				^ _method.GetHashCode()
				^ _message.GetHashCode()
				^ _time.GetHashCode();
		}

		private bool StandardEquals(EventMessage other)
		{
			return _source == other._source
				&& _event == other._event
				&& _type == other._type
				&& _method == other._method
				&& _message == other._message
				&& _time == other._time
                && Equals(_exception, other._exception);
		}

		private bool EventDetailsEquals(EventMessage other)
		{
			return Equals(_details, other._details);
		}

		private bool ParametersEquals(EventMessage other)
		{
			// Make sure there are parameters.

			if ( _parameters == null )
				return other._parameters == null;
			if ( other._parameters == null )
				return false;

			// Make sure lengths are the same.

			if ( _parameters.Length != other._parameters.Length )
				return false;

			// Iterate.

			for ( int index = 0; index < _parameters.Length; ++index )
			{
				if ( !_parameters[index].Equals(other._parameters[index]) )
					return false;
			}

			return true;
		}

		#endregion

		#region ISerializable Members

		/// <summary>
		/// Initializes and Event Message with the serialization info and the streaming context.
		/// Generally used for Event Message Serialization.
		/// </summary>
		/// <param name="info">The serialized objects storage.</param>
		/// <param name="streamingContext"></param>
		protected EventMessage(SerializationInfo info, StreamingContext streamingContext)
		{
			BinarySerializer.ReadObjectDataForBinarySerializable(this, info);
		}
	
		/// <summary>
		/// Add the objects field values to the serialization info object.
		/// </summary>
        /// <param name="info"></param>
		/// <param name="streamingContext"></param>
		public void GetObjectData(SerializationInfo info, StreamingContext streamingContext)
		{
			BinarySerializer.WriteObjectDataForBinarySerializable(this, info);
		}

		#endregion

		#region IXmlSerializable Members

		public XmlSchema GetSchema()
		{
			return null;
		}

		public void WriteOuterXml(XmlWriter xmlWriter)
		{
			// Create an adaptor.

			var adaptor = new XmlWriteAdaptor(xmlWriter, Constants.Xml.Namespace);
			adaptor.WriteStartElement(Constants.Xml.EventMessageElement);

			// Add the XSI namespace if not already there.

			adaptor.WriteNamespace(Constants.Xsi.Prefix, Constants.Xsi.Namespace);
			adaptor.WriteNamespace(Constants.Xml.UtilityPrefix, Constants.Xml.UtilityNamespace);
			adaptor.WriteNamespace(Constants.Xml.TypePrefix, Constants.Xml.TypeNamespace);

			// Add contents.

			WriteXml(adaptor);

			adaptor.WriteEndElement();
		}

		public void WriteXml(XmlWriter xmlWriter)
		{
			// Create an adaptor.

			var adaptor = new XmlWriteAdaptor(xmlWriter, Constants.Xml.Namespace);

			// Add contents.

			WriteXml(adaptor);
		}

		private void WriteXml(XmlWriteAdaptor adaptor)
		{
			// Source.

			adaptor.WriteStartElement(Constants.Xml.SourceElement);
			adaptor.WriteName(_source);
			adaptor.WriteEndElement();

			// Event.

			adaptor.WriteStartElement(Constants.Xml.EventElement);
			adaptor.WriteName(_event);
			adaptor.WriteEndElement();

			// Type, Method, Message, Time, Sequence.

			adaptor.WriteElement(Constants.Xml.TypeElement, _type);
			adaptor.WriteElement(Constants.Xml.MethodElement, _method);
			adaptor.WriteElement(Constants.Xml.MessageElement, _message);

			// The message time is in UTC, but System.Xml.XmlConvert assumes it's local time and writes
			// the local timezone. Use LinkMe.Framework.Type.DateTime to avoid this.

			DateTime time = DateTime.FromSystemDateTime(_time, TimeZone.UTC);
			adaptor.WriteElement(Constants.Xml.TimeElement, TypeXmlConvert.ToString(time));

			if (_sequence != 0)
			{
				adaptor.WriteElement(Constants.Xml.SequenceElement, TypeXmlConvert.ToString(_sequence));
			}

            // Exception.

            if (_exception != null)
            {
                adaptor.WriteStartElement(Constants.Xml.ExceptionElement);
                _exception.WriteXml(adaptor.XmlWriter);
                adaptor.WriteEndElement();
            }

		    // Details and Parameters.

			WriteDetails(adaptor);
			WriteParameters(adaptor);
		}

		public void ReadOuterXml(XmlReader xmlReader)
		{
			Reset();

		    // Create an adaptor.

			var adaptor = new XmlReadAdaptor(xmlReader, Constants.Xml.Namespace);
			if ( adaptor.IsReadingElement(Constants.Xml.EventMessageElement) )
				ReadXml(adaptor);
		}

	    public void ReadXml(XmlReader xmlReader)
		{
            Reset();

			// Create an adaptor.

			var adaptor = new XmlReadAdaptor(xmlReader, Constants.Xml.Namespace);
			ReadXml(adaptor);
		}

		private void ReadXml(XmlReadAdaptor adaptor)
		{
			ReadStandard(adaptor);
			ReadDetails(adaptor);
			ReadParameters(adaptor);
		}

		private void WriteDetails(XmlWriteAdaptor adaptor)
		{
			// Iterate.

			if ( _details != null )
			{
				adaptor.WriteStartElement(Constants.Xml.DetailsElement);

				foreach ( IEventDetail detail in _details )
				{
					adaptor.WriteStartElement(detail.Name);
					((IXmlSerializable) detail).WriteXml(adaptor.XmlWriter);
					adaptor.WriteEndElement();
				}

				adaptor.WriteEndElement();
			}
		}

		private void WriteParameters(XmlWriteAdaptor adaptor)
		{
			// Only write something if there is something to write.

			if ( _parameters != null )
			{
				adaptor.WriteStartElement(Constants.Xml.ParametersElement);

				// Iterate.

				foreach ( EventParameter parameter in _parameters )
					parameter.WriteXml(adaptor);

				adaptor.WriteEndElement();
			}
		}

        private void Reset()
        {
            _source = string.Empty;
            _event = string.Empty;
            _type = string.Empty;
            _method = string.Empty;
            _message = string.Empty;
            _time = new System.DateTime();
            _details = null;
            _parameters = null;
        }

		private void ReadStandard(XmlReadAdaptor adaptor)
		{
			// Source.

			if ( adaptor.ReadElement(Constants.Xml.SourceElement) )
			{
				_source = adaptor.ReadName();
				adaptor.ReadEndElement();
			}

			// Event.

			if ( adaptor.ReadElement(Constants.Xml.EventElement) )
			{
				_event = adaptor.ReadName();
				adaptor.ReadEndElement();
			}

			// Type, Method, Message, Time, Sequence.

			_type = adaptor.ReadElementString(Constants.Xml.TypeElement, string.Empty);
			_method = adaptor.ReadElementString(Constants.Xml.MethodElement, string.Empty);
			_message = adaptor.ReadElementString(Constants.Xml.MessageElement, string.Empty);
			string timeXml = adaptor.ReadElementString(Constants.Xml.TimeElement, string.Empty);

			// The time is in the LinkMe.Framework.Type.DateTime format and should always be in UTC.

			DateTime time = TypeXmlConvert.ToDateTime(timeXml);
			Debug.Assert(time.TimeZone == TimeZone.UTC, "time.TimeZone == TimeZone.UTC");

			_time = time.ToSystemDateTime();

			string sequence = adaptor.ReadElementString(Constants.Xml.SequenceElement, string.Empty);
			_sequence = (sequence.Length == 0 ? 0 : TypeXmlConvert.ToInt32(sequence));

            // Exception.

            _exception = ExceptionInfo.FromXml(adaptor, Constants.Xml.ExceptionElement);
		}

		private void ReadDetails(XmlReadAdaptor adaptor)
		{
			_details = new EventDetails();
			if ( adaptor.ReadElement(Constants.Xml.DetailsElement) )
			{
				while ( adaptor.ReadElement() )
				{
					string name = adaptor.Name;
					IEventDetailFactory factory = InstrumentationManager.GetEventDetailFactory(name);
					if ( factory != null )
					{
						IEventDetail detail = factory.CreateInstance();
						((IXmlSerializable) detail).ReadXml(adaptor.XmlReader);
						_details.Add(detail);
					}

					adaptor.ReadEndElement();
				}

				adaptor.ReadEndElement();
			}
		}

		private void ReadParameters(XmlReadAdaptor adaptor)
		{
			if ( adaptor.ReadElement(Constants.Xml.ParametersElement) )
			{
				// Look for the elements.

				var parameters = new ArrayList();
				while ( adaptor.ReadElement(Constants.Xml.ParameterElement) )
				{
					parameters.Add(EventParameter.ReadXml(adaptor));
					adaptor.ReadEndElement();
				}

				// Set up the real parameters.

				if ( parameters.Count > 0 )
				{
					_parameters = new EventParameter[parameters.Count];
					for ( int index = 0; index < _parameters.Length; ++index )
						_parameters[index] = (EventParameter) parameters[index];
				}

				adaptor.ReadEndElement();
			}
		}

		#endregion

		#region IBinarySerializable Members

		public void Write(BinaryWriter writer)
		{
			WriteStandard(writer);
			WriteDetails(writer);
			WriteParameters(writer);
		}

		private void WriteStandard(BinaryWriter writer)
		{
			var adaptor = new BinaryWriteAdaptor(writer);
			adaptor.Write(_event);
			adaptor.Write(_source);
			adaptor.Write(_type);
			adaptor.Write(_method);
			adaptor.Write(_message);
			adaptor.Write(_time.Ticks);
			adaptor.Write(_sequence);

            adaptor.Write(_exception != null);
            if (_exception != null)
            {
                ((IBinarySerializable) _exception).Write(writer);
            }
		}

		private void WriteDetails(BinaryWriter writer)
		{
			// Groups

			if ( _details == null )
			{
				writer.Write(0);
			}
			else
			{
				writer.Write(_details.Count);
				((IBinarySerializable) _details).Write(writer);
			}
		}

		private void WriteParameters(BinaryWriter writer)
		{
			// Parameters

			if ( _parameters == null )
			{
				writer.Write(0);
			}
			else
			{
				writer.Write(_parameters.Length);
				foreach ( EventParameter parameter in _parameters )
					parameter.Write(writer);
			}
		}

		public void Read(BinaryReader reader)
		{
            Reset();
			ReadStandard(reader);
			ReadDetails(reader);
			ReadParameters(reader);
		}

		private void ReadStandard(BinaryReader reader)
		{
			var adaptor = new BinaryReadAdaptor(reader);
			_event = adaptor.ReadString();
			_source = adaptor.ReadString();
			_type = adaptor.ReadString();
			_method = adaptor.ReadString();
			_message = adaptor.ReadString();
			_time = new System.DateTime(adaptor.ReadInt64());
			_sequence = adaptor.ReadInt32();

            if (adaptor.ReadBoolean())
            {
                _exception = ExceptionInfo.FromBinary(reader);
            }
		}

		private void ReadDetails(BinaryReader reader)
		{
			// Event groups

			int count = reader.ReadInt32();
			if ( count == 0 )
			{
				_details = null;
			}
			else
			{
				_details = new EventDetails();
				((IBinarySerializable) _details).Read(reader);
			}
		}

		private void ReadParameters(BinaryReader reader)
		{
			// Parameters

			int count = reader.ReadInt32();
			if ( count == 0 )
			{
				_parameters = null;
			}
			else
			{
				_parameters = new EventParameter[count];
				for ( int index = 0; index < count; ++index )
					_parameters[index] = EventParameter.Read(reader);
			}
		}

		#endregion

		#region ICloneable Members

		public EventMessage Clone()
		{
			var message = new EventMessage(_event, _source, _type, _method, _message, _time, _exception);

			// Event groups

			if ( _details != null )
				message._details = _details.Clone();

			// Parameters

			if ( _parameters != null )
			{
				message._parameters = new EventParameter[_parameters.Length];
				for ( int index = 0; index < _parameters.Length; ++index )
					message._parameters[index] = _parameters[index].Clone();
			}

			return message;
		}

        object ICloneable.Clone()
		{
			return Clone();
		}

		#endregion

		#region IInternable Members

		public void Intern(Interner interner)
		{
			const string method = "Intern";

			if (interner == null)
				throw new NullParameterException(GetType(), method, "interner");

			interner.Intern(ref _event);
			interner.Intern(ref _source);
			interner.Intern(ref _type);
			interner.Intern(ref _method);
			interner.Intern(ref _message);

            interner.Intern(_exception);
            interner.Intern(_details);

            if (_parameters != null)
            {
                foreach (EventParameter param in _parameters)
                {
                    param.Intern(interner);
                }
            }
		}

		#endregion

		internal void SetDetails(EventDetails details)
		{
			_details = details;
		}

		internal void SetParameters(EventParameter[] parameters)
		{
			_parameters = parameters;
		}

		internal void SetSequence(int sequence)
		{
			_sequence = sequence;
		}

        internal void SetException(ExceptionInfo exception)
        {
            _exception = exception;
        }

	    #region Members

		private static int _errorCount;

		private string _event;
		private string _source;
		private string _type;
		private string _method;
		private string _message;
		private System.DateTime _time;
        private ExceptionInfo _exception;
		private int _sequence;
		private EventParameter[] _parameters;
		private EventDetails _details;

		#endregion
	}
}
