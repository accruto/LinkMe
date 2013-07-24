using System.IO;
using System.Runtime.Serialization;
using System.Threading;
using System.Xml;
using System.Xml.Schema;
using LinkMe.Framework.Utility.Exceptions;
using LinkMe.Framework.Utility.Xml;

namespace LinkMe.Framework.Utility.Event
{
	[System.Serializable]
	public sealed class ThreadDetail
		:	IEventDetail,
		ISerializable,
		System.ICloneable,
		IXmlSerializable,
		IBinarySerializable,
		IInternable
	{
		#region Constructors

		public ThreadDetail()
		{
		}

		private ThreadDetail(SerializationInfo info, StreamingContext streamingContext)
		{
			m_threadId = info.GetInt32(Constants.Serialization.ThreadDetail.ThreadId);
			m_apartmentState = (ApartmentState) info.GetInt32(Constants.Serialization.ThreadDetail.ApartmentState);
			m_culture = info.GetString(Constants.Serialization.ThreadDetail.Culture);
			m_threadName = info.GetString(Constants.Serialization.ThreadDetail.ThreadName);
			m_state = (ThreadState) info.GetInt32(Constants.Serialization.ThreadDetail.State);
			m_priority = (ThreadPriority) info.GetInt32(Constants.Serialization.ThreadDetail.Priority);
			m_isThreadPoolThread = info.GetBoolean(Constants.Serialization.ThreadDetail.IsThreadPoolThread);
			m_sequence = info.GetInt32(Constants.Serialization.ThreadDetail.Sequence);
		}

		#endregion

		#region Properties

		public int ThreadId
		{
			get { return m_threadId; }
		}

		public ApartmentState ApartmentState
		{
			get { return m_apartmentState; }
		}

		public string Culture
		{
			get { return m_culture == null ? string.Empty : m_culture; }
		}

		public string ThreadName
		{
			get { return m_threadName == null ? string.Empty : m_threadName; }
		}

		public ThreadState State
		{
			get { return m_state; }
		}

		public ThreadPriority Priority
		{
			get { return m_priority; }
		}

		public bool IsThreadPoolThread
		{
			get { return m_isThreadPoolThread; }
		}

		public int Sequence
		{
			get { return m_sequence; }
		}

		#endregion

		#region System.Object Members

		public override bool Equals(object other)
		{
			if ( !(other is ThreadDetail) )
				return false;
			ThreadDetail otherDetails = (ThreadDetail) other;
			return m_threadId == otherDetails.m_threadId
				&& m_apartmentState == otherDetails.m_apartmentState
				&& m_culture == otherDetails.m_culture
				&& m_threadName == otherDetails.m_threadName
				&& m_state == otherDetails.m_state
				&& m_priority == otherDetails.m_priority
				&& m_isThreadPoolThread == otherDetails.m_isThreadPoolThread
				&& m_sequence == otherDetails.m_sequence;
		}

		public override int GetHashCode()
		{
			return m_threadId.GetHashCode()
				^ m_apartmentState.GetHashCode()
				^ m_culture.GetHashCode()
				^ m_threadName.GetHashCode()
				^ m_state.GetHashCode()
				^ m_priority.GetHashCode()
				^ m_isThreadPoolThread.GetHashCode()
				^ m_sequence.GetHashCode();
		}

		#endregion

		#region IEventDetail Members

		string IEventDetail.Name
		{
			get { return typeof(ThreadDetail).Name; }
		}

        EventDetailValues IEventDetail.Values
        {
            get
            {
                var values = new EventDetailValues();
                values.Add(Constants.Serialization.ThreadDetail.ApartmentState, m_apartmentState);
                values.Add(Constants.Serialization.ThreadDetail.Culture, m_culture);
                values.Add(Constants.Serialization.ThreadDetail.IsThreadPoolThread, m_isThreadPoolThread);
                values.Add(Constants.Serialization.ThreadDetail.Priority, m_priority);
                values.Add(Constants.Serialization.ThreadDetail.Sequence, m_sequence);
                values.Add(Constants.Serialization.ThreadDetail.State, m_state);
                values.Add(Constants.Serialization.ThreadDetail.ThreadId, m_threadId);
                values.Add(Constants.Serialization.ThreadDetail.ThreadName, m_threadName);
                return values;
            }
        }

        void IEventDetail.Populate()
		{
			Thread currentThread = Thread.CurrentThread;
            m_threadId = Thread.CurrentThread.ManagedThreadId;
			m_apartmentState = currentThread.GetApartmentState();
			m_culture = currentThread.CurrentCulture.DisplayName;
			m_threadName = currentThread.Name;
			if ( m_threadName == null )
				m_threadName = string.Empty;
			m_state = currentThread.ThreadState;
			m_priority = currentThread.Priority;
			m_isThreadPoolThread = currentThread.IsThreadPoolThread;

			object threadSequence = Thread.GetData(m_slot);
            if ( threadSequence == null )
				m_sequence = 1;
			else
				m_sequence = ((int) threadSequence) + 1;
			Thread.SetData(m_slot, m_sequence);
		}

		#endregion

		#region ISerializable Members

		public void GetObjectData(SerializationInfo info, StreamingContext streamingContext)
		{
			info.AddValue(Constants.Serialization.ThreadDetail.ThreadId, m_threadId);
			info.AddValue(Constants.Serialization.ThreadDetail.ApartmentState, m_apartmentState);
			info.AddValue(Constants.Serialization.ThreadDetail.Culture, m_culture);
			info.AddValue(Constants.Serialization.ThreadDetail.ThreadName, m_threadName);
			info.AddValue(Constants.Serialization.ThreadDetail.State, m_state);
			info.AddValue(Constants.Serialization.ThreadDetail.Priority, m_priority);
			info.AddValue(Constants.Serialization.ThreadDetail.IsThreadPoolThread, m_isThreadPoolThread);
			info.AddValue(Constants.Serialization.ThreadDetail.Sequence, m_sequence);
		}

		#endregion

		#region IXmlSerializable Members

		public XmlSchema GetSchema()
		{
			return null;
		}

		public void WriteOuterXml(XmlWriter writer)
		{
			XmlWriteAdaptor adaptor = new XmlWriteAdaptor(writer, Constants.Xml.Namespace);
			adaptor.WriteStartElement(Constants.Xml.ThreadDetail.Name);
			WriteXml(adaptor);
			adaptor.WriteEndElement();
		}

		public void WriteXml(XmlWriter writer)
		{
			XmlWriteAdaptor adaptor = new XmlWriteAdaptor(writer, Constants.Xml.Namespace);
			WriteXml(adaptor);
		}

		private void WriteXml(XmlWriteAdaptor adaptor)
		{
			adaptor.WriteNamespace(Constants.Xml.Prefix, Constants.Xml.Namespace);

			adaptor.WriteElement(Constants.Xml.ThreadDetail.ThreadId, XmlConvert.ToString(m_threadId));
			adaptor.WriteElement(Constants.Xml.ThreadDetail.ApartmentState, m_apartmentState.ToString());
			adaptor.WriteElement(Constants.Xml.ThreadDetail.Culture, m_culture);
			adaptor.WriteElement(Constants.Xml.ThreadDetail.ThreadName, m_threadName);
			adaptor.WriteElement(Constants.Xml.ThreadDetail.State, m_state.ToString());
			adaptor.WriteElement(Constants.Xml.ThreadDetail.Priority, m_priority.ToString());
			adaptor.WriteElement(Constants.Xml.ThreadDetail.IsThreadPoolThread, XmlConvert.ToString(m_isThreadPoolThread));
			adaptor.WriteElement(Constants.Xml.ThreadDetail.Sequence, XmlConvert.ToString(m_sequence));
		}

		public void ReadOuterXml(XmlReader reader)
		{
			XmlReadAdaptor adaptor = new XmlReadAdaptor(reader, Constants.Xml.Namespace);
			if ( adaptor.IsReadingElement(Constants.Xml.ThreadDetail.Name) )
			{
				ReadXml(adaptor);
				adaptor.ReadEndElement();
			}
		}

		public void ReadXml(XmlReader reader)
		{
			XmlReadAdaptor adaptor = new XmlReadAdaptor(reader, Constants.Xml.Namespace);
			ReadXml(adaptor);
		}

		private void ReadXml(XmlReadAdaptor adaptor)
		{
			m_threadId = XmlConvert.ToInt32(adaptor.ReadElementString(Constants.Xml.ThreadDetail.ThreadId));
			m_apartmentState = (ApartmentState) System.Enum.Parse(typeof(ApartmentState), adaptor.ReadElementString(Constants.Xml.ThreadDetail.ApartmentState));
			m_culture = adaptor.ReadElementString(Constants.Xml.ThreadDetail.Culture);
			m_threadName = adaptor.ReadElementString(Constants.Xml.ThreadDetail.ThreadName);
			m_state = (ThreadState) System.Enum.Parse(typeof(ThreadState), adaptor.ReadElementString(Constants.Xml.ThreadDetail.State));
			m_priority = (ThreadPriority) System.Enum.Parse(typeof(ThreadPriority), adaptor.ReadElementString(Constants.Xml.ThreadDetail.Priority));
			m_isThreadPoolThread = XmlConvert.ToBoolean(adaptor.ReadElementString(Constants.Xml.ThreadDetail.IsThreadPoolThread));
			m_sequence = XmlConvert.ToInt32(adaptor.ReadElementString(Constants.Xml.ThreadDetail.Sequence));
		}

		#endregion

		#region IBinarySerializable Members

		void IBinarySerializable.Write(BinaryWriter writer)
		{
			writer.Write(m_threadId);
			writer.Write((int) m_apartmentState);
			writer.Write(m_culture);
			writer.Write(m_threadName);
			writer.Write((int) m_state);
			writer.Write((int) m_priority);
			writer.Write(m_isThreadPoolThread);
			writer.Write(m_sequence);
		}

		void IBinarySerializable.Read(BinaryReader reader)
		{
			m_threadId = reader.ReadInt32();
			m_apartmentState = (ApartmentState) reader.ReadInt32();
			m_culture = reader.ReadString();
			m_threadName = reader.ReadString();
			m_state = (ThreadState) reader.ReadInt32();
			m_priority = (ThreadPriority) reader.ReadInt32();
			m_isThreadPoolThread = reader.ReadBoolean();
			m_sequence = reader.ReadInt32();
		}

		#endregion

		#region ICloneable Members

		object System.ICloneable.Clone()
		{
			return MemberwiseClone();
		}

		#endregion

		#region IInternable Members

		public void Intern(Interner interner)
		{
			const string method = "Intern";

			if (interner == null)
				throw new NullParameterException(GetType(), method, "interner");

			m_culture = interner.Intern(m_culture);
			m_threadName = interner.Intern(m_threadName);
		}

		#endregion

		#region Member Variables

		private int m_threadId;
		private ApartmentState m_apartmentState;
		private string m_culture = string.Empty;
		private string m_threadName = string.Empty;
		private ThreadState m_state;
		private ThreadPriority m_priority;
		private bool m_isThreadPoolThread;
		private int m_sequence;
		private static System.LocalDataStoreSlot m_slot = Thread.AllocateDataSlot();

		#endregion
	}

	public class ThreadDetailFactory
		:	IEventDetailFactory
	{
		public string Name
		{
			get { return typeof(ThreadDetail).Name; }
		}

		public IEventDetail CreateInstance()
		{
			return new ThreadDetail();
		}
	}
}
