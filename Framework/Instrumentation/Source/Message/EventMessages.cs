using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;
using System.Xml;
using System.Xml.Schema;

using LinkMe.Framework.Utility;
using LinkMe.Framework.Utility.Exceptions;
using LinkMe.Framework.Utility.Xml;
using LinkMe.Framework.Type;

using DateTime = LinkMe.Framework.Type.DateTime;

namespace LinkMe.Framework.Instrumentation.Message
{
	[System.Serializable]
	public class EventMessages
		:	IEnumerable<EventMessage>,
		ISerializable,
		IXmlSerializable,
		IBinarySerializable
	{
		#region Constructors

		public EventMessages()
		{
			m_messages = new List<EventMessage>();
		}

		public EventMessages(string xml)
		{
			// Deserialize the XML.

			ReadXml(new XmlTextReader(new StringReader(xml)));
		}

		private EventMessages(List<EventMessage> messages)
		{
			Debug.Assert(messages != null, "messages != null");
			m_messages = messages;
		}

		#endregion

		#region Operations

		public int Count
		{
			get { return m_messages.Count; }
		}

		/// <summary>
		/// Gets the EventMessage in the EventMessages collection.
		/// </summary>
		public EventMessage this[int index]
		{
			get { return (EventMessage) m_messages[index]; }
		}

		/// <summary>
		/// Adds an EventMessage object to the List.
		/// </summary>
		/// <param name="eventMessage"></param>
		/// <returns></returns>
		public void Add(EventMessage message)
		{
			if (message == null)
				throw new  NullParameterException(GetType(), "Add", "message");

			m_messages.Add(message);
		}

		internal EventMessages GetRange(int startIndex, int maxCount)
		{
			Debug.Assert(startIndex < m_messages.Count, "startIndex < m_messages.Count");

			int count = System.Math.Min(m_messages.Count - startIndex, maxCount);
			var range = m_messages.GetRange(startIndex, count);

			return new EventMessages(range);
		}

		public override bool Equals(object other)
		{
			if ( !(other is EventMessages) )
				return false;

			// Compare the count of messages.

			EventMessages otherMessages = (EventMessages) other;
			if ( m_messages.Count != otherMessages.m_messages.Count )
				return false;

			// Compare each message.

			for ( int index = 0; index < m_messages.Count; ++index )
			{
				if ( !m_messages[index].Equals(otherMessages.m_messages[index]) )
					return false;
			}

			return true;
		}

		public override int GetHashCode()
		{
			int hashcode = 0;
			foreach ( EventMessage message in m_messages )
				hashcode ^= message.GetHashCode();
			return hashcode;
		}

	    IEnumerator IEnumerable.GetEnumerator()
	    {
	        return GetEnumerator();
	    }

	    #endregion

		#region IEnumerable Members

		public IEnumerator<EventMessage> GetEnumerator()
		{
			return m_messages.GetEnumerator();
		}

		#endregion

		#region ISerializable Members

		/// <summary>
		/// 
		/// </summary>
		/// <param name="serializationInfo"></param>
		/// <param name="streamingContext"></param>
		protected EventMessages(SerializationInfo info, StreamingContext streamingContext)
		{
			m_messages = new List<EventMessage>();
			int count = info.GetInt32(Constants.Serialization.EventMessageCount);
			for ( int index = 0; index < count; ++index )
				m_messages.Add((EventMessage) info.GetValue(Constants.Serialization.EventMessage + index.ToString(), typeof(EventMessage)));
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="serializationInfo"></param>
		/// <param name="streamingContext"></param>
		public void GetObjectData(SerializationInfo info, StreamingContext streamingContext)
		{
			info.AddValue(Constants.Serialization.EventMessageCount, m_messages.Count);
			for ( int index = 0; index < m_messages.Count; ++index )
				info.AddValue(Constants.Serialization.EventMessage + index.ToString(), m_messages[index]);
		}

		#endregion

		#region IXmlSerializable Members

		public XmlSchema GetSchema()
		{
			return null;
		}

		public void WriteOuterXml(XmlWriter xmlWriter)
		{
			XmlWriteAdaptor adaptor = new XmlWriteAdaptor(xmlWriter, Constants.Xml.Namespace);
			adaptor.WriteStartElement(Constants.Xml.EventMessagesElement);

			// Add the XSI namespace.

			adaptor.WriteNamespace(Constants.Xsi.Prefix, Constants.Xsi.Namespace);
			adaptor.WriteNamespace(Constants.Xml.UtilityPrefix, Constants.Xml.UtilityNamespace);
			adaptor.WriteNamespace(Constants.Xml.TypePrefix, Constants.Xml.TypeNamespace);

			// Write the messages.

			WriteXml(adaptor);

			adaptor.WriteEndElement();
		}

		public void WriteXml(XmlWriter xmlWriter)
		{
			XmlWriteAdaptor adaptor = new XmlWriteAdaptor(xmlWriter, Constants.Xml.Namespace);

			// Add the XSI namespace.

			adaptor.WriteNamespace(Constants.Xsi.Prefix, Constants.Xsi.Namespace);
			adaptor.WriteNamespace(Constants.Xml.TypePrefix, Constants.Xml.TypeNamespace);

			// Write the messages.

			WriteXml(adaptor);
		}

		private void WriteXml(XmlWriteAdaptor adaptor)
		{
			// Iterate through each message.
			
			foreach ( IXmlSerializable message in m_messages )
				message.WriteOuterXml(adaptor.XmlWriter);
		}

		public void ReadOuterXml(XmlReader xmlReader)
		{
			m_messages = new List<EventMessage>();
			XmlReadAdaptor adaptor = new XmlReadAdaptor(xmlReader, Constants.Xml.Namespace);
			if ( adaptor.IsReadingElement(Constants.Xml.EventMessagesElement) )
				ReadXml(adaptor);
		}

		public void ReadXml(XmlReader xmlReader)
		{
            m_messages = new List<EventMessage>();
			XmlReadAdaptor adaptor = new XmlReadAdaptor(xmlReader, Constants.Xml.Namespace);
			ReadXml(adaptor);
		}

		private void ReadXml(XmlReadAdaptor adaptor)
		{
			while ( adaptor.ReadElement(Constants.Xml.EventMessageElement) )
			{
				IXmlSerializable message = new EventMessage();
				message.ReadXml(adaptor.XmlReader);
				adaptor.ReadEndElement();
				m_messages.Add(message as EventMessage);
			}
		}
		
		#endregion

		#region IBinarySerializable Members

		/// <summary>
		/// 
		/// </summary>
		/// <param name="writer"></param>
		public void Write(BinaryWriter writer)
		{
			// Write out the count and then each message.

			writer.Write(m_messages.Count);
			foreach ( IBinarySerializable serializable in m_messages )
				serializable.Write(writer);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="reader"></param>
		public void Read(BinaryReader reader)
		{
			// Read the count.

			m_messages = new List<EventMessage>();
			int count = reader.ReadInt32();
			for ( int index = 0; index < count; ++index )
			{
				// Read a new message.

				EventMessage message = new EventMessage();
				message.Read(reader);
				m_messages.Add(message);
			}
		}

		#endregion

		private List<EventMessage> m_messages;
	}
}
