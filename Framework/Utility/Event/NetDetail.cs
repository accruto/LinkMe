using System.IO;
using System.Runtime.Serialization;
using System.Xml;
using System.Xml.Schema;
using LinkMe.Framework.Utility.Exceptions;
using LinkMe.Framework.Utility.Xml;

namespace LinkMe.Framework.Utility.Event
{
	[System.Serializable]
	public sealed class NetDetail
		:	IEventDetail,
		ISerializable,
		System.ICloneable,
		IXmlSerializable,
		IBinarySerializable,
		IInternable
	{
		#region Constructors

		public NetDetail()
		{
		}

		private NetDetail(SerializationInfo info, StreamingContext streamingContext)
		{
			m_appDomainName = info.GetString(Constants.Serialization.NetDetail.AppDomainName);
		}

		#endregion

		#region Properties

		public string AppDomainName
		{
			get { return m_appDomainName == null ? string.Empty : m_appDomainName; }
		}

		#endregion

		#region System.Object Members

		public override bool Equals(object other)
		{
			if ( !(other is NetDetail) )
				return false;
			NetDetail otherDetails = (NetDetail) other;
			return m_appDomainName == otherDetails.m_appDomainName;
		}

		public override int GetHashCode()
		{
			return m_appDomainName.GetHashCode();
		}

		#endregion

		#region IEventDetail Members

		string IEventDetail.Name
		{
			get { return typeof(NetDetail).Name; }
		}

        EventDetailValues IEventDetail.Values
        {
            get
            {
                var values = new EventDetailValues();
                values.Add(Constants.Serialization.NetDetail.AppDomainName, m_appDomainName);
                return values;
            }
        }

        void IEventDetail.Populate()
		{
			m_appDomainName = m_generateAppDomainName;
		}

		#endregion

		#region ISerializable Members

		public void GetObjectData(SerializationInfo info, StreamingContext streamingContext)
		{
			info.AddValue(Constants.Serialization.NetDetail.AppDomainName, m_appDomainName);
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
			adaptor.WriteStartElement(Constants.Xml.NetDetail.Name);
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

			adaptor.WriteElement(Constants.Xml.NetDetail.AppDomainNameElement, m_appDomainName);
		}

		public void ReadOuterXml(XmlReader reader)
		{
			XmlReadAdaptor adaptor = new XmlReadAdaptor(reader, Constants.Xml.Namespace);
			if ( adaptor.IsReadingElement(Constants.Xml.NetDetail.Name) )
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
			m_appDomainName = adaptor.ReadElementString(Constants.Xml.NetDetail.AppDomainNameElement);
		}

		#endregion

		#region IBinarySerializable Members

		void IBinarySerializable.Write(BinaryWriter writer)
		{
			writer.Write(m_appDomainName);
		}

		void IBinarySerializable.Read(BinaryReader reader)
		{
			m_appDomainName = reader.ReadString();
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

			m_appDomainName = interner.Intern(m_appDomainName);
		}

		#endregion

		#region Member Variables

		private static string m_generateAppDomainName = System.AppDomain.CurrentDomain.FriendlyName;

		private string m_appDomainName = string.Empty;

		#endregion
	}

	public class NetDetailFactory
		:	IEventDetailFactory
	{
		public string Name
		{
			get { return typeof(NetDetail).Name; }
		}

		public IEventDetail CreateInstance()
		{
			return new NetDetail();
		}
	}
}
