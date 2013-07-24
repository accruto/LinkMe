using System.IO;
using System.Reflection;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml;
using System.Xml.Schema;
using LinkMe.Framework.Utility.Event;
using LinkMe.Framework.Utility.Net;
using LinkMe.Framework.Utility.Xml;

namespace LinkMe.Framework.Utility.Exceptions
{
	/// <summary>
	/// The ultimate base class for all exceptions.
	/// </summary>
	[System.Serializable]
	public abstract class BaseException
		:	System.Exception,
			IXmlSerializable,
			IBinarySerializable,
			ISerializable
	{
		#region Constructors

		static BaseException()
		{
			m_eventDetailFactories.Add(new ProcessDetailFactory());
			m_eventDetailFactories.Add(new NetDetailFactory());
			//m_eventDetailFactories.Add(new ThreadDetailFactory());
			m_eventDetailFactories.Add(new SecurityDetailFactory());
		}

		protected BaseException(string source, string method, string message, System.Exception innerException)
			:	base(message, innerException)
		{
			Source = source == null ? string.Empty : source;
			m_method = method == null ? string.Empty : method;
			m_time = System.DateTime.Now.ToUniversalTime();
			CreateDetails();
		}

		protected BaseException(System.Type source, string method, string message, System.Exception innerException)
			:	this(source.AssemblyQualifiedName, method, message, innerException)
		{
		}

		protected BaseException(string source, string method, string message)
			:	base(message)
		{
			Source = source == null ? string.Empty : source;
			m_method = method == null ? string.Empty : method;
			m_time = System.DateTime.Now.ToUniversalTime();
			CreateDetails();
		}

		protected BaseException(System.Type source, string method, string message)
			:	this(source.AssemblyQualifiedName, method, message)
		{
		}

		protected BaseException(string source, string method, System.Exception innerException)
			:	this(source, method, string.Empty, innerException)
		{
		}

		protected BaseException(System.Type source, string method, System.Exception innerException)
			:	this(source, method, string.Empty, innerException)
		{
		}

		protected BaseException(string source, string method)
			:	this(source, method, string.Empty)
		{
		}

		protected BaseException(System.Type source, string method)
			:	this(source, method, string.Empty)
		{
		}

		protected BaseException(SerializationInfo info, StreamingContext context)
			:	base(info, context)
		{
			m_stackTrace = info.GetString(Constants.Serialization.Exception.StackTrace);
			m_method = info.GetString(Constants.Serialization.Exception.Method);
			m_time = (System.DateTime) info.GetValue(Constants.Serialization.Exception.Time, typeof(System.DateTime));
			m_details = (EventDetails) info.GetValue(Constants.Serialization.Exception.Details, typeof(EventDetails));
		}

		protected BaseException()
			:	base()
		{
			m_method = string.Empty;
			CreateDefaultDetails();
		}

		#endregion

		#region ISerializable Members

		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue(Constants.Serialization.Exception.StackTrace, StackTrace);
			info.AddValue(Constants.Serialization.Exception.Method, m_method);
			info.AddValue(Constants.Serialization.Exception.Time, m_time);
			info.AddValue(Constants.Serialization.Exception.Details, m_details);
		}

		#endregion

		#region IXmlSerializable Members

		public XmlSchema GetSchema()
		{
			return null;
		}

		public void ReadOuterXml(XmlReader xmlReader)
		{
			XmlReadAdaptor adaptor = new XmlReadAdaptor(xmlReader, GetXmlNamespace());
			if ( adaptor.IsReadingElement(Constants.Xml.Exception.ExceptionElement) )
				ReadXml(adaptor);
		}

		public void ReadXml(XmlReader xmlReader)
		{
			XmlReadAdaptor adaptor = new XmlReadAdaptor(xmlReader, GetXmlNamespace());
			ReadXml(adaptor);
		}

		private void ReadXml(XmlReadAdaptor adaptor)
		{
			// Standard.

			adaptor.ReadElementString(Constants.Xml.Exception.MessageElement, string.Empty);
			Source = adaptor.ReadElementString(Constants.Xml.Exception.SourceElement, string.Empty);
			m_method = adaptor.ReadElementString(Constants.Xml.Exception.MethodElement, string.Empty);
			m_time = XmlConvert.ToDateTime(adaptor.ReadElementString(Constants.Xml.Exception.TimeElement), XmlDateTimeSerializationMode.Utc);
			System.Exception innerException = ReadInnerException(adaptor);
			if ( innerException != null )
				SetInnerException(innerException);
			m_stackTrace = adaptor.ReadElementString(Constants.Xml.Exception.StackTraceElement, string.Empty);
			ReadDetails(adaptor);

			// Add derived exception contents.

			ReadContents(adaptor);
		}

		public void WriteOuterXml(XmlWriter xmlWriter)
		{
			XmlWriteAdaptor adaptor = new XmlWriteAdaptor(xmlWriter, GetXmlNamespace());
			adaptor.WriteStartElement(Constants.Xml.Exception.ExceptionElement);
			adaptor.WriteNamespace(GetXmlNamespace());
			WriteNamespaces(adaptor);
			adaptor.WriteAttribute(Constants.Xsi.TypeAttribute, Constants.Xsi.Prefix, Constants.Xsi.Namespace, GetType().Name);
			WriteXml(adaptor);
			adaptor.WriteEndElement();
		}

		public void WriteXml(XmlWriter xmlWriter)
		{
			XmlWriteAdaptor adaptor = new XmlWriteAdaptor(xmlWriter, GetXmlNamespace());
			adaptor.WriteNamespace("lm", GetXmlNamespace());
			WriteNamespaces(adaptor);
			adaptor.WriteAttribute(Constants.Xsi.TypeAttribute, Constants.Xsi.Prefix, Constants.Xsi.Namespace, GetType().Name);
			WriteXml(adaptor);
		}

		private void WriteXml(XmlWriteAdaptor adaptor)
		{
			// Standard.

			adaptor.WriteElement(Constants.Xml.Exception.MessageElement, Message);
			adaptor.WriteElement(Constants.Xml.Exception.SourceElement, Source);
			adaptor.WriteElement(Constants.Xml.Exception.MethodElement, Method);
			adaptor.WriteElement(Constants.Xml.Exception.TimeElement, XmlConvert.ToString(m_time, XmlDateTimeSerializationMode.Utc));
			WriteInnerException(adaptor, InnerException);
			adaptor.WriteElement(Constants.Xml.Exception.StackTraceElement, StackTrace);
			WriteDetails(adaptor);
			WriteContents(adaptor);
		}

		#endregion

		#region IBinarySerializable Members

		void IBinarySerializable.Write(BinaryWriter writer)
		{
			// Standard.

			writer.Write(Source == null ? string.Empty : Source);
			writer.Write(m_method);
			writer.Write(XmlConvert.ToString(m_time, XmlDateTimeSerializationMode.Utc));
			WriteInnerException(writer);
			WriteStackTrace(writer);
			WriteDetails(writer);

			// Derived.

			WriteContents(writer);
		}

		void IBinarySerializable.Read(BinaryReader reader)
		{
			// Standard.

			Source = reader.ReadString();
			m_method = reader.ReadString();
			m_time = XmlConvert.ToDateTime(reader.ReadString(), XmlDateTimeSerializationMode.Utc);
			ReadInnerException(reader);
			ReadStackTrace(reader);
			ReadDetails(reader);

			// Derived.

			ReadContents(reader);
		}

		#endregion

		#region Properties

		public override string StackTrace
		{
			get
			{
				// Cache the stack trace for transition across binary boundaries.

				if ( m_stackTrace == null )
				{
					m_stackTrace = base.StackTrace;
					if ( m_stackTrace == null )
						m_stackTrace = string.Empty;
				}

				return m_stackTrace;
			}
		}

		public string Method
		{
			get { return m_method; }
		}

		public System.DateTime Time
		{
			get { return m_time; }
		}

		public EventDetails Details
		{
			get { return m_details; }
		}

		#endregion

		public override bool Equals(object other)
		{
			if ( !(other is BaseException) )
				return false;
			BaseException exception = (BaseException) other;

			// Check the inner exception.

			System.Exception innerException = InnerException;
			if ( innerException == null )
			{
				if ( exception.InnerException != null )
					return false;
			}
			else
			{
				if ( exception.InnerException == null )
					return false;

				if ( innerException is BaseException )
				{
					if ( !(exception.InnerException is BaseException) )
						return false;
					else if ( !innerException.Equals(exception.InnerException) )
						return false;
				}
				else
				{
					// Can't do much more then check the properties themselves.

					if ( innerException.Message != exception.InnerException.Message
						|| innerException.Source != exception.InnerException.Source
						|| innerException.StackTrace != exception.InnerException.StackTrace
						|| !object.Equals(innerException.InnerException, exception.InnerException.InnerException) )
						return false;
				}
			}

			return Source == exception.Source
				&& StackTrace == exception.StackTrace
				&& m_method == exception.m_method
				&& m_time == exception.m_time
				&& m_details.Equals(exception.m_details);
		}

		public override int GetHashCode()
		{
			return base.GetHashCode()
				^ m_method.GetHashCode()
				^ m_time.GetHashCode()
				^ m_details.GetHashCode();
		}

		protected virtual string GetXmlNamespace()
		{
			return Constants.Xml.Namespace;
		}

		protected virtual void WriteNamespaces(XmlWriteAdaptor adaptor)
		{
			adaptor.WriteNamespace(Constants.Xsi.Prefix, Constants.Xsi.Namespace);
		}

		protected virtual void WriteContents(XmlWriteAdaptor adaptor)
		{
		}

		protected virtual void ReadContents(XmlReadAdaptor adaptor)
		{
		}

		protected virtual void WriteContents(BinaryWriter writer)
		{
		}

		protected virtual void ReadContents(BinaryReader reader)
		{
		}

		private void WriteInnerException(BinaryWriter writer)
		{
			// InnerException

			System.Exception innerException = InnerException;
			if ( innerException != null )
			{
				// Indicate that the InnerException is present.

				writer.Write(true);

				// Indicate whether it supports IBinarySerializable.

				if ( innerException is IBinarySerializable )
				{
					writer.Write(true);
					BinarySerializer.Serialize(innerException, writer);
				}
				else
				{
					// Use the system formatter.

					writer.Write(false);
					BinaryFormatter formatter = new BinaryFormatter();
					formatter.Serialize(writer.BaseStream, innerException);
				}
			}
			else
			{
				writer.Write(false);
			}
		}

		private void ReadInnerException(BinaryReader reader)
		{
			// Check whether there is an inner exception.

			if ( !reader.ReadBoolean() )
				return;

			// An InnerException exists that needs to get de-serialised.

			System.Exception innerException = null;
			if ( reader.ReadBoolean() )
			{
				// It supports IBinarySerializable.

				innerException = (System.Exception) BinarySerializer.Deserialize(reader);
			}
			else
			{
				// Use the system formatter.

				BinaryFormatter formatter = new BinaryFormatter();
				innerException = (System.Exception) formatter.Deserialize(reader.BaseStream);
			}

			SetInnerException(innerException);
		}

		private static void WriteInnerException(XmlWriteAdaptor adaptor, System.Exception innerException)
		{
			adaptor.WriteStartElement(Constants.Xml.Exception.InnerExceptionElement);

			// InnerException.

			if ( innerException == null )
			{
				adaptor.WriteAttribute(Constants.Xsi.NilAttribute, Constants.Xsi.Prefix, Constants.Xsi.Namespace, true);
			}
			else
			{
				// Determine whether the exception is serializable itself.

				if ( innerException is IXmlSerializable )
				{
					adaptor.WriteAttribute(Constants.Xml.Exception.ClassAttribute, innerException.GetType().AssemblyQualifiedName);
					((IXmlSerializable) innerException).WriteOuterXml(adaptor.XmlWriter);
				}
				else
				{
					// The class to write out depends on whether or not this is a SystemException.

					if ( innerException is SystemException )
						adaptor.WriteAttribute(Constants.Xml.Exception.ClassAttribute, ((SystemException) innerException).Class);
					else
						adaptor.WriteAttribute(Constants.Xml.Exception.ClassAttribute, innerException.GetType().AssemblyQualifiedName);

					adaptor.WriteAttribute(Constants.Xml.Exception.IsSystemAttribute, true);
					WriteSystemException(adaptor, innerException);
				}
			}

			adaptor.WriteEndElement();
		}

		private static void WriteSystemException(XmlWriteAdaptor adaptor, System.Exception exception)
		{
			// Write out the root element, adding appropriate namespace and type attributes.

			adaptor.WriteStartElement(Constants.Xml.Exception.ExceptionElement);
			adaptor.WriteAttribute(Constants.Xsi.TypeAttribute, Constants.Xsi.Prefix, Constants.Xsi.Namespace, "Exception");

/*
 * 			if ( exception is SystemException )
				adaptor.WriteAttribute(Constants.Xml.ClassAttribute, ((SystemException) exception).Class);
			else
				adaptor.WriteAttribute(Constants.Xml.ClassAttribute, exception.GetType().AssemblyQualifiedName);
*/

			// Standard properties.

			adaptor.WriteElement(Constants.Xml.Exception.MessageElement, exception.Message);
			adaptor.WriteElement(Constants.Xml.Exception.SourceElement, exception.Source);
			WriteInnerException(adaptor, exception.InnerException);
			adaptor.WriteElement(Constants.Xml.Exception.StackTraceElement, exception.StackTrace);

			adaptor.WriteEndElement();
		}

		private static System.Exception ReadInnerException(XmlReadAdaptor adaptor)
		{
			System.Exception innerException = null;

			if ( adaptor.ReadElement(Constants.Xml.Exception.InnerExceptionElement) )
			{
				// Check whether the inner exception is set.

				bool isNil = adaptor.ReadAttributeBoolean(Constants.Xsi.NilAttribute, Constants.Xsi.Namespace, false);
				if ( !isNil )
				{
					// Determine the class.

					string className = adaptor.ReadAttributeString(Constants.Xml.Exception.ClassAttribute, string.Empty);
					bool isSystem = adaptor.ReadAttributeBoolean(Constants.Xml.Exception.IsSystemAttribute, false);

					// Move to the element.

					if ( adaptor.ReadElement(Constants.Xml.Exception.ExceptionElement) )
					{
						ClassInfo classInfo = null;

						if ( !isSystem )
						{
							if ( className.Length == 0 )
							{
								isSystem = true;
							}
							else
							{
								classInfo = new ClassInfo(className);
								if ( !classInfo.SupportsInterface<IXmlSerializable>() )
									isSystem = true;
							}
						}

						if ( isSystem )
						{
							innerException = ReadSystemException(adaptor, className);
						}
						else
						{
							// Create an instance of the exception.

							const BindingFlags constructorFlags = BindingFlags.Instance | BindingFlags.ExactBinding | BindingFlags.Public | BindingFlags.NonPublic;
							ConstructorInfo constructor = classInfo.GetNetType().GetConstructor(constructorFlags, null, new System.Type[] {}, null);
							if ( constructor == null )
								throw new System.ApplicationException("Cannot find constructor for class '" + classInfo.GetNetType().FullName + "'.");

							// Read the Xml.

							IXmlSerializable xmlSerializable = (IXmlSerializable) constructor.Invoke(new object[] {});
							xmlSerializable.ReadXml(adaptor.XmlReader);

							innerException = (System.Exception) xmlSerializable;
						}

						adaptor.ReadEndElement();
					}
				}

				adaptor.ReadEndElement();
			}

			return innerException;
		}

		private static System.Exception ReadSystemException(XmlReadAdaptor adaptor, string className)
		{
			// Standard properties.

			string message = adaptor.ReadElementString(Constants.Xml.Exception.MessageElement, string.Empty);
			string source = adaptor.ReadElementString(Constants.Xml.Exception.SourceElement, string.Empty);
			System.Exception innerException = ReadInnerException(adaptor);
			string stackTrace = adaptor.ReadElementString(Constants.Xml.Exception.StackTraceElement, string.Empty);
			return new SystemException(source, message, stackTrace, className, innerException);
		}

		private void SetInnerException(System.Exception innerException)
		{
			// Use reflection to modify the private member for InnerException.

			try
			{
				typeof(System.Exception).InvokeMember(
					Constants.Reflection.InnerException,
					BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.SetField,
					null, this, new object[] { innerException });
			}
			catch ( System.Exception )
			{
			}
		}

        private void WriteStackTrace(BinaryWriter writer)
		{
			writer.Write(StackTrace);
		}

		private void ReadStackTrace(BinaryReader reader)
		{
			m_stackTrace = reader.ReadString();
		}

		private void WriteDetails(BinaryWriter writer)
		{
			((IBinarySerializable) m_details).Write(writer);
		}

		private void ReadDetails(BinaryReader reader)
		{
			m_details = new EventDetails();
			((IBinarySerializable) m_details).Read(reader);
		}

		private void WriteDetails(XmlWriteAdaptor adaptor)
		{
			adaptor.WriteStartElement(Constants.Xml.Exception.DetailsElement);
			foreach ( IEventDetail detail in m_details )
			{
				adaptor.WriteStartElement(detail.Name);
				((IXmlSerializable) detail).WriteXml(adaptor.XmlWriter);
				adaptor.WriteEndElement();
			}
			adaptor.WriteEndElement();
		}

		private void ReadDetails(XmlReadAdaptor adaptor)
		{
			m_details = new EventDetails();
			if ( adaptor.ReadElement(Constants.Xml.Exception.DetailsElement) )
			{
				while ( adaptor.ReadElement() )
				{
					string name = adaptor.Name;
					IEventDetailFactory factory = m_eventDetailFactories[name];
					IEventDetail detail = factory.CreateInstance();
					((IXmlSerializable) detail).ReadXml(adaptor.XmlReader);
					m_details.Add(detail);
					adaptor.ReadEndElement();
				}

				adaptor.ReadEndElement();
			}
		}

		private void CreateDetails()
		{
			m_details = m_eventDetailFactories.CreateDetails();
			if ( m_details == null )
				m_details = new EventDetails();
		}

		private void CreateDefaultDetails()
		{
			m_details = new EventDetails();
			foreach ( IEventDetailFactory factory in m_eventDetailFactories )
				m_details.Add(factory.CreateInstance());
		}

		private static EventDetailFactories m_eventDetailFactories = new EventDetailFactories();

		private string m_stackTrace;
		private string m_method;
		private System.DateTime m_time;
		private EventDetails m_details;
	}
}