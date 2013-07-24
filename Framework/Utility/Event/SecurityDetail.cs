using System.IO;
using System.Runtime.Serialization;
using System.Security.Principal;
using System.Threading;
using System.Xml;
using System.Xml.Schema;
using LinkMe.Framework.Utility.Exceptions;
using LinkMe.Framework.Utility.Xml;

namespace LinkMe.Framework.Utility.Event
{
	[System.Serializable]
	public sealed class SecurityDetail
		:	IEventDetail,
			ISerializable,
			System.ICloneable,
			IXmlSerializable,
			IBinarySerializable,
			IInternable
	{
		#region Constructors

		public SecurityDetail()
		{
		}

		private SecurityDetail(SerializationInfo serializationInfo, StreamingContext streamingContext)
		{
			m_processUserName = serializationInfo.GetString(Constants.Serialization.SecurityDetail.ProcessUserName);
			m_authenticationType = serializationInfo.GetString(Constants.Serialization.SecurityDetail.AuthenticationType);
			m_isAuthenticated = serializationInfo.GetBoolean(Constants.Serialization.SecurityDetail.IsAuthenticated);
			m_userName = serializationInfo.GetString(Constants.Serialization.SecurityDetail.UserName);
		}

		#endregion

		#region Properties

		public string ProcessUserName
		{
			get { return m_processUserName == null ? string.Empty : m_processUserName; }
		}

		public string AuthenticationType
		{
			get { return m_authenticationType == null ? string.Empty : m_authenticationType; }
		}

		public bool IsAuthenticated
		{
			get { return m_isAuthenticated; }
		}

		public string UserName
		{
			get { return m_userName == null ? string.Empty : m_userName; }
		}

		#endregion

		#region System.Object Members

		public override bool Equals(object other)
		{
			if ( !(other is SecurityDetail) )
				return false;
			SecurityDetail otherDetails = (SecurityDetail) other;
			return m_processUserName == otherDetails.m_processUserName
				&& m_authenticationType == otherDetails.m_authenticationType
				&& m_isAuthenticated == otherDetails.m_isAuthenticated
				&& m_userName == otherDetails.m_userName;
		}

		public override int GetHashCode()
		{
			return m_processUserName.GetHashCode()
				^ m_authenticationType.GetHashCode()
				^ m_isAuthenticated.GetHashCode()
				^ m_userName.GetHashCode();
		}

		#endregion

		#region IEventDetail Members

		string IEventDetail.Name
		{
			get { return typeof(SecurityDetail).Name; }
		}

        EventDetailValues IEventDetail.Values
        {
            get
            {
                var values = new EventDetailValues();
                values.Add(Constants.Serialization.SecurityDetail.ProcessUserName, m_processUserName);
                values.Add(Constants.Serialization.SecurityDetail.AuthenticationType, m_authenticationType);
                values.Add(Constants.Serialization.SecurityDetail.IsAuthenticated, m_isAuthenticated);
                values.Add(Constants.Serialization.SecurityDetail.UserName, m_userName);
                return values;
            }
        }

        void IEventDetail.Populate()
		{
			m_processUserName = m_generateProcessUserName;
			IIdentity identity = Thread.CurrentPrincipal.Identity;
			m_authenticationType = identity.AuthenticationType;
			m_isAuthenticated = identity.IsAuthenticated;
			m_userName = identity.Name;
		}

		#endregion

		#region ISerializable Members

		public void GetObjectData(SerializationInfo serializationInfo, StreamingContext streamingContext)
		{
			serializationInfo.AddValue(Constants.Serialization.SecurityDetail.ProcessUserName, m_processUserName);
			serializationInfo.AddValue(Constants.Serialization.SecurityDetail.AuthenticationType, m_authenticationType);
			serializationInfo.AddValue(Constants.Serialization.SecurityDetail.IsAuthenticated, m_isAuthenticated);
			serializationInfo.AddValue(Constants.Serialization.SecurityDetail.UserName, m_userName);
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
			adaptor.WriteStartElement(Constants.Xml.SecurityDetail.Name);
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

			adaptor.WriteElement(Constants.Xml.SecurityDetail.ProcessUserNameElement, m_processUserName);
			adaptor.WriteElement(Constants.Xml.SecurityDetail.AuthenticationTypeElement, m_authenticationType);
			adaptor.WriteElement(Constants.Xml.SecurityDetail.IsAuthenticatedElement, XmlConvert.ToString(m_isAuthenticated));
			adaptor.WriteElement(Constants.Xml.SecurityDetail.UserNameElement, m_userName);
		}

		public void ReadOuterXml(XmlReader reader)
		{
			XmlReadAdaptor adaptor = new XmlReadAdaptor(reader, Constants.Xml.Namespace);
			if ( adaptor.IsReadingElement(Constants.Xml.SecurityDetail.Name) )
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
			m_processUserName = adaptor.ReadElementString(Constants.Xml.SecurityDetail.ProcessUserNameElement);
			m_authenticationType = adaptor.ReadElementString(Constants.Xml.SecurityDetail.AuthenticationTypeElement);
			m_isAuthenticated = XmlConvert.ToBoolean(adaptor.ReadElementString(Constants.Xml.SecurityDetail.IsAuthenticatedElement));
			m_userName = adaptor.ReadElementString(Constants.Xml.SecurityDetail.UserNameElement);
		}

		#endregion

		#region IBinarySerializable Members

		void IBinarySerializable.Write(BinaryWriter writer)
		{
			writer.Write(m_processUserName);
			writer.Write(m_authenticationType);
			writer.Write(m_isAuthenticated);
			writer.Write(m_userName);
		}

		void IBinarySerializable.Read(BinaryReader reader)
		{
			m_processUserName = reader.ReadString();
			m_authenticationType = reader.ReadString();
			m_isAuthenticated = reader.ReadBoolean();
			m_userName = reader.ReadString();
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

			m_processUserName = interner.Intern(m_processUserName);
			m_authenticationType = interner.Intern(m_authenticationType);
			m_userName = interner.Intern(m_userName);
		}

		#endregion

		#region Member Variables

		private static string m_generateProcessUserName = System.Environment.UserName;

		private string m_processUserName = string.Empty;
		private string m_authenticationType = string.Empty;
		private bool m_isAuthenticated;
		private string m_userName = string.Empty;

		#endregion
	}

	public class SecurityDetailFactory
		:	IEventDetailFactory
	{
		public string Name
		{
			get { return typeof(SecurityDetail).Name; }
		}

		public IEventDetail CreateInstance()
		{
			return new SecurityDetail();
		}
	}
}
